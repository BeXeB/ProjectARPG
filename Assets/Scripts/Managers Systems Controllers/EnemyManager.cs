using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyManager instance;

    List<GameObject> enemies;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
}
