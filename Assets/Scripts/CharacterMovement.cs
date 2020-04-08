using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rigid = null;
    public Animator animator = null;
    public CircleCollider2D bottomCollider = null;

    public float speed = 0f;
    public float jumpPower = 0f;

    private bool isLeft = false;
    private bool isLock = false;

    public void Lock() => isLock = true;
    public void UnLock() => isLock = false;

    private void Update()
    {
        UpdateMove();
        UpdateAction();
    }

    private void UpdateMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isMove", true);
            if (!isLock && !isLeft)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isLeft = true;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isMove", true);
            if (!isLock && isLeft)
            {
                transform.rotation = Quaternion.identity;
                isLeft = false;
            }
        }
        else
            animator.SetBool("isMove", false);

        if (!animator.GetBool("isJump") && !animator.GetBool("isJumpDown"))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                animator.SetBool("isJump", true);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }
    }

    private void UpdateJumping()
    {
        if (rigid.velocity.y == 0) return;
        if (rigid.velocity.y <= -Mathf.Epsilon)
        {
            bottomCollider.enabled = true;
            animator.SetBool("isJumpDown", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isJumpDown", false);

            if (rigid.velocity.y == 0) bottomCollider.enabled = false;
        }
    }

    private void UpdateAction()
    {
        if (isLock) return;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Attack");
            Lock();
        }
    }

    private void FixedUpdate()
    {
        UpdateJumping();

        if (animator.GetBool("isMove"))
        {
            if (isLock)
            {
                if (!animator.GetBool("isJump")) return;
            }

            transform.Translate(speed * Time.fixedDeltaTime, 0, 0);
        }
        
    }

}
