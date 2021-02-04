using System.Collections;
using UnityEngine;

public class AfterImageEffect : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("SpawnTrail", 0, 0.01f); // replace 0.2f with needed repeatRate
    }

    void SpawnTrail()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        trailPart.transform.position = transform.position;
        Destroy(trailPart, 0.05f); // replace 0.5f with needed lifeTime

        StartCoroutine("FadeTrailPart", trailPartRenderer);
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a -= 0.3f; // replace 0.5f with needed alpha decrement
        trailPartRenderer.color = color;

        yield return new WaitForEndOfFrame();
    }

}
