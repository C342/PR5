using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletObject;
    public Transform player;
    public float shootCooldown = 2f;
    public float shootSpeed = 40f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootCooldown)
        {
            ShootPlayer();
            timer = 0f;
        }
    }

    void ShootPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        GameObject Bullet = Instantiate(bulletObject, transform.position, Quaternion.identity);
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();

        rb.linearVelocity = direction * shootSpeed;

        Destroy(Bullet, 3f);
    }
}