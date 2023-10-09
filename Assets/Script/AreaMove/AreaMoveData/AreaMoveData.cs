using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaMoveData : MonoBehaviour, IAreaMoveData
{
    // �z��ł��ƃ������[�ɗD�����̂ł́H
    [SerializeField, Header("�K�w�ړ�")] private List<SetMove> _floorMoveList;
    [SerializeField, Header("�����ړ�")] private List<SetMove> _roomMoveList;
    private Dictionary<GameObject, Vector3> _floor;
    private Dictionary<GameObject, Vector3> _room;

    public Dictionary<GameObject, Vector3> Floor { get => _floor; }
    public Dictionary<GameObject, Vector3> Room { get => _room; }

    void Start()
    {
        _floor = new Dictionary<GameObject, Vector3>(_floorMoveList.Count);
        _room = new Dictionary<GameObject, Vector3>(_roomMoveList.Count);

        SetListToDictionary(_floorMoveList,_floor);
        SetListToDictionary(_roomMoveList,_room);
        /*
        foreach (SetMove floorMove in _floorMoveList)
        {
            _floor.Add(floorMove.MoveObject, floorMove.Destination.position);
            Debug.Log($"{floorMove.MoveObject}{floorMove.Destination.position}");
        }
        foreach (SetMove roomMove in _roomMoveList)
        {
            _room.Add(roomMove.MoveObject, roomMove.Destination.position);
            Debug.Log($"{roomMove.MoveObject}{roomMove.Destination.position}");
        }*/
    }
    // ���X�g���f�B�N�V���i���[�ɕۑ�����
    private void SetListToDictionary(List<SetMove> list, Dictionary<GameObject, Vector3> dictionary)
    {
        foreach (SetMove listIn in list)
        {
            dictionary.Add(listIn.MoveObject, listIn.Destination.position);
            Debug.Log($"{listIn.MoveObject}{listIn.Destination.position}");
        }
    }
    /*private void SaveListToDectionary(List<> list,)
    {
        foreach (SetFloor floorMove in _floorMoveList)
        {
            //if (IsListSetFloorElement(floorMove)) return;
            _floor.Add(floorMove.Floor.name, floorMove.Destination.position);
            Debug.Log($"{floorMove.Floor.name}{floorMove.Destination.position}");
        }
    }
    public void ListToDictionary<T>(List<T> moveList, Dictionary<GameObject, Vector3> move)
    {
        
        foreach (T moveElement in moveList)
        {
            dynamic dynamic = moveElement;
            move.Add(moveElement., moveElement.Destination.position);
        }
    }*/
}

// �ǂ̃t���A����ǂ̈ʒu�Ɉړ�����̂����i�[����
[Serializable] public class SetMove
{
    [SerializeField] private string _name;
    // �ǂ�����
    [SerializeField] private GameObject _moveObject;
    // �ǂ���
    [SerializeField] private Transform _destination;

    public GameObject MoveObject { get => _moveObject; }
    public Transform Destination { get => _destination; }
}


/*
 * // List�ɖ�肪���邩
    bool IsListSetFloorElement (SetFloor floorMove)
    {
        bool IsElement = !(floorMove.Floor == null || floorMove.Destination == null);
        if (!IsElement) Debug.Log("<color=red>List�̗v�f������܂���B</color>");
        return IsElement;
    }
    // �d�����Ă��邩
    bool IsListSetFloorOverlapping(SetFloor floorMove)
    {
        bool IsOverlapping = false;//TODE: �d�����Ă��邩�ǂ������m�F������
        if (IsOverlapping) Debug.Log("<color=red>List�̗v�f���d�����Ă��܂��B</color>");
        return IsOverlapping;
    }


// �t���A�ړ��p�̃{�^���ɂ�����
    // �������w�肵�ďꏊ�����[�v�����ă{�^���ɂ`�c�c����
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private List<ButtonSet> _buttonList;

ButtonSet buttonSet = new ButtonSet();
        foreach (var button in _buttonList) 
        {
            if (buttonSet.Button == null)
            {
                Debug.Log("<color=red>�{�^�����ݒ肳��Ă��܂���</color>");
                return;
            }
            // GameObject������ăA�^�b�`�����Ԃ��Ȃ��Ă���
            buttonSet.Button.onClick.AddListener(() => OnClick(button.Transform));
        }
[Serializable]
public class ButtonSet
{
    [SerializeField] private Button _button;
    [SerializeField] private Transform _position;

    internal Transform Transform { get => _position;}
    internal Button Button { get => _button;}
}*/