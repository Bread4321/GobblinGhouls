using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1.0f;
    public float timeDelay = 1f;
    public GameObject Weapon;
    private float timer = 0f;
    public GameObject ShootSpot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);

        timer += Time.deltaTime;
        if (Input.GetAxis("Fire1") == 1.0f && timer > timeDelay)
        {
            Invoke("Fire", 0);
            timer = 0;
        }
    }

    private void Fire()
    {
        Instantiate(Weapon, new Vector3(ShootSpot.transform.position.x, ShootSpot.transform.position.y, ShootSpot.transform.position.z), transform.rotation);
    }
}
