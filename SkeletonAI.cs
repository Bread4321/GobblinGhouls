using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAI : MonoBehaviour
{
    private float timer = 0f;
    public NavMeshAgent enemy; 
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.transform.position);
        // note for later, create a nav mesh in the other tester and in the thingy that we use at school I think, you go to window and then AI then select navigation then bake
    }
}
