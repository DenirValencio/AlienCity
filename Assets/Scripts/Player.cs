using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 600;
    public GameObject bulletPrefab;
    public Transform shotSpawner;

    private Animator anim;
    private Rigidbody2D rb2d;
    private bool facingRigth = true;
    private bool jump;
    private bool onGround = false;
    private Transform groundCheck;
    private float hForce = 0;

    private bool gunStd;
    private bool gunRun;
    private bool gunShoot;
    private float fireRate = 0.5f;
    private float nextFire;

    private bool isDead = false;

    // Use this for initialization
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

            if (onGround)
            {
                anim.SetBool("Jump", false);
            }

            if (Input.GetButtonDown("Jump") && onGround && !gunShoot)
            {
                jump = true;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (rb2d.velocity.y > 0)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
                }
            }

            if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                anim.SetTrigger("Shoot");
                GameObject tempBullet = Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
                if (!facingRigth)
                {
                    tempBullet.transform.eulerAngles = new Vector3(0, 0, 180);
                }
            }
            gunStd = Input.GetButton("Up");
            gunRun = Input.GetButton("Down");

            anim.SetBool("GunStand", gunStd);
            anim.SetBool("GunRunning", gunRun);

           if (gunStd || gunRun && onGround)
            {
                hForce = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (!gunRun && !gunStd)
            {
                hForce = Input.GetAxisRaw("Horizontal");

                anim.SetFloat("Speed", Mathf.Abs(hForce));
            }

            rb2d.velocity = new Vector2(hForce * speed, rb2d.velocity.y);

            if (hForce > 0 && !facingRigth)
            {
                Flip();
            }

            else if (hForce < 0 && facingRigth)
            {
                Flip();
            }

            if (jump)
            {
                anim.SetBool("Jump", true);
                jump = false;
                rb2d.AddForce(Vector2.up * jumpForce);
            }
        }
    }
    void Flip()
    {
        facingRigth = !facingRigth;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}