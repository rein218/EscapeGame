using UnityEngine;

public class ScanaManagerStatic : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameSceneManager.Instance.LoadScene(sceneName);
    }
}
