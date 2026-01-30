using UnityEngine;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    private float maxHealth;
    private float currentHealth;

    void Start()
    {
        if (healthText == null)
        {
            healthText = GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        currentHealth = health;
        UpdateHealthText();
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        UpdateHealthText();
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
        }
    }
}