using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

// ダイアルが合っているかどうかを判定しその処理を書いている
public class DialCheck : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] ItemData _goldItem;
    [SerializeField] ItemData _silverItem;
    private IPlayerController _iplayerController;
    //loveの判定
    [NonSerialized] public bool _TextL = false;
    [NonSerialized] public bool _TextO = false;
    [NonSerialized] public bool _TextV = false;
    [NonSerialized] public bool _TextE = false;

    //guolの判定
    [NonSerialized] public bool _guolG = false;
    [NonSerialized] public bool _guolU = false;
    [NonSerialized] public bool _guolO = false;
    [NonSerialized] public bool _guolL = false;

    [SerializeField] private GameObject _gimmikObj;

    private UIManager _uiManager;
    
    void Start()
    {
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }
    //正解のほう
    public async UniTaskVoid CollectLOVE()
    {
        if (_TextL && _TextO && _TextV && _TextE)
        {
            Inventory.s_Instance.Add(_goldItem);
            AudioManager.Instance.PlaySE(SESoundData.SE.GetItem);
            await _uiManager.DialogSystem.TypeDialogAsync("金のネジを手に入れた",true);
            _iplayerController.MoveStart();
            Destroy(_gimmikObj);
            _canvas.gameObject.SetActive(false);
            Debug.Log("正解");
        }
    }
    //間違ってるほう
    public async UniTaskVoid CollectGUOL()
    {
        if (_guolG && _guolU && _guolO && _guolL)
        {
            Inventory.s_Instance.Add(_silverItem);
            await _uiManager.DialogSystem.TypeDialogAsync("銀のネジを手に入れた", true);
            _iplayerController.MoveStart();
            Destroy(_gimmikObj);
            _canvas.gameObject.SetActive(false);
            Debug.Log("間違い");
        }
    }

    public void Cancel()
    {
        _canvas.gameObject.SetActive(false);
        _iplayerController.MoveStart();
    }
}
