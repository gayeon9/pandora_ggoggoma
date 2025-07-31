using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCscenes : MonoBehaviour
{
    public string sceneToLoad;

    private void OnMouseDown()
    {
        // 씬 이름이 설정되어 있을 때만 씬 전환
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
