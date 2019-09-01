using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class buttons : MonoBehaviour {



    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {

	}


    public void toGameButton()
    {
        SoundManager.Instance.PlaySE("ボタン音46");
        // SceneManager.LoadScene("Stage1");
        SingleSceneManager.Instance.ChangeScene(GameState.Stage1, "Game");
    }

    public void toTutorialButton()
    {
        SoundManager.Instance.PlaySE("ボタン音46");
        // SceneManager.LoadScene("Tutorial");
        SingleSceneManager.Instance.ChangeScene(GameState.Tutorial, "Title");
    }

    public void OnPointEnter()
    {
            SoundManager.Instance.PlaySE("button45");
    }

}
