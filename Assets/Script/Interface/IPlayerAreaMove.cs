using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// エリア移動関数がある
public interface IPlayerAreaMove
{
    UniTask FloorMove(GameObject obj, CancellationToken token);
    UniTask RoomMove(GameObject obj, CancellationToken token);
}
