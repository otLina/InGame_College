using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AttackAction
{
    /// <summary>
    /// ゲーム開始前の演出
    /// </summary>
    public class ReadyView : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        [SerializeField]
        private Image _overlayImage;

        /// <summary>
        /// READY...の表示
        /// </summary>
        /// <param name="waitTime">テキストが完全に表示されてから消え始めるまでの待ち時間</param>
        /// <param name="onComplete">演出終了時のコールバック</param>
        public void PlayReady(float waitTime, Action onComplete)
        {
            DOTween.Sequence()
                .Append(_text.rectTransform.DOAnchorPosY(100f, 1f))
                .Join(_text.DOFade(1f, 1f))
                .AppendInterval(waitTime)
                .Append(_text.DOFade(0f, 0.25f))
                .OnComplete(() => onComplete.Invoke());
        }

        /// <summary>
        /// START!の表示
        /// </summary>
        public void PlayStart()
        {
            _text.rectTransform.localScale = Vector2.zero;
            _text.text = "START!";
            _text.color = Color.red;
            _text.fontSize = (int)(_text.fontSize * 1.3f);

            DOTween.Sequence()
                .Append(_text.rectTransform.DOScale(1.3f, 0.15f))
                .Join(_overlayImage.DOFade(0f, 0.2f))
                .Append(_text.rectTransform.DOScale(0.7f, 0.15f))
                .Append(_text.rectTransform.DOScale(1f, 0.2f))
                .Append(_text.DOFade(0f, 0.5f));
        }
    }
}
