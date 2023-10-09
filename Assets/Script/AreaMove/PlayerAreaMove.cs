using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAreaMove : MonoBehaviour,IPlayerAreaMove
{
    private Fade _fade;
    [SerializeField] private int _frame = 1;
    [SerializeField] private int _alfaValue = 1;
    [SerializeField] private int _fadeInWaitTime = 1000;
    [SerializeField] private Transform _player;
    [SerializeField] private Image _panel;
    
    private IAreaMoveData _iareaMoveData;
    private IPlayerController _iplayerController;

    private void Start()
    {
        _fade = new Fade();
        _iareaMoveData = GetComponentInChildren<AreaMoveData>();// 子オブジェクトから検索
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    /// <summary>
    /// 階層移動
    /// </summary>
    public async UniTask FloorMove(GameObject obj, CancellationToken token)
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.StairsMove);
        _iplayerController.BusyStart();// Playerが動けないように
        Vector3 position = _iareaMoveData.Floor[obj];

        await _fade.FadeIn(_alfaValue, _frame, _panel, token);
        _player.position = position;//指定座標へ移動

        // 向きの反転
        Vector3 pos = _player.localScale;
        int turnScale = -1;
        _player.localScale = new Vector3(pos.x * turnScale, pos.y, pos.z);

        await UniTask.Delay(_fadeInWaitTime);
        AudioManager.Instance.StopSE();
        await _fade.FadeOut(_alfaValue, _frame, _panel, token);
        _iplayerController.MoveStart();// Playerが動けるように
    }
    /// <summary>
    /// 部屋移動
    /// </summary>
    /// <returns>UniTask</returns>
    public async UniTask RoomMove(GameObject obj, CancellationToken token)
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.OpenDoor);
        Vector3 position = _iareaMoveData.Room[obj];

        await _fade.FadeIn(_alfaValue, _frame, _panel, token);
        _player.position = position;//指定座標へ移動

        //左を向く　部屋のドアを右側に付けるようにしているため
        Vector3 turnRight = new Vector3(1, 1, 1);
        _player.localScale = turnRight;

        await UniTask.Delay(_fadeInWaitTime);
        AudioManager.Instance.PlaySE(SESoundData.SE.CloseDoor);
        await _fade.FadeOut(_alfaValue, _frame, _panel, token);
    }
}
