using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GargoyleAI: MonoBehaviour
{
    // timers
    private float timer = 0f;
    private int behaviourDelay = 3;

    // various prefabs and components used for attacks
    public GameObject weapon1;
    public GameObject attackCast;
    public GameObject ShootSpot;
    public GameObject SummonTarget;
    public NavMeshAgent enemy;
    private IEnumerator behaviour;
    private GameObject player;

    // some control variables
    private int behaviourCount = 0;
    private int busy = 1;
    // busy == 1 means it is not busy and does not stop movement
    // busy == 2 means it is busy and stops movement
    // busy == 3 means it is busy but does not stop movement

    // used for sound effects
    private AudioSource soundEffect;
    public AudioClip roar;
    public AudioClip fireball1;
    public AudioClip fireball2;
    public AudioClip summonNoise;
    

    // Start is called before the first frame update
    void Start()
    {
        soundEffect = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player"); 
    }

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        int randomBehaviour = 0;

        if (busy == 1)
        {
            enemy.isStopped = false;
            enemy.SetDestination(player.transform.position);
        } 
        else if (busy == 2)
        {
            this.transform.LookAt(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));
            enemy.isStopped = true;
        }
        else if (busy == 3) 
        {
            enemy.isStopped = false;
        }

        if (timer >= behaviourDelay && behaviourCount <= 5 && busy == 1)
        {
            // Will randomly select one of the 5 behaviours for the boss
            randomBehaviour = (int)Random.Range(2f, 3f);
            // Will random choose the delay between each behaviour
            behaviourDelay = (int)Random.Range(2f, 5f);
            behaviourCount++;
            busy = 2;
        }

        if (behaviourCount <= 4)
        {
            switch (randomBehaviour)
            {
                case 1:
                    // shoots fireballs at player
                    behaviour = Behaviour1(0.5f, 3);
                    StartCoroutine(behaviour);
                    break;

                case 2:
                    // charge on player then shoots a fireball           
                    behaviour = Behaviour2(4f);
                    StartCoroutine(behaviour);
                    break;

                case 3:
                    // fire shower for some amount of time idk
                    behaviour = Behaviour3(1f, 10);
                    StartCoroutine(behaviour);
                    break;
                case 4:
                    // summon some silly skeletons
                    behaviour = Behaviour4(2f, 5);
                    StartCoroutine(behaviour);
                    break;
            }
        } 
        else
        {
            // will activate after a certain number of behaviours have passed
            behaviour = Behaviour5(5f);
            StartCoroutine(behaviour);
        }   
    }

    private IEnumerator Behaviour1(float delay, int shotCount)
    {
        // this part spawns an indicator to where the fireball will go 
        float x = player.transform.position.x;
        float z = player.transform.position.z; 
        Instantiate(attackCast, new Vector3(x, 0f, z), player.transform.rotation);
        attackCast.GetComponent<DestroyObject>().setTimer(delay / 2f);

        yield return new WaitForSeconds(delay);

        // this part shoots the fireball
        GameObject projectile = Instantiate(weapon1, new Vector3(ShootSpot.transform.position.x, ShootSpot.transform.position.y, ShootSpot.transform.position.z), ShootSpot.transform.rotation);
        projectile.GetComponent<Projectile>().SetSpeed(40f);
        projectile.GetComponent<Projectile>().setDestroyDelay(2f);
        projectile.transform.LookAt(new Vector3(x, 1f, z));

        // this grabs a random fireball sound effect to use
        AudioClip randomSound = null;
        if ((int)Random.Range(1f, 3f) == 1)
        {
            randomSound = fireball1;
        }
        else
        {
            randomSound = fireball2;
        }
        soundEffect.PlayOneShot(randomSound, 1f);
        if (shotCount > 1)
        {
            // this is will the rest of the remaining fireballs

            behaviour = Behaviour1(delay, shotCount - 1);
            StartCoroutine(behaviour);
        }
        else
        {
            // the end of the behaviour when there are no more fireballs to shoot
            timer = 0f;
            busy = 1;
        }      
    }
    
    private IEnumerator Behaviour2(float delay)
    {
        // a sort of indicator of this behaviour 
        soundEffect.PlayOneShot(roar, 1F);

        yield return new WaitForSeconds(delay);

        // shows where exactly the dash will go to
        GameObject indicator = Instantiate(attackCast, new Vector3(player.transform.position.x, 0f, player.transform.position.z), ShootSpot.transform.rotation);
        indicator.GetComponent<DestroyObject>().setTimer(delay * 0.5f);

        // sends the dash in the direction of the indicator
        busy = 3;
        float newSpeed = 50f;
        float initialAcceleration = enemy.acceleration;
        enemy.acceleration = newSpeed;
        enemy.speed = newSpeed;       
        enemy.SetDestination(indicator.transform.position);

        // will calculate the rough amount of time to the indicator
        float timeToLocation = Vector3.Distance(indicator.transform.position, enemy.transform.position) / (newSpeed * 0.5f);

        yield return new WaitForSeconds(timeToLocation);

        // returns the speed to normal and shoots a quick fireball
        //his.transform.LookAt(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z)); He should turn to face the player after the charge but it doesn't work right now
        enemy.speed = 2f;
        enemy.acceleration = initialAcceleration;
        enemy.isStopped = true;
        behaviour = Behaviour1(0.5f, 1);
        StartCoroutine(behaviour);
    }

    private IEnumerator Behaviour3(float delay, int shotCount)
    {
        int numOfProjectiles = 10;

        float[] x = new float[numOfProjectiles];
        float[] y = new float[numOfProjectiles];
        float[] z = new float[numOfProjectiles];

        for (int i = 0; i < numOfProjectiles; i++) 
        {

            if (i == 0) // always ensures one fireball is sent to the players most recent location
            {
                x[i] = player.transform.position.x;
                y[i] = player.transform.position.y;
                z[i] = player.transform.position.z;
            } 
            else // these are random set around the player 
            {
                x[i] = player.transform.position.x + Random.Range(-5f, 5f);
                y[i] = player.transform.position.y + Random.Range(2f, 5f);
                z[i] = player.transform.position.z + Random.Range(-5f, 5f);
            }
            
            Instantiate(attackCast, new Vector3(x[i], 0f, z[i]), this.transform.rotation);
            attackCast.GetComponent<DestroyObject>().setTimer(delay + 1f);
        }
        
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < numOfProjectiles; i++) 
        { 
            GameObject projectile = Instantiate(weapon1, new Vector3(x[i], y[i] + 5f, z[i]), new Quaternion(-0.707f, 0, 0, -0.707f));
            projectile.GetComponent<Projectile>().SetSpeed(10f);
            projectile.GetComponent<Projectile>().setDestroyDelay(delay);

            // this grabs a random fireball sound effect to use
            AudioClip randomSound = null;          
            if ((int)Random.Range(1f, 3f) == 1)
            {
                randomSound = fireball1;
            }
            else
            {
                randomSound = fireball2;
            }
            soundEffect.PlayOneShot(randomSound, 0.7f);
        }
        

        if (shotCount > 1)
        {
            // will fire the remaining waves of projectiles
            behaviour = Behaviour3(delay, shotCount - 1);
            StartCoroutine(behaviour);
        }
        else
        {
            // will end the behaviour when there are no more waves of projectiles left
            timer = 0f;
            busy = 1;

        }
    }

    private IEnumerator Behaviour4(float delay, int summonCount)
    {
        
        yield return new WaitForSeconds(delay);

        // noise indicator of this behaviour
        soundEffect.PlayOneShot(summonNoise, 1f);

        // instantiates a skeleton with double the health of the normal skeleton at a random point in front of the gargoyle
        GameObject target = Instantiate(SummonTarget, new Vector3(enemy.transform.position.x + Random.Range(-2f, 2f), 1f, enemy.transform.position.z - 3f), enemy.transform.rotation);
        float newHealth = target.GetComponent<EnemyBase>().GetHealth() * 2f;
        target.GetComponent<EnemyBase>().SetHealth(newHealth);

        if (summonCount > 1)
        {
            // summons the rest of the skeletons
            behaviour = Behaviour4(delay, summonCount - 1);
            StartCoroutine(behaviour);
        }
        else
        {
            // ends the behaviour when there are no more skeletons to spawn
            timer = 0f;
            busy = 1;
        }
    }

    private IEnumerator Behaviour5(float delay)
    {
        // this is just to stop the boss for a little bit to give the player a break, then continue the next set of random behaviours
        yield return new WaitForSeconds(delay);
        behaviourCount = 0;
        timer = 0f;
        busy = 1;
    }
}
