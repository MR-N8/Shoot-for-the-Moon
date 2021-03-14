using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour
{
    //config parameters
    [SerializeField] NewPaddle currentPaddle;
    [SerializeField] float xPush;
    [SerializeField] float yPush;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float boostOnHit;

    public float maxSpeed;
    public float ballSpeed;

    //state
    private static readonly Vector2 paddleToBallVector = new Vector2(0,0.9f);
    public bool isLaunched = false;

    //Cached component references 
    AudioSource myAudioSource;
    public Rigidbody2D myRidgidBody2D;
    CinemachineVirtualCamera camera;
    BallTrail ballTrail;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myRidgidBody2D = GetComponent<Rigidbody2D>();
        ballTrail = GetComponent<BallTrail>();
        camera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // original meathod calls from the course 
        if (!isLaunched)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isLaunched = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(currentPaddle.transform.position.x, currentPaddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    public void AddMaxSpeed()
    {
        //Debug.Log("AddMaxSpeed called" + gameObject.name);
        maxSpeed += 6f;
        //ballTrail.AddNumBallz();
        // want to add more ball trail as max speed rises
    }


    private void FixedUpdate()
    {
        //Debug.Log(myRidgidBody2D.velocity.magnitude);
        ballSpeed = myRidgidBody2D.velocity.magnitude;
        if (myRidgidBody2D.velocity.magnitude > maxSpeed)
        {
            myRidgidBody2D.velocity = myRidgidBody2D.velocity.normalized * maxSpeed;
        }


    }

    public void KillSpeed()
    {
        StartCoroutine(KillSpeedRoutine());
    }

    private IEnumerator KillSpeedRoutine()
    {
        for (int i = 0; i < 15; i++)
        {
            myRidgidBody2D.velocity *= 0.5f;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (isLaunched && collision.enabled)
        {
            NewPaddle newPaddle = collision.gameObject.GetComponent<NewPaddle>();
            if (newPaddle != null && Input.GetButton("Catch"))
            {
                currentPaddle = newPaddle;
                isLaunched = false;
            }
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            //myRidgidBody2D.velocity += new Vector2(0,boostOnHit);
        }
    }
}
