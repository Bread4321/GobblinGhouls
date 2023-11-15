using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase: MonoBehaviour
{
    public float Health = 0;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    public float GetHealth()
    {
        return Health;
    }

    public void SetHealth(float value)
    {
        Health = value;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            if (other.GetComponent<Projectile>().getIsEnemy() == false)
            Health -= (other.GetComponent<Projectile>().GetDamage());
            Debug.Log(Health);

        }       
    }
}
