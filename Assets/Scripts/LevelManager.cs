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

    // ----- DRAG AND DROP PHASE -----
    // TODO CAMBIAR ESTO SEGÚN LAS NECESIDADES DE DISEÑO.
    private static readonly int[] _piecesPerLevel = { 3, 3, 3, 3 }; // piezas que hay por cada nivel ("array const")

    private int _nPieces; // numero de piezas draggables de este nivel (o de dropZones)
    private int _placedPieces; // piezas colocadas, incialmente cero.

    [SerializeField] private GameObject[] _pieces; // las piezas en sí.
    [SerializeField] private GameObject[] _dropZones; // las zonas de dropeo.
    // -------------------------------

    // ----- ACTION PHASE -----
    // TODO meter cosas aqui de la action phase.
    // ------------------------

    // ----- RESOLUTION PHASE -----
    bool _objetivo;  //Booleano que marca si se ha cumplido o no el objetivo
    // ----------------------------

    #endregion

    #region Metodos
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        initializeDragDropPhase();
        initializeActionPhase();
        initializeResolutionPhase();
    }

    private void Update()
    {
        // TODO no se si las coass de colisiones iban aqui ayuda :((

        // TODO lógica de si p ha palpado la zona de la dropzone.....
        // quiero hacer que cuando se detecte la colision pase un metodo 

        // TODO cómo hacemos que la pieza se ha colocado en el lugar correcto??
        // hacemos que correspondan con el índice de los arrays?
        // es decir:
        // _pioces[0] se corresponde con _dropZones[0] y así sucesivamente
    }

    public void finish()
    {
        GameManager.Instance.SetLevel(GameManager.Instance.GetActLevel(), true);
        //Volvemos al menú de los mapas
        GameManager.Instance.LoadScene(0);
    }

    // ----- DRAG AND DROP PHASE -----
    private void initializeDragDropPhase() {
        // numero del nivel.
        int nLevel = GameManager.instance.GetActLevel();

        // inicialmente no hay piezas colocadas
        _placedPieces = 0;

        // *En gamemanager pone k el primer nivel es el 1.
        // ASIGNA NUMERO DE DRAGGABLES Y OTRAS MOVIDAS (TODO) SEGUN EL NIVEL.
        /* 1-BRUJA, 2-RELOJERO, 3-CASTILLO, 4-FRANKENSTEIN(FINAL) */
        _nPieces = _piecesPerLevel[nLevel];

        // establece numero de drag pieces y drop zones.
        _pieces = new GameObject[_nPieces];
        _dropZones = new GameObject[_nPieces];
    }

    // Si las piezas colocadas es igual al numero que habia de piezas inicialmente,
    // que coincide con el numero de huecos también, quiere decir que todas las piezas
    // han sido colocadas.
    private bool arePiecesPlaced() {
        return _placedPieces == _nPieces;
    }

    // cuando una pieza es posicionada en el lugar correcto
    public void placePiece(GameObject p, GameObject dz) {
        // pone la drag piece en la place zone...
        p.transform.position = dz.transform.position;
        
        // este caso nunca debería suceder, pero lo hago por tener programación defensiva.
        if(_placedPieces >= _nPieces) {
            _placedPieces = _nPieces;
        }
        else {
            _placedPieces++;
        }
    }
    // -------------------------------

    // ----- ACTION PHASE -----
    private void initializeActionPhase() {
        // TODO
    }
    // ------------------------

    // ----- RESOLUTION PHASE -----
    private void initializeResolutionPhase() {
        _objetivo = false;
    }
    // ----------------------------

    #endregion
}
