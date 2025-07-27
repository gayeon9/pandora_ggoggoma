    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.Events;

    public enum ItemType
    {
        Mouse,
        Jar,
        Newspaper,
        PigBlood,
        Etc
    }

    [System.Serializable]
    public class Item 
    {
        public ItemType itemType;
        public string itemName;
        public int itemLevel;
        public Sprite itemImage;
        public Func<bool> onUse;      // 클릭 시 행동
        public bool consumable;       // 사용 후 제거 여부
    public bool Use()
    {
        return (onUse?.Invoke() ?? false) && consumable;
    }
}



