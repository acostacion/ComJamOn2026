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
    private const int LEVELS = 4; // niveles existentes
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
    }

    // Si las piezas colocadas es igual al numero que habia de piezas inicialmente,
    // que coincide con el numero de huecos también, quiere decir que todas las piezas
    // han sido colocadas.
    private bool arePiecesPlaced() {
        return _placedPieces == _nPieces;
    }

    // comprueba si p y dz son "pareja" (deben conectarse).
    private bool isWellConnected(GameObject p, GameObject dz) {
        bool pieceIndexFound = false;
        int i = 0;
        // busca el indice de la pieza.
        while(i <  _pieces.Length && pieceIndexFound == false) {
            if (_pieces[i] == p) {
                pieceIndexFound = true;
            }
            else {
                i++;
            }
        }

        // si coinciden en indices, es que las piezas son "pareja"
        return _dropZones[i] == dz;
    }

    // aumenta numero de piezas colocadas.
    private void increaseWellPlacedPieces() {
        // este caso nunca debería suceder, pero lo hago por tener programación defensiva.
        if (_placedPieces >= _nPieces) { _placedPieces = _nPieces; }
        else { _placedPieces++; }
    }

    // DENISA Este metodo se llamara cuando se haga OnCollisionEnter con las piezas Drag and Drop
    // cuando una pieza es posicionada en el lugar correcto
    public void placePiece(GameObject p, GameObject dz) {
        // si la conexión es CORRECTA.....
        if(isWellConnected(p, dz)) {
            // deshabilita las colisiones y los componentes de ambos para que no siga contando ni se pueda arrastrar con el raton.
            p.GetComponent<MeshCollider>().enabled = false;
            p.GetComponent<DragComponent>().enabled = false;
            dz.GetComponent<BoxCollider>().enabled = false;

            // pone la drag piece en la place zone un poco por delante...
            p.transform.position = dz.transform.position;
            p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, p.transform.position.z + 1);

            increaseWellPlacedPieces();
        }
        else {
            // TODO devuelve a la pieza a su posición original.
            // Se me ha ocurrido que se puede pasar la posicion original como parámetro y que haga un lerp hasta allí
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
