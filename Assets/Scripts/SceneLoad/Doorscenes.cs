using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class DoorCscenes : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject door;

     void Start()
    {
        door.SetActive(true);
    }

    private void OnMouseDown()
    {
        // 씬 이름이 설정되어 있을 때만 씬 전환
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            door.SetActive(false);
            StartCoroutine(WaitThreeSeconds());
            SceneManager.LoadScene(sceneToLoad);
        }

    }

    private IEnumerator WaitThreeSeconds()
    {
        Debug.Log("3초 대기 시작");

        yield return new WaitForSeconds(3f); // 3초 동안 대기

        Debug.Log("3초 경과");
    }

}
