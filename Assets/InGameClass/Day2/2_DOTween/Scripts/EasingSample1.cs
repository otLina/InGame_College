using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DOTweenSample
{
    public class EasingSample1 : MonoBehaviour
    {
        [SerializeField]
        private Dropdown _dropdown;

        private Ease _ease;

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Transform _target1;

        [SerializeField]
        private Transform _target2;

        [SerializeField]
        private Transform _target3;

        /// <summary>
        /// イージング名の登録
        /// </summary>
        private void Start()
        {
            foreach (Ease ease in Enum.GetValues(typeof(Ease)))
            {
                // 緩急関連のイージングのみ登録
                if ((int)ease < (int)Ease.InElastic)
                {
                    string name = Enum.GetName(typeof(Ease), ease);
                    var data = new Dropdown.OptionData();
                    data.text = $"{(int)ease} : Ease.{name}";
                    _dropdown.options.Add(data);
                }
            }
            _dropdown.RefreshShownValue();
        }

        /// <summary>
        /// イージングの選択
        /// </summary>
        /// <param name="n"></param>
        public void OnValueChanged(int n)
        {
            _ease = (Ease)n;
        }

        /// <summary>
        /// イージングの再生
        /// </summary>
        public void OnClick()
        {
            var toX = 5.0f;
            var toRotete = new Vector3(0, 180f, 0);
            var toScale = new Vector3(5f, 0.3f, 1f);

            // 3つのオブジェクトを同時にアニメーションするために
            // Sequenceを使用
            var sequence = DOTween.Sequence();
            sequence
                .Insert(0,
                    _target1.DOLocalMoveX(toX, 1f)
                        .SetEase(_ease))
                .Insert(0,
                    _target2.DOLocalRotate(toRotete, 1f)
                        .SetEase(_ease))
                .Insert(0,
                    _target3.DOScale(toScale, 1f)
                        .SetEase(_ease))
                .AppendInterval(0.5f)         // 0.5秒待機
                .SetLoops(2, LoopType.Yoyo)   // 折り返す
                .OnStart(() => _playButton.interactable = false)
                .OnComplete(() => _playButton.interactable = true);
        }
    }
}
