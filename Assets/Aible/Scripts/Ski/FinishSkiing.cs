using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Currency;

public class FinishSkiing : MonoBehaviour
{
    [SerializeField] private SkiMovement skiMovement;
    [SerializeField] private Canvas finishCanvas;

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

        yield return new WaitForSeconds(1.5f);

        SkiAnimation.PlayAnimation("Winning");
        skiMovement.rb.isKinematic = true;

        skiMovement.canMove = false;

        finishCanvas.gameObject.SetActive(true);

        Currency.Currency.AddCoinsToCurrency();
    }

    private void Update()
    {
        if (startSlowdown && skiMovement != null)
        {
            skiMovement.SlowDown();
        }
    }
}
