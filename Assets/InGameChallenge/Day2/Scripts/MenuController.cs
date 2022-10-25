using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    GameObject button;

    [SerializeField]
    GameObject buttonPanel;

    public Color openButtonColor;
    public Color closedButtonColor;

    [SerializeField]
    GameObject menuPanel;

    public enum MenuParam
    {
        isOpen,
        isClosed
    }

    public MenuParam param;

    // Start is called before the first frame update
    void Start()
    {
        param = MenuParam.isClosed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMenu()
    {
        if (param == MenuParam.isOpen)
        {
            //?A?j???[?V????
            menuPanel.transform.DOScale(0.0f, 0.5f).SetEase(Ease.OutBounce);
            buttonPanel.GetComponent<Image>().DOColor(closedButtonColor, -1);
            //?p?????[?^???X
            param = MenuParam.isClosed;
        } else if(param == MenuParam.isClosed)
        {
            //?A?j???[?V????
            menuPanel.transform.DOScale(2.0f, 0.5f).SetEase(Ease.InBounce);
            buttonPanel.GetComponent<Image>().DOColor(openButtonColor, -1);
            //?p?????[?^???X
            param = MenuParam.isOpen;
        }
    }
}
