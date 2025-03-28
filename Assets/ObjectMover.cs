using System.Collections;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;
    [SerializeField] private Vector3 moveOffset;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private AudioSource doorSound;

    private Vector3 leftClosedPos, leftOpenPos;
    private Vector3 rightClosedPos, rightOpenPos;
    private bool isOpen = false;

    private void Start()
    {
        leftClosedPos = leftDoor.position;
        leftOpenPos = leftClosedPos + moveOffset;

        rightClosedPos = rightDoor.position;
        rightOpenPos = rightClosedPos - moveOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Enemy")) && !isOpen)
        {
            StartCoroutine(MoveDoor(true));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Enemy")) && isOpen)
        {
            StartCoroutine(MoveDoor(false));
        }
    }

    private IEnumerator MoveDoor(bool open)
    {
        isOpen = open;
        doorSound?.Play();
        float elapsed = 0f;
        Vector3 leftStart = open ? leftClosedPos : leftOpenPos;
        Vector3 leftTarget = open ? leftOpenPos : leftClosedPos;
        Vector3 rightStart = open ? rightClosedPos : rightOpenPos;
        Vector3 rightTarget = open ? rightOpenPos : rightClosedPos;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;
            leftDoor.position = Vector3.Lerp(leftStart, leftTarget, t);
            rightDoor.position = Vector3.Lerp(rightStart, rightTarget, t);
            yield return null;
        }

        leftDoor.position = leftTarget;
        rightDoor.position = rightTarget;
    }
}