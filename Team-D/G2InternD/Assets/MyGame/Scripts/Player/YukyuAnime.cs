using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YukyuAnime : MonoBehaviour {
    //有給UIの移動
    RectTransform m_rect;
    CanvasGroup m_canvas;

	// Use this for initialization
	void Start () {
        m_rect = gameObject.GetComponent<RectTransform>();
        m_canvas = gameObject.GetComponent<CanvasGroup>();
        m_canvas.alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    BeginYukyu();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    EndYukyu();
        //}
	}
    public void BeginYukyu()
    {
        m_canvas.DOFade(1, 0.2f);
        m_rect.DOScale(1,0.2f);
        StartCoroutine(YukyuNow());
    }
    IEnumerator YukyuNow()
    {
        yield return new WaitForSeconds(2);
        EndYukyu();
    }
    public void EndYukyu()
    {
        m_canvas.DOFade(0, 0.2f).OnComplete(()=>
            m_rect.localScale = Vector3.zero);
       // m_rect.DOScale(0, 0.2f);

    }
}
