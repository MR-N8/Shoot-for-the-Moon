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
    private SpriteRenderer[] ball_srs;

    private readonly List<BallInfo> ball_infos = new List<BallInfo>();

    private void Awake()
    {
        ball_transforms = new Transform[num_ballz];
        ball_srs = new SpriteRenderer[num_ballz];
        Vector3 startPos = mainRenderer.transform.position;
        Color startColor = mainRenderer.color;
        for (int i = 0; i < num_ballz; i++)
        {
            ball_infos.Add(new BallInfo
            {
                position = startPos,
                color = startColor
            });
        }
        for (int i = 0; i < num_ballz; i++)
        {
            GameObject newBallObject = new GameObject("Ball" + i);
            SpriteRenderer ballSR = newBallObject.AddComponent<SpriteRenderer>();
            ballSR.sprite = mainRenderer.sprite;
            ballSR.sortingOrder = mainRenderer.sortingOrder - (i + 1);
            ballSR.color = mainRenderer.color;
            ball_transforms[i] = newBallObject.transform;
            ball_srs[i] = ballSR;
        }

        ball = FindObjectOfType<Ball>();
    }


    private void Update()
    {
        if (ball.hasStarted == true)
        {
            for (int i = 0; i < num_ballz; i++)
            {
                BallInfo info = ball_infos[i];
                ball_transforms[i].position = info.position;
                float alpha = 1f - ((float)i) / (float)num_ballz;
                ball_srs[i].color = new Color(info.color.r, info.color.g, info.color.b, alpha);
            }
            ball_infos.Insert(0, new BallInfo()
            {
                position = mainRenderer.transform.position,
                color = mainRenderer.color
            });
            ball_infos.RemoveAt(ball_infos.Count - 1);
        }
            
    }

    private struct BallInfo
    {
        public Vector3 position;
        public Color color;
    }
}
