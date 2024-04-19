using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmarkPoints : MonoBehaviour
{
    public Transform IndexFingerRayOrigin;
    public List<GameObject> Points;

    static public LandmarkPoints Instance;

    private void Awake()
    {
        Instance = this;
    }

    static public void ChangeMaterial(int handPointIndex, Material material)
    {
        Instance.Points[handPointIndex].GetComponent<MeshRenderer>().material = material;
    }

    static public Material GetMaterial(int handPointIndex)
    {
        return Instance.Points[handPointIndex].GetComponent<MeshRenderer>().materials[0];
    }
}
