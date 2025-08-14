using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class GlobalLightFadeIn : MonoBehaviour
{
    public Light2D globalLight;   // Hierarchy�� �ִ� Global Light 2D �Ҵ�
    public float fadeDuration = 1f;  // ������� �ð�
    public float targetIntensity = 1f; // ���� ��Ⱚ

    void Start()
    {
        if (globalLight != null)
        {
            globalLight.intensity = 0f; // ���� �� ��Ӱ�
            StartCoroutine(FadeInLight());
        }
    }

    IEnumerator FadeInLight()
    {
        float startIntensity = globalLight.intensity;
        float timeElapsed = 0f;
        waitforOneSec();
        while (timeElapsed < fadeDuration)
        {
            globalLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        globalLight.intensity = targetIntensity; // ����
    }

    IEnumerator waitforOneSec()
    {
        yield return new WaitForSeconds(1f);

    }

}
