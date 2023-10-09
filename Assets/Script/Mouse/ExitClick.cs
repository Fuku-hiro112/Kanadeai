using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitClick : MonoBehaviour, IClickAction
{
    [SerializeField] ItemData _keyItem;
    Fade _fade;
    private DialogSystem _dialogSystem;
    private IPlayerController _iplayerController;
    void Start()
    {
        _fade = new Fade();
        _dialogSystem = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ClickAction()
    {
        _iplayerController.BusyStart();
        ClickLockDoor().Forget();
    }
    private async UniTask ClickLockDoor()
    {
        await _dialogSystem.TypeDialogAsync("�J���Ȃ�...", isClick: true);
        if (HasItem(_keyItem))
        {
            await _dialogSystem.TypeDialogAsync($"{_keyItem.name}���g���܂����H");
            // �{�^������
            ButtonSystem btnSystem = ButtonSystem.s_Instance;
            btnSystem.ButtonEnable(true);                       // �\���E��\��
            btnSystem.ButtonAddListener("YesButton", ClickYes); // Yes�{�^���������蓖��
            btnSystem.ButtonAddListener("NoButton", ClickNo);  // No �{�^���������蓖��
        }
        else
        {
            _iplayerController.MoveStart();
        }
    }
    bool HasItem(ItemData item)=> Inventory.s_Instance.ItemList.Contains(item);
    /// <summary>
    /// Yes�{�^������
    /// </summary>
    /// <returns>UniTaskVoid</returns>
    private async UniTaskVoid ClickYes()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        Inventory.s_Instance.Remove(_keyItem);
        await _dialogSystem.TypeDialogAsync("�h�A���󂢂��B", isClick: true);
        //�Q�[���I��
        SceneManager.LoadScene("GameClearScene");
    }
    /// <summary>
    /// No�{�^������
    /// </summary>
    /// <returns>UniTaskVoid</returns>
    private void ClickNo()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        _iplayerController.MoveStart();
    }
}
