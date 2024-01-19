using Cysharp.Threading.Tasks;
using UnityEngine;

public class ObjectClick : MonoBehaviour, IClickAction
{
    [SerializeField] private ItemData _item;

    private UIManager _uiManager;
    private IPlayerController _iplayerController;

    void Start()
    {
        _uiManager         = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ClickAction()
    {
        ClickObjectAsync().Forget();
    }

    private async UniTaskVoid ClickObjectAsync()
    {
        // Busy��Ԃֈڍs
        _iplayerController.BusyStart();

        await _uiManager.DialogSystem.TypeDialogAsync($"{_item.name}������B", isClick: true);
        if (_item.descriptionList != null)
        {
            // ���������P�����\��
            foreach (var description in _item.descriptionList)
            {
                // description�ɉ��������Ă���Ȃ�
                if (description != null)// �l�X�g�[��
                {
                    await _uiManager.DialogSystem.TypeDialogAsync(description, isClick: true);
                }
            }
        }
        _iplayerController.MoveStart();
    }
}
