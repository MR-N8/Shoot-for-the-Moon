using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(new Vector3(-0.05f,-6.59f,0f), new Vector3 (0f, 0f,1f), 40 * Time.deltaTime);
    }
}
