using UnityEngine;
using System.Collections;

public class EnemyGeomLogic : MonoBehaviour {

    MeshRenderer meshRenderer;

    public void Init(Color color)
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = color;
        StartCoroutine("Grow");
    }

    IEnumerator Grow()
    {
        float OGScale = transform.localScale.x;
        transform.localScale = new Vector3(.01f, .01f, .01f);
        float growSpeed = SpawnerControl.EnemyGrowingSpeed;
        bool finished = false;
        while (!finished)
        {
            float growDelta = growSpeed * Time.deltaTime;
            transform.localScale += new Vector3(growDelta, growDelta, growDelta);
            if (transform.localScale.x >= OGScale)
                finished = true;
            yield return null;
        }
    }
}
