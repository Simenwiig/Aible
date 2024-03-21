using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public bool CanSpawnMobs;
    public Vector2 TimeBetweenSpawns = new Vector2(5f, 7f);

    [SerializeField] GameObject[] _mobs;

    private float _randomTime;
    private float _timer;

    private void Start()
    {
        _randomTime = Random.Range(TimeBetweenSpawns.x, TimeBetweenSpawns.y);
    }

    private void Update()
    {
        if (!CanSpawnMobs)
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
            _randomTime = Random.Range(TimeBetweenSpawns.x, TimeBetweenSpawns.y);
            SpawnMobs();
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    public void SpawnMobs()
    {
        int randMobIndex = Random.Range(0, _mobs.Length);

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
