using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile: MonoBehaviour
{
    public float speed;
    public int damage = 10;
    private float timer = 0;
    public float destroyDelay = 3f;
    public bool destroyOnHit = true;

    // Start is called before the first frame update
    void Start()
    {

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

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int damage)
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

    /*
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Projectile" && destroyOnHit == true)
        {
           Destroy(this.gameObject);
        }      
    }
    */
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (destroyOnHit)
        {
            Destroy(this.gameObject);
        }
    }
    */

    
}
