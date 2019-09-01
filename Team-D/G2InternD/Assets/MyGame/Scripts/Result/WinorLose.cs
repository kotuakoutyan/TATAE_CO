using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinorLose : MonoBehaviour {
    //勝敗時の画像表示
    public GameObject onePWin, twoPLose, twoPWin, onePLose, onePDraw, twoPDraw;

    //紙吹雪用の
    public GameObject oneP, twoP;

    public int PlayerPoint1P, PlayerPoint2P;

	// Use this for initialization
	void Start () {

        //PlayerPointの中を消去
        ResetScore();

        PlayerPoint1P = ScoreManager.Score1P;
        PlayerPoint2P = ScoreManager.Score2P;
        //PlayerPoint1P = 10;
        //PlayerPoint2P = 10;

    }

    // Update is called once per frame
    void Update () {
		
        //1Pwin
        if(PlayerPoint1P > PlayerPoint2P)
        {
            
            onePWin.SetActive(true);
            twoPLose.SetActive(true);
            oneP.SetActive(true);


        }
        //2Pwin
        if (PlayerPoint1P < PlayerPoint2P)
        {
            twoPWin.SetActive(true);
            onePLose.SetActive(true);
            twoP.SetActive(true);

        }
        //draw
        if(PlayerPoint1P == PlayerPoint2P)
        {
            onePDraw.SetActive(true);
            twoPDraw.SetActive(true);
        }
    }

    void ResetScore()
    {
        PlayerPoint1P = 0;
        PlayerPoint2P = 0;
    }
}
