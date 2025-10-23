using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health targetHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;

    [Header("Color Settings")]
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color lowHealthColor = Color.red;

    private float displayedHealth;

    private void Start()
    {
        if (targetHealth == null)
        {
            Debug.LogWarning($"{name}: No target Health assigned. Trying to find one on same GameObject...");
            targetHealth = GetComponent<Health>();
        }

        if (targetHealth != null && healthSlider != null)
        {
            healthSlider.maxValue = targetHealth.MaxHealth;
            healthSlider.value = targetHealth.CurrentHealth;
            displayedHealth = targetHealth.CurrentHealth;
        }
    }

    private void Update()
    {
        if (targetHealth == null || healthSlider == null)
            return;

        float targetValue = targetHealth.CurrentHealth;

        displayedHealth = Mathf.Lerp(displayedHealth, targetValue, Time.deltaTime * 10f);
        healthSlider.value = displayedHealth;

        UpdateHealthColor(displayedHealth / targetHealth.MaxHealth);
    }

    private void UpdateHealthColor(float normalizedValue)
    {
        if (fillImage != null)
        {
            fillImage.color = Color.Lerp(lowHealthColor, fullHealthColor, normalizedValue);
        }
    }
}