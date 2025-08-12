using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCscenes : MonoBehaviour
{
    public string sceneToLoad;

    private void OnMouseDown()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Core 매니저에 씬 전환 요청
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
