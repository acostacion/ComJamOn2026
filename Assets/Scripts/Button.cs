using UnityEngine;

public class Button : MonoBehaviour
{
   public void ChangeScene(int scene)
   {
        GameManager.Instance.LoadScene(scene);
   }
}
