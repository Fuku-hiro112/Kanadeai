using UnityEngine;
using UnityEngine.UI;

// �A�C�e���X���b�g�ɂ���A�C�R�����A�C�e���̉摜�ɕς���
public class InventorySlot : MonoBehaviour
{
    public Image Icon;
    public Text Name;

    ItemData item;
    //�A�C�e����UI�Ɉڂ�
    public void AddItem(ItemData newItem)
    {
        item = newItem;
        Icon.sprite = newItem.icon;
        Icon.color = Color.white; // �A�C�R���F��\��
        Name.text = newItem.name;
    }

    //�A�C�e����UI����Ȃ���
    public void ClearSlot()
    {
        item = null;
        Icon.sprite = null;
        Icon.color = Color.clear; // �A�C�R���𓧖���
        Name.text = null;
    }
}
