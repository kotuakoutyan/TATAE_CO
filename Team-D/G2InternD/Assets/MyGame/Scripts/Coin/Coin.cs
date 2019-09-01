using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Coins
{
    [RequireComponent(typeof(SphereCollider))]
    public class Coin : MonoBehaviour
    {
        [SerializeField] public int Score;

        [SerializeField] private float RotateSpeed = 10f;

        [SerializeField] private float RESPAN_ANIM_TIME = 0.5f;
        [SerializeField] private float ANIM_SCALE = 1.05f;

        [SerializeField] private float DROP_ANIM_TIME = 1.5f;
        [SerializeField] private float DROP_RANGE = 3.0f;
        [SerializeField] private float JUMP_POWER = 5.0f;


        public void Initialize(Vector3 position, int score = 1)
        {
            //　DoTweenでアニメーション処理を入れる
            var sequence = DOTween.Sequence();
            sequence.OnStart(() => { transform.localScale = Vector3.zero; });
            sequence.Append(transform.DOScale(Vector3.one, RESPAN_ANIM_TIME));
            sequence.Append(transform.DOPunchScale(Vector3.one * ANIM_SCALE, RESPAN_ANIM_TIME));

            gameObject.SetActive(true);
            transform.position = position;
            Score = score;
        }

        void FixedUpdate()
        {
            transform.rotation *= Quaternion.Euler(0, Time.deltaTime * RotateSpeed, 0);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Player1" 
             || collision.gameObject.tag == "Player2")
                gameObject.SetActive(false);
        }

        private void Destroy()
        {
            //　DoTweenでアニメーションを入れる処理
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(Vector3.zero, RESPAN_ANIM_TIME));

            gameObject.SetActive(false);
        }


        //　コインを浮かせるOffset
        private Vector3 POSITION_Y = new Vector3(0f, 0.5f, 0f);
        public void Drop(Vector3 position, int score = 1)
        {
            var jumpVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * DROP_RANGE;

            //　DoTweenでアニメーション処理を入れる
            var sequence = DOTween.Sequence();
            sequence.OnStart(() => { transform.localScale = Vector3.one; GetComponent<SphereCollider>().enabled = false; });
            sequence.Append(transform.DOJump(position + POSITION_Y + jumpVector, JUMP_POWER, 1, DROP_ANIM_TIME));
            sequence.OnComplete(() => { GetComponent<SphereCollider>().enabled = true; });

            gameObject.SetActive(true);
            transform.position = position;
            Score = score;
        }
    }
}
