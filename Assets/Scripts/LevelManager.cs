using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Controla todo lo que sucede en un nivel
    #region Referencias

    [SerializeField]
    GameObject Teatro;  //Todo lo referente a la parte del teatro

    [SerializeField]
    GameObject Investigacion;   //Todo lo referente a la parte de moverse e interactuar
    GameObject player;

    [SerializeField]
    GameObject Resolucion;

    #endregion

    #region Propiedades

    bool objetivo;  //Booleano que marca si se ha cumplido o no el objetivo

    #endregion

    #region Metodos

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objetivo = false;
    }

    public void finish()
    {
        //Volvemos al menú de los mapas
        GameManager.Instance.LoadScene(0);
    }

    #endregion
}
