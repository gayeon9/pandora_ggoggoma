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
        Knife, //단검(피 x)
        Knifeb, //단검(피o)
        Notebook,//거핀의 수첩
        Clothesb, //피가 묻은 하인복
        Clothesold, //낡은 하인복
        Clothest, //훔친 하인복
        Glass, //유리병
        well, //우물

        Mop, //대걸레







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



