using UnityEngine;

// ƒƒ“ƒo•Ï”‚Ì‚İ‚ğ‹¤—L‚·‚é‚Ì‚ÅŒp³‚¶‚á‚È‚¢•û‚ª‚æ‚³‚»‚¤
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
