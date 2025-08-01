using UnityEngine;

namespace SuperEasyRPG
{
    public class SentenceDialog
    {
        public string characterName;
        public Sprite characterImage;
        public string sentence;

        public SentenceDialog(string characterName, Sprite characterImage, string sentence)
        {
            this.characterName = characterName;
            this.characterImage = characterImage;
            this.sentence = sentence;
        }
    }
}