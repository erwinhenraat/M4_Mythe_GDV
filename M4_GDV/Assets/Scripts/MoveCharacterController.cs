using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCharacterController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAsset;
    [SerializeField] private string mapName;
    [SerializeField] private float moveSpeed = 250f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -20f;
    private InputActionMap map;
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction jumpAction;
    private float verticalVelocity;
    private CharacterController characterController;
    public Vector2 movementInput; //value will be usable in other scripts, for example to call animations


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        map = inputAsset.FindActionMap(mapName);
        moveAction = map.FindAction("Move");
        sprintAction = map.FindAction("Sprint");
        jumpAction = map.FindAction("Jump");
    }
    void OnEnable()
    {
        map.Enable();
    }
    void OnDisable()
    {
        map.Disable();
    }

    void Update()
    {
        movementInput = moveAction.ReadValue<Vector2>();

        if (sprintAction.IsPressed())
        {
            movementInput = movementInput.normalized * sprintMultiplier;
        }
        else
        {
            movementInput = movementInput.normalized;
        }

        // Forward movement
        Vector3 move = transform.forward * movementInput.y * moveSpeed * Time.deltaTime;

        // Rotation
        transform.Rotate(Vector3.up * movementInput.x * rotationSpeed * Time.deltaTime);

        // Gravity and jumping
        if (characterController.isGrounded)
        {
            verticalVelocity = -1f; // small downward force to stay grounded

            if (jumpAction.WasPressedThisFrame())
            {

                // v = sqrt(2 * |gravity| * jumpHeight)
                // the jump height is determined by the initial velocity and the gravity,
                // so we calculate the initial velocity needed to reach the desired jump height with the given gravity
                verticalVelocity = Mathf.Sqrt(2f * Mathf.Abs(gravity) * jumpHeight);
            }
        }
        else
        {
            //not grounded, apply gravity
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity * Time.deltaTime;

        characterController.Move(move);
    }
}
