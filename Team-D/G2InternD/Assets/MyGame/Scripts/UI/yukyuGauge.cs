using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yukyuGauge : MonoBehaviour {
    [SerializeField]
    PlayerYukyu m_playerObj = null;
    [SerializeField]
    Image m_gaugeImage = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_gaugeImage.rectTransform.sizeDelta = new Vector2(m_playerObj.ObstacleGauge * 4,25);
	}
}
