using Cysharp.Threading.Tasks;
using UnityEngine;

public class VaseClick : MonoBehaviour, IClickAction
{
    [SerializeField] private ItemData _guoletter;
    [SerializeField] private ItemData _frog;
    [SerializeField] private Sprite _brokenVaseSprite;
    [SerializeField] private AudioSource _audioSource;
    private bool _breakVase = false;
    private SpriteRenderer _vaseRenderer;

    private UIManager _uiManager;
    private IPlayerController _iplayerController;

    void Start()
    {
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _vaseRenderer = GetComponent<SpriteRenderer>();
    }

    public void ClickAction()
    {
        _iplayerController.BusyStart();
        ClickVase().Forget();
    }

    //�ԕr���N���b�N������
    private async UniTaskVoid ClickVase()
    {
        if (_breakVase)
        {
            await _uiManager.DialogSystem.TypeDialogAsync("���ꂽ�ԕr������", true);
        }
        else
        {
            await _uiManager.DialogSystem.TypeDialogAsync("�ԕr������", true);
            if (_guoletter != null)
            {
                await _uiManager.DialogSystem.TypeDialogAsync("�ԕr�̉��Ɏ������܂��Ă���", true);
                await _uiManager.DialogSystem.TypeDialogAsync($"{_guoletter.name}����ɓ��ꂽ", true);
                _audioSource.Play();
                _vaseRenderer.sprite = _brokenVaseSprite;
                Inventory.s_Instance.Add(_guoletter);
                _guoletter = null;
                _breakVase = true;
            }
            else if (_frog != null)
            {
                await _uiManager.DialogSystem.TypeDialogAsync($"�ԕr�̒���{_frog.name}������", true);
                await _uiManager.DialogSystem.TypeDialogAsync($"{_frog.name}����ɓ��ꂽ", true);
                Inventory.s_Instance.Add(_frog);
                _frog = null;
                GetComponent<AudioSource>().enabled = false;
            }
        }
        _iplayerController.MoveStart();
    }
}
