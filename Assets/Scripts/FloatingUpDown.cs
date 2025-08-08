using UnityEngine;

public class FloatingUpDown : MonoBehaviour
{
    public float amplitude = 0.5f;  // 위아래 움직임 크기
    public float frequency = 1f;    // 움직임 속도

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;  // 시작 위치 저장
    }

    void Update()
    {
        // 시간에 따라 Y 위치를 사인 함수로 계산
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
