using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownSkier : MonoBehaviour
{
    SkiMovement skiMovement;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            skiMovement = other.GetComponent<SkiMovement>();
            skiMovement.speed = 0;
            skiMovement.turnTorque = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            skiMovement.speed = skiMovement.startSpeed;
            skiMovement.turnTorque = skiMovement.maxTurnTorque;
        }
    }
}
