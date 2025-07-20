using UnityEngine;
using UnityEngine.UI;

public class MarkAnimationHandler : MonoBehaviour
{


    public void OnMarkAnimationEnd()
    {

        gameObject.SetActive(false);

    }
}
