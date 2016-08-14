using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]
public class SimpleAnimation : MonoBehaviour {
    [SerializeField]
    float timeBetweenFrames = .1f;
    [SerializeField]
    bool hideOnFinish = false;
    //[SerializeField]
    //float startDelay = 0f;
    [SerializeField]
    Sprite[] sprites;
    //[SerializeField]
    //bool playOnStart = false;
    //[SerializeField]
    //bool loop = false;
    //[SerializeField]
    //float loopTime = 1f;

    SpriteRenderer spriteRenderer;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartAnimation()
    {
        StopAllCoroutines();
        StartCoroutine("Animate");
    }	

    IEnumerator Animate()
    {
        foreach (Sprite s in sprites)
        {
            spriteRenderer.sprite = s;
            yield return new WaitForSeconds(timeBetweenFrames);
        }
        if (hideOnFinish)
            spriteRenderer.sprite = null;
    }
}
