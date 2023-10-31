using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchAI : MonoBehaviour
{
    private float timer = 0f;
    private float attackDelay = 0f;
    public GameObject weapon;
    public GameObject ShootSpot;
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

        Vector3 look = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
      
        this.transform.LookAt(look);

        if (attackDelay > 3f)
        {
            Attack();
            attackDelay = 0f;
        }

        //Finds a random spot around the player for the witch to spawn every 10 seconds
        if (timer > 10f)
        {
            float randomX = Random.Range(-1f, 1f) * Random.Range(4f, 7f);

            float randomZ = Random.Range(-1f, 1f) * Random.Range(4f, 7f);

            //Debug.Log(randomX + " " + randomZ); 

            playerVector = new Vector3(player.transform.position.x + randomX, player.transform.position.y, player.transform.position.z + randomZ);

            // checks if the witch can spawn in a spot without an obstacle or another enemy, also the timer is set to every 10 seconds for the teleport
            if (Physics.Raycast(player.transform.position, playerVector) == false && timer > 10f)
            {
                this.transform.position = playerVector;
                timer = 0;
            }
        }      
    }

    public void Attack()
    {
        Instantiate(weapon, new Vector3(ShootSpot.transform.position.x, ShootSpot.transform.position.y, ShootSpot.transform.position.z), ShootSpot.transform.rotation);
    }
}
