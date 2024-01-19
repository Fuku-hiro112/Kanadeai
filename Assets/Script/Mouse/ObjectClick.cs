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
        // Busy状態へ移行
        _iplayerController.BusyStart();

        await _uiManager.DialogSystem.TypeDialogAsync($"{_item.name}がある。", isClick: true);
        if (_item.descriptionList != null)
        {
            // 説明文を１文ずつ表示
            foreach (var description in _item.descriptionList)
            {
                // descriptionに何か書いてあるなら
                if (description != null)// ネスト深い
                {
                    await _uiManager.DialogSystem.TypeDialogAsync(description, isClick: true);
                }
            }
        }
        _iplayerController.MoveStart();
    }
}
