using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2EndCheck : MonoBehaviour
{
    GameManager gameManager;
    LastDoorLockCheck lastDoorLockCheck;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        lastDoorLockCheck = FindObjectOfType<LastDoorLockCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastDoorLockCheck.ChoiceNumber == 4)
        {
            gameManager.NextLevel();
        }
    }
}
