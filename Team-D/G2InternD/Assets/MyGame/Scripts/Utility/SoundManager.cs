using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    //ボリューム保存用のkeyとデフォルト値
    private const string m_bgmVolumeKey = "bgm_volume_key";
    private const string m_seVolumeKey = "se_volume_key";
    [SerializeField] private float m_bgmVolumeDefault = 1.0f;
    [SerializeField] private float m_seVolumeDefault = 1.0f;

    //BGMがフェードするのにかかる時間
    public const float m_bgmFadeSpeedRateHigh = 0.9f;
    public const float m_bgmFadeSpeedRateLow = 0.3f;
    private float m_bgmFadeSpeedRate = m_bgmFadeSpeedRateHigh;

    //次流すBGM名、SE名
    private string m_nextBGMName;
    private string m_nextSEName;

    //BGMをフェードアウト中か
    private bool m_isFadeOut = false;

    //BGM用、SE用に分けてオーディオソースを持つ
    public AudioSource AttachBGMSource, AttachSESource;

    //全Audioを保持
    private Dictionary<string, AudioClip> m_bgmDic, m_seDic;

    public bool m_footSound = true;

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        //リソースフォルダから全SE&BGMのファイルを読み込みセット
        m_bgmDic = new Dictionary<string, AudioClip>();
        m_seDic = new Dictionary<string, AudioClip>();

        object[] bgmList = Resources.LoadAll("Audio/BGM");
        object[] seList = Resources.LoadAll("Audio/SE");

        foreach (AudioClip bgm in bgmList)
        {
            m_bgmDic[bgm.name] = bgm;
        }
        foreach (AudioClip se in seList)
        {
            m_seDic[se.name] = se;
        }
    }
    // Use this for initialization
    void Start()
    {
        AttachBGMSource.volume = PlayerPrefs.GetFloat(m_bgmVolumeKey, m_bgmVolumeDefault);
        AttachSESource.volume = PlayerPrefs.GetFloat(m_seVolumeKey, m_seVolumeDefault);
    }
    //---------------------------SE-------------------------------//
    /// <summary>
    /// 指定したファイル名のSEを流す。
    /// 第二引数のdelayに指定した分だけ再生までの間隔をあける
    /// </summary>
    /// <param name="seName"></param>
    /// <param name="delay"></param>
    public void PlaySE(string seName, float delay = 0.0f)
    {
        if (!m_seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "という名前のSEがありません");
            return;
        }
        m_nextSEName = seName;
        Invoke("DelayPlaySE", delay);
    }
    private void DelayPlaySE()
    {
        if (m_footSound == true)
            AttachSESource.PlayOneShot(m_seDic[m_nextSEName] as AudioClip);
    }
    /// <summary>
    /// 3Dサウンド用　
    /// </summary>
    /// <param name="seName"></param>
    /// <param name="obj">AudioSourceがついてる鳴らす先のObject</param>
    public void PlaySE3D(string seName, AudioSource source, float deray = 0)
    {
        if (!m_seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "という名前のSEがありません");
            return;
        }
        m_nextSEName = seName;
        if (source != null)
            StartCoroutine(DelaySE3D(source, deray));
    }
    IEnumerator DelaySE3D(AudioSource source, float deray)
    {
        yield return new WaitForSeconds(deray);
        source.PlayOneShot(m_seDic[m_nextSEName]);
    }
    //---------------------------BGM-------------------------------//
    /// <summary>
    /// 指定したファイルの名のBGMを流す
    /// ただし、すでに流れている場合は前の曲をフェードアウトさせてから
    /// 第二引数のfadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
    /// </summary>
    public void PlayBGM(string bgmName, float fadeSpeedRate = m_bgmFadeSpeedRateHigh)
    {
        if (!m_bgmDic.ContainsKey(bgmName))
        {
            Debug.Log(bgmName + "という名前のBGMがありません");
            return;
        }
        //現在BGMが流れていない時はそのまま流す
        if (!AttachBGMSource.isPlaying)
        {
            m_nextBGMName = "";
            AttachBGMSource.clip = m_bgmDic[bgmName] as AudioClip;
            AttachBGMSource.Play();
        }
        //違うBGMが空流れているときは、流れているBGMをフェードアウトさせてから次を流す
        //同じBGMが流れているときはスルー
        else if (AttachBGMSource.clip.name != bgmName)
        {
            m_nextBGMName = bgmName;
            FadeOutBGM(fadeSpeedRate);
        }
    }

    /// <summary>
    /// 現在流れている曲をフェードアウトさせる
    /// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
    /// </summary>
    public void FadeOutBGM(float fadeSpeedRate = m_bgmFadeSpeedRateLow)
    {
        m_bgmFadeSpeedRate = fadeSpeedRate;
        m_isFadeOut = true;
    }
    // Update is called once per frame
    void Update()
    {
        //タイトル、ゲームシーンでBGM切り替え　
        //プレイヤーの状態3以上でBGM切り替え
        if (!m_isFadeOut)
        {
            return;
        }

        //徐々にボリュームを下げていき、ボリュームが0になったら戻し次の曲を流す
        AttachBGMSource.volume -= Time.deltaTime * m_bgmFadeSpeedRate;
        if (AttachBGMSource.volume <= 0)
        {
            AttachBGMSource.Stop();
            AttachBGMSource.volume = PlayerPrefs.GetFloat(m_bgmVolumeKey, m_bgmVolumeDefault);
            m_isFadeOut = false;
            if (!string.IsNullOrEmpty(m_nextBGMName))
            {
                PlayBGM(m_nextBGMName);
            }
        }
    }
    //------------------------音量変更-------------------------//
    public void ChangeVolume(float BGMVolume, float SEVolume)
    {
        AttachBGMSource.volume = BGMVolume;
        AttachSESource.volume = SEVolume;

        PlayerPrefs.SetFloat(m_bgmVolumeKey, BGMVolume);
        PlayerPrefs.SetFloat(m_seVolumeKey, SEVolume);
    }
    public void StopBGM()
    {
        if (AttachBGMSource.isPlaying)
        {
            FadeOutBGM(0.5f);
            AttachBGMSource.Stop();
        }
    }
    public void PlayRandomSE(AudioSource audioSource, float deray, params string[] one)
    {
        string s = null;
        int r = Random.Range(0, one.Length);
        s = one[r];

        if (m_footSound == true)
            PlaySE3D(s, audioSource, deray);
    }
    public void PlayRandomSE2(AudioSource audioSource, params string[] one)
    {
        string s = null;
        int r = Random.Range(0, one.Length);
        s = one[r];

        if (m_footSound == true)
            PlaySE(s);
    }
}