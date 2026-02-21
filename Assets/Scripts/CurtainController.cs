using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
using UnityEngine.InputSystem;


public class CurtainController : MonoBehaviour
{
    public AlembicStreamPlayer alembicPlayer;

    public enum CurtainState
    {
        Closed,
        Opening,
        Open,
        Closing
    }

    public CurtainState state = CurtainState.Closed;

    public float speed = 1.0f;

    void Update()
    {
        if (alembicPlayer == null) return;

        float time = alembicPlayer.CurrentTime;
        float duration = alembicPlayer.Duration;

        switch (state)
        {
            case CurtainState.Opening:
                time += speed * Time.deltaTime;
                if (time >= duration)
                {
                    time = duration;
                    state = CurtainState.Open;
                }
                break;

            case CurtainState.Closing:
                time -= speed * Time.deltaTime;
                if (time <= 0f)
                {
                    time = 0f;
                    state = CurtainState.Closed;
                }
                break;
        }

        alembicPlayer.CurrentTime = time;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ToggleCurtain();
        }
    }



    public void OpenCurtain()
    {
        state = CurtainState.Opening;
    }

    public void CloseCurtain()
    {
        state = CurtainState.Closing;
    }

    public void ToggleCurtain()
    {
        if (state == CurtainState.Open)
            CloseCurtain();
        else if (state == CurtainState.Closed)
            OpenCurtain();
    }
}