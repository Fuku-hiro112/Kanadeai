using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// �G���A�ړ��֐�������
public interface IPlayerAreaMove
{
    UniTask FloorMove(GameObject obj, CancellationToken token);
    UniTask RoomMove(GameObject obj, CancellationToken token);
}
