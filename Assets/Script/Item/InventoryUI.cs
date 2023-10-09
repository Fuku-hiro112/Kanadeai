using UnityEngine;

// ���[�v�ŉ����InventorySlot�ɃA�C�e�������邩�Ȃ����������Ă������
public class InventoryUI : MonoBehaviour
{

    public Transform SlotParent;

    InventorySlot[] _slots;
    void Start()
    {
        _slots = SlotParent.GetComponentsInChildren<InventorySlot>();
    }


    //�A�C�e�������邩�Ȃ���
    public void UpdateUI()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            //�A�C�e���\��
            if (i < Inventory.s_Instance.ItemList.Count)
            {
                _slots[i].AddItem(Inventory.s_Instance.ItemList[i]);
            }
            //�A�C�e�����\��
            else
            {
                _slots[i].ClearSlot();
            }
        }
    }
}
