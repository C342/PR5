using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Knockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float cooldownTime = 0.5f;
    [SerializeField] private float knockbackDecay = 5f;

    [SerializeField] private Transform playerCamera;
    private CharacterController controller;

    private Vector3 knockbackVelocity = Vector3.zero;
    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (playerCamera == null && Camera.main != null)
            playerCamera = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isOnCooldown)
        {
            FireShotgun();
            StartCooldown();
        }

        if (knockbackVelocity.magnitude > 0.1f)
        {
            controller.Move(knockbackVelocity * Time.deltaTime);

            knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, Time.deltaTime * knockbackDecay);
        }

        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
                isOnCooldown = false;
        }
    }

    private void FireShotgun()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("Player Camera not assigned to Knockback script!");
            return;
        }

        Vector3 knockbackDir = -playerCamera.forward.normalized;

        knockbackVelocity = knockbackDir * knockbackForce;

        Debug.DrawRay(transform.position, knockbackDir * 2f, Color.red, 1f);
    }

    private void StartCooldown()
    {
        isOnCooldown = true;
        cooldownTimer = cooldownTime;
    }
}