using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Button : MonoBehaviour
{
   [SerializeField] 
   Sprite sprite;

    [SerializeField] private Camera _camera;

    [SerializeField] private int _nScene;

    [SerializeField] private int _nUI;


    //Todo esto es para la animación de la cámara
    private Vector3 camaraCerca;
    private bool moving;
    [SerializeField] private float smoothCamera;

    private void Start()
    {
        //La cámara comienza en el punto 0
        _camera.transform.position = Vector3.zero;

        camaraCerca = new Vector3(0, -0.7f, 4.44f);
        moving = false;
    }

    public void Completado() // TODO
    {
        GetComponent<Image>().sprite = sprite;
    }

    public void ChangeScene(int scene)
    {
        GameManager.Instance.LoadScene(scene);
    }

    private void Update()
    {
        // RAYCAST A CARTEL CON EL INPUT NUEVO

        Vector3 mousePos = Mouse.current.position.ReadValue();

        Ray ray = _camera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        bool isMouseIn = Physics.Raycast(ray, out hit);

        if (_nScene != -1 && isMouseIn && Mouse.current.leftButton.IsPressed())
        {
            //Cambia de escena cuando se pulsa
            ChangeScene(_nScene);
        }
        else if (_nScene == -1 && isMouseIn && Mouse.current.leftButton.IsPressed())
        {
            moving = true;
            GetComponentInParent<Rigidbody>().
                transform.parent.GetComponentInParent<Rigidbody>().isKinematic = false;

            //Activamos la siguiente UI
            GameManager.Instance.SetUi(_nUI);
        }


        //Movimiento de la cámara
        if (moving)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, camaraCerca, smoothCamera);
            moving = _camera.transform.position != camaraCerca;
        }
    }
}
