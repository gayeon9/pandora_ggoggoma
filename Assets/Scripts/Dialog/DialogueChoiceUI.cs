using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueChoiceUI : MonoBehaviour
{
    public GameObject buttonPrefab;         // ������ ��ư ������
    public Transform buttonContainer;       // ��ư���� ������ ��ġ

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
                ClearChoices(); // ���� �� ����
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


