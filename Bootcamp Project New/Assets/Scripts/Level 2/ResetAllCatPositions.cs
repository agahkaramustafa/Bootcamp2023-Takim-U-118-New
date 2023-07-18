using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllCatPositions : MonoBehaviour
{
    public void RestartAllCatsPositions()
    {
        BroadcastMessage("ResetMonumentPositions");
    }

}
