using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : SingletonMonoBehaviour<Timer>
{
    public  bool StartFlag = false;
    [SerializeField]
    private float StartTime = 30;
    //[SerializeField]
    //Text m_timerText = null;
    // [SerializeField]
    //Text m_timeupText = null;

    private void Awake()
    {
        
    }
    private void Start()
    {
        var scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
        if(scene.name == "Stage1")
        Initialize(StartTime);
    }
    public void Initialize(float startTime)
    {
        StartFlag = true;
        StartTime = startTime;
        counter = 0;
    }

    float counter;
    void Update()
    {
        if (StartFlag)
        {
            StartTime -= Time.deltaTime;
            ////m_timerText.text = StartTime.ToString();
            //if (GetFinishFlag())
            //{
            //    //タイムアップ！！
            //    StartCoroutine(Finish());
            //}
        }
    }

    public bool GetFinishFlag()
    {
        return StartTime < 0f;
    }

    public float GetTime()
    {
        return StartTime;
    }
    
}
