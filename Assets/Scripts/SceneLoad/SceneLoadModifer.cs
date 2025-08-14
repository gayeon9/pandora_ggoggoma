
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
         
      //  SceneManager.sceneLoaded += OnSceneLoadedinventory; //inventoryUI���� refreshUI�� �־���
        SceneManager.sceneLoaded += OnSceneLoadedCamrea;  //ī�޶� ���� �� �÷��̾� ��ġ (0,0,0) ����
        
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
        yield return null; // 1������ ���
        Inventory.instance.onChangeItem?.Invoke();
    }

    private IEnumerator SetCameraTargetNextFrame()
    {
        // �� ������ ��� �� �� ������Ʈ ��� �ε�� ������
        yield return null;        
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found in scene!");
            yield break;
        }
        //��ġ �߰� ����
        Vector3 pos = new Vector3(0, 0, 0);
        player.transform.position = pos;

        var vcams = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
        foreach (var vcam in vcams)
        {
            vcam.Follow = player.transform;
        }
    

    }


    }

