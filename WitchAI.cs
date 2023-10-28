using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchAI : MonoBehaviour
{
    private float timer = 0f;
    private float attackDelay = 0f;
    public NavMeshAgent enemy;
    public GameObject weapon;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerVector = player.transform.position;
        timer += Time.deltaTime;
        attackDelay += Time.deltaTime;

        // fix this later you goober, he still rotates around when he shouldn't, just don't use look at
        /*
        Vector3 look = Quaternion.LookRotation(new Vector3(player.transform.position.x, 1, player.transform.position.z), Vector3.up).eulerAngles;
        this.transform.localRotation = Quaternion.Euler(look.x, 0, 0);
        */
        this.transform.LookAt(player.transform.position);

        if (attackDelay > 3f)
        {
            attackDelay = 0f;
        }

        //Finds a random spot around the player for the witch to spawn every 10 seconds
        if (timer > 10f)
        {
            playerVector = new Vector3(player.transform.position.x + Random.Range(-7f, 7f), player.transform.position.y, player.transform.position.z + Random.Range(-7f, 7f));
        }

        // checks if the witch can spawn in a spot without an obstacle or another enemy, also the timer is set to every 10 seconds for the teleport
        if (Physics.Raycast(player.transform.position, playerVector) == false && timer > 10f)
        {
            this.transform.position = playerVector;
            timer = 0;
        }
    }

    public void Attack()
    {
        Instantiate(weapon, this.transform.position, this.transform.rotation);
    }
}
