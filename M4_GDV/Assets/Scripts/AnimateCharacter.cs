using UnityEngine;

public class AnimateCharacter : MonoBehaviour
{
    private Animator animator;
    private MoveCharacterController moveCharacterController;
    void Awake()
    {
        animator = GetComponent<Animator>();
        moveCharacterController = transform.parent.GetComponent<MoveCharacterController>();

    }
    // Update is called once per frame
    void Update()
    {
        Vector2 movementInput = moveCharacterController.movementInput;

        animator.SetFloat("Vertical", movementInput.y);
    }
}
