using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

// �_�C�A���������Ă��邩�ǂ����𔻒肵���̏����������Ă���
public class DialCheck : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] ItemData _goldItem;
    [SerializeField] ItemData _silverItem;
    private IPlayerController _iplayerController;
    //love�̔���
    [NonSerialized] public bool _TextL = false;
    [NonSerialized] public bool _TextO = false;
    [NonSerialized] public bool _TextV = false;
    [NonSerialized] public bool _TextE = false;

    //guol�̔���
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
    //�����̂ق�
    public async UniTaskVoid CollectLOVE()
    {
        if (_TextL && _TextO && _TextV && _TextE)
        {
            Inventory.s_Instance.Add(_goldItem);
            AudioManager.Instance.PlaySE(SESoundData.SE.GetItem);
            await _uiManager.DialogSystem.TypeDialogAsync("���̃l�W����ɓ��ꂽ",true);
            _iplayerController.MoveStart();
            Destroy(_gimmikObj);
            _canvas.gameObject.SetActive(false);
            Debug.Log("����");
        }
    }
    //�Ԉ���Ă�ق�
    public async UniTaskVoid CollectGUOL()
    {
        if (_guolG && _guolU && _guolO && _guolL)
        {
            Inventory.s_Instance.Add(_silverItem);
            await _uiManager.DialogSystem.TypeDialogAsync("��̃l�W����ɓ��ꂽ", true);
            _iplayerController.MoveStart();
            Destroy(_gimmikObj);
            _canvas.gameObject.SetActive(false);
            Debug.Log("�ԈႢ");
        }
    }

    public void Cancel()
    {
        _canvas.gameObject.SetActive(false);
        _iplayerController.MoveStart();
    }
}
