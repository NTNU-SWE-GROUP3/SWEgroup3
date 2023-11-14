using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public List<InventorySlot> Equipments = new List<InventorySlot>();
    public void AddItem(itemObject _item, int _amount)
    {
        bool hasitem = false;
        for(int i=0; i<Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasitem = true;
                break;
            }
        }
        if(!hasitem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }
    }
    public void SellItem(itemObject _item, int _amount)
    {
        bool hasitem = false;
        for(int i=0; i<Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                hasitem = true;
                if(Container[i].amount >= _amount)
                {
                    Container[i].RemoveAmount(_amount);
                    //notify user the transaction is finished and how much money the user gets
                }
                else
                {
                    //notify user the transaction failed due to not having enough items to sell
                }
                break;
            }
        }
        if(!hasitem)
        {
            //notify user that the item does not exist in inventory
        }
    }
    public void EquipItem(itemObject _item)
    {
        bool hasitem = false;
        for(int i=0; i<Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                hasitem = true;
                bool isFull = true;
                for(int j=0; j<Equipments.Count; j++)
                {
                    if(Equipments[j].item == null)
                    {
                        isFull = false;
                        //can equip item
                        Equipments[j].item = _item;
                        Container[i].RemoveAmount(1);

                        //apply the effect of the equipped card style
                        break;
                    }
                }
                if(isFull)
                {
                    //notify user that he cannot equip anymore equipments. (Already full)
                }
                break;
            }
        }
        if(!hasitem)
        {
            //notify user that the card style is not found in inventory and equipment fails
        }
    }
    public void UnequipItem(itemObject _item)
    {
        bool isEquipped = false;
        for(int i=0; i<Equipments.Count; i++)
        {
            if(Equipments[i].item == _item)
            {
                isEquipped = true;
                Container.Add(new InventorySlot(_item, 1));
                Equipments[i].item = null;

                //remove the effect of the card style

                break;
            }
        }
        if(!isEquipped)
        {
            //notify user that the card style is not equipped
        }
    }
}
[System.Serializable]
public class InventorySlot
{
    public itemObject item;
    public int amount;
    public InventorySlot(itemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
    public void RemoveAmount(int value)
    {
        amount -= value;
    }
}
