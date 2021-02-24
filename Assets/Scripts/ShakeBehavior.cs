using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeBehavior : MonoBehaviour
{

    private Transform cameraTransform;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.09f;
    private float dampingSpeed = 0.5f; //A measure of how quickly the shake effect should evaporate
    Vector3 initialPosition;


    void Awake()
    {
     if (transform == null)
        {
            cameraTransform = GetComponent(typeof(Transform)) as Transform; 
        }
    }

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }


    // Update is called once per frame
    void Update()
    {
     if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }

        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 0.12f;
    }
}
