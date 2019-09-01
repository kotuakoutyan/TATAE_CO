using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(SphereCollider))]
public class Kui : MonoBehaviour
{
    [SerializeField] private int PRAISE_MAX = 3;
    public int PraiseCount = 0;

    [SerializeField] private float ALIVE_TIME = 100.0f;
    private float RespawnTime;

    [SerializeField] private float ANIM_TIME = 0.5f;
    [SerializeField] private float ANIM_SCALE = 1.1f;

    void OnEnable()
    {
        PraiseCount = PRAISE_MAX;
        RespawnTime = Time.time;
    }

    public void Initialize(Vector3 respawnPosition)
    {
        transform.position = respawnPosition + new Vector3(0,-3,0);
        //　DoTweenで出現するアニメーションを入れる
        var sequence = DOTween.Sequence();
        sequence.OnStart(() => {
            transform.localScale = Vector3.zero;
            transform.DOMove(respawnPosition, 0.3f);

        });
        sequence.Append(transform.DOScale(Vector3.one, ANIM_TIME));
        sequence.Append(transform.DOPunchScale(Vector3.one * ANIM_SCALE, ANIM_TIME));
        gameObject.SetActive(true);
       // transform.position = respawnPosition;
    }

    void Update()
    {
        if (Time.time - RespawnTime > ALIVE_TIME) Destroy();
    }

    public void Praise()
    {
        PraiseCount -= 1;
        if (PraiseCount == 0) DestroyDown();
    }

    private void Destroy()
    {
        //　DoTweenで消える処理を入れる
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + new Vector3(0, -3, 0),
            1)
            .OnComplete(()=> gameObject.SetActive(false)));

        
    }
    void DestroyDown()
    {
        //　DoTweenで消える処理を入れる
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + new Vector3(0,5,0),
            1)
            .OnComplete(() => gameObject.SetActive(false)));
    }

}
