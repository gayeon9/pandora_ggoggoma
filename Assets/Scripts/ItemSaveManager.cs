using System.Collections.Generic;
using UnityEngine;

public class ItemSaveManager : MonoBehaviour
{
    public static ItemSaveManager instance;

    [System.Serializable]
    public class ItemData
    {
        public string id;
        public int level;
    }

    private Dictionary<string, ItemData> itemStates = new Dictionary<string, ItemData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환에도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveItem(string id, int level)
    {
        if (itemStates.ContainsKey(id))
        {
            itemStates[id].level = level;
        }
        else
        {
            itemStates[id] = new ItemData { id = id, level = level };
        }
    }

    public int GetItemLevel(string id, int defaultLevel = 1)
    {
        if (itemStates.TryGetValue(id, out ItemData data))
        {
            return data.level;
        }
        return defaultLevel;
    }
}
