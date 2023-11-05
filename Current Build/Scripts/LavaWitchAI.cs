using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LavaWitchAI: MonoBehaviour
{
    private float timer = 0f;
    private float attackDelay = 3f;
    public NavMeshAgent enemy;
    public GameObject attackCast;
    public GameObject weapon;
    private IEnumerator coroutine;
    private bool activate = false;
    GameObject player;
    GameObject attackPoint = null;
    float x = 0f;
    float y = 0f;
    float z = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        timer = attackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        enemy.SetDestination(player.transform.position);
        float distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        
        if (distanceFromPlayer < 10f && timer > 8f)
        {      
            
            if (activate == false)
            {
                attackPoint = Instantiate(attackCast, new Vector3(player.transform.position.x, 0f, player.transform.position.z), Quaternion.identity);
                attackPoint.GetComponent<DestroyObject>().setTimer(attackDelay);
                activate = true;
                timer = 0;
            }                     
        }
        
        
        if (timer < attackDelay + 0.01f)
        {
            
            if (timer < attackDelay)
            {
                x = player.transform.position.x;
                y = player.transform.position.y - 0.45f;
                z = player.transform.position.z;
                attackPoint.transform.position = new Vector3(x, y, z);
            }
            else
            {               
                coroutine = Attack(x, y, z);
                StartCoroutine(coroutine);            
            }
            
            
        } 
        
        activate = false;
    }

    private IEnumerator Attack(float x, float y, float z)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject shockwave = Instantiate(weapon, new Vector3(x, y, z), Quaternion.identity); 
    }
}
