using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UICollider : MonoBehaviour
{
    BoxCollider _boxCollider;
    RectTransform _rectTransform;
    string menuButtonLayer = "MenuButton";

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rectTransform = GetComponent<RectTransform>();

        _boxCollider.size = new Vector3(_rectTransform.rect.width, _rectTransform.rect.height, 10);
        int layer = LayerMask.NameToLayer(menuButtonLayer);
        this.gameObject.layer = layer;
    }
}
