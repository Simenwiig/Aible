using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUI : MonoBehaviour
{
    [SerializeField] GameObject[] pcUI;
    [SerializeField] GameObject[] mobileUI;
    private void Awake()
    {
        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            foreach (var obj in pcUI)
            {
                obj.SetActive(false);
            }
            foreach (var obj in mobileUI)
            {
                obj.SetActive(true);
            }
        }
    }
}
