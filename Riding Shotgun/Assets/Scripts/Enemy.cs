using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletObject;
    public Transform player;
    public Transform firePoint;

    public float shootCooldown = 2f;
    public float shootSpeed = 40f;
    public float maxShootDistance = 100f;
    public LayerMask obstacleLayer;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootCooldown)
        {
            if (CanSeePlayer())
            {
                ShootPlayer();
            }
            timer = 0f;
        }
    }

    bool CanSeePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        float DistanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, maxShootDistance, ~0))
        {
            Color lineColor = (hit.transform == player) ? Color.green : Color.red;
            Debug.DrawLine(transform.position, hit.point, lineColor);

            return hit.transform == player;
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + direction * maxShootDistance, Color.gray);
            return false;
        }
    }

    void ShootPlayer()
    {
        Transform spawn = firePoint != null ? firePoint : transform;

        Vector3 direction = (player.position - spawn.position).normalized;
        GameObject Bullet = Instantiate(bulletObject, spawn.position, Quaternion.LookRotation(direction));
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = direction * shootSpeed;
            rb.angularVelocity = Vector3.zero;
        }

        Destroy(Bullet, 3f);

        Debug.DrawLine(spawn.position, spawn.position + direction * 3f, Color.white, 0.5f);
    }
}