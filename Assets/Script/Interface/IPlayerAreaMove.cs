using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// ƒGƒŠƒAˆÚ“®ŠÖ”‚ª‚ ‚é
public interface IPlayerAreaMove
{
    UniTask FloorMove(GameObject obj, CancellationToken token);
    UniTask RoomMove(GameObject obj, CancellationToken token);
}
