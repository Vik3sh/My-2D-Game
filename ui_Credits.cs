using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_Credits : MonoBehaviour
{
    private UI_FadeEffect fadeEffect;
    [SerializeField] private RectTransform react;
    [SerializeField] private float scrollSpeed = 200;
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private float offScreenPosition = 1800f;
    private bool creditSkipped;

    private void Update()
    {
        react.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        if(react.anchoredPosition.y >= offScreenPosition)
        {
           GoTOMainMenu();
        }
    }

    private void Awake()
    {
        fadeEffect = GetComponentInChildren<UI_FadeEffect>();
        fadeEffect.ScreenFade(0, 2);  
    }
    public void skipCredit()
    {
        if (creditSkipped == false)
        {
            scrollSpeed *= 10;
            creditSkipped = true;
        }
        else
        {
            GoTOMainMenu();
        }
    }


    private void GoTOMainMenu()
    {
        fadeEffect.ScreenFade(1, 1, SwitchToMainMenuScene);  
    }
    private void SwitchToMainMenuScene()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
