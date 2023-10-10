using UnityEngine;

// メンバ変数のみを共有するので継承じゃない方がよさそう
public class ClickBase : MonoBehaviour
{
    protected UIManager _uiManager;
    protected IPlayerController _iplayerController;

    void Start()
    {
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
}
