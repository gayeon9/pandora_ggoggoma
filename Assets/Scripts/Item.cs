using System;
using UnityEngine;

public enum ItemType
{
    Mouse,
    Jar,
    Newspaper,
    PigBlood,
    Knife, // 단검(피 x)
    Knifeb, // 단검(피 o)
    Notebook, // 거핀의 수첩
    Clothesb, // 피가 묻은 하인복
    Clothesold, // 낡은 하인복
    Clothest, // 훔친 하인복
    Glass, // 유리병
    well, // 우물
    Mop, // 대걸레
    wine, // 와인
    pail, //양동이
    Etc
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public int itemLevel;
    public Sprite itemImage;
    public bool consumable;  // 사용 후 제거 여부

    // 클릭 시 실행할 행동 (직접 Inspector에서 연결은 불가, 코드에서 할당)
    public Func<bool> onUse;

    public bool Use()
    {
        return (onUse?.Invoke() ?? false) && consumable;
    }

    public void IncreaseLevel()
    {
        itemLevel++;
        Debug.Log($"{itemName} 레벨이 {itemLevel}로 증가했습니다!");
    }
}
