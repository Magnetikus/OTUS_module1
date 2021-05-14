using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootRun : MonoBehaviour
{
    public AudioSource audioSource;
    Character character;
    int state;
    
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        character = GetComponentInParent<Character>(); 
    }

    public void SoundStart ()
    {
        audioSource.Play();
    }

    public void SoundStop ()
    {
        audioSource.Stop();
    }

    private void Update()
    {
        state = (int)character.state;
        if (state == 1 || state == 2) Debug.Log("audioStap");  //audioSource.Play();
        else audioSource.Stop();
    }
}
