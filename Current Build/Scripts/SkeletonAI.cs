using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAI: MonoBehaviour
{
    private float timer = 0f;
    private float attackDelay = 3f;
    public GameObject weapon;
    public GameObject ShootSpot;
    public NavMeshAgent enemy;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        attackDelay += Time.deltaTime;

        int randomBehaviour = 0;

        float distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if (timer >= 3)
        {
            randomBehaviour = (int)Random.Range(1f, 7f);
            timer = 0;
        }

        if (distanceFromPlayer < 2.0f && attackDelay > 4f)
        {
            GameObject attack = Instantiate(weapon, new Vector3(ShootSpot.transform.position.x, ShootSpot.transform.position.y, ShootSpot.transform.position.z), ShootSpot.transform.rotation, this.transform);
            attack.GetComponent<Projectile>().SetSpeed(0f);
            attack.GetComponent<Projectile>().enableDestroyOnHit(false);
            attackDelay = 0f;
        }

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
    }
}
