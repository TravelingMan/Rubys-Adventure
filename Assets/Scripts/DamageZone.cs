using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        var rubyController = other.GetComponent<RubyController>();

        if (rubyController != null)
        {
            rubyController.ChangeHealth(-1);
        }
    }
}
