using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public NavMeshAgent speed;
    // Start is called before the first frame update
    void Start()
    {
        //Getter character controller + speed on vanmeshagent
        controller = gameObject.GetComponent<CharacterController>();
        speed = gameObject.GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //get unity default input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        //Cant moove while on pause
        if (!PauseMenu.isPaused)
        {
            //Movement vector calcul
            Vector3 move = transform.right * x + transform.forward * z;
            //Movement vector applied
            controller.Move((move * speed.speed) * Time.deltaTime);
        }
    } 
}
  

