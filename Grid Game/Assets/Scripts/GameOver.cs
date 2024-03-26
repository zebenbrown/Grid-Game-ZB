using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private float FadeDuration;
    [SerializeField] private CanvasGroup canvasGroup;
    private void Awake()
    {
        GetComponent<CanvasGroup>();
        FadeIn();
    }

    void FadeIn()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, 1.2f);
    }

    public void FadeOut()
    {
        canvasGroup.DOFade(0f, FadeDuration);
    }
    
}
