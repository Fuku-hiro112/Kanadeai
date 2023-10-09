using System.Collections.Generic;
using UnityEngine;

// UIを反映させるためにInventoryUIに持っていくやつ
public class Inventory : MonoBehaviour
{

    public static Inventory s_Instance;

    InventoryUI _inventoryUI;
    public List<ItemData> ItemList = new List<ItemData>();

    void Start()
    {
        _inventoryUI = GetComponent<InventoryUI>();
        _inventoryUI.UpdateUI();
    }
    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
    }

    //アイテムを取ったときにUIに反映させる
    public void Add(ItemData item)
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.GetItem);
        ItemList.Add(item);
        _inventoryUI.UpdateUI();
    }

    //アイテムがなくなったときにUIに反映させる
    public void Remove(ItemData item)
    {
        ItemList.Remove(item);
        _inventoryUI.UpdateUI();
    }
}