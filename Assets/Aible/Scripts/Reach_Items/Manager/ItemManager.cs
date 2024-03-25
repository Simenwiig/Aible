using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class ItemManager : MonoBehaviour
{
    public static int NumberOfItemsCollected;
    public static ItemManager _ItemManager;

    static bool changeCoinAmount = true;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI itemCollectedText;

    [Header("Settings")]
    [SerializeField] private ItemSettinngs[] _itemSettings;

    [Header("Item Settings")]
    [SerializeField] private GameObject _item;
    [SerializeField] float _startAngle = 30f; // Starting angle in degrees
    [SerializeField] float _arcAngle = 120f; // Angle of the arc in degrees

    [HideInInspector] public bool CanItemFallDown;
    [HideInInspector] public float TimeBeforeAppleFallDown;

    private float _timeBetweenSpawns;
    private Vector2 _xItemLocation = new Vector2(3.0f, 4.2f);
    private Vector2 _yItemLocation = new Vector2(4.5f, 6.0f);

    private ObjectPool<GameObject> _itemPool;

    private float _timer;

    private void OnEnable()
    {
        Reach_Item_Actions.SetDifficulty += SetDifficulty;
        Reach_Item_Actions.ReleaseItem += RealeaseItem;
    }

    private void OnDisable()
    {
        Reach_Item_Actions.SetDifficulty -= SetDifficulty;
        Reach_Item_Actions.ReleaseItem -= RealeaseItem;
    }

    private void Awake()
    {
        _ItemManager = this;

        _itemPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(_item, this.transform);
        }, item =>
        {
            item.SetActive(true);
        }, item =>
        {
            item.SetActive(false);
        }, item =>
        {
            Destroy(item);
        }, false, 10, 50);
    }

    private void Start()
    {
        itemCollectedText.text = NumberOfItemsCollected.ToString();
        SpawnItem();
    }

    private void Update()
    {
        if (changeCoinAmount)
        {
            changeCoinAmount = false;
            itemCollectedText.text = NumberOfItemsCollected.ToString();


            if (NumberOfItemsCollected >= 50)
            {
                ChangeDifficulty(Difficulty.D_Level_5);
            }
            else if (NumberOfItemsCollected >= 40)
            {
                ChangeDifficulty(Difficulty.D_Level_4);
            }
            else if (NumberOfItemsCollected >= 30)
            {
                ChangeDifficulty(Difficulty.D_Level_3);
            }
            else if (NumberOfItemsCollected >= 20)
            {
                ChangeDifficulty(Difficulty.D_Level_2);
            }
            else if (NumberOfItemsCollected >= 10)
            {
                ChangeDifficulty(Difficulty.D_Level_1);
            }
            else
            {
                ChangeDifficulty(Difficulty.D_Level_0);
            }
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

    private void ChangeDifficulty(Difficulty difficulty)
    {
        Reach_Item_Actions.ChangeDifficulty(difficulty);
    }

    private void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.D_Level_0:
                SetLevel(0);
                break;
            case Difficulty.D_Level_1:
                SetLevel(1);
                break;
            case Difficulty.D_Level_2:
                SetLevel(2);
                break;
            case Difficulty.D_Level_3:
                SetLevel(3);
                break;
            case Difficulty.D_Level_4:
                break;
            case Difficulty.D_Level_5:
                break;
        }
    }

    private void SetLevel(int level)
    {
        ItemSettinngs itemSettinngs = _itemSettings[level];

        CanItemFallDown = itemSettinngs.CanItemFallDown;
        _timeBetweenSpawns = itemSettinngs.TimeBetweenSpawns;
        TimeBeforeAppleFallDown = itemSettinngs.TimeBeforeAppleFallDown;
        _xItemLocation = itemSettinngs.XItemLocation;
        _yItemLocation = itemSettinngs.YItemLocation;
    }

    public static void AddItem(int item)
    {
        NumberOfItemsCollected += item;
        changeCoinAmount = true;
    }

    private void SpawnItem()
    {
        float startAngleRad = Mathf.Deg2Rad * _startAngle;
        float arcAngleRad = Mathf.Deg2Rad * _arcAngle;

        float angle = Random.Range(startAngleRad, startAngleRad + arcAngleRad);

        // Calculate x and y positions using the generated angle
        float xPos = Random.Range(_xItemLocation.x, _xItemLocation.y) * Mathf.Cos(angle);
        float yPos = Random.Range(_yItemLocation.x, _yItemLocation.y) * Mathf.Sin(angle);

        // Apply offset to the spawn position
        Vector3 spawnPosition = new Vector3(xPos, yPos, 0) + transform.position;

        float yRot = Random.Range(0, 360);

        GameObject item = _itemPool.Get();
        item.transform.position = spawnPosition;
        item.transform.rotation = Quaternion.Euler(0, yRot, 0);
    }

    public void RealeaseItem(GameObject item)
    {
        _itemPool.Release(item);  
    }
}
