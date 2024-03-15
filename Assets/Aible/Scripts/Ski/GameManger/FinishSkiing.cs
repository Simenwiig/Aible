using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Currency;

public class FinishSkiing : MonoBehaviour
{
    [SerializeField] private SkiMovement skiMovement;
    [SerializeField] private Canvas finishCanvas;

    private bool hasFinished;

    private bool startSlowdown;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && !hasFinished)
        {
            hasFinished = true;
            StartCoroutine(FinishSki());
        }   
    }

    IEnumerator FinishSki()
    {
        SkiAnimation.PlayAnimation("Braking");

        startSlowdown = true;
        skiMovement.canUseInput = false;
        skiMovement.canAnimate = false;
        skiMovement.rLayerWeight = SkiAnimation.GetLayerWieght(1);
        skiMovement.lLayerWeight = SkiAnimation.GetLayerWieght(2);

        yield return new WaitForSeconds(1.5f);

        SkiAnimation.PlayAnimation("Winning");
        skiMovement.rb.isKinematic = true;

        skiMovement.canMove = false;

        yield return new WaitForSeconds(0.7f);

        finishCanvas.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;

        Currency.Currency.AddCoinsToCurrency();
    }

    private void Update()
    {
        if (startSlowdown && skiMovement != null)
        {
            skiMovement.SlowDown();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManger.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}