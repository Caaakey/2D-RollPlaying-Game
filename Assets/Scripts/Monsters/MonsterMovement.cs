using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Animator animator = null;
    public float speed = 1f;

    private float moveTime = 0f;
    private bool isMove = false;
    private float RangeX = 0f;
    private float waitTime = 0f;

    public virtual void Create(float rangeX)
    {
        RangeX = rangeX;
        moveTime = Random.Range(1.5f, 3f);
    }

    private void Update()
    {
        CalculateMove();
    }

    private void CalculateMove()
    {
        if (isMove) return;

        if (moveTime <= waitTime)
        {
            targetLocation = Random.Range(-RangeX, RangeX);

            if (targetLocation < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isLeft = true;
            }
            else
            {
                transform.rotation = Quaternion.identity;
                isLeft = false;
            }

            waitTime = 0f;
            moveTime = Random.Range(1.5f, 3f);

            animator.Play("Move", 0);
            isMove = true;
            return;
        }

        waitTime += Time.deltaTime;
    }

    private float targetLocation = 0f;
    private bool isLeft = false;
    private void FixedUpdate()
    {
        if (!isMove) return;

        transform.Translate(speed * Time.fixedDeltaTime, 0, 0);

        if (isLeft)
        {
            if (targetLocation >= transform.localPosition.x)
                SetIdle();
        }
        else
        {
            if (targetLocation <= transform.localPosition.x)
                SetIdle();
        }

        void SetIdle()
        {
            animator.Play("Idle", 0);
            isMove = false;
        }
    }
}
