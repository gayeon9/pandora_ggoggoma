using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class GlobalLightFadeIn : MonoBehaviour
{
    public Light2D globalLight;   // Hierarchy에 있는 Global Light 2D 할당
    public float fadeDuration = 1f;  // 밝아지는 시간
    public float targetIntensity = 1f; // 최종 밝기값

    void Start()
    {
        if (globalLight != null)
        {
            globalLight.intensity = 0f; // 시작 시 어둡게
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

        globalLight.intensity = targetIntensity; // 보정
    }

    IEnumerator waitforOneSec()
    {
        yield return new WaitForSeconds(1f);

    }

}
