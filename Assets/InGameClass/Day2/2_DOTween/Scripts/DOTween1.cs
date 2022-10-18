using DG.Tweening; // DOTweenを使う時は必須
using UnityEngine;

namespace DOTweenSample
{
    public class DOTween1 : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Material _material;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _material = GetComponent<Renderer>().material;
        }

        public void OnClick()
        {
            // 現在地から1秒かけて(2, 3, 4)に移動
            transform.DOMove(new Vector3(2, 3, 4), 1);

            // 現在地から1秒かけて(2, 3, 4)に移動
            //_rigidbody.DOMove(new Vector3(2, 3, 4), 1);

            // 現在の色から1秒かけて緑に変更
            //_material.DOColor(Color.green, 1);
        }

    }
}
