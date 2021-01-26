using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public float camSize;
    public Transform camPos;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
       cam = FindObjectOfType<Camera>();
       camSize = cam.orthographicSize;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
