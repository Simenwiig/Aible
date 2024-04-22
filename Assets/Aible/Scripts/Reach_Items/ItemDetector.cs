using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class ItemDetector : MonoBehaviour
{
    [SerializeField] private DrawPoseLandmark _drawPoseLandmark;
    [SerializeField] private Image _progressBar;
    [SerializeField] private float _timeBeforeButtonActivates = 1f;
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private LayerMask _reachItemLayer;

    private float _timer;

    private LandmarkPoints _bodyPoints;

    private Vector3 _rightOrigin;
    private Vector3 _leftOrigin;

    private void Update()
    {
        _bodyPoints = _drawPoseLandmark.BodyPoints;

        if (_bodyPoints == null || _drawPoseLandmark.IsBodyRemovedFromCamera)
        {
            Deactivate(_progressBar);
            return;
        }

        //camera
        Camera camera = Camera.main;

        // Right arm detector
        _rightOrigin = new Vector3(_bodyPoints.Points[16].transform.position.x, _bodyPoints.Points[16].transform.position.y, 0);       
        Collider[] rightCollider = Physics.OverlapCapsule(camera.transform.position, _rightOrigin, _radius, _reachItemLayer, QueryTriggerInteraction.Collide);
        if (rightCollider.Length > 0)
        {
            if (rightCollider[0].gameObject.tag == "ReachItem")
            {
                ItemDetected(rightCollider[0]);
            }
            else if(rightCollider[0].gameObject.tag == "Mob")
            {
                MobDetected(rightCollider[0]);
            }
            else if (rightCollider[0].gameObject.tag == "MenuButton")
            {
                CheckForButton(rightCollider[0]);
            }
        }

        // Left arm detector
        _leftOrigin = _bodyPoints.Points[15].transform.position;
        Collider[] leftCollider = Physics.OverlapCapsule(camera.transform.position, _leftOrigin, _radius, _reachItemLayer, QueryTriggerInteraction.Collide);
        if (leftCollider.Length > 0)
        {
            if (leftCollider[0].gameObject.tag == "ReachItem")
            {
                ItemDetected(leftCollider[0]);
            }
            else if(leftCollider[0].gameObject.tag == "Mob")
            {
                MobDetected(leftCollider[0]);
            }
            else if (leftCollider[0].gameObject.tag == "MenuButton")
            {
                CheckForButton(leftCollider[0]);
            }
        }

        if (leftCollider.Length <= 0 && rightCollider.Length <= 0)
        {
            Deactivate(_progressBar);
        }
    }

    private void ItemDetected(Collider collider)
    {
        ReachItem item = collider.gameObject.GetComponentInParent<ReachItem>();
        item.ItemReached();
        Reach_Item_Actions.ItemReached?.Invoke();
    }

    private void MobDetected(Collider collider)
    {
        Insect insect = collider.gameObject.GetComponentInParent<Insect>();
        StartCoroutine(insect.Die());
    }

    private void CheckForButton(Collider collider)
    {
        Button button = collider.gameObject.GetComponentInParent<Button>();
        _progressBar.transform.position = collider.gameObject.transform.position + Vector3.up * 1.3f;
        _timer += Time.deltaTime % 60;
        _progressBar.fillAmount = _timer / _timeBeforeButtonActivates;
        if (_timer >= _timeBeforeButtonActivates)
        {
            _progressBar.fillAmount = 0;
            ClickButton(button);
        }
    }

    private void ClickButton(Button button)
    {
        button.onClick.Invoke();
    }

    public void Deactivate(Image progressBar)
    {
        progressBar.fillAmount = 0;
        _timer = 0;
    }

    /*
    private void OnDrawGizmos()
    {
        if (_bodyPoints == null || _drawPoseLandmark.IsBodyRemovedFromCamera)
            return;

        // Set the color for the Gizmos
        Gizmos.color = Color.red;

        Camera camera = Camera.main;

        // Right arm capsule
        var rightBottom = camera.transform.position;
        var rightTop = _rightOrigin;
        Gizmos.DrawWireSphere(rightBottom, _radius); // Draw bottom sphere
        Gizmos.DrawWireSphere(rightTop, _radius);  // Draw top sphere
        Gizmos.DrawLine(rightBottom, rightTop);  // Draw connecting line
    }*/
}
