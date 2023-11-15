using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController: MonoBehaviour
{
    public float health = 0;
    private float tempHealth;
    public float speed = 1.0f;
    public float timeDelay = 1f;
    public float numProjectiles = 1f;
    public GameObject Weapon;
    private float timer = 0f;
    public GameObject ShootSpot;
    public Collider currentTrigger = null;
    public ItemManager itemManager; 
    GameObject enemy;
    private float localTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        if (itemManager.NumOfItem(0) > 0)
        {
            speed = speed + (0.2f * (float)itemManager.NumOfItem(0));
        }

        if (itemManager.NumOfItem(1) > 0)
        {
            health = health + (10f * itemManager.NumOfItem(1));
        }

        if (itemManager.NumOfItem(2) > 0)
        {
            Weapon.GetComponent<Projectile>().SetDamage(Weapon.GetComponent<Projectile>().GetDamage() + 2 * itemManager.NumOfItem(2));
        }

        if (itemManager.NumOfItem(3) > 0)
        {
            numProjectiles += 2 * itemManager.NumOfItem(3);
        }

        if (itemManager.NumOfItem(6) > 0)
        {
            timeDelay = timeDelay - (0.1f * (float)itemManager.NumOfItem(6));
        }

        tempHealth = health;
        timer = timeDelay;
        enemy = GameObject.FindWithTag("Enemy");
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        tempHealth = Mathf.Clamp(tempHealth, 0, health);

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
        
        {         
            if (tempHealth <= 0)
            {
                tempHealth = health;
                SceneManager.LoadScene("testing");
            }           
        }
       
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);

        timer += Time.deltaTime;
        // the thing that does the shooting
        if (Input.GetAxis("Fire1") == 1.0f && timer > timeDelay)
        {
            Invoke("Fire", 0);
            timer = 0;
        }

        //if bungus true then stand still
        if (itemManager.NumOfItem(7) > 0)
        {
            localTimer += Time.deltaTime;
            //Debug.Log(localTimer);
            if (Input.GetAxis("Vertical") == 0f && Input.GetAxis("Horizontal") == 0f && localTimer > 5f)
            {
                tempHealth += 2f;
                //Debug.Log(tempHealth);
            } 
            else if (Input.GetAxis("Vertical") != 0f && Input.GetAxis("Horizontal") != 0f)
            {
                localTimer = 0;
            }
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

    public float getTimeDelay()
    {
        return timeDelay;
    }

    public void setTimeDelay(float timeDelay)
    {
        this.timeDelay = timeDelay;
    }

    public float getTempHealth()
    {
        return tempHealth;
    }

    public void setTempHealth(float tempHealth)
    {
        this.tempHealth = tempHealth;
    }

    private void Fire()
    {
        if (numProjectiles == 1)
        {
            GameObject shot = Instantiate(Weapon, ShootSpot.transform.position, ShootSpot.transform.rotation);
            if (itemManager.NumOfItem(4) > 0)
            {
                LuckyFeather(shot);
            }
        }
        else
        {
            for (int i = 0; i < numProjectiles; i++)
            {
                float angle = Mathf.Lerp(-10.0f * numProjectiles / 2.0f, 10.0f * numProjectiles / 2.0f, (float)i / (float)(numProjectiles - 1));
                Vector3 direction = Quaternion.Euler(0, angle, 0) * ShootSpot.transform.forward;
                Vector3 position = ShootSpot.transform.position + direction;

                Quaternion spawnRotation = Quaternion.LookRotation(position - ShootSpot.transform.position);
                GameObject shot = Instantiate(Weapon, position, spawnRotation);
                if (itemManager.NumOfItem(4) > 0)
                {
                    LuckyFeather(shot);
                }
            }
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            if (other.GetComponent<Projectile>().getIsPlayer() == false)
            {
                tempHealth -= (other.GetComponent<Projectile>().GetDamage());
                Debug.Log(tempHealth);
            }           
        }

        if (other.tag == "Enemy")
        {
            tempHealth -= 1f;
            Debug.Log(tempHealth);
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

    private void LuckyFeather(GameObject weapon)
    {
        float chance = Random.Range(0f, 101f);
        if (chance <= 25f + (4f * itemManager.NumOfItem(4)))
        {
            weapon.GetComponent<Projectile>().SetDamage(weapon.GetComponent<Projectile>().GetDamage() * 2f);
        }
    }
}
