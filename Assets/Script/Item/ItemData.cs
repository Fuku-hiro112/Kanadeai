using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Create Item")]
public class ItemData : ScriptableObject
{
    new public string name = "New Item";
    public List<string> descriptionList;
    public Sprite icon = null;
    public Sprite realitySprite = null;
}
