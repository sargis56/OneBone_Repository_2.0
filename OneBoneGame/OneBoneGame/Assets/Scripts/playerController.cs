using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class playerController : MonoBehaviour {
	//public varriables
	public float speed = 5.0f;
	public float groundRadius; //radius of the groundcheck
	public float jumpForce = 700f;

	public Transform groundCheck;
	public LayerMask whatIsGround; //layers of that player can jump on (this case everything)

	public Image imageLifebar;
	public Sprite spriteLifebar60;
	public Sprite spriteLifebar50;
	public Sprite spriteLifebar40;
	public Sprite spriteLifebar30;
	public Sprite spriteLifebar20;
	public Sprite spriteLifebar10;
	public Sprite spriteLifebar0;

	public Image imageLives;
	public Sprite spriteLives8;
	public Sprite spriteLives7;
	public Sprite spriteLives6;
	public Sprite spriteLives5;
	public Sprite spriteLives4;
	public Sprite spriteLives3;
	public Sprite spriteLives2;
	public Sprite spriteLives1;

	public bool attacking;
    public GameObject attackEffect;
	public GameObject enemyExplosion;
    
	public int isBonnie;
	//ground check
	public bool grounded = false;
	float attackTimer;

	Rigidbody2D rBody;
    Animator anim;

    public int hp;
    private int storedHp;
    public int lives;

	public Transform currentSpawn;
	public GameObject otherPlayer;

    public bool gotHit = false;
    float gotHitTimer;
    public bool crouching = false;
	
    // Use this for initialization
    void Start () {
		rBody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        if (this.tag == "bonnie")
            isBonnie = 1;
        else
            isBonnie = -1;
		lives = 8;
        storedHp = hp;

    }
	void FixedUpdate()
	{
        rBody.freezeRotation = true;
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); //check if groundcheck overlap with the ground
		float horizMove = Input.GetAxis("Horizontal");
        float vertiMove = Input.GetAxis("Vertical");

        if (!gotHit)
        {
            if (!grounded && !Input.GetButton("Jump"))
                rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y - 0.5f); 
        }
        if (vertiMove < 0)
            crouching = true;
        else
            crouching = false;

        if (!gotHit || !crouching)
        {
            rBody.velocity = new Vector2(horizMove * speed, rBody.velocity.y); // basic movement

            if (horizMove > 0 && gameObject.transform.rotation.y != 180)  //flipping sprite
                transform.eulerAngles = new Vector2(0, 180);

            else if (horizMove < 0 && gameObject.transform.rotation.y != 0)  //flipping sprite
                transform.eulerAngles = new Vector2(0, 0);

        }

        anim.SetFloat("Speed", Mathf.Abs(horizMove));
        anim.SetFloat("VerticalDirection", vertiMove);
        anim.SetFloat("VerticalSpeed", rBody.velocity.y);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Attacking", attacking);
        anim.SetBool("GotHit", gotHit);
        anim.SetBool("Crouching", crouching);

    }
	// Update is called once per frame
	void Update()
	{
        if (!gotHit)
        {
            //this is a temp
            if (Input.GetKeyDown(KeyCode.Tab))
                isBonnie *= -1;

            if ((grounded) && Input.GetButtonDown("Jump")) //checking for the input and if groundcheck is overlapping with the ground
                rBody.velocity = new Vector2(rBody.velocity.x, jumpForce); //rBody.AddForce(new Vector2(0, jumpForce));
            if (Input.GetButtonDown("Fire1"))
                attacking = true;
            if (attacking)
                attackTimer += (1 * Time.deltaTime);
            if (attacking && attackTimer > 0.3)
                AttackDone();
        }
        if (gotHit)
            gotHitTimer += (1 * Time.deltaTime);
        if (gotHit && gotHitTimer > 0.3)
            GotHitDone();

        //if (Input.GetKeyDown(KeyCode.Mouse0))   //sword spawn
        //	timer = Time.time + 1f;

            //if (Time.time < timer)
            //{
            //	if (this.tag == "bonnie")
            //	{
            //		sword.SetActive(true);
            //		attacking = true;
            //	}
            //}
            //else
            //{
            //	sword.SetActive(false);
            //	attacking = false;
            //}

        if (hp <= 0)
		{
			this.transform.position = currentSpawn.position;
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			hp = 60;
			updateLife ();
			lives--;
			updateLives ();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "enemy")
		{
            if (!attacking && !gotHit)
            {
                hp -= collision.gameObject.GetComponent<enemy>().damage;
                updateLife();
                updateLives();
                Debug.Log(hp);
                gotHit = true;
                if (gameObject.transform.rotation.y == 0) //LEFT
                    rBody.AddForce(transform.right*3f);
                else if (gameObject.transform.rotation.y == 180) //RIGHT
                    rBody.AddForce(transform.right * -3f);
            }
            else
            {
                Instantiate(attackEffect, collision.transform.position, Quaternion.identity);
                if (collision.gameObject.GetComponent<enemy>().hp <= 0)
                {
                    Instantiate(enemyExplosion, collision.transform.position, Quaternion.identity);
                    Destroy(collision.gameObject);
                }
                Debug.Log("enemy : " + collision.gameObject.GetComponent<enemy>().hp);
            }
		}

        if ((collision.gameObject.tag == "healthPickup") && (hp < storedHp))
        {
            hp += collision.gameObject.GetComponent<healthPickup>().health;
            updateLife();
            updateLives();
        }

        if (lives == 0)
        {
            SceneManager.LoadScene("Scenes/Level_End");
        }

    }
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "spawn")
		{
			currentSpawn = other.transform;
			otherPlayer.GetComponent<playerController>().currentSpawn = other.transform;
		}
	}

	public void updateLife()
	{

		Debug.Log(hp);

		switch (hp) 
		{
		case 60:
                imageLifebar.sprite = spriteLifebar60;
                break;
            case 59:
            case 58:
            case 57:
            case 56:
            case 55:
            case 54:
            case 53:
            case 52:
            case 51:
            case 50:
                imageLifebar.sprite = spriteLifebar50;
                break;
            case 49:
            case 48:
            case 47:
            case 46:
            case 45:
            case 44:
            case 43:
            case 42:
            case 41:
            case 40:
                imageLifebar.sprite = spriteLifebar40;
                break;
            case 39:
            case 38:
            case 37:
            case 36:
            case 35:
            case 34:
            case 33:
            case 32:
            case 31:
            case 30:
                imageLifebar.sprite = spriteLifebar30;
                break;
            case 29:
            case 28:
            case 27:
            case 26:
            case 25:
            case 24:
            case 23:
            case 22:
            case 21:
            case 20:
                imageLifebar.sprite = spriteLifebar20;
                break;
            case 19:
            case 18:
            case 17:
            case 16:
            case 15:
            case 14:
            case 13:
            case 12:
            case 11:
            case 10:
            case 9:
            case 8:
            case 7:
            case 6:
            case 5:
            case 4:
            case 3:
            case 2:
            case 1:
                imageLifebar.sprite = spriteLifebar10;
                break;
            case 0:
			imageLifebar.sprite = spriteLifebar0;
			break;
		default:
			imageLifebar.sprite = spriteLifebar60;
			break;
		}
	}
	public void updateLives()
	{
		switch (lives) 
		{
		case 8:
			imageLives.sprite = spriteLives8;
			break;
		case 7:
			imageLives.sprite = spriteLives7;
			break;
		case 6:
			imageLives.sprite = spriteLives6;
			break;
		case 5:
			imageLives.sprite = spriteLives5;
			break;
		case 4:
			imageLives.sprite = spriteLives4;
			break;
		case 3:
			imageLives.sprite = spriteLives3;
			break;
		case 2:
			imageLives.sprite = spriteLives2;
			break;
		case 1:
			imageLives.sprite = spriteLives1;
			break;
		default:
			imageLives.sprite = spriteLives8;
			break;
		}
	}
    private void AttackDone()
    {
        attacking = false;
        attackTimer = 0;
    }
    private void GotHitDone()
    {
        gotHit = false;
        gotHitTimer = 0;
    }

}
