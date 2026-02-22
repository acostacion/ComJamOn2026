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
        if (other.transform.root == transform.root) return;

        Debug.Log(this.name + vidaact);
        if (other.GetComponentInParent<Attack>() != null
            && other.GetComponentInParent<Attack>().getAttacking())
        { 
            Debug.Log("Pega el player");
        
            vidaact -= other.gameObject.GetComponentInParent<Attack>().getDamage();
            //Comprobamos si sigue vivo
            if (vidaact <= 0) Destroy(gameObject);

            other.gameObject.GetComponentInParent<Attack>().Atacado();

            Debug.Log(this.name + vidaact);
        }

        if (other.GetComponentInParent<EnemyComponent>() != null
            && other.GetComponentInParent<EnemyComponent>().getAttacking())
        {
            Debug.Log("Pega el bicho");
            vidaact -= other.GetComponentInParent<EnemyComponent>().getDamage();
            if (vidaact <= 0)
            {
                Reinicio();
                other.gameObject.GetComponentInParent<Health>().Reinicio();

            }
            Debug.Log(this.name + vidaact);
        }
    }

    public void Reinicio()
    {
        vidaact = vida;
    }
}
