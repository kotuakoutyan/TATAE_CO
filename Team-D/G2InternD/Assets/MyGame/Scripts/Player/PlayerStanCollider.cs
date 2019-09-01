using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStanCollider : MonoBehaviour {

    [SerializeField]
    PlayerController m_controller = null;
    //自分が１Pか２Pか
    PlayerMode m_mode;

    //プレイヤー1がスタン可能なら
    bool isAttack1P = false;
    //プレイヤー2がスタン可能なら
    bool isAttack2P = false;
    // Use this for initialization
    void Start () {
        if (m_controller == null) Debug.Log("playerの親がアタッチされてないよ");
        m_mode = m_controller.m_playerMode;
	}

    
    private void OnTriggerStay(Collider other)
    {
        //自分が1Pの場合
        if (m_mode == PlayerMode.Player1)
        {
            if (other.gameObject.tag == "Player2")
            {
                //自分は攻撃、相手はダメージ
                m_controller.IsAttack = true;
            }
        }
        //自分が２Pの場合
        else if (m_mode == PlayerMode.Player2)
        {
            if (other.gameObject.tag == "Player1")
            {
                m_controller.IsAttack = true;
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        //自分が1Pの場合
        if (m_mode == PlayerMode.Player1)
        {
            if (other.gameObject.tag == "Player2")
            {
                m_controller.IsAttack = false;
            }
        }
        //自分が２Pの場合
        else if (m_mode == PlayerMode.Player2)
        {
            if (other.gameObject.tag == "Player1")
            {
                m_controller.IsAttack = false;

            }
        }
    }
}
