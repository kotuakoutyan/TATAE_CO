using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlackBG : MonoBehaviour 
{
    [SerializeField]
    CanvasGroup m_canvas = null;
    // Use this for initialization
    void Start()
    {

    }
    public void Begin()
    {
        m_canvas.DOFade(1, 0.3f);
    }
    public void End()
    {
        m_canvas.DOFade(1, 0.3f);
    }

}


