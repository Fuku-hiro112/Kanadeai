using UnityEngine;

// �����o�ϐ��݂̂����L����̂Ōp������Ȃ������悳����
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
