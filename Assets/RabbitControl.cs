using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitControl : MonoBehaviour {

    public float speed = 2;
    Rigidbody2D myBody = null;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    // Use this for initialization
    void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
        LevelController.current.setStartPosition(transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Animator animator = GetComponent<Animator>();
        float runVal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(runVal) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = runVal * speed;
            myBody.velocity = vel;
        }
        if (Mathf.Abs(runVal) > 0)
            animator.SetBool("run", true);
        else
            animator.SetBool("run", false);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (runVal < 0)
            sr.flipX = true;
        else if (runVal > 0)
            sr.flipX = false;
        float diff = Time.deltaTime;
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
            isGrounded = true;
        else
            isGrounded = false;
        Debug.DrawLine(from, to, Color.red);
        if (Input.GetButtonDown("Jump") && isGrounded)
            this.JumpActive = true;
        if (this.JumpActive)
        {
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }
        if (this.isGrounded)
            animator.SetBool("jump", false);
        else
            animator.SetBool("jump", true);
    }

}
