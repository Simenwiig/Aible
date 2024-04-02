using Mediapipe.Unity.Sample.HandTracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class LandmarkUIActivation : MonoBehaviour
{
    private float _timer;

    public void CheckForButton(Image progressBar, Vector3 origin, Vector3 direction, float raycastLength, LayerMask buttonLayer, float timeBeforeButtonActivates)
    {
        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, raycastLength, buttonLayer))
        {
            Debug.DrawRay(origin, direction * raycastLength, Color.green);
            Button button = hit.transform.gameObject.GetComponentInParent<Button>();
            progressBar.transform.position = hit.point + Vector3.up * 0.9f;
            _timer += Time.deltaTime % 60;
            progressBar.fillAmount = _timer / timeBeforeButtonActivates;
            if (_timer >= timeBeforeButtonActivates)
            {
                progressBar.fillAmount = 0;
                ClickButton(button);
            }
        }
        else
        {
            Debug.DrawRay(origin, direction * raycastLength, Color.red);
            Deactivate(progressBar);
        }
    }

    public void Deactivate(Image progressBar)
    {
        progressBar.fillAmount = 0;
        _timer = 0;
    }

    private void ClickButton(Button button)
    {
        button.onClick.Invoke();
    }
}
