using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
   [SerializeField] 
   Sprite sprite;
    public void ChangeScene(int scene)
   {
        GameManager.Instance.LoadScene(scene);
   }

    public void Completado()
    {
        GetComponent<Image>().sprite = sprite;
    }
}
