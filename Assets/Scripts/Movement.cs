using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    Transform myTransform;
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
        myTransform = GetComponent<Transform>();
        flip = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movePos = new Vector3(moveInput.x, myTransform.position.y, myTransform.position.z);
        characterController.Move(new Vector3(moveInput.x * Time.deltaTime * vel, 0, 0));
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
