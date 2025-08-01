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
        Bucket,
        Etc
    }

    [System.Serializable]
    public class Item 
    {
        public ItemType itemType;
    
        public string itemName;
        public int itemLevel;
        public Sprite itemImage;
        public Sprite borderImage;
        public Func<bool> onUse;      // Ŭ�� �� �ൿ
        public bool consumable;
        public Vector3 itemScale = Vector3.one; 
    // ��� �� ���� ����
    public bool Use()
    {
        return (onUse?.Invoke() ?? false);// && consumable;
    }
}



