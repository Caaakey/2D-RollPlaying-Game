using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Animator animator = null;
    public Rigidbody2D rigid = null;
    public float speed = 1.5f;
    public float jumpPower = 10f;

    private bool isMove = false;
    private bool isReverse = false;

    private void Update()
    {
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

        if (animator.GetBool("isJump"))
        {
            if (rigid.velocity.y < 0)
            {
                animator.SetBool("isJumpDown", true);
            }
        }
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        animator.SetBool("isJump", false);
    }

    private void Jump()
    {

    }

}
