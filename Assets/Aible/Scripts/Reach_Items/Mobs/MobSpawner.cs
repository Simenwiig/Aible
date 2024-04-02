using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MobSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MobSpawnPoint _spawnPoint;
    [SerializeField] private Basket _mobTarget;
    [Header("Settings")]
    [SerializeField] private MobSpawnerSettings[] _spawnerSettings;

    MobSpawnerSettings _mobSpawnerSettings;

    [SerializeField] private List<GameObject> _mobs;

    private ObjectPool<GameObject> _flyPool;
    private ObjectPool<GameObject> _beePool;

    private bool _canSpawnMobs;
    private float _randomTime;
    private float _timer;

    private void OnEnable()
    {
        Reach_Item_Actions.SetDifficulty += SetDifficulty;
        Reach_Item_Actions.ReleaseMob += ReleaseMob;
    }

    private void OnDisable()
    {
        Reach_Item_Actions.SetDifficulty -= SetDifficulty;
        Reach_Item_Actions.ReleaseMob -= ReleaseMob;
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
                SetLevel(0);
                break;
            case Difficulty.D_Level_3:
                SetLevel(1);
                break;
            case Difficulty.D_Level_4:
                SetLevel(2);
                break;
            case Difficulty.D_Level_5:
                SetLevel(3);
                break;
        }
    }

    private void SetLevel(int level)
    {
        _mobSpawnerSettings = _spawnerSettings[level];

        _canSpawnMobs = _mobSpawnerSettings.CanSpawnMobs;
        _randomTime = Random.Range(_mobSpawnerSettings.TimeBetweenSpawns.x, _mobSpawnerSettings.TimeBetweenSpawns.y);

        if(_mobSpawnerSettings.MobTypes.Count > 0)
        {
            ActivateObjectPools(_mobSpawnerSettings);
            SpawnMobs(_mobSpawnerSettings.MobTypes.Count - 1);
        }
    }

    private void ActivateObjectPools(MobSpawnerSettings mobSpawnerSettings)
    {    
        for (int i = 0; i < mobSpawnerSettings.MobTypes.Count; i++)
        {
            switch (mobSpawnerSettings.MobTypes[i])
            {
                case MobType.MT_Fly:
                    if(_flyPool == null)
                    {
                        _flyPool = new ObjectPool<GameObject>(() =>
                        {
                            return Instantiate(_mobs[0], this.transform);
                        }, mob =>
                        {
                            mob.SetActive(true);
                        }, mob =>
                        {
                            mob.SetActive(false);
                        }, mob =>
                        {
                            Destroy(mob);
                        }, false, 6, 10);
                        Debug.LogWarning("CreateFlyPool");
                    }
                    break;
                case MobType.MT_Bee:
                    if (_beePool == null)
                    {
                        _beePool = new ObjectPool<GameObject>(() =>
                        {
                            return Instantiate(_mobs[1], this.transform);
                        }, mob =>
                        {
                            mob.SetActive(true);
                        }, mob =>
                        {
                            mob.SetActive(false);
                        }, mob =>
                        {
                            Destroy(mob);
                        }, false, 6, 10);
                        Debug.LogWarning("CreateBeePool");
                    }
                    break;
            }
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

    private void ReleaseMob(Insect mob)
    {
        switch (mob.MobType)
        {
            case MobType.MT_Fly:
                _flyPool.Release(mob.gameObject);
                break;
            case MobType.MT_Bee:
                _beePool.Release(mob.gameObject);
                break;
        }  
    }

    private void SpawnMobs(int mobIndex = default)
    {
        GameObject mob;
        MobType mobType;

        if (mobIndex == default)
        {
            int rndIndex = Random.Range(0, _mobSpawnerSettings.MobTypes.Count);
            mobType = _mobSpawnerSettings.MobTypes[rndIndex];
        }
        else
        {
            mobType = _mobSpawnerSettings.MobTypes[mobIndex];
        }

        switch (mobType)
        {
            case MobType.MT_Fly:
                mob = _flyPool.Get();
                break;
            case MobType.MT_Bee:
                mob = _beePool.Get();
                break;
            default:
                return;
        }

        Insect insect = mob.GetComponent<Insect>();
        insect._basket = _mobTarget;

        float randomY = Random.Range(_spawnPoint.SpawnPoints[0].position.y, _spawnPoint.SpawnPoints[1].position.y);
        float x;

        int rnd = Random.Range(0, 2);

        if (rnd == 0)
        {
            x = _spawnPoint.SpawnPoints[0].position.x;
        }
        else
        {
            x = -_spawnPoint.SpawnPoints[0].position.x;
        }

        mob.transform.position = new Vector3(x, randomY, _spawnPoint.SpawnPoints[0].position.z);

        if (insect.IsMoving)
        {
            _timer = _randomTime;
            return;
        }

        insect.startMovement = true;
    }
}
