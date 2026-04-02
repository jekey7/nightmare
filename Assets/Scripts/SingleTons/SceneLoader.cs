using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, IScene
{
    // ¾À ·Îµå
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
