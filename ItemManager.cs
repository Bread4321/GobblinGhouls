using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    private string[] weaponList = { "Feather", "Baster", "Fork" };
    private string[] itemList = { "Cranberries", "Mashed Potatoes", "Gravy", "Deviled Egg", 
                                  "Lucky Feather", "Pumpkin Pie", "Apple Pie", "Bustling Fungus" };


    private string currentWeapon;
    private int level = 1;
    private int[] items = {0, 0, 0, 0, 0, 0, 0, 0};

    private int randomIndex;
    private int stageIndex = 0;
    private bool isLoaded = false;

    public GameObject itemUpgrades;
    public GameObject weaponUpgrades;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        randomIndex = Random.Range(0, weaponList.Length);
        currentWeapon = weaponList[randomIndex];
        Debug.Log("Current Weapon: " + currentWeapon + " Level: " + level);
    }

    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Upgrades")
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            if (itemUpgrades.activeInHierarchy && isLoaded == false)
            {
                string[] itemListClone = (string[])itemList.Clone();

                for (int i = 0; i < 3; i++)
                {
                    GameObject slot = GameObject.Find("Item" + i);

                    do
                    {
                        randomIndex = Random.Range(0, itemListClone.Length);
                    } while (itemListClone[randomIndex] == null);

                    GameObject prefabClone = (GameObject) Instantiate(Resources.Load(itemListClone[randomIndex]), slot.transform);
                    prefabClone.name = itemListClone[randomIndex];
                    itemListClone[randomIndex] = null;
                }
                isLoaded = true;
            }

            if (weaponUpgrades.activeInHierarchy && isLoaded == false)
            {
                int slotIndex = 0;
                for (int i = 0; i < weaponList.Length; i++)
                {
                    if (currentWeapon != weaponList[i])
                    {
                        GameObject slot = GameObject.Find("Weapon" + slotIndex);
                        GameObject prefabClone = (GameObject) Instantiate(Resources.Load(weaponList[i]), slot.transform);
                        prefabClone.name = weaponList[i];
                        slotIndex++;
                    }
                }
                isLoaded = true;
            }
            
        } else
        {
            stageIndex = scene.buildIndex;
        }
      
    }


    public void AddItem()
    {
        string item = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name;

        for (int i = 0; i < itemList.Length; i++)
        {
            if (item == itemList[i])
            {
                items[i]++;
                Debug.Log("An item was added");
            }
        }
        itemUpgrades.SetActive(false);
        weaponUpgrades.SetActive(true);
        isLoaded = false;
    }
    
    public void ChangeWeapon()
    {
        GameObject weapon = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
        currentWeapon = weapon.name;
        level = 1;
        Debug.Log(currentWeapon + level);
        SceneManager.LoadScene(stageIndex + 1);
        isLoaded = false;
    }

    public void UpgradeWeapon()
    {
        level++;
        Debug.Log("Current Weapon: " + currentWeapon + " Level: " + level);
        SceneManager.LoadScene(stageIndex + 1);
        isLoaded = false;
    }

    public string GetCurrentWeapon()
    {
        return currentWeapon;
    }


    public int GetDamage()
    {
        if (currentWeapon == "Feather")
        {
            switch (level)
            {
                case 1:
                    return 2;
                case 2:
                    return 4;
                case 3:
                    return 4;
                case 4:
                    return 8;
            }
        } else if (currentWeapon == "Baster")
        {
            switch (level)
            {
                case 1:
                    return 4;
                case 2:
                    return 4;
                case 3:
                    return 6;
                case 4:
                    return 10;
            }
        } else
        {
            switch (level)
            {
                case 1:
                    return 4;
                case 2:
                    return 6;
                case 3:
                    return 12;
                case 4:
                    return 14;
            }
        }
        return 0;
    }

    public int NumOfItem(int index)
    {
        return items[index];
    }
}