using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollider : MonoBehaviour
{
    string menuButtonLayer = "MenuButton";

    private void Awake()
    { 
        GameObject UIColliderObj = new GameObject("UICollider", typeof(RectTransform));
        UIColliderObj.transform.parent = gameObject.transform;
        RectTransform UIRect = UIColliderObj.GetComponent<RectTransform>();
        UIRect.localScale = Vector3.one;
        UIRect.anchoredPosition3D = Vector3.zero;

        UIColliderObj.AddComponent(typeof(BoxCollider));
        BoxCollider boxCollider = UIColliderObj.GetComponent<BoxCollider>();

        RectTransform rectTransform = GetComponent<RectTransform>();
        boxCollider.size = new Vector3(rectTransform.rect.width, rectTransform.rect.height, 10);

        int layer = LayerMask.NameToLayer(menuButtonLayer);
        UIColliderObj.layer = layer;
    }
}
