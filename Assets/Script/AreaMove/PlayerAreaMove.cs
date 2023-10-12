using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

// �v���C���[�̃G���A�ړ�
public class PlayerAreaMove : MonoBehaviour,IPlayerAreaMove
{
    // �G���A�ړ����̃t�F�[�h�ݒ�p
    [SerializeField] private int _frame = 1;
    [SerializeField] private int _alfaValue = 1;
    [SerializeField] private int _fadeInWaitTime = 1000;

    [SerializeField] private Transform _player;
    [SerializeField] private Image _panel;
    
    private UIManager _uiManager;
    private IAreaMoveData _iareaMoveData;
    private IPlayerController _iplayerController;

    private void Start()
    {
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _iareaMoveData = GetComponentInChildren<AreaMoveData>();// �q�I�u�W�F�N�g���猟��
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    // �������삪���������̂ł܂Ƃ߂���
    // iplayerController�̌Ăяo�����𓝈ꂵ����
    /// <summary>
    /// �K�w�ړ�
    /// </summary>
    public async UniTask FloorMove(GameObject obj, CancellationToken token)
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.StairsMove);// �K�iSE
        _iplayerController.BusyStart();// Player�������Ȃ��悤��

        Vector3 position = _iareaMoveData.Floor[obj];

        await _uiManager.Fade.FadeIn(_alfaValue, _frame, _panel, token);
        _player.position = position;//�w����W�ֈړ�

        // �����̔��]
        Vector3 pos = _player.localScale;
        int turnScale = -1;
        _player.localScale = new Vector3(pos.x * turnScale, pos.y, pos.z);

        await UniTask.Delay(_fadeInWaitTime);
        AudioManager.Instance.StopSE();
        await _uiManager.Fade.FadeOut(_alfaValue, _frame, _panel, token);

        _iplayerController.MoveStart();// Player��������悤��
    }
    /// <summary>
    /// �����ړ�
    /// </summary>
    /// <returns>UniTask</returns>
    public async UniTask RoomMove(GameObject obj, CancellationToken token)
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.OpenDoor);// �h�ASE
        Vector3 position = _iareaMoveData.Room[obj];

        await _uiManager.Fade.FadeIn(_alfaValue, _frame, _panel, token);
        _player.position = position;//�w����W�ֈړ�

        //���������@�����̃h�A���E���ɕt����悤�ɂ��Ă��邽��
        Vector3 turnRight = new Vector3(1, 1, 1);
        _player.localScale = turnRight;

        await UniTask.Delay(_fadeInWaitTime);
        AudioManager.Instance.PlaySE(SESoundData.SE.CloseDoor);
        await _uiManager.Fade.FadeOut(_alfaValue, _frame, _panel, token);
    }

}
