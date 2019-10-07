using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    
    [SerializeField] private float timeInvincible = 1.0f;
    [SerializeField] private GameObject projectilePrefab = null;
    
    public int Health
    {
        get { return _currentHealth; }
    }

    [SerializeField] private float _speed = 3.0f;
    private int _currentHealth;
    private Rigidbody2D _rigidbody2D;
    private bool _isInvincible;
    private float _invincibleTimer;
    private Animator _animator;
    private Vector2 _lookDirection = new Vector2(1, 0);
    private Vector2 _movement;
    
    // Audio
    private AudioSource _audioSource;
    [SerializeField] private AudioClip throwClip = null;
    [SerializeField] private AudioClip getHurt = null;
    
    // Animation cache
    private static readonly int LookXAnim = Animator.StringToHash("Look X");
    private static readonly int LookYAnim = Animator.StringToHash("Look Y");
    private static readonly int SpeedAnim = Animator.StringToHash("Speed");
    private static readonly int HitAnim = Animator.StringToHash("Hit");

    void Start()
    {
        //Application.targetFrameRate = 20;  // Good for testing at shitty FPS
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currentHealth = maxHealth;
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        _movement = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(_movement.x, 0.0f) || !Mathf.Approximately(_movement.y, 0.0f))
        {
            _lookDirection.Set(_movement.x, _movement.y);
            _lookDirection.Normalize();
        }
        
        _animator.SetFloat(LookXAnim, _lookDirection.x);
        _animator.SetFloat(LookYAnim, _lookDirection.y);
        _animator.SetFloat(SpeedAnim, _movement.sqrMagnitude);
        
        if (_isInvincible)
        {
            _invincibleTimer -= Time.deltaTime;
            if (_invincibleTimer < 0)
            {
                _isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKey(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(_rigidbody2D.position + Vector2.up * 0.2f,
                _lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NPC character = hit.collider.GetComponent<NPC>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + Time.fixedDeltaTime * _speed * _movement);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (_isInvincible) return;
            _isInvincible = true;
            _invincibleTimer = timeInvincible;
            
            _animator.SetTrigger(HitAnim);
            PlaySound(getHurt);
        }
        
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(_currentHealth / (float)maxHealth);
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(
            projectilePrefab, 
            _rigidbody2D.position + Vector2.up * 0.5f, 
            Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(_lookDirection, 300);
        
        _animator.SetTrigger("Launch");
        PlaySound(throwClip);
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}