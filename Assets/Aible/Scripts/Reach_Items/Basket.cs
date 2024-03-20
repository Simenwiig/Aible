using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] GameObject[] _apples;

    private int _appleIndex = -1;

    public void AddApple()
    {
        _appleIndex++;

        if (_appleIndex < _apples.Length)
        {
            _apples[_appleIndex].SetActive(true);
        }
    }
}
