using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyComponent : MonoBehaviour
{
    Transform _tf;

    private float _posZ;
    private float _posY;

    [SerializeField] private float _vel = 3.0f;

    // posiciones entre las que oscila el malo
    [SerializeField] private GameObject _posA;
    [SerializeField] private GameObject _posB;

    private GameObject _actualGoing;

    [SerializeField] private GameObject _triggerArma;
    [SerializeField] private int _damage = 1;
    private float _cooldown;

    void Start() {
        _tf = GetComponent<Transform>();

        // se dirige a B porque esta en A al empezar.
        _actualGoing = _posB;

        // inicialmente desactivado.
        _triggerArma.SetActive(false);
    }

    void Update()
    {
        movement();

        // si cooldown aun no ha llegado a cero...
        if (_cooldown > 0) {
            _cooldown -= Time.deltaTime;
        }
        else {
            attack();
            _cooldown = Random.Range(2.0f, 4.0f); // vuelve a poner el cooldown aleatorio en ese rango tras atacar
        }
    }

    private void movement() {
        transform.position = Vector3.MoveTowards(transform.position, _actualGoing.transform.position, _vel * Time.deltaTime);

        if (transform.position == _posA.transform.position) _actualGoing = _posB;

        if (transform.position == _posB.transform.position) _actualGoing = _posA;
    }

    private void attack() {
        //Activamos el trigger del arma
        _triggerArma.SetActive(true);
        Invoke(nameof(deactivateWeapon), 0.5f); // no se desactiva hasta pasados 3 segundos.
    }

    private void deactivateWeapon() {
        _triggerArma.SetActive(false);
    }
    
}

