using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    //config parameters 
    //[Range(0.1f,10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 83;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;
    [SerializeField] Image chargeGauge;

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
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = string.Format("{0:0.00}", ball.yPush);
        float chargeAmmount = ball.yPush / ball.maxSpeed; //normalizing the speed (dividing the value we want by it's max value normalizes it between 0 and 1) 
        chargeGauge.fillAmount = chargeAmmount;
        chargeGauge.color = Color.Lerp(Color.white, Color.red, chargeAmmount);

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
