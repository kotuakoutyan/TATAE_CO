using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class titlemanager : MonoBehaviour {

    private bool menuFlag;      //trueでメニューボタン表示

    public GameObject b_toGameButton;
    public GameObject b_toTutorialButton;

    // Use this for initialization
    void Start () {
        ScoreManager.ResetMasterScore();
        menuFlag = false;
        b_toGameButton = GameObject.Find("toGameButton");
        b_toTutorialButton = GameObject.Find("toTutorialButton");

        b_toGameButton.SetActive(false);
        b_toTutorialButton.SetActive(false);

        //GameObject.Find("toGameButton").SetActive(false);
        //GameObject.Find("toTutorialButton").SetActive(false);


    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ToutchScreen()
    {
        //音
        SoundManager.Instance.PlaySE("ボタン音23");

        Destroy(this.gameObject);
        menuFlag = true;

        b_toGameButton.SetActive(true);
        b_toTutorialButton.SetActive(true);
        //GameObject.Find("toGameButton").SetActive(true);
        //GameObject.Find("toTutorialButton").SetActive(true);

    }



}
