using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartSkiing : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private SkiMovement skiMovement;
    [SerializeField] private Canvas startCanvas;
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private TextMeshProUGUI startCounterText;

    [Header("Settings")]
    [SerializeField] bool dontStart;
    [SerializeField] bool instantlyStart;
    [SerializeField] private int startCounter = 3;

    [Header("MoveStartStick")]
    [SerializeField] private Transform startStick;
    [SerializeField] private Transform stickEndPoint;



    private void Start()
    {
        startCanvas.gameObject.SetActive(true);
        StartCoroutine(StartSki());
    }

    IEnumerator StartSki()
    {
        if (dontStart)
        {
            startCanvas.gameObject.SetActive(false);
            yield break;
        }

        if (!instantlyStart)
        {
            int counter = startCounter;
            startCounterText.text = counter.ToString();

            for (int i = 0; i < startCounter; i++)
            {
                startCounterText.text = counter.ToString();
                yield return new WaitForSeconds(1);
                counter--;
            }
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
        }

        startCanvas.gameObject.SetActive(false);

        StartCoroutine(skiMovement.StartMovement());
        moveStartStick();
    }

    void moveStartStick()
    {
        StartCoroutine(MoveTo.Position(startStick, stickEndPoint.position, 0.1f));
        StartCoroutine(MoveTo.Rotation(startStick, stickEndPoint.rotation, 0.1f));
    }
}
