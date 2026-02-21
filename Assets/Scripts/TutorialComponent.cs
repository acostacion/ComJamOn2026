using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialComponent : MonoBehaviour
{
    [Tooltip("Carteles en orden")]
    public GameObject[] billboards;

    [Tooltip("Tiempo en segundos que cada cartel permanece visible (0 = avanzar manualmente)")]
    public float displayTime = 0f;

    private int currentIndex = -1;
    private float timer = 0f;

    void Start()
    {
        // Oculta todos los carteles al inicio
        foreach (GameObject b in billboards)
        {
            if (b != null) b.SetActive(false);
        }

        // Muestra el primer cartel
        NextStep();
    }

    void Update()
    {
        if (currentIndex >= 0 && currentIndex < billboards.Length && displayTime > 0)
        {
            timer += Time.deltaTime;
            if (timer >= displayTime)
            {
                NextStep();
            }
        }

        // Permitir avanzar manualmente con tecla
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            NextStep();
        }
    }

    public void NextStep()
    {
        // Oculta el cartel actual
        if (currentIndex >= 0 && currentIndex < billboards.Length)
        {
            if (billboards[currentIndex] != null) billboards[currentIndex].SetActive(false);
        }

        // Avanza al siguiente
        currentIndex++;

        if (currentIndex < billboards.Length)
        {
            if (billboards[currentIndex] != null) billboards[currentIndex].SetActive(true);
            timer = 0f; // reinicia timer si usamos tiempo
        }
        else
        {
            Debug.Log("Tutorial completado!");
        }
    }
}