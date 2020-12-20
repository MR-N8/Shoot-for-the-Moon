using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    //config parameters
    [SerializeField] NewPaddle paddle1;
    [SerializeField] float xPush;
    [SerializeField] float yPush;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    public float maxSpeed;
    public float ballSpeed;

    //state
    Vector2 paddleToBallVector;
    public bool hasStarted = false;

    //Cached component references 
    AudioSource myAudioSource;
    Rigidbody2D myRidgidBody2D;
    BallTrail ballTrail;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRidgidBody2D = GetComponent<Rigidbody2D>();
        ballTrail = GetComponent<BallTrail>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // original meathod calls from the course 
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
            if (Input.GetButtonDown("Fire1"))
            {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);

        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    public void AddMaxSpeed()
    {
        Debug.Log("AddMaxSpeed called" + gameObject.name);
        maxSpeed += 10f;
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
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor),
            Random.Range(0f,randomFactor));

        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRidgidBody2D.velocity += velocityTweak;
        }
    }

}
