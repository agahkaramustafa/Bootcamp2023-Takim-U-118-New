using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEmissionPart : MonoBehaviour
{
    private Material material;
    private MeshRenderer meshRenderer;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = GetComponent<Material>();
    }

    public void EnableEmission(float activatedTime)
    {
        StartCoroutine(nameof(EnableEmissionRoutine), activatedTime);
    }

    IEnumerator EnableEmissionRoutine(float activatedTime)
    {
        meshRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(activatedTime);
        meshRenderer.material.DisableKeyword("_EMISSION");
    }
    
}
