using UnityEngine;
using System.Collections;

public class MissileLogic : BulletLogic {
    [SerializeField]
    float explosionRadius;

    void OnTriggerEnter(Collider col)        
    {
        if (col.tag == "Enemy")
        {
            foreach (Collider c in Physics.OverlapSphere(transform.position, explosionRadius))
            {
                if (c.tag == "Enemy")
                    c.GetComponent<EnemyLogic>().Kill(transform.position, true);
            }
            Destroy(gameObject);
        }  
    }
}
