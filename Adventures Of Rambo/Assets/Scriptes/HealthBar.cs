using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthFillImage; // Reference to the HealthFill image
    [SerializeField] private Health playerHealth; // Reference to the player's Health script

    private void Start()
    {
        if (playerHealth != null)
        {
            // Subscribe to the health change event
            playerHealth.OnHealthChanged += UpdateHealthBar;
            UpdateHealthBar(); // Initialize health bar value
        }
    }

    private void UpdateHealthBar()
    {
        if (playerHealth != null)
        {
            float healthPercentage = (float)playerHealth.CurrentHealth / playerHealth.MaxHealth;
            healthFillImage.fillAmount = healthPercentage;
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHealthBar;
        }
    }
}
