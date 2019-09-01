using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScorePaneler : MonoBehaviour
{
    [SerializeField] Text ScoreP1_Text;
    [SerializeField] Text ScoreP2_Text;

    [SerializeField] Text Timer_Text;
    [SerializeField] Text Timeup_Text;

    void Start()
    {
        Debug();
    }

    void Debug()
    {
        //ScoreManager.ResetMasterScore();
        //Timer.Instance.Initialize(5);
    }

    void Update()
    {
        ScoreP1_Text.text = ScoreManager.Score1P.ToString();
        ScoreP2_Text.text = ScoreManager.Score2P.ToString();

        Timer_Text.text = Mathf.Ceil(Timer.Instance.GetTime()).ToString();
        if (Timer.Instance.GetFinishFlag())
        {
            StartCoroutine(Finish());
        }
        //Debug();
    }
    IEnumerator Finish()
    {
        Timer.Instance.StartFlag = false;
        GameObject.FindWithTag("Player1").GetComponent<PlayerController>().CanMove = false;
        GameObject.FindWithTag("Player2").GetComponent<PlayerController>().CanMove = false;

        SoundManager.Instance.PlaySE("ホイッスル・連続");
        Timeup_Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        // SceneManager.LoadScene("Result");
        SingleSceneManager.Instance.ChangeScene(GameState.Result, "Result");
    }
}