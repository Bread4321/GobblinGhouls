using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    public float timeDelay = 1f;
    public float numProjectiles = 1f;
    public GameObject Weapon;
    private float timer = 0f;
    public GameObject ShootSpot;
    public Collider currentTrigger = null;
    public ItemManager itemManager;
    
    void Start()
    {
        timer = timeDelay;
    }

    
    void Update()
    {
        if (currentTrigger != null)
        {


            if (Input.GetButtonDown("Interact"))
            {
                if (currentTrigger.gameObject.name == "EndLevel")
                {
                    StartCoroutine(EndLevel());
                }

            }
        }





                transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);

        timer += Time.deltaTime;
        if (Input.GetAxis("Fire") == 1.0f && timer > timeDelay)
        {
            Invoke("Fire", 0);
            timer = 0;
        }
    }

    public float getProjectileCount()
    {
        return numProjectiles;
    }

    public void setProjectileCount(float count)
    {
        numProjectiles = count;
    }

    public float getSpeed()
    {
        return speed;
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Fire()
    {
        if (numProjectiles == 1)
        {
            Instantiate(Weapon, ShootSpot.transform.position, ShootSpot.transform.rotation);
        } 
        else
        {
            for (int i = 0; i < numProjectiles; i++)
            {
                float angle = Mathf.Lerp(-10.0f * numProjectiles / 2.0f, 10.0f * numProjectiles / 2.0f, (float)i / (float)(numProjectiles - 1));
                Vector3 direction = Quaternion.Euler(0, angle, 0) * ShootSpot.transform.forward;
                Vector3 position = ShootSpot.transform.position + direction;

                Quaternion spawnRotation = Quaternion.LookRotation(position - ShootSpot.transform.position);
                Instantiate(Weapon, position, spawnRotation);
            }
        }        
    }

    private IEnumerator EndLevel()
    {
        currentTrigger.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Upgrades");
    }

    private void OnTriggerExit(Collider collider)
    {
        currentTrigger = null;
    }
    private void OnTriggerStay(Collider collider)
    {
        currentTrigger = collider;
    }
}
