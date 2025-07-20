using System.Collections.Generic;
using System.IO;
using UnityEngine;

//세이브 로드 담당
public class SaveManager : MonoBehaviour
{
    public Transform player;
    public GameProgressManager progressManager;

    private string path;

    void Start()
    {
        path = Application.persistentDataPath + "/savefile.json";
        Debug.Log("저장 경로: " + path);
    }

    // 버튼에서 호출할 저장 함수
    public void Save()
    {
        SaveData data = new SaveData();

        // 1. 플레이어 위치 저장
        data.playerX = player.position.x;
        data.playerY = player.position.y;

        // 2. 진행 상황 저장
        data.currentStage = progressManager.currentStage;
        data.completedQuests = new List<string>(progressManager.completedQuests);

        // 3. JSON 저장
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log(" 저장 완료!");
    }

    // 버튼에서 호출할 불러오기 함수
    public void Load()
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("세이브 파일이 없습니다!");
            return;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // 1. 위치 복원
        player.position = new Vector3(data.playerX, data.playerY, 0);

        // 2. 진행상황 복원
        progressManager.currentStage = data.currentStage;
        progressManager.completedQuests = new List<string>(data.completedQuests);

        Debug.Log(" 불러오기 완료!");
    }
}
