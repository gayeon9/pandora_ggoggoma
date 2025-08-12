using System.Collections.Generic;
using Mono.Cecil;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableObjsInitializer : MonoBehaviour
{
    public Button initButton;

    private void Start()
    {
        initButton.onClick.AddListener(clicked);

    }
    private void clicked()
    {
        var ScriptableObjs = Resources.LoadAll<Item>("Item");
        Debug.Log($"Loaded Items: {ScriptableObjs.Length}");
        foreach (var ScriptableObj in ScriptableObjs)
    {

            ScriptableObj.itemLevel = 1;
            if (ScriptableObj.itemType == ItemType.Mouse)
            {
                var spr = Resources.Load<Sprite>("UI/Áã_ÀÚ´Â Áã");
                if (spr) ScriptableObj.itemImage = spr;
            }

        }

    }


}
