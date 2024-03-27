using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Canvas _startCanvas;
    [SerializeField] private TextMeshProUGUI _startCounterText;
    [SerializeField] private int _startCounter = 3;

    bool _allreadyStarted;

    private void Start()
    {        
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameScreen());
    }

    public IEnumerator StartGameScreen()
    {
        if (_allreadyStarted)
            yield break;

        _allreadyStarted = true;
        _startCanvas.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;

        int counter = _startCounter;
        _startCounterText.text = counter.ToString();

        for (int i = 0; i < _startCounter; i++)
        {
            _startCounterText.text = counter.ToString();
            yield return new WaitForSeconds(1);
            counter--;
        }

        _startCanvas.gameObject.SetActive(false);
        _allreadyStarted = false;
    }
}
