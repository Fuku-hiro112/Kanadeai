using System.Collections.Generic;
using UnityEngine;

// UI�𔽉f�����邽�߂�InventoryUI�Ɏ����Ă������
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

    //�A�C�e����������Ƃ���UI�ɔ��f������
    public void Add(ItemData item)
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.GetItem);
        ItemList.Add(item);
        _inventoryUI.UpdateUI();
    }

    //�A�C�e�����Ȃ��Ȃ����Ƃ���UI�ɔ��f������
    public void Remove(ItemData item)
    {
        ItemList.Remove(item);
        _inventoryUI.UpdateUI();
    }
}