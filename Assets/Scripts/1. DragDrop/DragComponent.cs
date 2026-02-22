using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DragComponent : MonoBehaviour
{
    [Tooltip("Camera to calculate mouse position")] 
    [SerializeField] private Camera camera;

    private float fixedZ;

    private Vector3 mousePosition;

    private bool grabbedByMouse;


    private Vector3 originalPosition;

    private DropComponent currentZone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (camera == null)
            camera = Camera.main;

        fixedZ = transform.position.z;
        mousePosition = Vector3.zero;
        grabbedByMouse = false;
        originalPosition = transform.position;
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
                    grabbedByMouse = true;
                }
            }
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            grabbedByMouse = false;
            TryDrop();
        }

        //Si mantengo pulsado en un objeto, lo arrastro
        if (grabbedByMouse)
        {
            GetMousePosition();
            transform.position = new Vector3(mousePosition.x, mousePosition.y, fixedZ);
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

    void TryDrop()
    {
        if (currentZone != null)
        {
            LevelManager.instance.placePiece(gameObject, currentZone.gameObject);
        }
        else
        {
            ReturnToOrigin();
        }
    }

    public void ReturnToOrigin()
    {
        // Si no hay zona de drop, volver al origen
        transform.position = originalPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        DropComponent zone = other.GetComponent<DropComponent>();

        if (zone != null)
        {
            currentZone = zone;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DropComponent zone = other.GetComponent<DropComponent>();

        if (zone != null && currentZone == zone)
        {
            currentZone = null;
        }
    }
}
