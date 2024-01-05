using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSkiing : MonoBehaviour
{
    [SerializeField] private SkiMovement skiMovement;

    private bool hasFinished;

    private bool startSlowdown;

    private void OnTriggerEnter(Collider other)
    {
        if (hasFinished)
            return;

        StartCoroutine(FinishSki());
    }

    IEnumerator FinishSki()
    {
        SkiAnimation.PlayAnimation("Braking");

        startSlowdown = true;
        skiMovement.canUseInput = false;
        skiMovement.canAnimate = false;

        yield return new WaitForSeconds(1f);

        SkiAnimation.PlayAnimation("Winning");
        skiMovement.rb.isKinematic = true;

        skiMovement.canMove = false;
    }

    private void Update()
    {
        if (startSlowdown && skiMovement != null)
        {
            skiMovement.SlowDown();
        }
    }
}
