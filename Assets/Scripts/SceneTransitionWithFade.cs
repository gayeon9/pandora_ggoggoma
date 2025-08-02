using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class SceneTransitionWithFade : MonoBehaviour
{
    public AudioSource audioSource;      
    public float delayBeforeFade = 2f;   //소리 줄이기 전 대기 시간
    public float fadeDuration = 3f;      //페이드 아웃 시간 
    public string nextSceneName;         // 넘어갈 씬 이름

    void Start()
    {
        
        if (audioSource != null)
        {
            audioSource.Play();
        }

        
        StartCoroutine(FadeOutAndLoadScene());
    }

    IEnumerator FadeOutAndLoadScene()
    {
        
        yield return new WaitForSeconds(delayBeforeFade);

        float startVolume = audioSource.volume;

        
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();

        
        SceneManager.LoadScene(nextSceneName);
    }
}
