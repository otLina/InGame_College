using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    GameObject button;

    [SerializeField]
    GameObject panel;

    public enum MenuParam
    {
        isOpen,
        isClosed
    }

    public MenuParam param = MenuParam.isClosed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMenu()
    {
        if (param == MenuParam.isOpen)
        {
            //アニメーション
            panel.transform.DOScale(2f, 0.5f).SetEase(Ease.OutBounce);
            //パラメータ変更
            param = MenuParam.isClosed;
        } else if(param == MenuParam.isClosed)
        {
            //アニメーション
            panel.transform.DOScale(0f, 0.5f).SetEase(Ease.InBounce);
            //パラメータ変更
            param = MenuParam.isOpen;
        }
    }
}
