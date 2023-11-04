using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MagicSkeletonAI: MonoBehaviour
{
    private float timer = 0f;
    private float attackDelay = 0f;
    public GameObject weapon;
    public GameObject weaponCast;
    public GameObject ShootSpot;
    public NavMeshAgent enemy;
    private IEnumerator coroutine;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        timer = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerVector = player.transform.position;
        float distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        timer += Time.deltaTime;
        attackDelay += Time.deltaTime;

        // fix this later you goober, he still rotates around when he shouldn't, just don't use look at, I fixed it

        Vector3 look = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);

        this.transform.LookAt(look);

        // This should work like it finds the position of the player after 4 seconds and start the attack, then should spawn the attack where they were at when initially called
        // it's like a delayed attack that only punishes a lack of movement
        if (attackDelay > 3f)
        {
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = player.transform.position.z;

            coroutine = Attack(x, y, z);
            StartCoroutine(coroutine);
            //Debug.Log("start of attack");
            attackDelay = 0f;
        }

        // note for later but the purpose is to keep the skeleton man within 10f of the player, currently it actually goes the players position at the time when 
        // the skeleton is 10f away rather than keeping a 10f distance at most times. Now he does that like intended
        if (distanceFromPlayer > 10f)
        {
            enemy.SetDestination(player.transform.position);
        }

        if (distanceFromPlayer <= 10f) 
        {
            enemy.SetDestination(this.transform.position);
        }      
    }

    // I want to note that I want the skeleton to summon a small circle under the player to attack him 
    private IEnumerator Attack(float x, float y, float z)
    {
        Instantiate(weaponCast, new Vector3(x, 0f, z), this.transform.rotation);
        yield return new WaitForSeconds(2);
        // fix the angles. They work now
        //Debug.Log("end of attack");
        Instantiate(weapon, new Vector3(x, y + 5f, z), new Quaternion(-0.707f, 0, 0, -0.707f));
    }
}
