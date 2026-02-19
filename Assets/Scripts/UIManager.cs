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
        {
            menu.SetActive(false);
        }

        //Cogemos los botones del mapa para marcar los que estén completados
        Button[] b = menus[(int)GameManager.menus.MAP].GetComponentsInChildren<Button>();

        for (int i = 0; i < b.Length; i++) {

            if(GameManager.instance.GetLevel(i + 1))
            {
                b[i].Completado();
            }
        }

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
