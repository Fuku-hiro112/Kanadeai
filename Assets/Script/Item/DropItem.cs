using UnityEngine;
using UnityEngine.UI;

// �A�C�e���擾
public class DropItem : MonoBehaviour
{
    public ItemData Item;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Item.realitySprite;
    }
    //�A�C�e����������Ƃ�
    public void GetItem()
    {
        Inventory.s_Instance.Add(Item);
        Destroy(gameObject);
    }
}
