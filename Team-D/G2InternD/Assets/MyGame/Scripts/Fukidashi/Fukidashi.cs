using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;

namespace Fukidashi
{
    public class Fukidashi : MonoBehaviour
    {
        private Color START_COLOR_IMAGE = new Color(1, 1, 1, 0.8f);
        private Color END_COLOR_IMAGE = new Color(1, 1, 1, 0.0f);

        [SerializeField] Color START_COLOR_TEXT = new Color(0.8f, 0, 0, 0.8f);
        [SerializeField] Color END_COLOR_TEXT = new Color(0.8f, 0, 0, 0.0f);

        [SerializeField] float ANIM_TIME = 1.0f;
        [SerializeField] float ANIM_SCALE = 1.01f;
        [SerializeField] float FADE_TIME = 2.0f;

        private RectTransform RectTransform;
        private Image FukidashiImage = null;
        private Text FukidashiText = null;

        void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            FukidashiImage = GetComponent<Image>();
            FukidashiText = GetComponentInChildren<Text>();
        }

        public void Initialize(string text)
        {
            Debug.Log("すごい！！");

            gameObject.SetActive(true);
            FukidashiText.text = text;

            var sequence = DOTween.Sequence();
            sequence.OnStart(() => { FukidashiImage.color = START_COLOR_IMAGE; FukidashiText.color = START_COLOR_TEXT; RectTransform.localScale = Vector3.zero;
            });
            sequence.Append(RectTransform.DOScale(Vector3.one, ANIM_TIME));
            //sequence.Append(RectTransform.DOPunchScale(Vector3.one * ANIM_SCALE, ANIM_TIME));
            sequence.Append(DOTween.To(
                () => FukidashiImage.color, 
                color => FukidashiImage.color = color, 
                END_COLOR_IMAGE,
                FADE_TIME
                ));
            sequence.Join(DOTween.To(
                () => FukidashiText.color,
                color => FukidashiText.color = color,
                END_COLOR_TEXT,
                FADE_TIME
                ));
            sequence.OnComplete(() => { gameObject.SetActive(false); Debug.Log("Sequence Finish"); });
        }
    }
}