using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage = 0;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (timer > 3)
        {
            Destroy(this.gameObject);
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Projectile")
        {
           Destroy(this.gameObject);
        }      
    }
}
