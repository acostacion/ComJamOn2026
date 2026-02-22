using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    Transform myTransform;
    SpriteRenderer sprite;
    float flip;
    Vector2 moveInput;

    private float posZ;
    private float posY;

    [SerializeField]
    private float vel = 5;

    [SerializeField]
    private bool canFlip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        sprite = GetComponent<SpriteRenderer>();
        myTransform = GetComponent<Transform>();
        flip = 1;   //Siempre comenzamos mirando a la derecha

        posZ = myTransform.position.z;
        posY = myTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(new Vector3(moveInput.x * Time.deltaTime * vel, 0, 0));

        transform.position = new Vector3(transform.position.x, posY, posZ);

        //Si cambiamos de dirección volteamos el sprite
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
