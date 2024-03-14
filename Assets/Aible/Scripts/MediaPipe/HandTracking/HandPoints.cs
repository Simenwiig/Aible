using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPoints : MonoBehaviour
{
    public Transform IndexFingerRayOrigin;
    public List<GameObject> _HandPoints;

    static public HandPoints Instance;

    private void Awake()
    {
        Instance = this;
    }

    static public void ChangeMaterial(int handPointIndex, Material material)
    {
        Instance._HandPoints[handPointIndex].GetComponent<MeshRenderer>().material = material;
    }

    static public Material GetMaterial(int handPointIndex)
    {
        return Instance._HandPoints[handPointIndex].GetComponent<MeshRenderer>().materials[0];
    }
}
