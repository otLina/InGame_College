using UnityEngine;
using UnityEngine.UI;

namespace AttackAction
{
    /// <summary>
    /// 経過時間を表示するUI
    /// </summary>
    public class TimeView : MonoBehaviour
    {
        [SerializeField]
        private Text _timeText;

        /// <summary>
        /// 渡された時間を表示
        /// XX.XX.XXのフォーマットに整形する
        /// </summary>
        public void SetTime(float time)
        {
            _timeText.text = string.Format("{0:D2}.{1:D2}.{2:D2}",
                (int)time,
                (int)(time * 100) % 100,
                (int)(time * 10000) % 100
            );
        }

        public string GetTime()
        {
            return _timeText.text;
        }
    }
}
