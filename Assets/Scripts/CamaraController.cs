using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public enum CameraState
    {
        PositionA,
        MovingToB,
        PositionB,
        MovingToA
    }

    public CameraState state = CameraState.PositionA;

    public float moveDistance = 5f;
    public float moveSpeed = 2f;
    public float rotateSpeed = 180f;

    private Vector3 startPos;
    private Vector3 targetPos;

    private Quaternion startRot;
    private Quaternion targetRot;

    private bool isRotating = false;

    void Update()
    {
        switch (state)
        {
            case CameraState.MovingToB:
                MoveForward(CameraState.PositionB);
                break;

            case CameraState.MovingToA:
                MoveForward(CameraState.PositionA);
                break;
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ToggleCamera();
        }

        if (isRotating)
        {
            RotateStep();
        }
    }

    void MoveForward(CameraState targetState)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPos) < 0.01f && !isRotating)
        {
            startRot = transform.rotation;
            targetRot = startRot * Quaternion.Euler(0, 180f, 0);
            isRotating = true;
            state = targetState; 
        }
    }

    void RotateStep()
    {
        // Giramos con RotateTowards para no acumular error
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotateSpeed * Time.deltaTime
        );

        // Si ya llegamos al target
        if (Quaternion.Angle(transform.rotation, targetRot) < 0.1f)
        {
            transform.rotation = targetRot; 
            isRotating = false;
        }
    }

    public void ToggleCamera()
    {
        if (state == CameraState.PositionA)
        {
            startPos = transform.position;
            targetPos = startPos + transform.forward * moveDistance;
            state = CameraState.MovingToB;
        }
        else if (state == CameraState.PositionB)
        {
            startPos = transform.position;
            targetPos = startPos + transform.forward * moveDistance;
            state = CameraState.MovingToA;
        }
    }
}