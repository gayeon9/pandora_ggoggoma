using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KitchenDoor : MonoBehaviour
{
    public string KitchensceneToLoad;
    public GameObject door;

    private void Start()
    {
        door.SetActive(true);
    }

    private void OnMouseDown()
    {
        door.SetActive(false);
        StartCoroutine(WaitThreeSeconds());
        SceneManager.LoadScene(KitchensceneToLoad);


    }

    private IEnumerator WaitThreeSeconds()
    {
        yield return new WaitForSeconds(1f);

    }




}
