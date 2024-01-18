using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    bool hasHit;

    Vector3 playerPos;

    SkiMovement skiMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && !hasHit)
        {
            hasHit = true;

            skiMovement = other.GetComponent<SkiMovement>();
            PlayerFall();
        }
        else if(other.transform.tag == "PlayerBodyPart" && !hasHit)
        {
            hasHit = true;

            skiMovement = other.GetComponentInParent<SkiMovement>();
            PlayerFall();
        }

    }

    private void PlayerFall()
    {
        skiMovement.canMove = false;
        skiMovement.rb.isKinematic = true;

        playerPos = skiMovement.transform.position;

        SkiAnimation.PlayAnimation("Fall");

        StartCoroutine(MoveTo.Position(skiMovement.transform,
            new Vector3(skiMovement.transform.position.x, playerPos.y, skiMovement.transform.position.z), 0.4f, 2.167f + 1.1f));

        StartCoroutine(StandUp());
    }


    private IEnumerator StandUp()
    {
        yield return new WaitForSeconds(0.4f + 2.167f + 1.2f);

        skiMovement.rb.isKinematic = false;
        skiMovement.canMove = true;
    }
}
