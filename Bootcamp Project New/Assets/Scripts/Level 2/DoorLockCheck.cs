using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockCheck : MonoBehaviour
{
    public int ChoiceNumber { get { return choiceNumber; } }
    private int choiceNumber = 0;

    private bool isClosed = true;
    private float t = 0f;
    private Vector3 endPos;
    private AudioSource audioSource;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        endPos = new Vector3(transform.position.x, 8f, transform.position.z);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (choiceNumber == 4 && isClosed)
        {
            // open the gate function with LERP
            isClosed = false;
            StartCoroutine(OpenDoorRoutine(transform.position, endPos));
        }
    }

    public void IncreaseChoiceNumber()
    {
        choiceNumber += 1;
    }

    public void ResetChoiceNumber()
    {
        choiceNumber = 0;
    }

    IEnumerator OpenDoorRoutine(Vector3 startPos, Vector3 endPos)
    {
        audioSource.Play();
        while (t <= .99f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t);
            t += Time.deltaTime * 0.2f;
            yield return new WaitForEndOfFrame();
        }

        transform.position = endPos;
        t = 0;

        audioSource.Stop();
        StopCoroutine("MoveMonumentRoutine");
    }
}
