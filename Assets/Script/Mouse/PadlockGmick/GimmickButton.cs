using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GimmickButton : MonoBehaviour
{
    //�R�����g����
    [SerializeField] Text _text;

    [SerializeField] List<string> _textChange;

    public string s_TergetLove;
    public string s_TersetGuol;

    private int currentIndex = 0;

    DialCheck _dialCheck;

    void Start()
    {
        _text.text = _textChange[currentIndex];
        _dialCheck = transform.parent.GetComponent<DialCheck>();
    }
    //�{�^�����������玟�̕����ɕς��
    public void ChangeNextLetter()
    {
        currentIndex = (currentIndex + 1) % _textChange.Count;
        _text.text = _textChange[currentIndex];
    }
    /// <summary>
    /// �{�^��L�̏���
    /// </summary>
    public void Button_L()
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.Click);
        ChangeNextLetter();// ���̕�����
        if (_text.text == s_TergetLove)
        {
            _dialCheck._TextL = true;
        }
        else if (_text.text == s_TersetGuol)
        {
            _dialCheck._guolG = true;
        }
        else
        {
            _dialCheck._guolG = false;
            _dialCheck._TextL = false;
        }
        _dialCheck.CollectLOVE().Forget();
        _dialCheck.CollectGUOL().Forget();
    }
    /// <summary>
    /// �{�^��O�̏���
    /// </summary>
    public void Button_O()
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.Click);
        ChangeNextLetter();// ���̕�����
        if (_text.text == s_TergetLove)
        {
            _dialCheck._TextO = true;
        }
        else if (_text.text == s_TersetGuol)
        {
            _dialCheck._guolU = true;
        }
        else
        {
            _dialCheck._guolU = false;
            _dialCheck._TextO = false;
        }
        _dialCheck.CollectLOVE().Forget();
        _dialCheck.CollectGUOL().Forget();
    }
    /// <summary>
    /// �{�^��V�̏���
    /// </summary>
    public void Button_V()
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.Click);
        ChangeNextLetter();// ���̕�����
        if (_text.text == s_TergetLove)
        {
            _dialCheck._TextV = true;
        }
        else if (_text.text == s_TersetGuol)
        {
            _dialCheck._guolO = true;
        }
        else
        {
            _dialCheck._guolO = false;
            _dialCheck._TextV = false;
        }
        _dialCheck.CollectLOVE().Forget();
        _dialCheck.CollectGUOL().Forget();
    }
    /// <summary>
    /// �{�^��E�̏���
    /// </summary>
    public void Button_E()
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.Click);
        ChangeNextLetter();// ���̕�����
        if (_text.text == s_TergetLove)
        {
            _dialCheck._TextE = true;
        }
        else if (_text.text == s_TersetGuol)
        {
            _dialCheck._guolL = true;
        }
        else
        {
            _dialCheck._guolL = false;
            _dialCheck._TextE = false;
        }
        _dialCheck.CollectLOVE().Forget();
        _dialCheck.CollectGUOL().Forget();
    }
}
