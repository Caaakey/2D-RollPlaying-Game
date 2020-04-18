using UnityEngine;

public class MonsterModule : MonsterMovement
{
    public float MaxHp = 0;

    private float currentHp = 0;

    private void Awake()
    {
        currentHp = MaxHp;
    }

    protected override bool OnHit(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0) return true;
        return false;
    }


}
