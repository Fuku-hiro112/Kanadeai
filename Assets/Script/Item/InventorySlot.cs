using UnityEngine;
using UnityEngine.UI;

// アイテムスロットにあるアイコンをアイテムの画像に変える
public class InventorySlot : MonoBehaviour
{
    public Image Icon;
    public Text Name;

    ItemData item;
    //アイテムをUIに移す
    public void AddItem(ItemData newItem)
    {
        item = newItem;
        Icon.sprite = newItem.icon;
        Icon.color = Color.white; // アイコン色を表示
        Name.text = newItem.name;
    }

    //アイテムをUIからなくす
    public void ClearSlot()
    {
        item = null;
        Icon.sprite = null;
        Icon.color = Color.clear; // アイコンを透明に
        Name.text = null;
    }
}
