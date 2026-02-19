using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Referencias

    
    [SerializeField] 
    GameObject[] menus;
    #endregion

    #region Propiedades

    private GameObject UI;  //Marca el menu que se esta mostrando actualmente
    #endregion

    #region Metodos

    private void Start()
    {
        //Marcamos todos los menus como desactivados
        foreach (GameObject menu in menus)
            menu.SetActive(false);

        UI = menus[GameManager.Instance.GetUi()];
        UI.SetActive(true);
    }

    public void Play()
    {
        UI.SetActive(false);
        UI = menus[(int)GameManager.menus.MAP];
        UI.SetActive(true);

        GameManager.instance.SetUi((int)GameManager.menus.MAP);
    }

    public void MainMenu()
    {
        UI.SetActive(false);
        UI = menus[(int)GameManager.menus.MAINMENU];
        UI.SetActive(true);
        GameManager.instance.SetUi((int)GameManager.menus.MAINMENU);
    }
    #endregion
}
