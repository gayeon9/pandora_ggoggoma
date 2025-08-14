
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
using System.Collections;
using SuperEasyRPG;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SceneLoadModifer : MonoBehaviour
{
    

    void OnEnable()
    {
         
      //  SceneManager.sceneLoaded += OnSceneLoadedinventory; //inventoryUI에서 refreshUI를 넣어줌
        SceneManager.sceneLoaded += OnSceneLoadedCamrea;  //카메라 추적 및 플레이어 위치 (0,0,0) 조정
        
    }

    void OnDisable()
    {
     //   SceneManager.sceneLoaded -= OnSceneLoadedinventory;
        SceneManager.sceneLoaded -= OnSceneLoadedCamrea;

    }



    void OnSceneLoadedinventory(Scene scene, LoadSceneMode mode)
    {
        if (Inventory.instance != null)
        {
            StartCoroutine(InvokeChangeItemNextFrame());
        }
    }
    private void OnSceneLoadedCamrea(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(SetCameraTargetNextFrame());
    }



    private IEnumerator InvokeChangeItemNextFrame()
    {
        yield return null; // 1프레임 대기
        Inventory.instance.onChangeItem?.Invoke();
    }

    private IEnumerator SetCameraTargetNextFrame()
    {
        // 한 프레임 대기 → 씬 오브젝트 모두 로드될 때까지
        yield return null;        
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found in scene!");
            yield break;
        }
        //위치 추가 조정
        Vector3 pos = new Vector3(0, 0, 0);
        player.transform.position = pos;

        var vcams = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
        foreach (var vcam in vcams)
        {
            vcam.Follow = player.transform;
        }
    

    }


    }

