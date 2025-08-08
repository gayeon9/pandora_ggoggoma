
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadModifer : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Inventory.instance != null)
        {
            Inventory.instance.onChangeItem?.Invoke();
        }
    }
}

