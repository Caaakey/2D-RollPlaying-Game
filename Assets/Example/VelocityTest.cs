using UnityEngine;

public class VelocityTest : MonoBehaviour
{
    public Rigidbody2D rigid = null;
    public float force = 10f;
    public float maxCameraPosX;

    public void OnHit()
    {
        transform.position = new Vector3(
            maxCameraPosX - 0.01f, transform.position.y, 0);

        Vector2 velocity = new Vector2(-rigid.velocity.x, 100f * Time.fixedDeltaTime);

        rigid.velocity = Vector3.zero;
        rigid.velocity = velocity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            rigid.AddForce(new Vector2(force * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
    }

    private bool isHit = false;
    private void FixedUpdate()
    {
        if (isHit) return;
        if (maxCameraPosX < transform.position.x)
        {
            isHit = true;
            OnHit();
        }
    }


}