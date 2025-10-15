using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float smoothing = 10f;

    private Vector3 currentVelocity = Vector3.zero;

    void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        Vector3 targetVelocity = moveDirection * targetSpeed;

        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, smoothing * Time.deltaTime);

        transform.position += currentVelocity * Time.deltaTime;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothing * Time.deltaTime);
        }
    }
}