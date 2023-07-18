using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1EndCheck : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            gameManager.NextLevel();
        }
    }
}
