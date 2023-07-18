using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private AudioSource audioSource;
    private DoorEmissionPart doorEmissionPart;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        doorEmissionPart = GetComponentInChildren<DoorEmissionPart>();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip);
            doorEmissionPart.EnableEmission(12f);
        }
    }

}
