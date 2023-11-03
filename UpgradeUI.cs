using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUI : MonoBehaviour
{
    private string[] weaponList = { "FeatherImage", "BasterImage", "ForkImage" };
    public ArrayList playerWeapons = new ArrayList();
    private int randomIndex;
   
    void Start()
    {


        for (int i = 0; i < 3; i++)
        {
            GameObject slot = GameObject.Find("Upgrade" + i);


            do
            {
                randomIndex = Random.Range(0, weaponList.Length);
            } while (weaponList[randomIndex] == null);

            Debug.Log(randomIndex);

            Instantiate(Resources.Load(weaponList[randomIndex]), slot.transform);
            weaponList[randomIndex] = null;
        }
    }

    public void addItem()
    {
        GameObject item = EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject;
        playerWeapons.Add(item);
    }

    
    void Update()
    {
        
    }
}
