using System.Collections.Generic;
using UnityEngine;

// AreaMoveData�̃C���^�[�t�F�[�X
public interface IAreaMoveData
{
    // �ǂ�����ǂ��Ɉړ����邩���i�[����Ă���
    Dictionary<GameObject, Vector3> Floor { get; }
    Dictionary<GameObject, Vector3> Room { get; }
}
