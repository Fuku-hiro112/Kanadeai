using UnityEngine;

// ループで回ってInventorySlotにアイテムがあるかないかを持ってくいやつ
public class InventoryUI : MonoBehaviour
{

    public Transform SlotParent;

    InventorySlot[] _slots;
    void Start()
    {
        _slots = SlotParent.GetComponentsInChildren<InventorySlot>();
    }


    //アイテムがあるかないか
    public void UpdateUI()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            //アイテム表示
            if (i < Inventory.s_Instance.ItemList.Count)
            {
                _slots[i].AddItem(Inventory.s_Instance.ItemList[i]);
            }
            //アイテムを非表示
            else
            {
                _slots[i].ClearSlot();
            }
        }
    }
}
