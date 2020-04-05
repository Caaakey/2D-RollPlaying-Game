using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Animator animator = null;
    public Rigidbody2D rigid = null;
    public CircleCollider2D bottomCollider = null;
    public float speed = 1.5f;
    public float jumpPower = 5f;
    public AnimationClip attackClip = null;
    public AnimationClip airAttackClip = null;

    private bool isMove = false;
    private bool isReverse = false;
    private bool isDontMove = false;
    
    private void Update()
    {
        OnAttackControll();
        OnMoveControll();
    }

    private void OnAttackControll()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Attack");

            if (animator.GetBool("isJump") || animator.GetBool("isJumpDown"))
            {
                CancelInvoke("OnMoveContinue");
                Invoke("OnMoveContinue", airAttackClip.length);
            }
            else
            {
                isMove = false;
                isDontMove = true;

                CancelInvoke("OnMoveContinue");
                Invoke("OnMoveContinue", attackClip.length);
            }
        }
    }

    private void OnMoveControll()
    {
        if (isDontMove) return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(false);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(true);
        }
        else
        {
            isMove = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!animator.GetBool("isJump"))
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                animator.SetBool("isJump", true);
                animator.SetBool("isJumpDown", false);
            }
        }

        animator.SetBool("isMove", isMove);
    }

    private void FixedUpdate()
    {
        if (isMove)
            transform.Translate(speed * Time.fixedDeltaTime, 0, 0);

        Jumping();
    }

    private void Move(bool isLeft)
    {
        isMove = true;

        if (isReverse != isLeft)
        {
            //  삼항 연산자
            //  isLeft ? -> if (isLeft == true)
            //  !isLeft ? -> if (isLeft != true)
            transform.localRotation =
                isLeft ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

            isReverse = isLeft;
        }
    }

    private void Jumping()
    {
        if (rigid.velocity.y == 0) return;
        if (rigid.velocity.y <= -Mathf.Epsilon)
        {
            bottomCollider.enabled = true;
            animator.SetBool("isJumpDown", true);
            animator.SetBool("isMove", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isJumpDown", false);

            if (rigid.velocity.y == 0)
                bottomCollider.enabled = false;
        }
    }

    private void OnMoveContinue()
    {
        animator.ResetTrigger("Attack");
        isDontMove = false;
    }

}
