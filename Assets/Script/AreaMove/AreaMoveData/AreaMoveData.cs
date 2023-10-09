using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaMoveData : MonoBehaviour, IAreaMoveData
{
    // 配列でやるとメモリーに優しいのでは？
    [SerializeField, Header("階層移動")] private List<SetMove> _floorMoveList;
    [SerializeField, Header("部屋移動")] private List<SetMove> _roomMoveList;
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
    // リストをディクショナリーに保存する
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

// どのフロアからどの位置に移動するのかを格納する
[Serializable] public class SetMove
{
    [SerializeField] private string _name;
    // どこから
    [SerializeField] private GameObject _moveObject;
    // どこに
    [SerializeField] private Transform _destination;

    public GameObject MoveObject { get => _moveObject; }
    public Transform Destination { get => _destination; }
}


/*
 * // Listに問題があるか
    bool IsListSetFloorElement (SetFloor floorMove)
    {
        bool IsElement = !(floorMove.Floor == null || floorMove.Destination == null);
        if (!IsElement) Debug.Log("<color=red>Listの要素がありません。</color>");
        return IsElement;
    }
    // 重複しているか
    bool IsListSetFloorOverlapping(SetFloor floorMove)
    {
        bool IsOverlapping = false;//TODE: 重複しているかどうかを確認したい
        if (IsOverlapping) Debug.Log("<color=red>Listの要素が重複しています。</color>");
        return IsOverlapping;
    }


// フロア移動用のボタンにしたい
    // 引数を指定して場所をループさせてボタンにＡＤＤする
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private List<ButtonSet> _buttonList;

ButtonSet buttonSet = new ButtonSet();
        foreach (var button in _buttonList) 
        {
            if (buttonSet.Button == null)
            {
                Debug.Log("<color=red>ボタンが設定されていません</color>");
                return;
            }
            // GameObjectを作ってアタッチする手間を省いている
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