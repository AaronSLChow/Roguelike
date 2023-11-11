using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    public ParticleSystem dust;

    [SerializeField] private float dashSpeed;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;
    bool isDashing;
    bool canDash;

    void Start()
    {
        canDash = true;
    }

    void Update()
    {
        LookAtMouse();

        if (movement.x != 0 || movement.y != 0 || isDashing)
            CreateDust(movement.x);
        else
            PauseDust();

        if (isDashing)
        {
            return;
        }

        //Input

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        //Movement

        if (isDashing)
        {
            return;
        }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(movement.x * dashSpeed, movement.y * dashSpeed);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    private void LookAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = mousePos - new Vector2(transform.position.x, transform.position.y);
    }

    void CreateDust(float xDir)
    {
        var em = dust.emission;
        em.enabled = true;

        if(isDashing)
            em.rateOverTime = 100f;
        else
            em.rateOverTime = 20f;

        var vel = dust.velocityOverLifetime;
        float linVel = 0.2f;

        if (xDir >= 1)
            vel.x = -linVel;
        else if (xDir <= -1)
            vel.x = +linVel;
        else
            vel.x = 0f;
    }

    void PauseDust()
    {
        var em = dust.emission;
        em.rateOverTime = 0f;
    }
}
