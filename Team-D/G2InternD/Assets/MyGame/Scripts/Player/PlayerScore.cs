using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {
    //プレイヤー所持P
    public int CurrentPoint { get; private set; }
    

    [SerializeField,Header("ポイント所持上限")]
    int MaxPoint = 10;
    [SerializeField, Header("所持PText")]
    Text m_haveText = null;

    PlayerController m_controller;

    public void AddPoint(int add)
    {
        // m_controller = gameObject.GetComponent<PlayerController>();
        SoundManager.Instance.PlaySE("coin04");
        CurrentPoint += add;
        m_controller.DisSpeed(0.3f);
        if (MaxPoint < CurrentPoint)
            //元に戻す
            CurrentPoint = MaxPoint;
        m_haveText.text = CurrentPoint.ToString();
    }
    public void DamagePoint(int minus)
    {
        CurrentPoint -= minus;
        m_controller.AddSpeed(0.3f);
        if (CurrentPoint < 0)
            CurrentPoint = 0;
        m_haveText.text = CurrentPoint.ToString();
    }
    //称えるとき所持P減らしてスコアを増やす
    public void LaudPoint(int add)
    {
        CurrentPoint -= add;
        m_controller.AddSpeed(0.3f);
        //スコア増やす
        ScoreManager.AddMasterScore(10, m_controller.m_playerMode);
        m_haveText.text = CurrentPoint.ToString();
    }
	// Use this for initialization
	void Start ()
    {
        m_controller = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
