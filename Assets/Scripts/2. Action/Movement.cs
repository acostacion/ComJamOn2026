using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    SpriteRenderer sprite;
    Animator animator;

    float flip;
    public Vector2 moveInput;

    private float posZ;
    private float posY;

    [SerializeField] private float vel = 5;
    [SerializeField] private bool canFlip;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        flip = 1;

        posZ = transform.position.z;
        posY = transform.position.y;
    }

    void Update()
    {
        characterController.Move(new Vector3(moveInput.x * Time.deltaTime * vel, 0, 0));

        transform.position = new Vector3(transform.position.x, posY, posZ);

        // Animacion
        if(animator != null )
            animator.SetFloat("Speed", Mathf.Abs(moveInput.x));

        // Flip sprite
        if (canFlip && moveInput.x != flip && moveInput.x != 0)
        {
            sprite.flipX = !sprite.flipX;
            flip = moveInput.x;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}