using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.IMGUI.Controls;

public class ItemManager : MonoBehaviour
{
    public static int NumberOfItemsCollected;
    [Header("References")]
    [SerializeField] private TextMeshProUGUI itemCollectedText;

    [Header("Item Settings")]
    [SerializeField] private GameObject _item;
    [SerializeField] private float _timeBetweenSpawns = 3f;
    public bool CanItemFallDown;
    public float TimeBeforeAppleFallDown = 5f;

    [SerializeField] float startAngle = 30f; // Starting angle in degrees
    [SerializeField] float arcAngle = 120f; // Angle of the arc in degrees

    static bool changeCoinAmount = true;

    public static ItemManager _ItemManager;

    private float _timer;

    private void Awake()
    {
        _ItemManager = this;
    }

    private void Start()
    {
        NumberOfItemsCollected = 0;
        SpawnItem();
    }

    private void Update()
    {
        if (changeCoinAmount)
        {
            changeCoinAmount = false;
            itemCollectedText.text = NumberOfItemsCollected.ToString();
        }

        if(_timer >= _timeBetweenSpawns)
        {
            _timer = 0;
            SpawnItem();
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    public static void AddItem(int item)
    {
        NumberOfItemsCollected += item;
        changeCoinAmount = true;
    }

    private void SpawnItem()
    {
        float startAngleRad = Mathf.Deg2Rad * startAngle;
        float arcAngleRad = Mathf.Deg2Rad * arcAngle;

        float angle = Random.Range(startAngleRad, startAngleRad + arcAngleRad);

        // Calculate x and z positions using the generated angle
        float xPos = Random.Range(3.0f, 4.2f) * Mathf.Cos(angle);
        float yPos = Random.Range(4.5f, 6.0f) * Mathf.Sin(angle);

        // Apply offset to the spawn position
        Vector3 spawnPosition = new Vector3(xPos, yPos, 0) + transform.position;

        float yRot = Random.Range(0, 360);

        GameObject item = GameObject.Instantiate(_item, spawnPosition, Quaternion.Euler(0, yRot, 0),this.transform) as GameObject;
    }
}
