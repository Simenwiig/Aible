using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobSpawnerSettings", menuName = "ScriptableObjects/Mob Spawner Settings")]
public class MobSpawnerSettings : ScriptableObject
{
    public bool CanSpawnMobs;
    public Vector2 TimeBetweenSpawns = new Vector2(10, 12);
    public List<MobType> MobTypes;
}
