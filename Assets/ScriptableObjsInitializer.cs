using UnityEngine;

[DefaultExecutionOrder(-2000)]
public class ScriptableObjsInitializer : MonoBehaviour
{
    private static bool initialized = false;

    private void Awake()
    {
        if (initialized)
        {
            Destroy(gameObject);
            return;
        }

        initialized = true;
        RunInit();
        Destroy(gameObject); // 초기화 후 자기 자신 제거
    }

    private void RunInit()
    {
        var scriptableObjs = Resources.LoadAll<Item>("Item");
        Debug.Log($"[ItemInitializer] Loaded Items: {scriptableObjs.Length}");

        foreach (var so in scriptableObjs)
        {
            so.itemLevel = 1;
            so.isInventory = false;
            if (so.itemType == ItemType.Mouse)
            {
                var spr = Resources.Load<Sprite>("UI/쥐_자는 쥐");
                Debug.Log(spr != null ? "Mouse 이미지 설정 완료" : "Mouse 이미지 로드 실패");
                if (spr != null) so.itemImage = spr;
            }
        }
    }
}
