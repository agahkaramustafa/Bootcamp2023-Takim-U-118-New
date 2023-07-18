using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllCubePositions : MonoBehaviour
{
    public void RestartAllCubesLights()
    {
        BroadcastMessage("ResetCubeLights");
    }
}
