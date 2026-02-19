using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DragComponent : MonoBehaviour
{
    [Tooltip("Object to move")] 
    [SerializeField] private GameObject targetObject;

    [Tooltip("Minimum distance until drag starts")] 
    [SerializeField] private float minDragDistance = 0.1f;

    [Tooltip("Camera to calculate mouse position")] 
    [SerializeField] private Camera camera;

    private float fixedZ;

    private Vector3 mousePosition;

    private bool isDraggable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (camera == null)
            camera = Camera.main;

        fixedZ = transform.position.z;
        mousePosition = Vector3.zero;
        isDraggable = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        //Si pulso, vemos si estamos sobre un objeto
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //Si coincide
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    isDraggable = true;
                }
            }
        }

        //Si mantengo pulsado en un objeto, lo arrastro
        if (Mouse.current.leftButton.isPressed && isDraggable)
        {
            GetMousePosition();
            transform.position = new Vector3(mousePosition.x, mousePosition.y, fixedZ);
        }
        
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDraggable = false;
        }
    }

    Vector3 GetMousePosition()
    {
        //Vamos a trazar un rayo desde la camara hasta el cursor del raton
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        //Guardaremos la informacion en el RaycastHit
        RaycastHit rayHit;
        //Guardamos la posicion del raton
        if (Physics.Raycast(ray, out rayHit))
        {
            mousePosition = rayHit.point;
        }

        return mousePosition;
    }
}
