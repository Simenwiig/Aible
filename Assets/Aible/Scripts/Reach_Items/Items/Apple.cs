using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Apple : ReachItem
{   
    private float _timer;
    private Rigidbody _rb;

    private Vector3 _originalRotation;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _timer = 0;
        _originalRotation = transform.eulerAngles;
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
        Shake();

        yield return new WaitForSeconds(2);

        _rb.isKinematic = false;

        yield return new WaitForSeconds(3);

        transform.DOKill();
        transform.eulerAngles = _originalRotation;

        _rb.isKinematic = true;

        Reach_Item_Actions.ReleaseItem(this.gameObject);
    }

    private void Shake()
    {
        transform.DOShakeRotation(3, new Vector3(25, 0, 0), 2, 0, true, ShakeRandomnessMode.Harmonic);
    }
}
