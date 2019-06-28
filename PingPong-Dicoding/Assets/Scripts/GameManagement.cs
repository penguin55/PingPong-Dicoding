using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    
    [Header("Score Properties")] 
    [SerializeField] private TextMeshProUGUI scoreTextP1;
    [SerializeField] private TextMeshProUGUI scoreTextP2;
    [HideInInspector] public int scoreP1;
    [HideInInspector] public int scoreP2;
    
    [Header("Map Properties")] 
    public Vector2 mapSize;

    [Header("Win Properties")] 
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private int winScore;
    [HideInInspector] public bool freeze;

    public GameObject ball;
    
    [Header("UIManager")] 
    [SerializeField] private UIInGameManager UIManager;

    private void Start()
    {
        instance = this;
        StartCoroutine(PlayTheGame());
    }

    public void UpdateScore(int _scoreP1, int _scoreP2)
    {
        scoreP1 += _scoreP1;
        scoreP2 += _scoreP2;
        scoreTextP1.text = "" + scoreP1;
        scoreTextP2.text = "" + scoreP2;
        
        if (scoreP1 == winScore) Win("Player 1");
        if (scoreP2 == winScore) Win("Player 2");
    }

    void Win(string player)
    {
        freeze = true;
        ball.GetComponent<Ball>().StopBall();
        StartCoroutine(WinDelay(player));
    }

    private IEnumerator WinDelay(string player)
    {
        yield return new WaitForSeconds(0.2f);
        winPanel.SetActive(true);
        playerText.text = player;
    }

    private IEnumerator PlayTheGame() // When first play, this is to prepare anything on game and gave delay to complete transition
    {
        freeze = true;
        UIManager.Transition("TransitionIn");
        yield return new WaitForSeconds(2.2f);
        ball.GetComponent<Ball>().Initialize();
        freeze = false;
    }
}
