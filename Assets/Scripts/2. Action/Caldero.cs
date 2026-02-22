using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Caldero : MonoBehaviour
{
    //Maneja el caldero para saber si hay que cambiar de escena o no
    private Dictionary<string, bool> pociones;

    [SerializeField]
    private string[] p;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pociones = new Dictionary<string, bool>();

        for (int i = 0; i < p.Length; i++)
            pociones[p[i]] = false;

    }

    public void AddPotion(string potion)
    {
        Debug.Log("Añadido: " + potion);
        pociones[potion] = true;

        bool completo = true;
        //Comprobamos si están todas
        foreach (KeyValuePair<string, bool> pc in pociones)
        {
            completo = pc.Value;
            if (!completo)
                break;
        }

        if (completo)
        {
            LevelManager.instance.controlPhase(LevelManager.Phases.ACTION, false);
            LevelManager.instance.controlPhase(LevelManager.Phases.RESOLUTION, true);
        }

    }
}
