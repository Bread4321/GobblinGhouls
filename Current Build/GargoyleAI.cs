using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GargoyleAI: MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        int randomBehaviour = 0;

        //float distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if (timer >= 3)
        {
            randomBehaviour = (int)Random.Range(1f, 7f);
            timer = 0;
        }

        
    }
}
