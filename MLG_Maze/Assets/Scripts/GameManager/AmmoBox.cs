using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //Add ammo to player pocket and despawn the box
        AmmoDisplay.ammoNumber = AmmoDisplay.ammoNumber + 10;
        Destroy(gameObject);
    }
}
