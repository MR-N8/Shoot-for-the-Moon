using System.Collections.Generic;
using UnityEngine;

public class BallTrail : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer mainRenderer;
    [SerializeField]
    private int num_ballz;

    Ball ball;

    private Transform[] ball_transforms;

    private readonly List<Vector3> ball_positions = new List<Vector3>();

    private void Awake()
    {
        ball_transforms = new Transform[num_ballz];
        Vector3 startPos = mainRenderer.transform.position;
        for (int i = 0; i < num_ballz; i++)
        {
            ball_positions.Add(startPos);
        }
        for (int i = 0; i < num_ballz; i++)
        {
            GameObject newBallObject = new GameObject("Ball" + i);
            SpriteRenderer ballSR = newBallObject.AddComponent<SpriteRenderer>();
            ballSR.sprite = mainRenderer.sprite;
            ballSR.sortingOrder = mainRenderer.sortingOrder - (i + 1);
            Color c = mainRenderer.color;
            float alpha = 1f - ((float)i) / (float)num_ballz;
            ballSR.color = new Color(c.r, c.g, c.b, alpha);
            ball_transforms[i] = newBallObject.transform;
        }

        ball = FindObjectOfType<Ball>();
    }


    private void Update()
    {
        if (ball.hasStarted == true)
        {
            for (int i = 0; i < num_ballz; i++)
            {
                ball_transforms[i].position = ball_positions[i];
            }
            ball_positions.Insert(0, mainRenderer.transform.position);
            ball_positions.RemoveAt(ball_positions.Count - 1);
        }
            
    }

}
