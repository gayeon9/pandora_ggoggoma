using System;
using UnityEngine;

namespace SuperEasyRPG
{
    public class DialogUI : MonoBehaviour
    {
        public SentenceDialogPanel sentencePanel;
        public AnswerDialogPanel answerPanel;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void ShowSentenceDialog(SentenceDialog dialog, Action onNext)
        {
            sentencePanel.ShowDialog(dialog, onNext);
        }

        public void ShowChoiceDialog(string[] choices, Action<string, int> onChoiceSelected)
        {
            answerPanel.ShowDialog(choices, onChoiceSelected);
        }
    }
}