
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
using System.Collections;
using SuperEasyRPG;

public class SceneLoadModifer : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoadedinventory;
        SceneManager.sceneLoaded += OnSceneLoadedCamrea;

    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoadedinventory;
        SceneManager.sceneLoaded -= OnSceneLoadedCamrea;

    }

    void OnSceneLoadedinventory(Scene scene, LoadSceneMode mode)
    {
        if (Inventory.instance != null)
        {
            Inventory.instance.onChangeItem?.Invoke();
        }
    }


    private void OnSceneLoadedCamrea(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(SetCameraTargetNextFrame());
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

        var vcams = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
        foreach (var vcam in vcams)
        {
            vcam.Follow = player.transform;
        }
    

    }


    }

