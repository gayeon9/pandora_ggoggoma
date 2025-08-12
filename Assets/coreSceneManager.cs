using UnityEngine.SceneManagement;
using UnityEngine;

public class coreSceneManager : MonoBehaviour
{
    public static coreSceneManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SwitchScene(string current, string next)
    {
        StartCoroutine(SwitchSceneRoutine(current, next));
    }

    private System.Collections.IEnumerator SwitchSceneRoutine(string current, string next)
    {
        var scene = SceneManager.GetSceneByName(current);
        if (scene.isLoaded)
            yield return SceneManager.UnloadSceneAsync(current);

        yield return SceneManager.LoadSceneAsync(next, LoadSceneMode.Additive);
    }
}