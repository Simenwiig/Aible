using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public GameObject[] _apples;

    private int AppleIndex = -1;

    [HideInInspector] public int HighestAppleIndex;

    private void OnEnable()
    {
        Reach_Item_Actions.ItemReached += AddApple;
    }

    private void OnDisable()
    {
        Reach_Item_Actions.ItemReached -= AddApple;
    }

    private void Awake()
    {
        if (AppleIndex > 0)
        {
            ItemManager.NumberOfItemsCollected = AppleIndex;
        }
        else
        {
            ItemManager.NumberOfItemsCollected = 0;
        }
        
        HighestAppleIndex = AppleIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddApple();
        }
    }

    public void AddApple()
    {
        AppleIndex++;

        if (HighestAppleIndex < AppleIndex)
        {
            HighestAppleIndex = AppleIndex;
        }

        if (AppleIndex < _apples.Length)
        {
            var apple = _apples[AppleIndex];
            if (!apple.activeInHierarchy)
            {
                apple.SetActive(true);
            }
            else
            {
                AddApple();
            }
        }
    }

    public void RemoveApple(int index)
    {
        if (AppleIndex < _apples.Length)
        {
            var apple = _apples[index];
            apple.SetActive(false);
            
            AppleIndex = -1;

            if (HighestAppleIndex == index)
                HighestAppleIndex--;
        }
    }
}
