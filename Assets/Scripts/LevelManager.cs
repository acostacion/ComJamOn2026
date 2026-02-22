using UnityEngine;

public class LevelManager : MonoBehaviour {
    public enum Phases { DRAGDROP, ACTION, RESOLUTION}

    //Controla todo lo que sucede en un nivel
    #region Referencias
    // ----- PHASES -----
    // Objetos que contienen dentro todos los elementos de cada fase (para activar y desactivar los hijos).
    public GameObject _dragDropPhase;
    public GameObject _actionPhase;
    public GameObject _resolutionPhase;
    // NOTA: son publicas por si se necesitan en otro script.
    // ------------------

    // [SerializeField] private GameObject _player; // jugador

    #endregion

    #region Propiedades
    public static LevelManager instance = null;

    // ----- DRAG AND DROP PHASE -----
    // TODO CAMBIAR ESTO SEG�N LAS NECESIDADES DE DISE�O.
    private static readonly int[] _piecesPerLevel = { 2, 2, 3 }; // piezas que hay por cada nivel ("array const")

    private int _nPieces; // numero de piezas draggables de este nivel (o de dropZones)
    private int _placedPieces; // piezas colocadas, incialmente cero.
    // -------------------------------

    // ----- ACTION PHASE -----
    // ------------------------

    // ----- RESOLUTION PHASE -----
    bool _objetivo;  //Booleano que marca si se ha cumplido o no el objetivo
    // ----------------------------

    #endregion

    #region Metodos

    //Usado en drag como singleton
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    
    void Start() {
        // numero del nivel. //TODO. NO HAY GM EN EL MENU DE PRUEBA, LO DEJO COMENTADO
        int nLevel = GameManager.Instance.GetActLevel();

        // inicialmente no hay piezas colocadas
        _placedPieces = 0;

        // *En gamemanager pone k el primer nivel es el 1.
        // ASIGNA NUMERO DE DRAGGABLES Y OTRAS MOVIDAS (TODO) SEGUN EL NIVEL.
        /* 1-BRUJA, 2-RELOJERO, 3-CASTILLO, 4-FRANKENSTEIN(FINAL) */
        _nPieces = _piecesPerLevel[nLevel - 1];

        startLevel();
    }

    private void Update()
    {
     
    }

    public void finish()
    {
        GameManager.Instance.SetLevel(GameManager.Instance.GetActLevel(), true);
        //Volvemos al men� de los mapas
        GameManager.Instance.LoadScene(0);
    }

    // ----- DRAG AND DROP PHASE -----

    #region Drag and Drop

    // ESTADO INICIAL:
    // 1. DRAG DROP ACTIVA
    // 2. ACTION DESACTIVADA
    // 3. RESOLUTION DESACTIVADA
    private void startLevel()
    {
        controlPhase(Phases.DRAGDROP, true);
        controlPhase(Phases.ACTION, false);
        controlPhase(Phases.RESOLUTION, false);
    }

    public void controlPhase(Phases p, bool activate) {
        switch (p) {
            case Phases.DRAGDROP:
                _dragDropPhase.SetActive(activate);
                break;
            case Phases.ACTION:
                _actionPhase.SetActive(activate);
                break;
            case Phases.RESOLUTION: 
               _resolutionPhase.SetActive(activate);
                break;
            default:break;
        }
    }

    // Si las piezas colocadas es igual al numero que habia de piezas inicialmente,
    // que coincide con el numero de huecos tambi�n, quiere decir que todas las piezas
    // han sido colocadas.
    private bool arePiecesPlaced() {
        return _placedPieces == _nPieces;
    }

    // aumenta numero de piezas colocadas.
    private void increaseWellPlacedPieces()
    {
        // este caso nunca deber�a suceder, pero lo hago por tener programaci�n defensiva.
        if (_placedPieces >= _nPieces) { _placedPieces = _nPieces; }
        else { _placedPieces++; }
    }

    // Este metodo se llamara cuando se haga OnCollisionEnter con las piezas Drag and Drop
    // cuando una pieza es posicionada en el lugar correcto
    public void placePiece(GameObject p, GameObject dz)
    {
        DropComponent drop = dz.GetComponent<DropComponent>();
        DragComponent drag = p.GetComponent<DragComponent>();

        if (drop == null || drag == null) {
            return;
        }

        // si es correcta y la zona est� libre
        if (drop.IsCorrectPiece(p) && !drop.IsOccupied)
        {
            // colocar
            drop.PlaceObject(p.transform);

            // bloquear pieza
            MeshCollider col = p.GetComponent<MeshCollider>();
            if (col != null) col.enabled = false;

            drag.enabled = false;

            // bloquear zona
            BoxCollider dzCol = dz.GetComponent<BoxCollider>();
            if (dzCol != null) dzCol.enabled = false;

            increaseWellPlacedPieces();

            if (arePiecesPlaced())
            {
                controlPhase(Phases.DRAGDROP, false);
                controlPhase(Phases.ACTION, true);
            }
        }
        else {
            drag.ReturnToOrigin();
        }
    }
    #endregion

    // -------------------------------

    // ----- ACTION PHASE -----
    #region Action
  

    #endregion

    // ------------------------

    #endregion
}
