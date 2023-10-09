using Cysharp.Threading.Tasks;
using UnityEngine;

public class ObjectClick : MonoBehaviour, IClickAction
{
    [SerializeField] private ItemData _item;
    private DialogSystem _dialogSystem;
    private IPlayerController _iplayerController;

    void Start()
    {
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _dialogSystem = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
    }

    public void ClickAction()
    {
        ClickObjectAsync().Forget();
    }

    private async UniTaskVoid ClickObjectAsync()
    {
        // Busy��Ԃֈڍs
        _iplayerController.BusyStart();

        await _dialogSystem.TypeDialogAsync($"{_item.name}������B", isClick: true);
        if (_item.descriptionList != null)
        {
            // ���������P�����\��
            foreach (var description in _item.descriptionList)
            {
                // description�ɉ��������Ă���Ȃ�
                if (description != null)// �l�X�g�[��
                {
                    await _dialogSystem.TypeDialogAsync(description, isClick: true);
                }
            }
        }
        _iplayerController.MoveStart();
    }
}
