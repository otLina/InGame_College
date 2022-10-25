using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class SlimeController : MonoBehaviour
{
    [SerializeField]
    private GameObject slimeGroup;
    
    [SerializeField]
    private GameObject slime;
    private Vector3 slimePos;

    [SerializeField]
    private GameObject angerImage;

    [SerializeField]
    private GameObject damageGroup;
    public float xMin = -120.0f;
    public float xMax = -100.0f;
    public float yMin = 50.0f;
    public float yMax = 100.0f;

    [SerializeField]
    private GameObject damageNum;

    public Color attackedColor;


    // Start is called before the first frame update
    void Start()
    {
        var slimeSequence = DOTween.Sequence();

        slimePos = slimeGroup.GetComponent<RectTransform>().localPosition;

        slimeSequence.Append(slime.transform.DOScaleY(0.5f, 1.5f).SetEase(Ease.OutCubic))
            .Append(slime.transform.DOScaleY(2.0f, 0.5f).SetEase(Ease.OutCirc))
            .Insert(1.5f, slimeGroup.transform.DOLocalJump(
            endValue: slimePos,
            jumpPower: 130.0f,
            numJumps: 1,
            duration: 1.0f
            ));

        slimeSequence.SetEase(Ease.OutFlash).SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attacked()
    {
        var sequence = DOTween.Sequence();

        //?_???[?W????
        slime.GetComponent<RawImage>().DOColor(
            attackedColor,
            0.3f
        ).SetEase(Ease.OutFlash, 2);

        //?F???X
        slimeGroup.transform.DOPunchScale(duration: 0.4f, punch: new Vector3(0.3f, 0.3f, 0.3f)).SetEase(Ease.OutElastic);
        

        //?{???}?[?N?\??
        angerImage.transform.DOScale(0.7f, 0.25f).SetLoops(2, LoopType.Yoyo);

        //?_???[?W???\??
        /*Vector3 position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
        Instantiate(damageGroup, position, Quaternion.identity, slimeGroup.transform);*/
        sequence.Append(damageGroup.transform.DOScale(1.5f, 0.3f))
            .Append(damageGroup.transform.DOScale(1f, 0.3f))
            .AppendInterval(1)
            .Append(damageGroup.transform.DOScale(0f, 0.1f));
        //Destroy(damageGroup);

        //??????
        damageNum.GetComponent<TextMeshProUGUI>().text = Random.Range(100, 200).ToString();
    }

    public void AttackedSpecialSkill()
    {
        
    }
}
