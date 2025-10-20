using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float StartHealth;
    private float health;

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = Mathf.Max(0, value);

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