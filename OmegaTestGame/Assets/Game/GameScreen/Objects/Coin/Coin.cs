using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;

    private int _destroyedTime;
    private float _freezeY;
    
    private enum Status
    {
        Idle,
        Falling,
        Ground
    }

    private Status _status = Status.Idle;
    
    private void Awake()
    {
        _timer.CallOnTimerEnd += () => Destroy(gameObject);   
    }

    private void FixedUpdate()
    {
        switch (_status)
        {
                case Status.Idle:
                    return;
                case Status.Falling:
                    if (transform.position.y <= _freezeY)
                    {
                        _rigidbody.isKinematic = true;
                        _status = Status.Ground;
                    }
                    break;    
                case Status.Ground:
                    _timer.StartTimer(_destroyedTime,Timer.Mode.Once);
                    _animator.SetBool("StartRotate",true);
                    _status = Status.Idle;
                    break;
        }
    }

    public void Initialize(int destroySeconds,float freezeY)
    {
        _destroyedTime = destroySeconds;
        _freezeY = freezeY;
        _status = Status.Falling;
    }
}
