using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params 
    [SerializeField] AudioClip destroyBlockSound;
    [SerializeField] GameObject blockSparklesVfx;
    [SerializeField] Sprite[] hitSprites;

    // cached refrence 
    Level level;
    GameSession gamestatus;
    Ball ball;

    //state vars
    [SerializeField] int timesHit; //serialized for debug purposes 
    int timesHitBuster;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        gamestatus = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
        //CountBreakableBlocks();
    }

    //private void CountBreakableBlocks()
    //{
    //    if (tag == "Breakable")
    //    {
    //        level.CountBlocks();
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Dud ")
        {
            ball.KillSpeed();
        } 

        if (tag == "Buster")
        {        
            HandleBusterHit();
        }

        if (tag == "Dud ")
        {
            Debug.Log("Dud HandleHit Called");
            HandleHit();
        }

        if (tag == "Max Speed")
        {
            Debug.Log("Max Speed HandleHit Called");
            HandleHit();
        }

    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitStrike();
        }

    }

    private void HandleBusterHit()
    {
       if(ball.ballSpeed >= 30)
        {
            Debug.Log("Buster count added!");
            timesHitBuster++;
        }
       if(timesHitBuster==10)
        {
            DestroyBlock();
        }
    }

    private void ShowNextHitStrike()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block Sprite is missing from array" + gameObject.name, gameObject);
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        //level.BlockDestroyed();
        TriggerSparklesVFX();

        if (tag == "Max Speed")
        {
          ball.AddMaxSpeed();
        }
        
        
    }

    private void PlayBlockDestroySFX()
    {
        gamestatus.AddToScore();
        AudioSource.PlayClipAtPoint(destroyBlockSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVfx, transform.position, transform.rotation);
        Destroy(sparkles, 2);
    }

}
