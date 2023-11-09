using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    card_style,
    skill
}
public abstract class itemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
}