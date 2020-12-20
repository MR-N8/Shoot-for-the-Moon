using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour
{

    float timeBtwSpawns;
    [SerializeField] float startTimeBtwSpawns;
    [SerializeField] float lifeTime = 3f;

    public GameObject echo;

    public Rigidbody2D rb; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GenerateEcho();
    }

    public void GenerateEcho()
    {
        if (timeBtwSpawns <= 0)
        {
            GameObject go = Instantiate(echo, transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
            Destroy(go, lifeTime);
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
