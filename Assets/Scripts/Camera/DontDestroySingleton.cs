using UnityEngine;

public class DontDestroySingleton : MonoBehaviour
{
    private static DontDestroySingleton instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // 이미 있으면 새로 생긴 것 파괴
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환해도 유지
    }
}
