using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : ReachItem
{   
    private float _timer;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _timer = 0;
    }

    override public void ItemReached()
    {
        base.ItemReached();
    }

    private void OnEnable()
    {
        _timer = 0;
    }

    private void Update()
    {
        if (!ItemManager._ItemManager.CanItemFallDown)
            return;

        if (_timer >= ItemManager._ItemManager.TimeBeforeAppleFallDown)
        {
            _timer = 0;
            StartCoroutine(FallDown());
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    private IEnumerator FallDown()
    {
        _rb.isKinematic = false;

        yield return new WaitForSeconds(3);

        _rb.isKinematic = true;

        Reach_Item_Actions.ReleaseItem(this.gameObject);
    }
}
