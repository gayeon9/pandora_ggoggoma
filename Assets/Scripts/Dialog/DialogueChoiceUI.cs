using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueChoiceUI : MonoBehaviour
{
    public GameObject buttonPrefab;         // 선택지 버튼 프리팹
    public Transform buttonContainer;       // 버튼들이 생성될 위치

    private List<GameObject> spawnedButtons = new List<GameObject>();

 
    public void ShowChoices(List<DialogChoice> choices)
    {
        ClearChoices();

        foreach (var choice in choices)
        {
            GameObject buttonGO = Instantiate(buttonPrefab, buttonContainer);
            Button button = buttonGO.GetComponent<Button>();
            Text text = buttonGO.GetComponentInChildren<Text>();
            text.text = choice.choiceText;

            button.onClick.AddListener(() =>
            {
                choice.onSelected?.Invoke();
                ClearChoices(); // 선택 후 제거
            });

            spawnedButtons.Add(buttonGO);
        }
    }

    public void ClearChoices()
    {
        foreach (var btn in spawnedButtons)
        {
            Destroy(btn);
        }
        spawnedButtons.Clear();
    }
}


