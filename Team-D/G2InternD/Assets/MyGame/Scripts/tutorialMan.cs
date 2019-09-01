using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialMan : MonoBehaviour {

    public int tutorialNum;     //チュートリアル画像のスライド数

    public GameObject tutorial0;
    public GameObject tutorial1;

    //追加(使わないのにボタンが表示されるのを消すよう)
    public GameObject NextButton, BackButton;

    // Use this for initialization
    void Start () {
        tutorial0 = GameObject.Find("tutorial0");
        tutorial1 = GameObject.Find("tutorial1");

        tutorial0.SetActive(true);
        tutorial1.SetActive(false);

        tutorialNum = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (tutorialNum < 0)
        {
            tutorialNum = 0;
        }
        if (tutorialNum > 1)
        {
            tutorialNum = 1;
        }

        if (tutorialNum == 0)
        {
            BackButton.SetActive(false);
            NextButton.SetActive(true);

            tutorial0.SetActive(true);
            tutorial1.SetActive(false);
        }
        if (tutorialNum == 1)
        {
            BackButton.SetActive(true);
            NextButton.SetActive(false);

            tutorial0.SetActive(false);
            tutorial1.SetActive(true);
        }
    }


    public void T_pushBack()
    {
        SoundManager.Instance.PlaySE("ボタン音47");
        tutorialNum--;
    }

    public void T_pushNext()
    {
        SoundManager.Instance.PlaySE("ボタン音46");
        tutorialNum++;
    }


    public void T_pushEnd()
    {
        SoundManager.Instance.PlaySE("ボタン音46");
        SingleSceneManager.Instance.ChangeScene(GameState.Title);
    }

    public void OnPointEnter()
    {
        SoundManager.Instance.PlaySE("button45");
    }

}
