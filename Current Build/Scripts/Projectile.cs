using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject itemManager;
    private GameObject player;
    public float speed;
    public float damage = 0;
    private float timer = 0;
    public float destroyDelay = 0f;
    public bool destroyOnHit = true;
    public bool isEnemy;
    public bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameObject.FindWithTag("GameController");
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (timer > destroyDelay)
        {
            Destroy(this.gameObject);
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void setDestroyDelay(float destroyDelay)
    {
        this.destroyDelay = destroyDelay;
    }

    public void enableDestroyOnHit(bool enable)
    {
        destroyOnHit = enable;
    }

    public void setIsEnemy(bool isEnemy)
    {
        this.isEnemy = isEnemy;
    }

    public bool getIsEnemy()
    {
        return isEnemy;
    }

    public void setIsPlayer(bool isPlayer)
    {
        this.isPlayer = isPlayer;
    }

    public bool getIsPlayer()
    {
        return isPlayer;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isEnemy == true) 
        {
            if (other.tag != "Projectile" && other.tag != "Enemy" && destroyOnHit == true)
            {
                Destroy(this.gameObject);
            }
        } 
        else if (isPlayer == true)
        {
            if (other.tag != "Projectile" && other.tag != "Player" && destroyOnHit == true)
            {
                if (itemManager.GetComponent<ItemManager>().NumOfItem(5) > 0)
                {
                    player.GetComponent<PlayerController>().setTempHealth(player.GetComponent<PlayerController>().getTempHealth() + (1f * itemManager.GetComponent<ItemManager>().NumOfItem(5)));
                }
                Destroy(this.gameObject);
            }
        }      
    }
}
