using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad_Animations : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.enabled)
        {
            Debug.Log("paddle collison enabled");
            myAnimationController.SetTrigger("BallHitTop");
        }
        
    }

}
