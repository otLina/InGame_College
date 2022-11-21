using DG.Tweening;
using UnityEngine;

namespace HitEffectAnswer
{
    public class EnemyWeaponControl : MonoBehaviour
    {
        // Redererコンポーネントへの参照
        [SerializeField]
        private Renderer _renderer;

        // マテリアルへの参照
        private Material _mat;
        // デフォルトカラーの保存
        private Color _defaultColor;

        /// <summary>
        /// 回転武器の時間
        /// </summary>
        [SerializeField]
        private float rotateTime = 1.0f;

        private void Start()
        {
            _mat = _renderer.material;
            _defaultColor = _mat.color;

            var seq = DOTween.Sequence()
                .AppendInterval(3f)         // 3秒待機
                .Append(_mat.DOColor(Color.red, 1f).SetEase(Ease.Flash, 5))
                .Append(transform
                            .DORotate(new Vector3(0, 360, 0), rotateTime)
                            .SetRelative())
                .Append(_mat.DOColor(_defaultColor, 0.1f))
                .SetLoops(-1, LoopType.Restart);
        }
    }
}
