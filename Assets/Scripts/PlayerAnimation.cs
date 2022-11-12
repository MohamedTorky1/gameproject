using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float lastMoveTime;

    private bool isJumping;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        lastMoveTime= Time.time;
    }

    void Update()
    {
        float horizonal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        animateCharacter(horizonal, vertical);
    }
    
    void animateCharacter(float horizonal, float vertical)
    {
        if (!isJumping)
        {
            if (Mathf.Abs(horizonal) > 0)
            {
                animator.SetBool("idle2", false);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    animator.SetBool("run", true);
                    animator.SetBool("walk", false);
                }
                else
                {
                    animator.SetBool("run", false);
                    animator.SetBool("walk", true);
                }
                if (horizonal < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
                lastMoveTime = Time.time;
            }
            else
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
            }
            if (Time.time - lastMoveTime > 4)
            {
                animator.SetBool("idle2", true);
                lastMoveTime = Time.time;
                StartCoroutine(WaitForUdleDone());
            }
            if (vertical > 0)
            {
                animator.SetBool("idle2", false);
                isJumping = true;
            }
        }
        animator.SetBool("jump", isJumping);
    }

    private IEnumerator WaitForUdleDone()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("idle2", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isJumping)
            isJumping = false;
    }
}
