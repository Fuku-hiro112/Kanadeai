using Cysharp.Threading.Tasks;
using UnityEngine;

public class DebugVase : MonoBehaviour, IClickAction
{
    [SerializeField] private ItemData _guoletter;
    [SerializeField] private Sprite _vaseSprite;
    [SerializeField] private AudioSource _audioSource;
    private SpriteRenderer _vaseRenderer;

    private IPlayerController _iplayerController;
    private DialogSystem _dialogSystem;

    void Start()
    {
        _vaseRenderer = GetComponent<SpriteRenderer>();
        _dialogSystem      = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ClickAction()
    {
        _iplayerController.BusyStart();
        ClickVase().Forget();
    }

    //�ԕr���N���b�N������
    private async UniTaskVoid ClickVase()
    {
        Debug.Log("�N���b�N");
        Inventory.s_Instance.Add(_guoletter);
        await _dialogSystem.TypeDialogAsync($"{_guoletter.name}����ɓ��ꂽ", true);
        _vaseRenderer.sprite = _vaseSprite;
        _audioSource.Play();
        _iplayerController.MoveStart();
        gameObject.GetComponent<DebugVase>().enabled = false;
        _iplayerController.MoveStart();
    }
}
