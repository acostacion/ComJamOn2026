using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialComponent : MonoBehaviour
{
    [Tooltip("Carteles en orden")]
    [SerializeField] private TutorialController[] billboards;

    [Tooltip("Tiempo en segundos que cada cartel permanece visible (0 = avanzar manualmente)")]
    public float displayTime = 0f;

    private int currentIndex = -1;
    private float timer = 0f;

    private bool waiting = false;

    void Start()
    {
        // Oculta todos los carteles al inicio
        foreach (TutorialController b in billboards)
        {
            if (b != null)
            {
                b.InitializeHidden();
            }
        }

        // Muestra el primer cartel
        NextStep();
    }

    void Update()
    {
        if (waiting)
        {
            if (billboards[currentIndex].IsHidden())
            {
                waiting = false;
                ShowNext();
            }
            return;
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            NextStep();
        }
    }

    public void NextStep()
    {
        if (currentIndex >= 0 && currentIndex < billboards.Length)
        {
            billboards[currentIndex].Hide();
            waiting = true;
            return;
        }

        ShowNext();
    }

    void ShowNext()
    {
        currentIndex++;

        if (currentIndex < billboards.Length)
            billboards[currentIndex].Show();
        else
            Debug.Log("Tutorial completado!");
    }
}