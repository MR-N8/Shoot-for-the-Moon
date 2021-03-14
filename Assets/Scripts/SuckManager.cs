using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckManager : MonoBehaviour
{

    [SerializeField] private Ball ball;
    [SerializeField] private SpriteRenderer ballSprite;
    [SerializeField] private ParticleSystem ballParticles; 
    [SerializeField] private NewPaddle[] paddles;
    [SerializeField] private CinemachineBrain brainCamera;

    [SerializeField] private Color regularColor = Color.white;
    [SerializeField] private Color suckColor = Color.black;


    public NewPaddle closestPaddle = null;
    public static SuckManager instance;
    //singleton pattern so that we can access closest paddle easily from anywhere

    void Awake()
    {
        instance = this;
    }

    private float colorValue = 0;
    private const float FADE_TIME = 0.5f;
    public bool IsSucking { get; private set; }
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
            IsSucking = true;
            colorValue += Time.deltaTime/FADE_TIME;
            ballParticles.Play();
            GameObject camObject = brainCamera.ActiveVirtualCamera.VirtualCameraGameObject;
            ShakeBehavior currentCameraShakeBehavior = camObject.GetComponent<ShakeBehavior>();
            if (currentCameraShakeBehavior != null) {
                currentCameraShakeBehavior.TriggerShake();
            }
            else
            {
                Debug.LogError("No shake behavior on virtual camera: " + camObject.name);
            }
            if (closestPaddle != null)
            {
                //Debug.Log($"!!!!closest paddle!!!! {closestPaddle.name}");
               
                Vector2 forceDirection = (closestPaddle.transform.position - ball.transform.position).normalized;
                if(forceDirection.y < 0)
                {
                    ball.myRidgidBody2D.AddForce(forceDirection * 30000 *Time.deltaTime); //was 100 before
                }
            }
        }
        else
        {
            IsSucking = false;
            colorValue -= Time.deltaTime / FADE_TIME;
            ballParticles.Stop();
        }
        colorValue = Mathf.Min(1, Mathf.Max(0, colorValue));
        ballSprite.color = Color.Lerp(regularColor, suckColor, colorValue);
    }
}
