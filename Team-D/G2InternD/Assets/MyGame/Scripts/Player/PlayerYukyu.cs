using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerYukyu : MonoBehaviour {

    //お邪魔ゲージ度
    public float ObstacleGauge { get; private set; }
    //たまるスピード
    public float GaugeSpeed = 0.05f;
    //ゲージがたまっているか
    public bool CanAction = false;

    [SerializeField, Header("ゲージMax値")]
    float m_gaugeMax = 100;
    [SerializeField, Header("オーラ画像")]
    GameObject m_auraImage = null;
    
    //ゲージがアクティブか
    bool m_isActiveGauge = false;
    

    PlayerScore m_score;
    // Use this for initialization
    void Start () {
        m_score = gameObject.GetComponent<PlayerScore>();
        m_isActiveGauge = true;
	}

    float counter;
	// Update is called once per frame
	void Update () {
        if (m_isActiveGauge)
        {//一定時間ごとにゲージ１ずつたまっていく
            counter += Time.deltaTime;
            if (GaugeSpeed < counter)
            {
                AddGauge();
            }
        }
	}
    //有給アクションを実行する
    public void ActionYukyu()
    {
        SoundManager.Instance.PlaySE("手足・殴る、蹴る03");
        Debug.Log("アクション！");
        m_auraImage.SetActive(false);
        ResetGauge();
        //  スピードを再入力する(有利不利でたまる速度が変わる)
        GaugeSpeed = 0.05f;
        CanAction = false;
    }
    void AddGauge()
    {
        counter = 0;
        ObstacleGauge++;
        if (ObstacleGauge > m_gaugeMax){
            ObstacleGauge = m_gaugeMax;
            CanAction = true;
            m_auraImage.SetActive(true);
        }
        //Debug.Log(ObstacleGauge);
        
    }
    void ResetGauge()
    {
        ObstacleGauge = 0;
    }
}
