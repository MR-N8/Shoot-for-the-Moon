using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Color switchON = new Color(0, 0, 0, 1);
    public Color switchOff = new Color(1, 1, 1, 1);

    [SerializeField] SpriteRenderer spriteRenderer;
    
    bool isON = false; 
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("switch OFF on start");
        spriteRenderer.color = switchOff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>()!= null)
        {
            Debug.Log($"Collision with swtitch triggered! {collision.gameObject.name}");
            OnBallEnter();
        }
    }

    private void OnBallEnter()
    {
        spriteRenderer.color = switchON;

        isON = true;
    }
}
