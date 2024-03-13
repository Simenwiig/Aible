using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UICollider : MonoBehaviour
{
    BoxCollider _boxCollider;
    RectTransform _rectTransform;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rectTransform = GetComponent<RectTransform>();

        _boxCollider.size = new Vector3(_rectTransform.rect.width, _rectTransform.rect.height, 10);
    }
}
