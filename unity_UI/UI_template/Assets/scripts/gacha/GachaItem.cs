using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaItem 
{
    public int id ;
    public string itemName;
    public Sprite itemSprite;
    public GachaItem(int _id, string _itemName, Sprite _itemSprite)
    {
        id = _id;
        itemName = _itemName;
        itemSprite = _itemSprite;
    }
}
