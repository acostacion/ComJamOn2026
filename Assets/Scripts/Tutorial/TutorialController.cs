using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public enum BillboardState
    {
        Hidden,
        AppearingDown,
        AppearingBounceUp,
        AppearingBounceDown,
        Visible,
        DisappearingBounceUp,
        DisappearingUp
    }

    public BillboardState state = BillboardState.Hidden;

    [Header("Settings")]
    public float dropDistance = 1.5f;
    public float moveSpeed = 3f;
    public float bounceHeight = 0.15f;
    public float bounceSpeed = 6f;

    private Vector3 visiblePos;
    private Vector3 hiddenPos;
    private Vector3 bouncePos;

    void Update()
    {
        switch (state)
        {
            case BillboardState.AppearingDown:
                MoveTowards(visiblePos, BillboardState.AppearingBounceUp);
                break;

            case BillboardState.AppearingBounceUp:
                MoveTowards(bouncePos, BillboardState.AppearingBounceDown);
                break;

            case BillboardState.AppearingBounceDown:
                MoveTowards(visiblePos, BillboardState.Visible);
                break;

            case BillboardState.DisappearingBounceUp:
                MoveTowards(bouncePos, BillboardState.DisappearingUp);
                break;

            case BillboardState.DisappearingUp:
                MoveTowards(hiddenPos, BillboardState.Hidden);
                break;
        }
    }

    void MoveTowards(Vector3 target, BillboardState nextState)
    {
        float speed = (nextState == BillboardState.Visible || nextState == BillboardState.Hidden)
            ? moveSpeed
            : bounceSpeed;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            transform.position = target;
            state = nextState;
        }
    }

    public void InitializeHidden()
    {
        visiblePos = transform.position;
        hiddenPos = visiblePos + Vector3.up * dropDistance;
        bouncePos = visiblePos + Vector3.up * bounceHeight;

        transform.position = hiddenPos;
        state = BillboardState.Hidden;
    }

    public void Show()
    {
        state = BillboardState.AppearingDown;
    }

    public void Hide()
    {
        state = BillboardState.DisappearingBounceUp;
    }

    public bool IsHidden()
    {
        return state == BillboardState.Hidden;
    }

    public bool IsVisible()
    {
        return state == BillboardState.Visible;
    }
}