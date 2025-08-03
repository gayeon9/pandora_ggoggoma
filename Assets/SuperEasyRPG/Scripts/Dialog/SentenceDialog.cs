using Unity.VisualScripting;
using UnityEngine;

namespace SuperEasyRPG
{
    public class SentenceDialog
    {
        public string characterName;
        public Sprite characterImage;
        public string sentence;
        public bool autoHideDialog;
        public SentenceDialog(string characterName, Sprite characterImage, string sentence, bool autoHideDialog)
        {
            this.characterName = characterName;
            this.characterImage = characterImage;
            this.sentence = sentence;
            this.autoHideDialog = autoHideDialog;
        }
    }
}