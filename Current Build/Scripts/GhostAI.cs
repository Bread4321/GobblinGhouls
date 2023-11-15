using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI: MonoBehaviour
{
    private float timer = 0f;
    public UnityEngine.AI.NavMeshAgent enemy;
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
        enemy.SetDestination(player.transform.position);
        Vector3 playerVector;

        if (timer > 5f)
        {
            playerVector = new Vector3(player.transform.position.x + Random.Range(-7f, 7f), 1, player.transform.position.z + Random.Range(-7f, 7f));

            if (Physics.Raycast(player.transform.position, playerVector) == false)
            {
                this.transform.position = playerVector;
                timer = 0;
            }
        }
    }
}
