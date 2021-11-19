using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    public AudioSource collectSound;

    void OnTriggerEnter(Collider other)
    {
        //Add a key to player inventory and despawn the key
       
        ScoringSystem.theScore += 1;
        Destroy(gameObject);
    }
}
