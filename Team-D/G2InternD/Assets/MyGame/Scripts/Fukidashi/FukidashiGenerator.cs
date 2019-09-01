using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fukidashi
{
    /// <summary>
    /// 各プレイヤーにアタッチ、参照を持ってもらってPraiseメソッド呼ぶ
    /// </summary>
    public class FukidashiGenerator : MonoBehaviour
    {
        [SerializeField] private Fukidashi[] Fukidashis;
        [SerializeField] private string[] Comments = new string[]
        {
            "すごい！", "さすがだ！", "いいセンスだ", "よくやった!", "Prefect！"
        };

        //　デバッグ用に
        [SerializeField] float timer = 0.5f;
        void Update()
        {
            if (timer < 0)
            {
               // Praise();
                timer = 0.5f;
            }
            else timer -= Time.deltaTime;
        }

        //　讃えるメソッドを外部から呼ぶ
        public void Praise()
        {
            Fukidashi fukidashi = null;
            for (int i = 0; i < Fukidashis.Length; i++)
            {
                if (!Fukidashis[i].gameObject.activeSelf)
                {
                    fukidashi = Fukidashis[i];
                    break;
                }
            }

            if (fukidashi != null) fukidashi.Initialize(Comments[Random.Range(0, Comments.Length)]);
        }
    }
}