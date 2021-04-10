using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPaddle : MonoBehaviour
{

    //config params 
   
    float moveSpeed = 25f;
    [SerializeField] float padding = 0f;
    [SerializeField] int tilt = 15;
    [SerializeField] private SpriteRenderer paddleSpriteRenderer; 
    

    float xMin = -12f;
    float xMax = 22f;


    void Update()
    {
        Move();
        Tilt();
        Sucking();
        ChangeMoveSpeed();
    }

    private void ChangeMoveSpeed()
    {
        if(Ball.instance.ballIsLocked == true)
        {
            moveSpeed = 5f;
        }
        else if (Ball.instance.ballIsLocked == false)
        {
            moveSpeed = 25f;
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        transform.position = new Vector2(newXPos, transform.position.y);

    }

    private void Tilt()
    {
        bool isRight = Input.GetButton("Fire2");
        bool isLeft = Input.GetButton("Fire3");
        transform.eulerAngles = GetTiltAngle(isRight, isLeft);
    }

    private Vector3 GetTiltAngle(bool isRight, bool isLeft)
    {
        Vector3 tiltAngle;
        if (isRight && isLeft)
        {
            tiltAngle = new Vector3(0, 0, 0);
        }
        else if (isRight)
        {
            tiltAngle = new Vector3(0, 0, tilt);
        }
        else if (isLeft)
        {
            tiltAngle = new Vector3(0, 0, -tilt);
        }
        else
        {
            tiltAngle = new Vector3(0, 0, 0);
        }

        return tiltAngle;
    }

    private void Sucking()
    {
        paddleSpriteRenderer.color = Color.red;
    }


    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

    }
}
