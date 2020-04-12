using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModule : CharacterMovement
{
    private static CharacterModule instance = null;
    public static CharacterModule Get { get => instance; }

    private void Awake()
    {
        instance = this;
    }

}
