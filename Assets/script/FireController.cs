using UnityEngine;

public class FireController : MonoBehaviour
{
    private bool isBeingExtinguished = false;
    private float extinguishSpeed = 1f;
    private Vector3 originalScale;

    void Start() { originalScale = transform.localScale; }

    void Update()
    {
        if (isBeingExtinguished)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * extinguishSpeed);
            if (transform.localScale.magnitude <= 0.05f)
            {
                gameObject.SetActive(false);
                isBeingExtinguished = false;
            }
        }
    }

    public void Extinguish() { isBeingExtinguished = true; }
}