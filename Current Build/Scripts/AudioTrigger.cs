using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{

    private GameObject Audio;
    public int startDelay = 0;
    public AudioClip newMusic;
    public bool playMusic;
    public bool stopMusic;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GameObject.FindWithTag("Audio");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (newMusic != null)
        {
            Audio.GetComponent<AudioSource>().clip = newMusic;
        }

        if (other.tag == "Player") 
        {
            if (playMusic == true && stopMusic == true)
            {
                Debug.Log("playMusic and stopMusic are set to true, cannot do both");               
            } 
            else if (stopMusic == true) 
            {
                StartCoroutine(StopMusic());
            }
            else if (playMusic == true)
            {
                StartCoroutine(PlayMusic());
            }           
            else
            {
                Debug.Log("Neither playMusic and stopMusic are set, set one or the other to true");
            }
        }
    }

    private IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(startDelay);
        Audio.GetComponent<AudioSource>().Play();
        Destroy(this.gameObject);
    }

    private IEnumerator StopMusic()
    {
        yield return new WaitForSeconds(startDelay);
        Audio.GetComponent<AudioSource>().Stop();
        Destroy(this.gameObject);
    }
}
