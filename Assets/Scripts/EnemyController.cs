using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private bool vertical = false;
    [SerializeField] private float changeTime = 3.0f;
    [SerializeField] private ParticleSystem smokeEffect = null;
    
    private Rigidbody2D _rigidbody2D;
    private float _timer;
    private int _direction = 1;
    private Animator _animator;
    private bool _broken = true;
    
    // Animation cache
    private static readonly int MoveXAnim = Animator.StringToHash("Move X");
    private static readonly int MoveYAnim = Animator.StringToHash("Move Y");
    private static readonly int FixedAnim = Animator.StringToHash("Fixed");
    
    // Audio clips
    private AudioSource _audioSource;
    [SerializeField] private AudioClip repairSound = null;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _timer = changeTime;
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (!_broken) return;  // Robot doesn't move when fixed
        
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _direction *= -1;
            _timer = changeTime;
        }
        
        Vector2 position = _rigidbody2D.position;

        if (vertical)
        {
            position.y += speed * Time.deltaTime * _direction;
            _animator.SetFloat(MoveXAnim, 0);
            _animator.SetFloat(MoveYAnim, _direction);
        }
        else
        {
            position.x += speed * Time.deltaTime * _direction;
            _animator.SetFloat(MoveXAnim, _direction);
            _animator.SetFloat(MoveYAnim, 0);
        }
        

        _rigidbody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        _broken = false;
        _rigidbody2D.simulated = false;
        _animator.SetTrigger(FixedAnim);
        smokeEffect.Stop();
        _audioSource.Stop();
        _audioSource.PlayOneShot(repairSound);
    }
}
