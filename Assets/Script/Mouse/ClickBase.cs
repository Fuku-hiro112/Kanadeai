using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBase : MonoBehaviour
{
    private IPlayerController _iPlayerController;
    private UIManager _uiManager;

    // Unity��Start��override�����Ȃ��Ă��A�����I�ɃI�[�o�[���C�h�����@������override�����Ƃ������͎�����new����Ă�����ۂ�
    protected void Start()// base.Start();�Őe��Start���ĂԂ���protected
    {
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _iPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public virtual void ClickAction() { }
}
