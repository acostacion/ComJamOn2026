using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private int vida;

    private int vidaact;

    private void Start()
    {
        vidaact = vida;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Attack>() != null)
        {
            vidaact -= other.gameObject.GetComponentInParent<Attack>().getDamage();
            //Comprobamos si sigue vivo
            //Si su vida está por debajo de 0 lo eliminamos

            //Si es el player reiniciamos la batalla. Todos recuperan su vida
            if (gameObject.tag == "Player" && vidaact <= 0)
            {
                Reinicio();
                other.gameObject.GetComponentInParent<Health>().Reinicio();
            }
            else if (vidaact <= 0) Destroy(gameObject);
        }
    }

    public void Reinicio()
    {
        vidaact = vida;
    }
}
