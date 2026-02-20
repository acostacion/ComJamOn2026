using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    SpriteRenderer sprite;
    bool flip;
    Vector2 moveInput;

    [SerializeField]
    private float vel = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        sprite = GetComponent<SpriteRenderer>();
        flip = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movePos = new Vector3(moveInput.x, 0, moveInput.y);
        characterController.Move(movePos * Time.deltaTime * vel);
        if (moveInput.x > 0)
            sprite.flipX = true;
        else if (moveInput.x < 0) 
            sprite.flipX = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
