using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private ParticleSystem collectibleEffect = null;
    [SerializeField] private AudioClip collectedClip = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        RubyController rubyController = other.GetComponent<RubyController>();

        if (rubyController != null && rubyController.Health < rubyController.maxHealth)
        {
            rubyController.ChangeHealth(1);
            Instantiate(collectibleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
            rubyController.PlaySound(collectedClip);
        }
    }
}
