using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialisation : MonoBehaviour
{
    [SerializeField]
    GameObject Player = null;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Player, Vector3.zero, Quaternion.identity);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


    }

   

}