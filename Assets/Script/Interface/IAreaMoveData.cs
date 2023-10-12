using System.Collections.Generic;
using UnityEngine;

// AreaMoveDataのインターフェース
public interface IAreaMoveData
{
    // どこからどこに移動するかが格納されている
    Dictionary<GameObject, Vector3> Floor { get; }
    Dictionary<GameObject, Vector3> Room { get; }
}
