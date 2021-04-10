using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour
{
    public static Ball instance;
    
    //config parameters
    [SerializeField] NewPaddle currentPaddle;
    public float yPush;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float boostOnHit;
    


    public float maxSpeed;
    public float ballSpeed;
    public bool ballIsLocked = true;

    public event Action maxSpeedChanged;

    //state
    private static readonly Vector2 paddleToBallVector = new Vector2(0,1.3f);
    public bool isLaunched = false;

    //Cached component references 
    AudioSource myAudioSource;
    public Rigidbody2D myRidgidBody2D;
    CinemachineVirtualCamera camera;
    BallTrail ballTrail;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("There is more than one ball fuck face: "+ this.name);
        }
        instance = this;
    }

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
            ChargeShot();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            isLaunched = true;
            ballIsLocked = false;
            Vector3 pushVector = currentPaddle.transform.TransformDirection(new Vector3(0, yPush, 0));
            GetComponent<Rigidbody2D>().velocity = pushVector;
            yPush = 0f;
        }
    }

    private void LockBallToPaddle()
    {
        transform.position = currentPaddle.transform.TransformPoint(paddleToBallVector);
        ballIsLocked = true;
    }

    private void ChargeShot()
    {
        if (isLaunched == false)
        {
          if(SuckManager.instance.IsSucking == true)
            {
                Debug.Log("charging");
                yPush = Mathf.Clamp(yPush+(maxSpeed*Time.deltaTime*1.75f), 0, maxSpeed);
            }
        }
    }

    public void AddMaxSpeed()
    {
        //Debug.Log("AddMaxSpeed called" + gameObject.name);
        maxSpeed += 6f;
        maxSpeedChanged?.Invoke();
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
            if (newPaddle != null && Input.GetButton("Fire1"))
            {
                currentPaddle = newPaddle;
                isLaunched = false;
            }
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            //myRidgidBody2D.velocity += new Vector2(0,boostOnHit);
        }
    }
}
