using UnityEngine;
using System.Collections;
public class DestroyWhenNotVisible : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
