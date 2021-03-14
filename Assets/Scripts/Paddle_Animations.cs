using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle_Animations : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;
    [SerializeField] private GameObject ballHitsPaddleParticles;
    private static readonly int BALL_HITS_PADDLE_TOP = Animator.StringToHash("BallHitsPaddleTop"); 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.enabled)
        {
            myAnimationController.SetTrigger(BALL_HITS_PADDLE_TOP);
            
            GameObject sparkles = Instantiate(ballHitsPaddleParticles, transform.position, transform.rotation);
            Destroy(sparkles, 2);
        }

        
    }

}
