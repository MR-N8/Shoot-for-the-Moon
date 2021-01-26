using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    //config parameters 
    //[Range(0.1f,10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 83;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled; 

    //state 
    [SerializeField] int currentScore = 0;

    //cached reference
    Ball ball;


    private void Awake()
    {
        //Debug.Log("GameSession attached to: " + gameObject.name, gameObject);

        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject); 
        }
        
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        scoreText.text = ball.ballSpeed.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ball.ballSpeed.ToString();
    }

    public void AddToScore()
    {
        currentScore = currentScore + pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
            Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

}
