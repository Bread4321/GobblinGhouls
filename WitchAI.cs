using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchAI : MonoBehaviour
{
    private float timer = 0f;
    public NavMeshAgent enemy;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //enemy = this.NavMeshAgent;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerVector = player.transform.position;
        timer += Time.deltaTime;
        if (timer > 5f)
        {
            playerVector = new Vector3(player.transform.position.x + Random.Range(-5f, 5f), player.transform.position.y, player.transform.position.z + Random.Range(-5f, 5f));
        }
        if (Physics.Raycast(player.transform.position, playerVector) == false && timer > 5f)
        {
            //Debug.Log("spawn");
            this.transform.Translate(playerVector);
            timer = 0;
        }
    }
}
