using UnityEngine;

public class Cluenote : MonoBehaviour
{
    public GameObject noteImage;

    public void OnButtonClick()
    {
        if (noteImage != null)
        {
            noteImage.SetActive(!noteImage.activeSelf);
        }
    }
}

