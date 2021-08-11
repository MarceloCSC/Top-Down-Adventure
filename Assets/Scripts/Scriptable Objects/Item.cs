
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{

    [SerializeField] Sprite itemSprite;
    [TextArea(4, 6)]
    public string itemDescription;
    public bool isKey;

}
