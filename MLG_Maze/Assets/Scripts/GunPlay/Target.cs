using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    
    //Update life og the gameobject with dmg taken
    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    }
    //Die = anihilated from the scene
    void Die()
    {
        Destroy(gameObject);
    }
}
