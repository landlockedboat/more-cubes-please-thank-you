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
    [SerializeField]
    private float healthRegenRate = 1f;
    [SerializeField]
    private float healthRegenTime = .1f;
    private float currentHealth;
    private MeshRenderer meshRenderer;

    private static PlayerHealth playerHealth;

    public static PlayerHealth instance
    {
        get
        {
            if (!playerHealth)
            {
                playerHealth = FindObjectOfType<PlayerHealth>();
                if (!playerHealth)
                {
                    Debug.LogError("There needs to be one active PlayerHealth script on a GameObject in your scene.");
                }
                else
                {
                    playerHealth.Init();
                }
            }

            return playerHealth;
        }
    }

    void Init()
    {
        currentHealth = maxHealth;
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        StartCoroutine("Heal");
    }

    public static float MaxHealth
    {
        get
        {
            return instance.maxHealth;
        }

        set
        {
            instance.maxHealth = value;
            instance.currentHealth = instance.maxHealth;
        }
    }

    public static void Hurt(float amount) {
        instance.currentHealth -= amount;
        instance.meshRenderer.material.color =
            Color.Lerp(instance.healthyColor, instance.hurtColor, 
            (instance.maxHealth - instance.currentHealth) / instance.maxHealth);
        if (instance.currentHealth < 0)
        {
            Destroy(instance.gameObject);
        }
    }

    IEnumerator Heal()
    {
        while (true)
        {
            if(currentHealth < maxHealth)
            {
                currentHealth += healthRegenRate;                
            }
            yield return new WaitForSeconds(healthRegenTime);
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.OnUpgradesShown, OnUpgradesShown);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.OnUpgradesShown, OnUpgradesShown);
    }

    void OnUpgradesShown()
    {
        currentHealth = maxHealth;
    }
}
