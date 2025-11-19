using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health targetHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;

    [Header("Settings")]
    [SerializeField] private float slideSpeed = 10f;

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

        if (targetHealth == null)
        {
            Debug.LogWarning($"{name}: No Health component found! Health bar cannot function!");
            enabled = false;
            return;
        }

        if (healthSlider != null)
        {
            healthSlider.maxValue = targetHealth.MaxHealth;
            healthSlider.minValue = targetHealth.CurrentHealth;
        }

        displayedHealth = targetHealth.CurrentHealth;
    }

    private void Update()
    {
        if (targetHealth == null || healthSlider == null)
            return;

        float targetValue = targetHealth.CurrentHealth;

        displayedHealth = Mathf.Lerp(displayedHealth, targetValue, Time.deltaTime * slideSpeed);
        healthSlider.value = displayedHealth;

        float percent = displayedHealth / targetHealth.MaxHealth;
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