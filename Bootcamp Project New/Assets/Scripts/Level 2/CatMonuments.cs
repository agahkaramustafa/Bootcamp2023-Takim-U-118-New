using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMonuments : MonoBehaviour
{
    private Vector3 startMarker;
    private Vector3 endMarker;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip movingStoneClip;
    [SerializeField] private int choiceNumber;
    [SerializeField] [Range(0,5)] private int interactionRange = 3;

    private AudioSource audioSource;
    private DoorLockCheck doorLockCheck;
    private ResetAllCatPositions resetAllCatPositions;
    private Transform playerTransform;
    private float distanceToPlayer = Mathf.Infinity;
    private float t = 0f;
    private bool isDown = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        startMarker = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endMarker = new Vector3(transform.position.x, -1f, transform.position.z);

        playerTransform = FindObjectOfType<Player>().transform;
        audioSource = GetComponent<AudioSource>();
        resetAllCatPositions = GetComponentInParent<ResetAllCatPositions>();
        doorLockCheck = FindObjectOfType<DoorLockCheck>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {

        distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceToPlayer <= interactionRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isDown)
            {
                if (choiceNumber == doorLockCheck.ChoiceNumber)
                {
                    isDown = true;
                    // move down the cat monument with LERP
                    StartCoroutine(MoveMonumentRoutine(startMarker, endMarker));

                    // Increase choice number in check script
                    doorLockCheck.IncreaseChoiceNumber();
                }
                else
                {
                    // Move up all of the monuments
                    // BroadcastMessage(nameof(ResetMonumentPositions));
                    resetAllCatPositions.RestartAllCatsPositions();

                    // Reset the choice number in check script
                    doorLockCheck.ResetChoiceNumber();
                }
            }   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void ResetMonumentPositions()
    {
        // use LERP to get them in their initial position
        StartCoroutine(MoveMonumentRoutine(transform.position, startMarker));
        isDown = false;
    }

    IEnumerator MoveMonumentRoutine(Vector3 startPos, Vector3 endPos)
    {
        audioSource.PlayOneShot(movingStoneClip);
        while (t <= .99f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t);
            t += Time.deltaTime * 0.35f;
            yield return new WaitForEndOfFrame();
        }

        transform.position = endPos;
        t = 0;

        StopCoroutine("MoveMonumentRoutine");
    }
}
