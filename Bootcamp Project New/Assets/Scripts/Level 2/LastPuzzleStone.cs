using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPuzzleStone : MonoBehaviour
{
    [SerializeField] private int choiceNumber;
    [SerializeField] private Light activationLight;
    
    private LastDoorLockCheck lastDoorLockCheck;
    private ResetAllCubePositions resetAllCubePositions;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        lastDoorLockCheck = FindObjectOfType<LastDoorLockCheck>();
        resetAllCubePositions = FindObjectOfType<ResetAllCubePositions>();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (choiceNumber == lastDoorLockCheck.ChoiceNumber)
        {
            activationLight.gameObject.SetActive(true);
            lastDoorLockCheck.IncreaseChoiceNumber();
        }
        else
        {
            resetAllCubePositions.RestartAllCubesLights();

            // Reset the choice number in check script
            lastDoorLockCheck.ResetChoiceNumber();
        }
            
    }

    public void ResetCubeLights()
    {
        activationLight.gameObject.SetActive(false);
    }
}
