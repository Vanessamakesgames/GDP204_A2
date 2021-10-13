using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveDirection;
    BoxCollider2D bc2d;
    bool onGround, facingRight;
    Rigidbody2D rb2d;
    Transform t;
    Vector3 cameraPos;

    [Header ("Camera")]
    [SerializeField]
    Camera mainCamera;

    [Header ("Player movement stats")]
    [SerializeField]
    float health;
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float runSpeed;
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    float groundDeceleration;
    [SerializeField]
    float fallMultiplier;
    [SerializeField]
    float lowJumpMultiplier;

    [Header("Ground Checker")]
    [SerializeField]
    public LayerMask groundLayer;
    [SerializeField]
    public Transform groundCheckerObj;
    [SerializeField]
    float checkerRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        health = 100.0f;
        bc2d = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
        moveDirection = 0f;
        walkSpeed = 2.5f;
        runSpeed = 5f;
        facingRight = true;
        jumpHeight = 5f;
        onGround = false;
        fallMultiplier = 2.5f;
        lowJumpMultiplier = 2f;
        checkerRadius = 0.01f;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForGround();
        Jump();
        BetterJump();
        Collisions();
        Camera();      
    }

    void FixedUpdate()
    {

    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy;
        if (Input.GetKey(KeyCode.LeftShift)) moveBy = x * runSpeed;
        else moveBy = x * walkSpeed;
        rb2d.velocity = new Vector2(moveBy, rb2d.velocity.y);
    }

    private void Jump()
    {
        if (onGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
                onGround = false;
                Debug.Log("Jump");
            }
        }
    }

    private void BetterJump()
    {
        if (rb2d.velocity.y < 0) rb2d.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) rb2d.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
    }

    private void Camera()
    {
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, t.position.y, cameraPos.z);
        }
    }

    private void CheckForGround()
    {
        Collider2D hit = Physics2D.OverlapCircle(groundCheckerObj.position, checkerRadius, groundLayer);
        if (hit != null) onGround = true;
        else onGround = false;
    }

    private void Collisions()
    {

    }
}
