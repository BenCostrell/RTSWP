using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private int playerNum;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Collider2D col;
    [SerializeField]
    private float groundAccel;
    [SerializeField]
    private float aerialAccel;
    [SerializeField]
    private float groundDrag;
    [SerializeField]
    private float aerialDrag;
    public bool grounded;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float groundCheckRayCastDist;
    [SerializeField]
    private float platformDropThreshold;
    private Collider2D currentPlatformCollider;
    private List<Collider2D> currentlyIgnoredPlatforms;
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckIfOutOfBounds();
        CheckIfGrounded();
        Move();
    }

    public void Init(int playerNum_)
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        currentlyIgnoredPlatforms = new List<Collider2D>();
        playerNum = playerNum_;
        transform.position = Services.Main.playerSpawns[playerNum - 1];
        sr.color = Services.Main.playerColors[playerNum - 1];
        Services.EventManager.Register<ButtonPressed>(OnButtonPressed);
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal_P" + playerNum);
        if (grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x * groundDrag, rb.velocity.y);
            rb.velocity += new Vector2(horizontalInput * groundAccel, 0);
        }
        else
        {
            rb.velocity *= aerialDrag;
            rb.velocity += new Vector2(horizontalInput * aerialAccel, 0);
        }
        float verticalInput = Input.GetAxis("Vertical_P" + playerNum);
        if (verticalInput < platformDropThreshold && grounded) Drop();
        else AllowPlatformCollision();
    }

    void OnButtonPressed(ButtonPressed e)
    {
        if (e.playerNum == playerNum) {
            if (e.button == "A")
            {
                if (grounded) Jump();
            }
        }
    }

    void Drop()
    {
        Physics2D.IgnoreCollision(col, currentPlatformCollider);
        currentlyIgnoredPlatforms.Add(currentPlatformCollider);
    }

    void AllowPlatformCollision()
    {
        for (int i = 0; i < currentlyIgnoredPlatforms.Count; i++)
        {
            Physics2D.IgnoreCollision(col, currentlyIgnoredPlatforms[i], false);
        }
        currentlyIgnoredPlatforms.Clear();
    }

    void Jump()
    {
        rb.velocity += jumpSpeed * Vector2.up;
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.layer == Services.Main.groundLayer)
        {
        }
    }

    void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,
            groundCheckRayCastDist, Services.Main.groundLayer);
        //Debug.DrawRay(transform.position, groundCheckRayCastDist * Vector2.down, Color.red, 0.1f);
        if (hit)
        {
            grounded = true;
            currentPlatformCollider = hit.collider;
        }
        else grounded = false;
    }

    void CheckIfOutOfBounds()
    {
        if(transform.position.x < Services.Main.rightBoundary)
        {
            transform.position = new Vector2(
                Services.Main.leftBoundary - 1,
                transform.position.y);
        }
        else if(transform.position.x > Services.Main.leftBoundary)
        {
            transform.position = new Vector2(
                Services.Main.rightBoundary + 1,
                transform.position.y);
        }
        if(transform.position.y < Services.Main.botBoundary)
        {
            transform.position = new Vector2(
                transform.position.x,
                Services.Main.topOfScreen);
        }
    }
}
