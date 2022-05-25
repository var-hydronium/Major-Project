using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public AudioClip audio;
    public AudioSource source;
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
   public void hurt(int damage)
    {
        //source.PlayOneShot(audio);
        health -= damage;
        Debug.Log(health);
    }
}
