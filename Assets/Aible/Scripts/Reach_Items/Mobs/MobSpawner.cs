using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private MobSpawnerSettings[] _spawnerSettings;

    private List<GameObject> _mobs;

    private bool _canSpawnMobs;
    private float _randomTime;
    private float _timer;

    private void OnEnable()
    {
        Reach_Item_Actions.SetDifficulty += SetDifficulty;
    }

    private void OnDisable()
    {
        Reach_Item_Actions.SetDifficulty -= SetDifficulty;
    }

    private void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.D_Level_0:
                SetLevel(0);
                break;
            case Difficulty.D_Level_1:
                SetLevel(0);
                break;
            case Difficulty.D_Level_2:
                SetLevel(1);
                break;
            case Difficulty.D_Level_3:
                break;
            case Difficulty.D_Level_4:
                break;
            case Difficulty.D_Level_5:
                break;
        }
    }

    private void SetLevel(int level)
    {
        MobSpawnerSettings mobSpawnerSettings = _spawnerSettings[level];

        _canSpawnMobs = mobSpawnerSettings.CanSpawnMobs;
        _randomTime = Random.Range(mobSpawnerSettings.TimeBetweenSpawns.x, mobSpawnerSettings.TimeBetweenSpawns.y);

        if (mobSpawnerSettings.Mobs.Count < 0)
            return;

        _mobs.Clear();
        for (int i = 0; i < mobSpawnerSettings.Mobs.Count; i++)
        {
            _mobs.Add(mobSpawnerSettings.Mobs[i]);
        }
    }

    private void Update()
    {
        if (!_canSpawnMobs)
        {
            if (_timer > 0)
            {
                _timer = 0;
            }
            return;
        }

        if (_timer >= _randomTime)
        {
            _timer = 0;
            SpawnMobs();
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    public void SpawnMobs()
    {
        int randMobIndex = Random.Range(0, _mobs.Count);

        GameObject mob = _mobs[randMobIndex];

        Insect insect = mob.GetComponent<Insect>();

        if (insect.IsMoving)
        {
            _timer = _randomTime;
            return;
        }

        mob.SetActive(true);
        insect.startMovement = true;
    }
}
