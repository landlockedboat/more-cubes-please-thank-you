using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Color healthyColor;
    [SerializeField]
    private Color hurtColor;
    [SerializeField]
    private float maxHealth = 100;
    private float health;
    private MeshRenderer meshRenderer;

    public void Hurt(float amount) {
        health -= amount;
        meshRenderer.material.color =
            Color.Lerp(healthyColor, hurtColor, (maxHealth - health) / maxHealth);
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    
    }

    // Update is called once per frame
    void Update()
    {

    }


}
