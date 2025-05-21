using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeEffect : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    public void ScreenFade(float targetAlpha, float duraation, System.Action onceComplete = null)
    {
        StartCoroutine(FadeCoroutine(targetAlpha, duraation, onceComplete));
    }
    private IEnumerator FadeCoroutine(float tragetAlpha, float duration, System.Action onComplete)
    {
        float time = 0;
        Color currentColor = fadeImage.color;

        float startAlpha = currentColor.a;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, tragetAlpha, time / duration); 
            fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;  
        }
        fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, tragetAlpha);

        onComplete?.Invoke();
    }
}
