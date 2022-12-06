using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AttackAction
{
    /// <summary>
    /// ゲームクリア時のUI
    /// </summary>
    public class FinishView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private Text _resultText;

        public void Play(string timeText)
        {
            _resultText.text = "TIME " + timeText;
            _canvasGroup.DOFade(1f, 0.2f);
        }
    }
}
