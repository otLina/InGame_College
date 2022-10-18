using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DOTweenSample
{
    public class EasingSample2 : MonoBehaviour
    {
        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Transform _target1;

        [SerializeField]
        private Transform _target2;

        [SerializeField]
        private Transform _target3;

        [SerializeField]
        private Transform _target4;

        /// <summary>
        /// イージングの再生
        /// </summary>
        public void OnClick()
        {
            var toY = 5.0f;

            var amplitudeElastic = 3f;  // 振幅
            var periodicElastic = 0.1f; // 振動周期

            var overshootBack = 4f;     // 逆方向に変化する度合い

            var overshootFlash = 4;     // 反復回数(整数型)
            var periodicFlash = 0.5f;   // 減衰度合い(-1～1)

            // 3つのオブジェクトを同時にアニメーションするために
            // Sequenceを使用
            var sequence = DOTween.Sequence();
            sequence
                .Insert(0,
                    _target1.DOLocalMoveY(toY, 1f)
                        .SetEase(Ease.InElastic,
                            amplitudeElastic,
                            periodicElastic))
                .Insert(0,
                    _target2.DOLocalMoveY(toY, 1f)
                        .SetEase(
                            Ease.InBack,
                            overshootBack))
                .Insert(0,
                    _target3.DOLocalMoveY(toY, 1f)
                        .SetEase(Ease.InBounce))
                .Insert(0,
                    _target4.DOLocalMoveY(toY, 1f)
                        .SetEase(
                            Ease.Flash,
                            overshootFlash,
                            periodicFlash))
                .AppendInterval(0.5f)         // 0.5秒待機
                .SetLoops(2, LoopType.Yoyo)   // 折り返す
                .OnStart(() => _playButton.interactable = false)
                .OnComplete(() => _playButton.interactable = true);
        }
    }
}
