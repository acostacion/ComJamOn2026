using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GrabComponent : MonoBehaviour
{
    [Tooltip("Minimum distance for interaction")]
    [SerializeField] private float interactionDistance = 1.0f;

    private Key interactKey = Key.P;

    private Transform player;

    private bool grabbedByKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

        //Para gestionar si esta escena tiene player o no
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Calculamos la distancia entre el jugador y el objeto
        float dist = Vector3.Distance(player.position, transform.position);

        //Si estamos a suficiente distancia y se pulsa la tecla
        if (dist <= interactionDistance &&
            Keyboard.current[interactKey].wasPressedThisFrame)
        {
            //Cogemos el objeto
            grabbedByKey = true;
        }

        //Si tenemos un objeto agarrado y soltamos la tecla
        else if (grabbedByKey &&
            Keyboard.current[interactKey].wasReleasedThisFrame)
        {
            grabbedByKey = false;
        }

        if (grabbedByKey)
        {
            transform.position = new Vector3(player.position.x + 1, player.position.y, player.position.z);
        }

    }
}
