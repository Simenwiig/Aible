using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : ReachItem
{   
    [SerializeField] private float _timer;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _timer = 0;
    }

    override public void ItemReached()
    {
        base.ItemReached();
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
        rb.useGravity = true;

        yield return new WaitForSeconds(3);

        Destroy(this);
    }
}
