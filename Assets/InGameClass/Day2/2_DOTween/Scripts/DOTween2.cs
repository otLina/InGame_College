using DG.Tweening; // DOTweenを使う時は必須
using UnityEngine;

namespace DOTweenSample
{
    public class DOTween2 : MonoBehaviour
    {
        public void OnClick1()
        {
            transform.DOMoveX(5, 2.5f);
        }
        
        public void OnClick2()
        {
            transform.DOLocalMove(new Vector3(5f, 0, 0), 1f)
                .SetEase(Ease.InOutQuart);

        }

        public void OnClick3()
        {
            transform.DOLocalMove(new Vector3(5f, 0, 0), 1f)
                    .OnComplete(() => gameObject.SetActive(false));
        }
    }
}
