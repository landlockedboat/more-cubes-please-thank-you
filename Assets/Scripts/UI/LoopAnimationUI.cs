using UnityEngine;
using System.Collections;

public abstract class LoopAnimationUI : MonoBehaviour {
    [Header("Animation Loop")]
    [SerializeField]
    float timeToStart = 0f;
    [Range(0, 10)]
    [SerializeField]
    float loopDuration = 1f;
    [SerializeField]
    bool animateOnStart = true;
    [SerializeField]
    bool animateOnEnable = false;
    [SerializeField]
    bool resetValuesOnRestart = false;

    
    float currentTime;

    void Start () {
        InitValues();
        if(animateOnStart)
            StartCoroutine("LoopAnimation");
    }

    public void StartAnimation()
    {
        if (resetValuesOnRestart)
            ResetValues();
        StartCoroutine("LoopAnimation");
    }

    void OnEnable()
    {
        if (animateOnEnable)
        {
            InitValues();
            StartCoroutine("LoopAnimation");
        }
    }

    IEnumerator LoopAnimation()
    {
        yield return new WaitForSeconds(timeToStart);
        currentTime = 0;
        bool animating = true;
        while (true)
        {
            currentTime += Time.deltaTime;
            float animationProgress = currentTime / loopDuration;
            if (animating)
            {                
                Animate(animationProgress);
            }
            else
            {
                Deanimate(animationProgress);
            }
            if (currentTime >= loopDuration)
            {
                animating = !animating;
                currentTime = 0;
            }                
            yield return null;
        }
    }

    protected abstract void Animate(float progress);

    protected abstract void Deanimate(float progress);

    protected abstract void InitValues();

    protected abstract void ResetValues();
}
