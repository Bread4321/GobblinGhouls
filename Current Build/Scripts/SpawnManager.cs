using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager: MonoBehaviour
{
    public GameObject[] Enemy;
    public int numEnemy = 0;
    private int tempEnemyCount = 0;
    private float timer = 0f;
    public float spawnDelay = 20f;
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnDelay;
        tempEnemyCount = numEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (tempEnemyCount > 0 && timer >= spawnDelay)
        {
            GameObject enemyObject = Enemy[(int)Random.Range(0f, Enemy.Length)];
            int randomX = Random.Range(-25, 25);
            int randomZ = Random.Range(-25, 25);
            if (Physics.OverlapSphere(new Vector3(randomX, 1, randomZ), 0.5f).Length != 0)
            {
                randomX = Random.Range(-25, 25);
                randomZ = Random.Range(-25, 25);
                //Debug.Log("no worky");
            }
            else
            {
                //Debug.Log(randomX + " - " + 1 + " - " + randomZ);
                Vector3 spawnPos = new Vector3(randomX, 1, randomZ);
                Instantiate(enemyObject, spawnPos, new Quaternion(0.0f, -1.0f, 0.0f, -1.0f));
                tempEnemyCount--;
                timer = 0;
            }
        }
    }
}
