using System.Collections;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    private GameObject player;
    public GameObject doorPanel;
    public float rotationDuration = 1f;
    public float openAngle = 90f;
    public float interactDistance = 2f;

    private bool isRotating = false;
    private bool isOpen = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (doorPanel != null)
        {
            closedRotation = doorPanel.transform.rotation;
            openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isRotating && player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < interactDistance)
            {
                StartCoroutine(RotateDoor(isOpen ? closedRotation : openRotation));
                // // transform.Rotate(90, 0, 0);
                isOpen = !isOpen;
            }
        }
        // doorPanel.transform.Rotate(Vector3.up * 90 * Time.deltaTime);
        
    }

    IEnumerator RotateDoor(Quaternion targetRotation)
    {
        isRotating = true;
        Quaternion startRotation = doorPanel.transform.rotation;
        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            doorPanel.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / rotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        doorPanel.transform.rotation = targetRotation;
        isRotating = false;
    }
}
