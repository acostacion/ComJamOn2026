using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    #region Propiedades

    public static GameManager instance = null;

    //Necesitamos saber que niveles hemos completado
    //Guardamos el nivel y si se ha completado o no
    //El primer nivel es el 1
    private Dictionary<int, bool> niveles;

    private int level;
    [SerializeField] private int numLevels;
    [SerializeField] private GameObject levels;

    //Guardamos que menu estaba antes de cambiar de escena
    public enum menus
    {
        MAINMENU,
        MAP
    }

    private int ui;
    #endregion

    #region getters/Setters
    //Hacemos un acceso para que puedan acceder al singleton
    static public GameManager Instance { get { return instance; } }

    //Devuelve si un nivel ha sido completado o no
    public bool GetLevel(int level)
    {
        if (niveles.ContainsKey(level))
            return niveles[level];
        return false;
    }

    public void SetLevel(int level, bool value)
    {
        if (niveles.ContainsKey(level))
            niveles[level] = value;
    }

    public int GetActLevel() { return level; }

    public int GetUi()
    {
        return ui;
    }

    public void SetUi(int _ui)
    {
        ui = _ui;

        switch (ui)
        {
            case (int)menus.MAP:
                //instanciamos las cajas de los niveles
                for (int i = 0; i < numLevels; i++)
                {
                    Vector3 pos = new Vector3(0, 0, 0);
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    Instantiate(levels, pos, rot);
                }
                break;
        }
    }

    #endregion

    #region Metodos
    //Mantenemos siempre una única instancia de GameManager
    //No permitimos que se elimine entre escenas
    private void Awake()
    {
        //Si la instancia es nula  la asignamos
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            ui = (int)menus.MAINMENU;   //Menu que tiene que salir al iniciar el juego
        }
        else
        {
            //Si ya existe una instancia no creamos otra
            Destroy(this.gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        niveles = new Dictionary<int, bool>();

        //Inicializamos la lista de los niveles
        for (int i = 0; i < numLevels; i++)
            niveles.Add(i + 1, false);
    }

    //Cargamos una escena nueva
    public void LoadScene(int scene)
    {
        level = scene;

        // cambia musica
        if (MusicManager.Instance != null)
            MusicManager.Instance.PlayMusicForScene(scene);

        SceneManager.LoadScene(scene);
    } 

    #endregion

}
