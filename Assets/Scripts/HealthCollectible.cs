using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public ParticleSystem collectibleEffect;
    
    [SerializeField] private AudioClip collectedClip;

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
