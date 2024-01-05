using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public static IEnumerator Position(Transform target, Vector3 targetEndPosition, float duration, float startDelay = 0)
    {
        yield return new WaitForSeconds(startDelay);

        Vector3 startPosition = target.position;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            target.position = Vector3.Lerp(startPosition, targetEndPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.position = targetEndPosition; // Ensure the player reaches the new target position
    }

    public static IEnumerator Rotation(Transform target, Quaternion targetEndRotation, float duration, float startDelay = 0)
    {
        yield return new WaitForSeconds(startDelay);

        Quaternion startRotation = target.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            target.rotation = Quaternion.Lerp(startRotation, targetEndRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.rotation = targetEndRotation; // Ensure the player reaches the new target rotation
    }
}
