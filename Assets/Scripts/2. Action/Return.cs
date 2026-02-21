using System.Collections;
using UnityEngine;

public class Return : MonoBehaviour
{

    private Transform myTransform;
    private Vector3 originalPos;

    [SerializeField]
    private GameObject caldero;
    [SerializeField]
    private string color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTransform = GetComponent<Transform>();
        originalPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        caldero.GetComponent<Caldero>().AddPotion(color);
        Destroy(this.gameObject);
    }

    //Si el objeto colisiona esperamos 3s y si no se ha cogido o no es el sitio correcto
    //Vuelve a su posición de origen
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
            StartCoroutine(OriginalPosition());
    }

    IEnumerator OriginalPosition()
    {
        yield return new WaitForSeconds(3); // Espera 3 segundos
        myTransform.position = originalPos;
    }
}
