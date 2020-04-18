using UnityEngine;

public class MoneyModule : MonoBehaviour
{
    private Rigidbody2D rigid = null;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(
            Random.Range(-0.35f, 0.35f),
            Random.Range(0.3f, 0.6f));

        rigid.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")
            || collision.name.Equals("Effector")) return;

        CharacterModule.Get.GetInventory.money++;
        rigid.simulated = false;
        Destroy(gameObject);
    }
    
}
