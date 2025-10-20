using System.Xml;
using UnityEngine;

public class DamageWeapon : MonoBehaviour
{
    public float MaxDmg = 50f;
    public float MinDmg = 5f;
    public float BulletRange;

    private Transform PlayerCamera;

    private void Start()
    {
        PlayerCamera = Camera.main.transform;
    }

    public void Shoot()
    {
        Ray gunRay = new Ray(PlayerCamera.position, PlayerCamera.forward);

        if (Physics.Raycast(gunRay, out RaycastHit hitInfo, BulletRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out Entity enemy))
            {
                float distance = hitInfo.distance;
                float t = Mathf.Clamp01(distance / BulletRange);
                float scaledDamage = Mathf.Lerp(MaxDmg, MinDmg, t);

                enemy.Health -= scaledDamage;

                Debug.Log($"Hit {enemy.name} for {scaledDamage:F1} damage at {distance:F1}m");
            }
        }
    }
}