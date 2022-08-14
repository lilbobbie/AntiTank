using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static readonly PlayerManager _instance = new PlayerManager();

    public float health;

    private PlayerManager()
    {

    }

    public float GetHealth()
    {
        float output = health;
        return health;
    }
}
