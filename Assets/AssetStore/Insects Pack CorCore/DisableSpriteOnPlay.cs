using UnityEngine;
namespace CorCoreInsects
{

    public class DisableSpriteOnPlay : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the SpriteRenderer component is found
        if (spriteRenderer != null)
        {
            // Disable the SpriteRenderer component
            spriteRenderer.enabled = false;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found on this GameObject.");
        }
    }
}
}