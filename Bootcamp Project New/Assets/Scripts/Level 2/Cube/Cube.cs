using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Light pointLight;
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

        ChooseParent();
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

    private void TurnOnTheLight()
    {
        // Set active the light component.
        pointLight.gameObject.SetActive(true);
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
                // Turn on the light component.
                TurnOnTheLight();
            }
            else
            {
                // Fall down
                rb.useGravity = true;
            }
        }
    }

}

