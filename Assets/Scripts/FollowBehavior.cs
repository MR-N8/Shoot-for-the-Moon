using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavior : MonoBehaviour
{

    [SerializeField] protected Transform trackingTarget;

    [SerializeField] float yOffset;
    [SerializeField] float zOffset;

    Camera cam;
    Ball ball;


    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(trackingTarget.position.x, trackingTarget.position.y + yOffset,trackingTarget.position.z + zOffset);

        if (ball.maxSpeed > 21)
            cam.orthographicSize = 20f;
        if (ball.maxSpeed < 21)
            cam.orthographicSize = 10f;

        if (Input.GetKey(KeyCode.KeypadPlus))
            cam.orthographicSize -= .1f;
        if (Input.GetKey(KeyCode.KeypadMinus))
            cam.orthographicSize += .1f;

    }
}
