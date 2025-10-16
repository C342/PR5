using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float StartHealth;
    private float health;

    public float Health
    {
        get
        {
            return Health;
        }
        set
        {
            health = value;
            Debug.Log(Health);

            if (health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        Health = StartHealth;
    }
}