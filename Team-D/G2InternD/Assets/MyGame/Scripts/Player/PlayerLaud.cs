using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤー称えてるときアクション
public class PlayerLaud : MonoBehaviour {

    //称え中か
    public bool IsLaud { get; set; }

   [SerializeField]
    Fukidashi.FukidashiGenerator m_fukidashi = null;
    [SerializeField, Header("減らしていくスピード")]
    float m_disSpeed = 0.5f;//１Pごとにどのくらい減らしていくか

    PlayerController m_controller;
    PlayerScore m_score;
    PlayerImageAnimation Anim;

    //パーティクルフラグ
    public bool ParticleFlg, ParticleFlg2;

	// Use this for initialization
	void Start () {

        IsLaud = false;
        m_controller=gameObject.GetComponent<PlayerController>();
        m_score = gameObject.GetComponent<PlayerScore>();
        Anim = GetComponent<PlayerImageAnimation>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Kui")
        {
            //プレイヤー1なら
            if (m_controller.m_playerMode == PlayerMode.Player1)
            {
                KuiLaud(KeyCode.Q, other);
                //2P
                ParticleFlg2 = true;
            }
            else if (m_controller.m_playerMode == PlayerMode.Player2)
            {
                KuiLaud(KeyCode.U, other);
                //1P
                ParticleFlg = true;
                
            }

        }
    }
    //杭にポイントをささげる
    void KuiLaud(KeyCode code,Collider other)
    {
        if (Input.GetKeyDown(code))
        {
            if (m_score.CurrentPoint <= 0) return;
            if (Timer.Instance.GetFinishFlag()) return;
            //自分の持ってる所持ポイント減らして杭に得点入れてスコアに得点入れる
            SoundManager.Instance.PlaySE("people-stadium-cheer1");
            Debug.Log("ささげた！！");
            m_score.LaudPoint(1);
            other.gameObject.GetComponent<Kui>().Praise();
            m_fukidashi.Praise();
            FindObjectOfType<balloonManager>().CreateBaloon(m_controller.m_playerMode);

            //　アニメーション処理
            Anim.SetImage(ImageState.Praise, Vector3.zero);
            
            //紙吹雪を生成する
            if(ParticleFlg == true)
            {
                var PaperStorm = GameObject.Find("PaperStorm");
                PaperStorm.GetComponent<ParticleSystem>().Play();
                ParticleFlg = false;
            }
            if(ParticleFlg2 == true)
            {
                var PaperStorm2 = GameObject.Find("PaperStorm2");
                PaperStorm2.GetComponent<ParticleSystem>().Play();
                ParticleFlg2 = false;
            }
        }
    }
}
