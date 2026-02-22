using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private GameObject triggerArma;

    [SerializeField]
    private int damage;

    private bool attacking;

    public int getDamage()
    {
        return damage;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerArma.SetActive(false);
        attacking = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(Ataque());
    }

    IEnumerator Ataque()
    {
        //Activamos el trigger del arma
        triggerArma.SetActive(true);
        yield return new WaitForSeconds(0.5f); // Espera 3 segundos
        triggerArma.SetActive(false);
    }
}
