using UnityEngine;
using UnityEngine.UI;

// アイテム取得
public class DropItem : MonoBehaviour
{
    public ItemData Item;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Item.realitySprite;
    }
    //アイテムを取ったとき
    public void GetItem()
    {
        Inventory.s_Instance.Add(Item);
        Destroy(gameObject);
    }
}
