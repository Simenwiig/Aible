using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSettinngs", menuName = "ScriptableObjects/Item Settinngs")]
public class ItemSettinngs : ScriptableObject
{
    public bool CanItemFallDown;
    public float TimeBetweenSpawns = 5f;
    public float TimeBeforeAppleFallDown = 5f;
    public Vector2 XItemLocation = new Vector2(3.0f, 4.2f);
    public Vector2 YItemLocation = new Vector2(4.5f, 6.0f);
}
