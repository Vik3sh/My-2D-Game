using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InGame : MonoBehaviour
{
    public static UI_InGame instance;
    public UI_FadeEffect fadeEffect;

    public void Awake()
    {
        instance = this;
        fadeEffect = GetComponentInChildren<UI_FadeEffect>();
    }

    private void Start()
    {
        fadeEffect.ScreenFade(0, 1);
    }
}
