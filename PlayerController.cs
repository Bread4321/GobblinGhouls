using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float maxHealth = 100;
    private float health;
    public float speed = 1.0f;
    public float timeDelay = 1f;
    public float numProjectiles = 1f;
    public GameObject weapon;
    private float timer = 0f;
    public GameObject ShootSpot;
    GameObject enemy;
    public Collider currentTrigger = null;
    public ItemManager itemManager;

    
    void Start()
    {
        health = maxHealth;
        timer = timeDelay;
        enemy = GameObject.FindWithTag("Enemy");
        itemManager = (ItemManager)GameObject.Find("Item Manager").GetComponent("ItemManager");
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

        
        if (health <= 0)
        {
            Debug.Log("probably dead but note that I should send you to a game over screen");
        }

        
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);

        timer += Time.deltaTime;
        // the thing that does the shooting
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
        weapon = (GameObject)Resources.Load(itemManager.GetCurrentWeapon() + "Model");
        

        if (numProjectiles == 1)
        {
            GameObject projectile = Instantiate(weapon, ShootSpot.transform.position, ShootSpot.transform.rotation);
            projectile.GetComponent<Projectile>().SetDamage(itemManager.GetDamage());
            
        }
        else
        {
            for (int i = 0; i < numProjectiles; i++)
            {
                float angle = Mathf.Lerp(-10.0f * numProjectiles / 2.0f, 10.0f * numProjectiles / 2.0f, (float)i / (float)(numProjectiles - 1));
                Vector3 direction = Quaternion.Euler(0, angle, 0) * ShootSpot.transform.forward;
                Vector3 position = ShootSpot.transform.position + direction;

                Quaternion spawnRotation = Quaternion.LookRotation(position - ShootSpot.transform.position);
                GameObject projectile = Instantiate(weapon, position, spawnRotation);
                projectile.GetComponent<Projectile>().SetDamage(itemManager.GetDamage());
            }
        }
    }

    private IEnumerator EndLevel()
    {
        currentTrigger.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Upgrades");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            health -= (collision.gameObject.GetComponent<Projectile>().GetDamage());
            Debug.Log(health + "/" + maxHealth);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        currentTrigger = null;
    }
    private void OnTriggerStay(Collider collider)
    {
        currentTrigger = collider;
    }

    public void LoseHealth(int num)
    {
        health -= num;
        Debug.Log(health + "/" + maxHealth);
    }
}
