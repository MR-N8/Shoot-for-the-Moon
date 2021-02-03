using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckManager : MonoBehaviour
{

    [SerializeField] private Ball ball;
    [SerializeField] private NewPaddle[] paddles;
   

    public NewPaddle closestPaddle = null;
    public static SuckManager instance;
    //singleton pattern so that we can access closest paddle easily from anywhere

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("instance var fired");
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // to get the balls distance from the paddle 
        
        float closestDistance = float.MaxValue;
        closestPaddle = null;

        for (int i = 0; i < paddles.Length; i++)
        {
            NewPaddle paddle = paddles[i];
            if (paddle != null)
            {
                float distance = Vector3.Distance(paddle.transform.position, ball.transform.position);
                //Debug.Log($"paddle name, {paddle.name} distance {distance}");

                if (distance < closestDistance && paddle.transform.position.y < ball.transform.position.y)
                {
                    closestDistance = distance;
                    closestPaddle = paddle;
                }
            }
        }

        if (Input.GetButton("Fire1"))
        {
           
            if (closestPaddle != null)
            {
                //Debug.Log($"!!!!closest paddle!!!! {closestPaddle.name}");
               
                Vector2 forceDirection = (closestPaddle.transform.position - ball.transform.position).normalized;
                if(forceDirection.y < 0)
                {
                    ball.myRidgidBody2D.AddForce(forceDirection * 100);

                }

                

                //ball.myRidgidBody2D.isKinematic = true;
                //ball.myRidgidBody2D.collisionDetectionMode = false; 

                }
        }
    }
}
