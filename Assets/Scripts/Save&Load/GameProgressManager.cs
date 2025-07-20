using System.Collections.Generic;
using UnityEngine;


/// 게임의 진행 상태 (스테이지, 퀘스트 등)

public class GameProgressManager : MonoBehaviour
{
    public int currentStage = 1;
    public List<string> completedQuests = new List<string>();

    void Start()
    {
        // 시작 시 기본값 설정,진행상황에 맞게 변경가능
        completedQuests.Add("Tutorial");
    }
}
