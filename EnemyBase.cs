using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float Health = 0;
    GameObject player;
    private ParticleSystem effect;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        effect = gameObject.GetComponent<ParticleSystem>();
    }

    
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Health -= (collision.gameObject.GetComponent<Projectile>().GetDamage());
            Debug.Log(Health);
            effect.Play();
        }
    }
}
