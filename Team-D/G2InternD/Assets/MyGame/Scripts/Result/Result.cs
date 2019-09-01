using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//ボタンイベント作るときの必要なもの
using UnityEngine.EventSystems;

public class Result : MonoBehaviour {

    public Button ReturnButton;

    public Button ExitButton;

    public GameObject Button;

    public bool MouseClickFlg  = false;

	// Use this for initialization
	void Start () {
        //音
        SoundManager.Instance.PlaySE("ジングル・ファンファーレ10");

        //ボタンイベントを登録する
        //entry
        var Trigger = ReturnButton.GetComponent<EventTrigger>();

        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        
        entry.callback.AddListener(ButtonEnter);

        Trigger.triggers.Add(entry);

        //ボタンイベント出る 
        //exit
        //var ExitTrigger = ExitButton.GetComponent<EventTrigger>();

        var exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;

        exit.callback.AddListener(ButtonExit);


        Trigger.triggers.Add(exit);
	}

    void ButtonEnter(BaseEventData EventData)
    {
        //マウスが画像の中に入るとイベントが起こる
        //この中にイベント内容を書く
        SoundManager.Instance.PlaySE("button45");
        Debug.Log("ok");
        ReturnButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2((float)1.2, (float)1.2);

        MouseClickFlg = true;
    }
	
    void ButtonExit(BaseEventData EventData)
    {
        Debug.Log("ko");
        ReturnButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        MouseClickFlg = false;

    }

    // Update is called once per frame
    void Update () {
        if (MouseClickFlg == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseClick();
            }
        }
    }

    public void MouseClick()
    {
        //シーン切り替えタイトル行
        {
            SoundManager.Instance.PlaySE("ボタン音46");
            ReturnButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2((float)0.8, (float)0.8);

            // SceneManager.LoadScene("title");
            SingleSceneManager.Instance.ChangeScene(GameState.Title, "Title");
        }
    }
}
