using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject pj;
    //public new Vector3 respawn;
    private Rigidbody2D rb2d;
    //private Animator anim;
    //Blink material;
    SpriteRenderer sprite;
    public float jumpForce;
    private float speed = 6;
    private float horizontal;
    public bool grounded;
    private float lastShoot;
    bool isInmune;
    public float inmunityTime;
    public float knockBackForceX;
    public float knockBackForceY;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //material = GetComponent<Blink>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        //anim.SetBool("running", horizontal != 0.0f);

        if (horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        else if (horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        Debug.DrawRay(transform.position, Vector3.down * 0.65f, Color.red);

        if (Physics2D.Raycast(transform.position, Vector3.down, 0.65f))
        {
            grounded = true;
        }

        else grounded = false;

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            jump();
        }

        //if (Input.GetKey(KeyCode.Space) && Time.time > lastShoot + 0.25f)
        //{
        //    shoot();
        //    lastShoot = Time.time;
        //}
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
    }

    private void jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce);
    }

    //private void shoot()
    //{
    //    Vector3 bDirection;

    //    if (transform.localScale.x == 3.0f) bDirection = Vector3.right;

    //    else

    //    {
    //        bDirection = Vector3.left;
    //    }

    //    GameObject bullet = Instantiate(bulletPrefab, transform.position + bDirection * 0.4f, Quaternion.identity);
    //    bullet.GetComponent<bullet>().setDirection(bDirection);
    //}
}
