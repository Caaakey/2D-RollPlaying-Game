using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 9);
    }


}
