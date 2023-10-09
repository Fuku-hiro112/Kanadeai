using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSystem : MonoBehaviour
{
    [SerializeField] public List<Button> _listButton;
    public static ButtonSystem s_Instance;
    private CancellationToken _token;

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;

            // �V�[�����������A�K�v�Ȃ��ƍl���R�����g�A�E�g����
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ButtonEnable(false);
        _token = this.GetCancellationTokenOnDestroy();
    }

    /// <summary>
    /// �{�^���̋@�\�����蓖�ā@�I�o�[���[�h�R
    /// </summary>
    public void ButtonAddListener(string btnName,Func<CancellationToken,UniTaskVoid> func)// UniTaskVoid��Ԃ�token�������Ŏ��ꍇ
    {
        Button button = _listButton.Find(b=> b.name == btnName);
        button.onClick.RemoveAllListeners();// �{�^���ɐݒ�ς݂̃��\�b�h������
        button.onClick.AddListener(() => func.Invoke(_token));
    }
    public void ButtonAddListener(string btnName,Func<UniTaskVoid> func)// UniTaskVoid��Ԃ������������Ȃ��ꍇ
    {
        Button button = _listButton.Find(b=> b.name == btnName);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => func.Invoke());
    }
    public void ButtonAddListener(string btnName,Action action)//�Ԃ�l�������������Ȃ��ꍇ
    {
        Button button = _listButton.Find(b=> b.name == btnName);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => action.Invoke());
    }

    /// <summary>
    /// �{�^���̕\���E��\��
    /// </summary>
    public void ButtonEnable(bool show)
    {
        foreach (Button button in _listButton)
        {
            button.gameObject.SetActive(show);
        }
    }
}
