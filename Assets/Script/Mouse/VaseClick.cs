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
    private DialogSystem _dialogSystem;
    private IPlayerController _iplayerController;

    void Start()
    {
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _dialogSystem = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
        _vaseRenderer = GetComponent<SpriteRenderer>();
    }

    public void ClickAction()
    {
        _iplayerController.BusyStart();
        ClickVase().Forget();
    }

    //花瓶をクリックした後
    private async UniTaskVoid ClickVase()
    {
        if (_breakVase)
        {
            await _dialogSystem.TypeDialogAsync("割れた花瓶がある", true);
        }
        else
        {
            await _dialogSystem.TypeDialogAsync("花瓶がある", true);
            if (_guoletter != null)
            {
                await _dialogSystem.TypeDialogAsync("花瓶の下に紙が挟まっている", true);
                await _dialogSystem.TypeDialogAsync($"{_guoletter.name}を手に入れた", true);
                _audioSource.Play();
                _vaseRenderer.sprite = _brokenVaseSprite;
                Inventory.s_Instance.Add(_guoletter);
                _guoletter = null;
                _breakVase = true;
            }
            else if (_frog != null)
            {
                await _dialogSystem.TypeDialogAsync($"花瓶の中に{_frog.name}がいる", true);
                await _dialogSystem.TypeDialogAsync($"{_frog.name}を手に入れた", true);
                Inventory.s_Instance.Add(_frog);
                _frog = null;
                GetComponent<AudioSource>().enabled = false;
            }
        }
        _iplayerController.MoveStart();
    }
}
