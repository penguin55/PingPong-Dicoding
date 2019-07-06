using System.Collections;
using TMPro;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;

    [Header("SkillManager")] 
    public SkillManager skillManagerPaddle1;
    public SkillManager skillManagerPaddle2;
    
    [Header("Paddle Object")] 
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject aI;
    
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
    [SerializeField] private UIManager UIManager;

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

        string opponent = GameVariable._mode.Equals("PvP") ? "Player 2" : "AI";
        
        if (scoreP1 == winScore) Win("Player 1");
        if (scoreP2 == winScore) Win(opponent);
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
        
        AudioManager.instance.PlayWinPanelOpen();
        
        winPanel.SetActive(true);
        playerText.text = player;
    }

    // When first play, this is to prepare anything on game and gave delay to complete transition
    private IEnumerator PlayTheGame() 
    {
        SpawnPlayer(GameVariable._mode);
        freeze = true;
        UIManager.Transition("TransitionIn");
        yield return new WaitForSeconds(2.2f);
        ball.GetComponent<Ball>().Initialize();
        freeze = false;
    }

    // To spawn the paddle and determine the game is player vs player or versus an AI
    private void SpawnPlayer(string mode)
    {
        GameObject paddle = Instantiate(player1, new Vector2(-8, 0), Quaternion.identity);
        paddle.GetComponent<CharacterBehaviour>().skillManager = skillManagerPaddle1;
        switch (mode)
        {
            case "PvP" :
                paddle = Instantiate(player2, new Vector2(8, 0), Quaternion.identity);
                paddle.GetComponent<CharacterBehaviour>().skillManager = skillManagerPaddle2;
                break;
                
            case "AI" :
                paddle = Instantiate(aI, new Vector2(8, 0), Quaternion.identity);
                paddle.GetComponent<CharacterBehaviour>().skillManager = skillManagerPaddle2;
                break;
            
            case "AIExpert" :
                paddle = Instantiate(aI, new Vector2(8, 0), Quaternion.identity);
                paddle.GetComponent<CharacterBehaviour>().skillManager = skillManagerPaddle2;
                break;
        }
    }
}
