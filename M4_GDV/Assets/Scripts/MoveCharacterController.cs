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

    private InputAction sprintAction;
    private InputAction moveAction;
    private InputAction jumpAction;
    private CharacterController characterController;
    private Animator animator;

    private Vector2 movementInput;
    private float verticalVelocity;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
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
                // Only allow jump if the landing animation has finished
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                bool landingDone = !stateInfo.IsName("Landing") || stateInfo.normalizedTime >= 1f;

                if (landingDone)
                {
                    // v = sqrt(2 * |gravity| * jumpHeight)
                    verticalVelocity = Mathf.Sqrt(2f * Mathf.Abs(gravity) * jumpHeight);
                    animator.SetTrigger("JumpTrigger");
                }
            }
        }
        else
        {
            //not grounded, apply gravity           
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity * Time.deltaTime;

        characterController.Move(move);

        // Animation
        animator.SetFloat("InputVertical", movementInput.y);
        animator.SetBool("Grounded", characterController.isGrounded);
    }
}
