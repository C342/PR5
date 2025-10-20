using UnityEngine;

public class AudioOnClick : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private float cooldownTime = 3f;

    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isOnCooldown)
        {
            PlaySound();
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

    private void PlaySound()
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource not assigned!");
            return;
        }

        if (clickSound == null)
        {
            Debug.LogWarning("AudioClip not assigned!");
            return;
        }

        audioSource.Stop();
        audioSource.PlayOneShot(clickSound);
    }

    private void StartCooldown()
    {
        isOnCooldown = true;
        cooldownTimer = cooldownTime;
    }
}