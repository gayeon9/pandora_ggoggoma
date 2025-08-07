using System.Collections.Generic;
using UnityEngine;

public class ItemLevelManager : MonoBehaviour
{
    public static ItemLevelManager Instance;

    private Dictionary<string, int> itemLevels = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시 삭제 방지
        }
        else
        {
            Destroy(gameObject);  // 중복 생성 방지
        }
    }

    public int GetLevel(string itemName)
    {
        if (itemLevels.TryGetValue(itemName, out int level))
            return level;
        else
        {
            itemLevels[itemName] = 1; // 기본 레벨 1
            return 1;
        }
    }

    public void SetLevel(string itemName, int level)
    {
        itemLevels[itemName] = level;
    }

    public void IncreaseLevel(string itemName)
    {
        if (itemLevels.TryGetValue(itemName, out int level))
            itemLevels[itemName] = level + 1;
        else
            itemLevels[itemName] = 2;
    }
}
