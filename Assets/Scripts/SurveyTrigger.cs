using UnityEngine;

public class SurveyTrigger : MonoBehaviour
{
    // 깜빡이는 사각형 오브젝트
    public GameObject survey;

    // 플레이어가 트리거 영역 근처에 있는지 체크하는 변수
    private bool isPlayerNear = false;

    void Start()
    {
        survey.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear)
        {
            // 0.5초 간격으로 깜빡이기
            if (Time.time % 1f < 0.5f)
                survey.SetActive(true);
            else
                survey.SetActive(false);
        }
        else
        {
            survey.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
