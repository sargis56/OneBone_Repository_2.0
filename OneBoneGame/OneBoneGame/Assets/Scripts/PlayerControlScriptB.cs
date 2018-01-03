using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScriptB : MonoBehaviour {
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float maxJump = 8f;
    public float speed = 3f;
    public BoxCollider2D feet;
    public bool attacking = false;

    private Rigidbody2D rb;
    private SpriteRenderer spr;
    private Animator anim;

    public int isBonnie = 1;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
    void FixedUpdate() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (!grounded && !Input.GetButton("Jump"))
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 0.5f);
        float horizMove = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizMove * speed, rb.velocity.y); 
        

        if (horizMove > 0 && gameObject.transform.rotation.y != 180)
        {
            feet.offset = new Vector2( -0.24f,feet.offset.y);
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (horizMove < 0 && gameObject.transform.rotation.y != 0)
        {
            feet.offset = new Vector2(0.75f, feet.offset.y);
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (Input.GetButtonDown("Fire1"))
            attacking = true;

        anim.SetFloat("Speed", Mathf.Abs(horizMove));
        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Attacking", attacking);
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Jump") && grounded)
            rb.velocity = new Vector2(rb.velocity.x, maxJump);

        if (Input.GetKey(KeyCode.Tab))
            isBonnie *= -1;
        
    }

    void AttackDone() {
        attacking = false;
    }
}
