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

    public Action SetDificulty;

    public Difficulty Difficulty
    {
        get => _difficulty;
        set
        {
            _difficulty = value;
            Reach_Item_Actions.SetDifficulty(_difficulty);
        }
    }

    [SerializeField] private Difficulty _startDifficulty = Difficulty.D_Level_0;

    private void Start()
    {
        Difficulty = _startDifficulty;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Difficulty = Difficulty.D_Level_3;
        }
    }
}
