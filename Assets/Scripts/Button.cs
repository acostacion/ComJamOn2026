using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Button : MonoBehaviour
{
   [SerializeField] 
   Sprite sprite; // TODO

    [SerializeField] private Camera _camera;

    [SerializeField] private int _nScene;

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

        if(isMouseIn && Mouse.current.leftButton.IsPressed())
        {
            ChangeScene(_nScene);
        }
    }
}
