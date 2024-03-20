using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Insect : MonoBehaviour
{
    public Transform Target;
    public float MoveDuration = 3f;
    public float TurnDuration = 1f;

    private void Start()
    {
        StartCoroutine(Movement());                       
    }

    private IEnumerator Movement()
    {
        Vector3 startPos = transform.position;

        MoveToTarget(Target.position, MoveDuration);

        yield return new WaitForSeconds(MoveDuration);

        TurnAround(new Vector3(0, -180, 90), TurnDuration);

        yield return new WaitForSeconds(TurnDuration);

        MoveToTarget(startPos, MoveDuration);
    }

    protected void MoveToTarget(Vector3 target, float duration)
    {
        transform.DOMove(target, duration);
    }

    protected void MoveToTarget(Vector3 target, float duration, Ease ease)
    {
        transform.DOMove(target, duration).SetEase(ease);
    }

    protected void TurnAround(Vector3 rotation, float duration)
    {
        transform.DORotate(rotation, duration);
    }
}
