using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal;
    float vertical;
    public float movSpeed;
    public float jumpForce;
    Rigidbody2D rb2d;
    IEnumerator dashCoroutine;
    bool isDashing;
    bool canDash = true;
    bool grounded;
    float direction = 1;
    float normalGravity;
    public int jumpCount;
    public int extraJumps = 1;
    float jumpCooldown;



    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        normalGravity = rb2d.gravityScale;
    }

    private void Update()
    {
        if (horizontal != 0)
        {
            direction = horizontal;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        Debug.DrawRay(transform.position, Vector3.down * 0.55f, Color.red);
        /*if (Physics2D.Raycast(transform.position, Vector3.down, 0.55f))
        {
            grounded = true;
        }

        else grounded = false;*/

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump();
            Debug.Log("hoa");
        }

        CheckGrounded();

        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash == true)
        {
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dash(.1f, 1);
            StartCoroutine(dashCoroutine);
        }
    }

    private void FixedUpdate()
    {
        rb2d.AddForce(new Vector2(horizontal * movSpeed, 0));
        if (isDashing)
        {
            rb2d.AddForce(new Vector2(direction * 10, 0), ForceMode2D.Impulse);
        }
    }

    private void jump()
    {

        if (grounded || jumpCount < extraJumps)
        {
            rb2d.velocity=new Vector2(0, jumpForce);
            jumpCount++;
        }
        
    }

    void CheckGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.55f))
        {
            grounded = true;
            jumpCount = 0;
            jumpCooldown = Time.time + 0.2f;
        }
        else if (Time.time < jumpCooldown)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    IEnumerator Dash(float dashDuration, float dashCooldown)
    {
        Vector2 originalVelocity = rb2d.velocity;
        isDashing = true;
        canDash = false;
        rb2d.gravityScale = 0;
        rb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb2d.gravityScale = normalGravity;
        rb2d.velocity = originalVelocity;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
