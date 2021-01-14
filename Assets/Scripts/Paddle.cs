using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //configuration parameters 

    [SerializeField] float screenWidthInUnits = 10f;
    [SerializeField] float paddleXMovementMax = 10f;
    [SerializeField] float paddleXMovementMin = 1f;

    //cached refrence  
    GameSession myGameSession;
    Ball myBall;


    // Start is called before the first frame updates
    void Start()
    {
        myGameSession = FindObjectOfType<GameSession>();
        myBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        //paddlePos.x = Mathf.Clamp(GetXPos(),paddleXMovementMin,paddleXMovementMax);
        paddlePos.x = Mathf.Clamp(mousePosInUnits, paddleXMovementMin, paddleXMovementMax);
        transform.position = paddlePos;
        
    }

    private float GetXPos()
    {
        if (myGameSession.IsAutoPlayEnabled())
        {
            return myBall.transform.position.x;
        }
        else 
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
