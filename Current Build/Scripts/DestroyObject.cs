using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject: MonoBehaviour
{
    public float timer = 0;
    private float timeCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount > timer)
        {
            Destroy(this.gameObject);
        }
    }

    public void setTimer(float timer)
    {
        this.timer = timer;
    }
}
