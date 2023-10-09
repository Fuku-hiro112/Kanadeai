using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAreaMoveData
{
    Dictionary<GameObject, Vector3> Floor { get; }
    Dictionary<GameObject, Vector3> Room { get; }
}
