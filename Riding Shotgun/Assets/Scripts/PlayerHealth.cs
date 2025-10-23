using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] private float maxDamageTaken = 15f;
    [SerializeField] private float minDamageTaken = 2.5f;

    private float currentHealth;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health playerHealth = collision.gameObject.GetComponent<Health>();

        if (playerHealth != null)
        {
            float randomDamage = Random.Range(minDamageTaken, maxDamageTaken);

            playerHealth.ChangeHealth(-randomDamage);

            Debug.Log($"{collision.gameObject.name} took {randomDamage:F1} damage!");
        }
    }

    public void ChangeHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log($"{gameObject.name} Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}