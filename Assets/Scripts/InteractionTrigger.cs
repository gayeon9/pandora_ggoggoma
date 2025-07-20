using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    // 깜빡이는 사각형 오브젝트
    public GameObject survey;

    // 플레이어가 스페이스바 누르면 뜨는 이미지 패널
    public GameObject imagePanel;

    // 플레이어가 트리거 영역 근처에 있는지 체크하는 변수
    private bool isPlayerNear = false;

    void Start()
    {
        survey.SetActive(false);
        imagePanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear)
        {
            if (Time.time % 1f < 0.5f)
                survey.SetActive(true);
            else
                survey.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                bool isActive = imagePanel.activeSelf;
                imagePanel.SetActive(!isActive);
            }
        }
        else
        {
            survey.SetActive(false);
            imagePanel.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            imagePanel.SetActive(false);
        }
    }
}
