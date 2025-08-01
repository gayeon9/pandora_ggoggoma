using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SuperEasyRPG
{
    public class AnswerDialogPanel : MonoBehaviour
    {
        public Button answerButtonPrefab;

        private List<Button> buttons = new List<Button>();

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowDialog(string[] choices, System.Action<string, int> onChoiceSelected)
        {
            DeleteAllExistingButtons();

            for (int i = 0; i < choices.Length; i++)
            {
                string choice = choices[i];
                Button answerButton = Instantiate(answerButtonPrefab, gameObject.transform);
                answerButton.GetComponentInChildren<TextMeshProUGUI>().text = choice;

                int index = i;  // Since i is captured by reference
                answerButton.onClick.AddListener(() =>
                {
                    onChoiceSelected(choice, index);
                    gameObject.SetActive(false);
                });
                answerButton.gameObject.SetActive(true);
                buttons.Add(answerButton);
            }

            gameObject.SetActive(true);
        }


        private void DeleteAllExistingButtons()
        {
            if (buttons.Count > 0)
            {
                foreach (var button in buttons)
                {
                    Destroy(button.gameObject);
                }
                buttons.Clear();
            }
        }
    }
}