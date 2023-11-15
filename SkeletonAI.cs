using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAI: MonoBehaviour
{
    public int health = 10;
    // private float timer = 0f;
    private float attackTimer = 0f;
    private float animationTimer = 0f;
    public float attackSpeed = 3f;
    // public GameObject weapon;
    // public GameObject ShootSpot;
    public Animator anim;
    private GameObject player;
    private NavMeshAgent enemy;
    public float speed = 5;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GetComponent<NavMeshAgent>();
        enemy.speed = speed;
    }

    
    void Update()
    {
        attackTimer += Time.deltaTime;
        animationTimer += Time.deltaTime;

        if (health <= 0)
        {
            StartCoroutine("skeletonDeath");
            
            
        } else
        {
            float distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

            if (distanceFromPlayer > 3)
            {
                enemy.SetDestination(player.transform.position);
                anim.Play("Walk");
            }
            else
            {
                if (attackTimer >= attackSpeed)
                {
                    anim.Play("Attack01");
                    player.GetComponent<PlayerController>().LoseHealth(10);
                    attackTimer = 0f;
                }
            }
        }

        // timer += Time.deltaTime;
        

        // int randomBehaviour = 0;

       
        /*
        if (timer >= 3)
        {
            randomBehaviour = (int)Random.Range(1f, 7f);
            timer = 0;
        }
        */

        /*
        if (distanceFromPlayer < 2.0f && attackDelay > 4f)
        {
            GameObject attack = Instantiate(weapon, new Vector3(ShootSpot.transform.position.x, ShootSpot.transform.position.y, ShootSpot.transform.position.z), ShootSpot.transform.rotation, this.transform);
            attack.GetComponent<Projectile>().SetSpeed(0f);
            attack.GetComponent<Projectile>().enableDestroyOnHit(false);
            attackDelay = 0f;
        }
        */
        /*
        if (randomBehaviour == 1)
        {
            //Debug.Log("behaviour left move");
            enemy.SetDestination(new Vector3(enemy.transform.position.x - 3.0f, enemy.transform.position.y, transform.position.z));
        }
        else if (randomBehaviour == 2)
        {
            //Debug.Log("behaviour right move");
            enemy.SetDestination(new Vector3(enemy.transform.position.x + 3.0f, enemy.transform.position.y, transform.position.z));
        }
        else if (randomBehaviour == 3)
        {
            //Debug.Log("behaviour left back move");
            enemy.SetDestination(new Vector3(enemy.transform.position.x - 3.0f, enemy.transform.position.y, transform.position.z + 3.0f));
        }
        else if (randomBehaviour == 4)
        {
            //Debug.Log("behaviour right back move");
            enemy.SetDestination(new Vector3(enemy.transform.position.x + 3.0f, enemy.transform.position.y, transform.position.z + 3.0f));
        }
        else if (randomBehaviour == 5)
        {
            //Debug.Log("behaviour left forward move");
            enemy.SetDestination(new Vector3(enemy.transform.position.x - 3.0f, enemy.transform.position.y, transform.position.z - 3.0f));
        }
        else if (randomBehaviour == 6)
        {
            //Debug.Log("behaviour right forward move");
            enemy.SetDestination(new Vector3(enemy.transform.position.x + 3.0f, enemy.transform.position.y, transform.position.z - 3.0f));
        }
        if (timer > 1)
        {
            enemy.SetDestination(player.transform.position);
        }
        */
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int value)
    {
        health = value;
    }

    private IEnumerator skeletonDeath()
    {
        enemy.velocity = new Vector3(0, 0, 0);
        anim.Play("Death01");
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        if (attackTimer >= attackSpeed)
        {
            anim.SetInteger("i", 1);
            player.GetComponent<PlayerController>().LoseHealth(10);
            attackTimer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemy.SetDestination(player.transform.position);
        anim.SetInteger("i", 0);

    }
    */
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            health -= (int) (collision.gameObject.GetComponent<Projectile>().GetDamage());
            anim.SetInteger("i", 2);
            
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        Projectile projectile = (Projectile) other.gameObject.GetComponent<Projectile>();
        health -= projectile.GetDamage();
        Debug.Log("Skeleton Health: " + health);
        anim.Play("Damage01");
        Destroy(other.gameObject);
    }
}
