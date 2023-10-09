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

    //‰Ô•r‚ðƒNƒŠƒbƒN‚µ‚½Œã
    private async UniTaskVoid ClickVase()
    {
        if (_breakVase)
        {
            await _dialogSystem.TypeDialogAsync("Š„‚ê‚½‰Ô•r‚ª‚ ‚é", true);
        }
        else
        {
            await _dialogSystem.TypeDialogAsync("‰Ô•r‚ª‚ ‚é", true);
            if (_guoletter != null)
            {
                await _dialogSystem.TypeDialogAsync("‰Ô•r‚Ì‰º‚ÉŽ†‚ª‹²‚Ü‚Á‚Ä‚¢‚é", true);
                await _dialogSystem.TypeDialogAsync($"{_guoletter.name}‚ðŽè‚É“ü‚ê‚½", true);
                _audioSource.Play();
                _vaseRenderer.sprite = _brokenVaseSprite;
                Inventory.s_Instance.Add(_guoletter);
                _guoletter = null;
                _breakVase = true;
            }
            else if (_frog != null)
            {
                await _dialogSystem.TypeDialogAsync($"‰Ô•r‚Ì’†‚É{_frog.name}‚ª‚¢‚é", true);
                await _dialogSystem.TypeDialogAsync($"{_frog.name}‚ðŽè‚É“ü‚ê‚½", true);
                Inventory.s_Instance.Add(_frog);
                _frog = null;
                GetComponent<AudioSource>().enabled = false;
            }
        }
        _iplayerController.MoveStart();
    }
}
