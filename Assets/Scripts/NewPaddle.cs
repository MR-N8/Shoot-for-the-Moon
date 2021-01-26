using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPaddle : MonoBehaviour
{

    //config params 
   
    float moveSpeed = 30f;
    [SerializeField] float padding = 0f;
    [SerializeField] int tilt = 15;

    

    float xMin = -12f;
    float xMax = 22f;

    // Start is called before the first frame update
    void Start()
    {
        //SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Tilt();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        transform.position = new Vector2(newXPos, transform.position.y);
        

    }

    private void Tilt()
    {
        if (Input.GetButtonDown("Fire3"))
        {

            transform.eulerAngles = new Vector3(0, 0, tilt);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            transform.eulerAngles = new Vector3(0, 0, -tilt);
        }


        if (Input.GetButtonUp("Fire3"))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

    }
}
