using UnityEngine;

public class MonsterModule : MonsterMovement
{
    public GameObject moneyPrefab = null;
    public float MaxHp = 0;
    public int prize = 0;
    public float spawnTime = 5f;

    private float currentHp = 0;

    private void Awake()
    {
        currentHp = MaxHp;
    }

    protected override bool OnHit(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            rigid.simulated = false;
            Location.SetRespawnMonster(spawnTime);

            int min = prize / 2;
            int max = prize * 2;
            int count = Random.Range(min, max);
            if (count != 0)
            {
                for (int i = 0; i < count; ++i)
                    Instantiate(moneyPrefab, transform.position, Quaternion.identity, null);
            }

            return true;
        }
        return false;
    }


}
