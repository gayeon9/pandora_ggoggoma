using System;
using System.Collections.Generic;

/// 저장할 데이터 구조 정의 


[Serializable]
public class SaveData
{
    // 플레이어 위치
    public float playerX;
    public float playerY;

    // 진행 상황
    public int currentStage;
    public List<string> completedQuests = new List<string>();
}
