using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachItem : MonoBehaviour
{
    virtual public void ItemReached()
    {
        ItemManager.AddItem(1);
    }
}
