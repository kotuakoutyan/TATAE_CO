using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState
{
    None,
    Title,
    Tutorial,
    Stage1,
    
    GameOver,
    Result
}
/// <summary>
/// マルチシーンを使わない場合のシーンマネージャー
/// </summary>
public class SingleSceneManager : SingletonMonoBehaviour<SingleSceneManager>
{
    //遷移にかかる時間
    public const float m_transSecond = 1f;
    string _deadSceneName = "Title";

    Dictionary<GameState, string> SceneName = new Dictionary<GameState, string>
    {
        { GameState.Title,"Title"},
        { GameState.Tutorial,"Tutorial"},
        { GameState.Stage1,"Stage1"},
        
        { GameState.GameOver,"GameOver"},
        { GameState.Result,"Result"},
    };

    public GameState state { get; set; }
    public GameState beforeStage { get; set; }
    public bool IsStageMoved { get; set; }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        IsStageMoved = true; //初期値がfalseで遷移しない...？

        if (state != GameState.Title)
        {
            var active = SceneManager.GetActiveScene();
            _deadSceneName = active.name;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("GameManager").Length != 1)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Escで終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
            Debug.Log("input escape");
        }
        
    }
    public void ChangeScene(GameState state, string bgmName = "")
    {
        if (!IsStageMoved) return;
        beforeStage = this.state;
        Instance.StartChangeState(state);
        this.state = state;
        Debug.Log(this.state + "," + beforeStage);
        if(bgmName != "") SoundManager.Instance.PlayBGM(bgmName);
    }
    //ステートだけ変える
    public void ChangeGameState(GameState state)
    {
        beforeStage = this.state;
        this.state = state;
    }
    public GameState GetSceneState()
    {
        return state;
    }

    void StartChangeState(GameState state)
    {
        StartCoroutine(TransState(state));
    }
    IEnumerator TransState(GameState state)
    {
        Resources.UnloadUnusedAssets();
        IsStageMoved = false;

        //フェードアウト
        GetComponentInChildren<BlackScreen>().FadeOut(0.3f);

        //仮に時間で遷移
        yield return new WaitForSeconds(m_transSecond / 2);
        SceneManager.LoadScene(SceneName[state]);

        //フェードイン
        GetComponentInChildren<BlackScreen>().FadeIn(0.3f);

        //仮に時間で処理
        yield return new WaitForSeconds(m_transSecond / 2);
        IsStageMoved = true;
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
                Application.Quit();
#endif
    }


}

