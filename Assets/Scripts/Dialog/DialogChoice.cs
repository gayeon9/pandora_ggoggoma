using System.Collections.Generic;

[System.Serializable]
public class DialogChoice
{
    public string choiceText;                // 선택지 문구
    public System.Action onSelected;        // 선택 시 실행할 로직

    List<DialogChoice> choices = new List<DialogChoice>
{
    new DialogChoice {
        choiceText = "놓아준다",
        onSelected = () =>
        {
            DialogueManager.Instance.StartDialogue(new[] { "쥐: 고마워요. 힌트를 드릴게요!" }, DialogueMode.Explanation);
            // 아이템 상태 변화, 인벤토리 미추가 등 처리
        }
    },
    new DialogChoice {
        choiceText = "잡아둔다",
        onSelected = () =>
        {
            DialogueManager.Instance.StartDialogue(new[] { "주인공: 싫어. 쥐를 가둬둔다." }, DialogueMode.Dialogue);
            
                        
        }
    }




};






}
