using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Material selectedCubeMaterial;
    [SerializeField] private Material normalCubeMaterial;

    // [SerializeField] private CubeType cubeType;
    [SerializeField] private Transform normalCubeParent;
    [SerializeField] private Transform selectedCubeParent;

    private Rigidbody rb;
    private ChooseCubeType chooseCubeType;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
        chooseCubeType = GetComponentInParent<ChooseCubeType>();

        AssignMaterials();
        ChooseParent();
    }

    private void AssignMaterials()
    {
        if (chooseCubeType.cubeType == CubeType.Normal)
        {
            // Assign the proper material to cube if its not selected.
            gameObject.GetComponent<MeshRenderer>().material = normalCubeMaterial;
        }
        else
        {
            // Assign the proper material to cube if its selected.
            gameObject.GetComponent<MeshRenderer>().material = selectedCubeMaterial;
        }
    }

    private void ChooseParent()
    {
        if (chooseCubeType.cubeType == CubeType.Normal)
        {
            // Put under Normal gameObject as child.
            gameObject.transform.parent = normalCubeParent.transform;
        }
        else
        {
            // Put under Selected gameObject as child.
            gameObject.transform.parent = selectedCubeParent.transform;
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (chooseCubeType.cubeType == CubeType.Selected)
            {
                // Change materials emission value to 1
                selectedCubeMaterial.EnableKeyword("_EMISSION");
            }
            else
            {
                // Fall down
                rb.useGravity = true;
            }
        }
    }

}

