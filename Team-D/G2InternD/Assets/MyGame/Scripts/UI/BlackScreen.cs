using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class BlackScreen : MonoBehaviour
{
    private CanvasGroup CanvasGroup;

    void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        FadeIn(0.5f);
    }

    public void FadeIn(float animTime = 1.0f)
    {
        DOTween.To(
            () => CanvasGroup.alpha,
            alpha => CanvasGroup.alpha = alpha,
            0.0f,
            animTime
        );
    }

    public void FadeOut(float animTime = 1.0f)
    {
        DOTween.To(
            () => CanvasGroup.alpha,
            alpha => CanvasGroup.alpha = alpha,
            1.0f,
            animTime
        );
    }
}