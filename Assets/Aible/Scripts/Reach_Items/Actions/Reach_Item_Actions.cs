using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Reach_Item_Actions
{
    public static Action ItemReached;
    public static Action<Difficulty> SetDifficulty;
    public static Action<Difficulty> ChangeDifficulty;
    public static Action<GameObject> ReleaseItem;
    public static Action<Insect> ReleaseMob;
}
