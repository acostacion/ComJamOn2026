using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Referencias

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject map;
    #endregion

    #region Propiedades

    private GameObject UI;  //Marca el menu que se esta mostrando actualmente
    #endregion

    #region Metodos

    private void Start()
    {
        //Marcamos todos los menus como desactivados
        mainMenu.SetActive(false);
        map.SetActive(false);

        UI = mainMenu;
        UI.SetActive(true);
    }

    public void Play()
    {
        UI.SetActive(false);
        UI = map;
        UI.SetActive(true);

    }

    public void MainMenu()
    {
        UI.SetActive(false);
        UI = mainMenu;
        UI.SetActive(true);
    }
    #endregion
}
