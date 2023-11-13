using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public ItemManager itemManager;
    void Start()
    {
        itemManager = (ItemManager) GameObject.Find("Item Manager").GetComponent("ItemManager");
    }

    public void AddItem()
    {
        itemManager.AddItem();
    }

    public void ChangeWeapon()
    {
        itemManager.ChangeWeapon();
    }

    public void UpgradeWeapon()
    {
        itemManager.UpgradeWeapon();
    }
}
