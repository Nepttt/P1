using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Image dashGauge;
    public float maxDashGaugeAmount = 100f;
    public float dashGaugeAmount = 100f;
    
    public float moveSpeed = 7f; // 이속
    public float jumpForce = 16f; // 점프 힘

    /* public float coyoteTime = 0.2f;
    public float coyoteTimeCounter; 언젠가 코요테 타임을 구현할 때 꺼낼 것 */

    /* public float jumpBufferTime = 0.2f;
    public float jumpBufferCounter; 언젠가 jump buffer를 구현할 때 꺼낼 것 */
    
    private Rigidbody2D _rigidbody;
    private TrailRenderer _trailRenderer;
    private Animator _animator;
    private Collider2D _collider;
    private SpriteRenderer _rend;
    private Image _image;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.05f;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private bool _active = true;
    
    [SerializeField] private float _dashingVelocity = 100f;
    [SerializeField] private float _dashingTime = 0.07f;

    private int _jumpLefts;
    
    private Vector2 _dashingDir;
    private bool _isDashing;
    private bool _canDash;
    public bool isJumping;

    private bool doubleJump;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _rend = GetComponent<SpriteRenderer>();
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if (!_active)
        {
            return;
        }

        var inputX = Input.GetAxisRaw("Horizontal"); // 좌우 화살표, A키, D키로 좌우 이동
        var inputY = Input.GetAxisRaw("Vertical"); // 상하 화살표, W키, S키로 상하 이동(의미없음)
        var dashInput = Input.GetButtonDown("Dash"); // 시프트 키로 대시
        var jumpInput = Input.GetButtonDown("Jump"); // 스페이스바 누르면 점프 두 번 누르면 더블 점프

        _rigidbody.velocity = new Vector2(inputX * moveSpeed, _rigidbody.velocity.y);

        if (dashInput && _canDash && dashGaugeAmount == 100f)
        {
            _isDashing = true;
            _canDash = false;
            _trailRenderer.emitting = true;
            _dashingDir = new Vector2(inputX, 0);
            dashGaugeAmount = 0;
            ReduceDashGauge(100f);
            
            if (_dashingDir == Vector2.zero)
            {
                _dashingDir = new Vector2(transform.localScale.x, 0);
            }

            StartCoroutine(StopDashing()); // 대시 멈추는 코루틴 호출
        }

        if (dashGaugeAmount < 100)
        {
            IncreaseDashGauge(1f);
        }

        if (_isDashing)
        {
            _rigidbody.velocity = _dashingDir.normalized * _dashingVelocity;
            return;
        }

        // 캐릭터 이동
        if (inputX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(inputX), 1, 1);
        }

        if (inputX > 0)
        {
            _rend.flipX = false;
        }
        
        else if (inputX < 0)
        {
            _rend.flipX = true;
        }
        
        
        if (IsGrounded())
        {
            _canDash = true;
            //coyoteTimeCounter = coyoteTime;
        }

        /*else
        {
            coyoteTimeCounter -= Time.deltaTime; 아무리 생각해도 코요테 타임과 더블 점프를 융합 못 하겠다 나중에 되면 구현함
        }
        */
        
        // 점프 & 더블 점프
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || doubleJump)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);

                doubleJump = !doubleJump;
            }
        }

        if (Input.GetButtonUp("Jump") && _rigidbody.velocity.y > 0f)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }
    }

    public void IncreaseDashGauge(float increasing)
    {
        dashGaugeAmount += increasing;
        dashGauge.fillAmount = Mathf.Clamp(dashGaugeAmount, 0, maxDashGaugeAmount);

        dashGauge.fillAmount = dashGaugeAmount / 100f;
    }

    public void ReduceDashGauge(float reduction)
    {
        dashGaugeAmount -= reduction;
        dashGauge.fillAmount = dashGaugeAmount / 100f;
    }

    public void IncreaseHpGauge(float increasing)
    {
        throw new NotImplementedException(); // 나중에 여기에 increasing만큼 HP 올려주는 코드 추가할 것 게이지 채우는 건 위에 거 참조
    }

    public void ReduceHpGauge(float reduction)
    {
        throw new NotImplementedException(); // 나중에 여기에 reduction만큼 HP 낮춰주는 코드 추가할 것
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        _trailRenderer.emitting = false;
        _isDashing = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, _collisionMask);
    }

    private IEnumerator GroundCheck()
    {
        Console.WriteLine("{0}", IsGrounded());
        yield return new WaitForSeconds(2);
    }
}

