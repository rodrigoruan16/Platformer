using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _playerRB;
    Animator playerAnimator;
    bool isRunning;
    float xDir = 0; // 1 direita e -1 esquerda

    [SerializeField]
    float xSpeed;

    [SerializeField]
    float ySpeed; // jump

    private int maxJumps = 2;
    private int jumpsLeft;

    void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = transform.GetChild(1).GetComponent<Animator>();
        jumpsLeft = maxJumps;
    }

    void Movimentar()
    {
        _playerRB.linearVelocityX = xDir * xSpeed;
        isRunning = Mathf.Abs(_playerRB.linearVelocityX) > Mathf.Epsilon;

        if (isRunning)
            FlipSprite();
    }

    void OnJump()
    {
        if (jumpsLeft <= 0) return;

        _playerRB.linearVelocityY = ySpeed;
        jumpsLeft--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpsLeft = maxJumps;
        }
    }
    void FlipSprite()
    {
        float sinal = _playerRB.linearVelocityX > 0 ? 1 : -1;
        transform.localScale = new Vector3(sinal, 1, 1);
    }

    // Evento disparado pelo player input
    void OnMove(InputValue inputValue)
    {
        xDir = inputValue.Get<Vector2>().x;
    }
    void FixedUpdate()
    {
        Movimentar();
        playerAnimator.SetBool("IsRunning", isRunning);
    }
}
