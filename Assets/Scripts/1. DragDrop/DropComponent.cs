using UnityEngine;

public class DropComponent : MonoBehaviour
{
    public bool IsOccupied { get; private set; }

    [Tooltip("Solution to the drop zone, to make sure its correct")]
    [SerializeField] private GameObject correctPiece;

    public void PlaceObject(Transform obj)
    {
        obj.position = transform.GetChild(0).position;
        obj.rotation = transform.GetChild(0).rotation;

        IsOccupied = true;
    }

    public bool IsCorrectPiece(GameObject piece)
    {
        return piece == correctPiece;
    }
}
