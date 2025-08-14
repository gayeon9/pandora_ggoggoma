using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemOnUseManager 
{
    public static bool Execute(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.pail:
                item.onUse = () => ExecutePail(item);
                Debug.Log("Glass 아이템 델리게이트 변수에 함수 넣기 성공");
                //
                return true;

            case ItemType.Knifeb:
                item.onUse = () => ExecuteKnifeb(item);
                Debug.Log("Knifeb 아이템 델리게이트 변수에 함수 넣기 성공");
                //
                return false;


            case ItemType.Mouse:
                item.onUse = () => ExecuteMouse(item);
                Debug.Log("mouse 아이템 델리게이트 변수에 함수 넣기 성공, 아이템 레벨 : "+ item.itemLevel);
                return false;

            case ItemType.Jar:
                item.onUse = () => ExecuteJar(item);
                Debug.Log("jar 아이템 델리게이트 변수에 함수 넣기 성공");

                return true;

            case ItemType.Newspaper:
                item.onUse = () => ExecuteNewspaper(item);
                Debug.Log("newspaper 아이템 델리게이트 변수에 함수 넣기 성공");
                //?? ??
                return false;

            case ItemType.Clothesold:
                item.onUse = () => ExecuteClothesold(item);
                Debug.Log("Clocthesold 아이템 델리게이트 변수에 함수 넣기 성공");
                //
                return true;


            case ItemType.Glass:
                item.onUse = () => ExecuteGlass(item);
                Debug.Log("Glass 아이템 델리게이트 변수에 함수 넣기 성공");
                //
                return true;

         


            default:
                return true;
        }
    }


    private static bool ExecutePail(Item item)
    {
        GameObject Scriptmachine = GameObject.Find("Scriptmachine");

        switch (item.itemLevel)
        {
            case 1:
                Debug.Log("Knifeb 아이템 델리게이트 변수있는 함수 호출 성공");
                CustomEvent.Trigger(Scriptmachine, "PailEvent1");
                return true;



            case 2:
                Debug.Log("Knifeb 아이템 델리게이트 변수있는 함수 호출 성공");
                CustomEvent.Trigger(Scriptmachine, "PailEvent2");
                return true;

            default: return false;
        }
    }


    private static bool ExecuteKnifeb(Item item)
    {
        GameObject Scriptmachine = GameObject.Find("Scriptmachine");

        switch (item.itemLevel)
        {
            case 1:
                Debug.Log("Knifeb 아이템 델리게이트 변수있는 함수 호출 성공");
                CustomEvent.Trigger(Scriptmachine, "KnifebEvent1");
                return true;



            default:
                Debug.Log("Knifeb 아이템 델리게이트 변수있는 함수 호출 성공");

                return true;
        }
    }




    private static bool ExecuteMouse(Item item)
    {
        GameObject Scriptmachine = GameObject.Find("Scriptmachine");



        switch (item.itemLevel)
        {
            case 1:

                Debug.Log("mouse 아이템 델리게이트 변수있는 함수 호출 성공");
                CustomEvent.Trigger(Scriptmachine, "MouseEvent1");
                 item.itemLevel++;
                return false;
            
               case 2:
                CustomEvent.Trigger(Scriptmachine, "MouseEvent2");
                return false; 

               case 3:
                CustomEvent.Trigger(Scriptmachine, "MouseEvent3");
                return false;

               case 4:
                CustomEvent.Trigger(Scriptmachine, "MouseEvent4");
                return false;

            default:
                return false;
        }

    }


   

    private static bool ExecuteJar(Item item)
    {
        switch (item.itemLevel)
        {
            case 0:
                Debug.Log("mouse 아이템 델리게이트 변수있는 함수 호출 성공");

                return true;



            default:
                return true;
        }
    }

    private static bool ExecuteGlass(Item item)
    {
        GameObject Scriptmachine = GameObject.Find("Scriptmachine");

        switch (item.itemLevel)
        {
            case 1:
                CustomEvent.Trigger(Scriptmachine, "GlassEvent1"); return true;

            case 2:
                CustomEvent.Trigger(Scriptmachine, "GlassEvent2"); return true;
            default:
                return true;
        }
    }


    private static bool ExecuteNewspaper(Item item)
    {
        switch (item.itemLevel)
        {
            case 0:
                DialogueManager.Instance.StartDialogue(
                    new[] { "신문이다." },
                    DialogueMode.Dialogue
                    
                );
                  item.isInventory = true;  //??

                return true;



            default:
                return false;
        }
    }
    
    private static bool ExecuteClothesold(Item item)
    {
        switch (item.itemLevel)
        {
            case 0:
                Debug.Log("옷 클릭 성공");
                return true;

            

            default:
                return false;
        }
    }

    


}