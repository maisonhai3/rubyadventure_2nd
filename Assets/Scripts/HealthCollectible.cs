using UnityEngine;

/// <summary>
///     Will handle giving health to the character when they enter the trigger.
/// </summary>
public class HealthCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeHealth(1);
            Destroy(gameObject);
        }
    }
}