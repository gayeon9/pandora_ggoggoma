using System.Collections.Generic;

[System.Serializable]
public class DialogChoice
{
    public string choiceText;                // ������ ����
    public System.Action onSelected;        // ���� �� ������ ����

    List<DialogChoice> choices = new List<DialogChoice>
{
    new DialogChoice {
        choiceText = "�����ش�",
        onSelected = () =>
        {
            DialogueManager.Instance.StartDialogue(new[] { "��: ������. ��Ʈ�� �帱�Կ�!" }, DialogueMode.Explanation);
            // ������ ���� ��ȭ, �κ��丮 ���߰� �� ó��
        }
    },
    new DialogChoice {
        choiceText = "��Ƶд�",
        onSelected = () =>
        {
            DialogueManager.Instance.StartDialogue(new[] { "���ΰ�: �Ⱦ�. �㸦 ���ֵд�." }, DialogueMode.Dialogue);
            
                        
        }
    }




};






}
