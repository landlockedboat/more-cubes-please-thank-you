using UnityEngine;
using System.Collections;

public class TutorialBulletLogic : MonoBehaviour {

    float speed;
    [SerializeField]
    float timeToDie = 3f;

    protected void Start()
    {
        speed = TutorialLogic.Speed;
        StartCoroutine("Die");
    }

    void Update()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(timeToDie);
        Destroy(gameObject);
    }
}
