using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    D_Level_0,
    D_Level_1,
    D_Level_2,
    D_Level_3,
    D_Level_4,
    D_Level_5,
}

public class DifficultyManager : MonoBehaviour
{
    private Difficulty _difficulty;

    private void OnEnable()
    {
        Reach_Item_Actions.ChangeDifficulty += ChangeDifficulty;
    }

    private void OnDisable()
    {
        Reach_Item_Actions.ChangeDifficulty -= ChangeDifficulty;
    }

    private Difficulty Difficulty
    {
        get => _difficulty;
        set
        {
            _difficulty = value;
            Reach_Item_Actions.SetDifficulty(_difficulty);
            Debug.Log(_difficulty);
        }
    }

    [SerializeField] private Difficulty _startDifficulty = Difficulty.D_Level_0;

    private void Start()
    {
        Difficulty = _startDifficulty;
    }

    private void ChangeDifficulty(Difficulty difficulty)
    {
        if (Difficulty != difficulty)
            Difficulty = difficulty;
    }
}
