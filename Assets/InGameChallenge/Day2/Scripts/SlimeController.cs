using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SlimeController : MonoBehaviour
{
    [SerializeField]
    private GameObject slimeGroup;
    
    [SerializeField]
    private GameObject slime;

    [SerializeField]
    private GameObject angerImage;

    [SerializeField]
    private GameObject damageGroup;
    public float xMin = -120.0f;
    public float xMax = -100.0f;
    public float yMin = 50.0f;
    public float yMax = 100.0f;

    public Color attackedColor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attacked()
    {
        var sequence = DOTween.Sequence();

        //ダメージ効果
        slime.GetComponent<RawImage>().DOColor(
            attackedColor,
            0.3f
        ).SetEase(Ease.OutFlash, 2);

        //色変更
        slimeGroup.transform.DOPunchScale(duration: 0.4f, punch: new Vector3(0.3f, 0.3f, 0.3f)).SetEase(Ease.OutElastic);

        //怒りマーク表示
        angerImage.transform.DOScale(0.7f, 0.25f).SetLoops(2, LoopType.Yoyo);

        //ダメージ数表示
        /*Vector3 position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
        Instantiate(damageGroup, position, Quaternion.identity, slimeGroup.transform);*/
        sequence.Append(damageGroup.transform.DOScale(1.5f, 0.3f))
            .Append(damageGroup.transform.DOScale(1f, 0.3f))
            .AppendInterval(1)
            .Append(damageGroup.transform.DOScale(0f, 0.1f));
    }

    public void AttackedSpecialSkill()
    {
        
    }
}
