using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.CompilerServices;

public class CinemachineSwitcher : MonoBehaviour
{
    private CinemachineVirtualCamera thisCamera;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<CinemachineVirtualCamera>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            Debug.Log("Ball Entered");
            thisCamera.Priority = 1;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Debug.Log("Ball Exited");
            thisCamera.Priority = 0;
        }
    }

}
