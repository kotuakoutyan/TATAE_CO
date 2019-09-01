using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode {
    Player1,
    Player2,
}

[RequireComponent(typeof(PlayerImageAnimation))]
public class PlayerController : MonoBehaviour {
    public float PlayerSpeed { get; private set; }
    //動けるか
    public bool CanMove { get; set; }
    //当たってるかどうかフラグ
    public bool IsAttack { get; set; }
    [SerializeField,Header("1P2P")]
    public  PlayerMode m_playerMode = PlayerMode.Player1;
    [SerializeField, Header("移動速度")]
    float m_moveSpeed = 10;
    [SerializeField, Header("スタン時間(秒) ")]
    float m_stanSeconds = 2;

    [SerializeField]
    ParticleSystem m_particle = null;

    [SerializeField]
    PlayerImageAnimation Anim;

    //相手プレイヤーオブジェクト
    GameObject m_otherPlayer = null;
    PlayerController m_otherController = null;
    PlayerScore m_score = null;
    PlayerYukyu m_yukyu = null;
    [SerializeField]
    YukyuAnime m_yukyuanime = null;
    // Use this for initialization
    void Start () {
        CanMove = true;
        IsAttack = false;
        //相手キャラのオブジェクトキャッシュしとく
        if(m_playerMode == PlayerMode.Player1)
            m_otherPlayer = GameObject.FindWithTag("Player2");
        else
            m_otherPlayer = GameObject.FindWithTag("Player1");
        m_otherController = m_otherPlayer.GetComponent<PlayerController>();
        m_score = gameObject.GetComponent<PlayerScore>();
        m_yukyu = gameObject.GetComponent<PlayerYukyu>();
        m_particle.Stop();
        PlayerSpeed = m_moveSpeed;

        Anim = GetComponent<PlayerImageAnimation>();
    }

    // Update is called once per frame
    void Update () {
        if (CanMove)
        {
            if (m_playerMode == PlayerMode.Player1)
            {
                //攻撃
                StanAttack(KeyCode.E);
                //移動
                Move(KeyCode.A, KeyCode.D, KeyCode.S, KeyCode.W);
            }
            else if (m_playerMode == PlayerMode.Player2)
            {
                
                //攻撃
                StanAttack(KeyCode.O);
                //移動
                Move(KeyCode.J, KeyCode.L, KeyCode.K, KeyCode.I);
            }
        }
        else
        {
            Stan();
        }
    }
    //移動
    private void Move(KeyCode left,KeyCode right, KeyCode up,KeyCode down)
    {
        float  speedX = 0, speedY = 0;
        if (Input.GetKey(left))
            speedX = -1;
        if (Input.GetKey(right))
            speedX = 1;
        if (Input.GetKey(up))
            speedY = -1;
        if (Input.GetKey(down))
            speedY = 1;
        //移動量
        var v = Vector3.Normalize(new Vector3(speedX,0,speedY));

        //　アニメーション処理
        if (v.magnitude > 0.1f) Anim.SetImage(ImageState.Run, v);
        else Anim.SetImage(ImageState.Wait, v);

        transform.position += v * Time.deltaTime * PlayerSpeed;
    }
    //スタン攻撃
    public void StanAttack(KeyCode code)
    {
        if (Input.GetKeyDown(code))
        {
            if (Timer.Instance.GetFinishFlag()) return;
            //スタンアクション
            Debug.Log("スタン攻撃してるよ");

            //相手に当たってる時だけDamageを与える
            if (IsAttack && m_yukyu.CanAction)
            {
                m_yukyu.ActionYukyu();
                m_otherController.Damage();
            }
        }
    }

    float m_stanTimer = 0;
    //ダメージ
    public void Damage()
    {
        Debug.Log("ダメージ！！");
        m_score.DamagePoint(1);
        //有給UIを出す
        m_yukyuanime.BeginYukyu();
        m_particle.Play();
        CanMove = false;
        m_stanTimer = 0;

        //　アニメーション処理
        Anim.SetImage(ImageState.Stan, Vector3.zero);
    }
    //スタン中処理
    void Stan()
    {//一定時間たったら動けるようになる
        m_stanTimer += Time.deltaTime;
        if (m_stanTimer > m_stanSeconds)
        {
            m_particle.Stop();
            CanMove = true;
        }
    }
    //たたえる状態
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            m_score.AddPoint(1);
        }
    }
    public void AddSpeed(float num)
    {
        Debug.Log(PlayerSpeed);
       PlayerSpeed += num;
        if (PlayerSpeed >5)
            PlayerSpeed = 5;
    }
    public void DisSpeed(float num)
    {
        Debug.Log(PlayerSpeed);

        PlayerSpeed -= num;
        if (PlayerSpeed < 2)
            PlayerSpeed = 2;
    }
}
