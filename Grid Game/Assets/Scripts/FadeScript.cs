using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreFade;
    [SerializeField] private TextMeshProUGUI highScoreFade;
    [SerializeField] private TextMeshProUGUI coordinatesFade;
    [SerializeField] private TextMeshProUGUI patternPlayingFade;
    [SerializeField] private Button startButtonFade;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float FadeDuration;

    private Image buttonImage;
    public CanvasGroup canvasGroup;
    public GridTile tile; 

    private Color c;


    private void Awake()
    {
        canvasGroup.alpha = 0f;
        if (canvasGroup == null)
        {
            Debug.Log("canvasGroup is null");
        }
    }
    private void Start()
    {
        tile = GetComponent<GridTile>();
    }

    public void CanvasFadeIn()
    {
        canvasGroup.DOFade(1, FadeDuration);
    }
    
    public void CanvasFade()
    {
        canvasGroup.DOFade(0, FadeDuration);
    }
    
}
