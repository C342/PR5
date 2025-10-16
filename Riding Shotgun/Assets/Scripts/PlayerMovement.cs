using UnityEngine;

public class First_Person_Movement : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private bool Sneaking = false;
    private float xRotation;

    [Header("Components")]
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController Controller;
    [SerializeField] private Transform Player;
    [Space]
    [Header("Movement Settings")]
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float LookSensitivity;
    [SerializeField] private float Gravity = 9.81f;
    [Space]
    [Header("Crouching")]
    [SerializeField] private bool Crouch = false;
    [SerializeField] private float CrouchSpeed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MoveCamera();

        if (Input.GetKey(KeyCode.LeftControl) && Crouch)
        {
            Player.localScale = new Vector3(1f, 0.5f, 1f);
            Sneaking = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Player.localScale = new Vector3(1f, 1f, 1f);
            Sneaking = false;
        }
    }
    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);


        if (Controller.isGrounded)
        {
            Velocity.y = -1f;

            if (Input.GetKeyDown(KeyCode.Space) && Sneaking == false)
            {
                Velocity.y = JumpForce;
            }
        }
        else
        {
            Velocity.y += Gravity * -2f * Time.deltaTime;
        }
        if (Sneaking)
        {
            Controller.Move(MoveVector * CrouchSpeed * Time.deltaTime);
        }
        else
        {
            Controller.Move(MoveVector * Speed * Time.deltaTime);
        }
        Controller.Move(Velocity * Time.deltaTime);

    }
    private void MoveCamera()
    {
        xRotation -= PlayerMouseInput.y * LookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(0f, PlayerMouseInput.x * LookSensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}