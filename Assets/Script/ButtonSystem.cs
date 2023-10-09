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

            // シーン分けをし、必要ないと考えコメントアウトした
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
    /// ボタンの機能を割り当て　オバーロード３
    /// </summary>
    public void ButtonAddListener(string btnName,Func<CancellationToken,UniTaskVoid> func)// UniTaskVoidを返しtokenを引数で持つ場合
    {
        Button button = _listButton.Find(b=> b.name == btnName);
        button.onClick.RemoveAllListeners();// ボタンに設定済みのメソッドを消す
        button.onClick.AddListener(() => func.Invoke(_token));
    }
    public void ButtonAddListener(string btnName,Func<UniTaskVoid> func)// UniTaskVoidを返し引数を持たない場合
    {
        Button button = _listButton.Find(b=> b.name == btnName);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => func.Invoke());
    }
    public void ButtonAddListener(string btnName,Action action)//返り値も引数を持たない場合
    {
        Button button = _listButton.Find(b=> b.name == btnName);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => action.Invoke());
    }

    /// <summary>
    /// ボタンの表示・非表示
    /// </summary>
    public void ButtonEnable(bool show)
    {
        foreach (Button button in _listButton)
        {
            button.gameObject.SetActive(show);
        }
    }
}
