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
    private CanvasGroup canvasGroup;
    public GridTile tile; 

    private Color c;

    private void Start()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();
        tile = GetComponent<GridTile>();
        if (canvasGroup == null)
        {
            Debug.Log("canvasGroup is null");
        }
    }

    public void BackgroundFade()
    {
        background.DOFade(0, FadeDuration).OnComplete(() =>
        {
            c = background.color;
            c.a = 0f;
            background.color = c;
        });    
    }
    
    public void CanvasFade()
    {
        canvasGroup.DOFade(0, FadeDuration);
    }
    
}
