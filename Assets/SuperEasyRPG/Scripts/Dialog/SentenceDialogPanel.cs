using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SuperEasyRPG
{
    public class SentenceDialogPanel : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI characterNameText;
        [SerializeField] private GameObject characterNameTextWrapper;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private Image characterImage;

        [Header("Settings")]
        public float wordDelayInSeconds = 0.1f;

        SentenceDialog dialog;
        IEnumerator writerCoroutine;
        Action onNext;
        AudioSource talkSound;

        // Start is called before the first frame update
        void Start()
        {
        }

        void Awake()
        {
            talkSound = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckKeyInput();
        }

        public void ShowDialog(SentenceDialog dialog, Action onNext)
        {
            if (writerCoroutine != null)
            {
                StopCoroutine(writerCoroutine);
            }

            this.onNext = onNext;
            this.dialog = dialog;

            if (dialog.characterName == "" || dialog.characterName == null)
            {
                characterNameTextWrapper.SetActive(false);
            } else
            {
                characterNameTextWrapper.SetActive(true);
                characterNameText.text = dialog.characterName;
            }
            characterImage.sprite = dialog.characterImage;
            if (dialog.characterImage == null) {
                characterImage.color = Color.clear;
            } else
            {
                characterImage.color = Color.white;
            }
            gameObject.SetActive(true);

            writerCoroutine = WriteDialogTextOneByOne(dialog.sentence);
            StartCoroutine(writerCoroutine);
        }

        private IEnumerator WriteDialogTextOneByOne(string text)
        {
            dialogText.text = "";
            talkSound.enabled = true;

            foreach (char textChar in text)
            {
                dialogText.text += textChar;
                yield return new WaitForSeconds(wordDelayInSeconds);
            }
            writerCoroutine = null;
            talkSound.enabled = false;
        }

        private void WriteInstantly()
        {
            if (writerCoroutine != null)
            {
                StopCoroutine(writerCoroutine);
                writerCoroutine = null;
                talkSound.enabled = false;
            }

            dialogText.text = dialog.sentence;
        }

        public void HideDialog()
        {
            gameObject.SetActive(false);
        }

        private void CheckKeyInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (writerCoroutine == null)
                {
                    HideDialog();
                    onNext();
                } else
                {
                    WriteInstantly();
                }
            }
        }
    }
}