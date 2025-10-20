using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private float weaponCooldown = 3f;

    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isOnCooldown)
        {
            PlayParticle();
            StartCooldown();
        }

        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
            }
        }
    }

    private void PlayParticle()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
        else
        {
            Debug.LogWarning("Particle system not assigned!");
        }
    }

    private void StartCooldown()
    {
        isOnCooldown = true;
        cooldownTimer = weaponCooldown;
    }
}