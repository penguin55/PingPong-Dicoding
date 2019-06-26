using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    
    [Header("Score Properties")] 
    [HideInInspector] public int scoreP1;
    [HideInInspector] public int scoreP2;
    [SerializeField] private TextMeshProUGUI scoreTextP1;
    [SerializeField] private TextMeshProUGUI scoreTextP2;
    
    [Header("Map Properties")] 
    public Vector2 mapSize;

    private void Start()
    {
        if (!instance) instance = this; //When the instance is null
        else Destroy(gameObject); //When the instance was created
    }

    public void UpdateScore(int _scoreP1, int _scoreP2)
    {
        scoreP1 += _scoreP1;
        scoreP2 += _scoreP2;
        scoreTextP1.text = "" + scoreP1;
        scoreTextP2.text = "" + scoreP2;
    }
}
