using UnityEngine;

public class CameraModule : MonoBehaviour
{
    public Transform playerTransform = null;
    public float playerX = 0f;
    public float speed = 2f;

    private void Awake()
    {
        playerX = playerTransform.localPosition.x;
    }

    public void FixedUpdate()
    {
        float x = Mathf.Lerp(
                    transform.localPosition.x,
                    playerX + playerTransform.localPosition.x,
                    speed * Time.deltaTime);

        transform.localPosition = new Vector3(x, 0, 0);
    }
}
