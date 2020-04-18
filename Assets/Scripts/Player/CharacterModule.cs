using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModule : CharacterMovement
{
    private static CharacterModule instance = null;
    public static CharacterModule Get { get => instance; }

    public float MaxHp = 10f;
    public float Damage = 1f;

    private float currentHp;
    private CharacterInventory inventory = new CharacterInventory();

    public CharacterInventory GetInventory { get => inventory; }

    private void Awake()
    {
        instance = this;
        currentHp = MaxHp;
    }

}
