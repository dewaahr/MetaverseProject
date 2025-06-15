using UnityEngine;

public class FireController : MonoBehaviour
{
    private bool isBeingExtinguished = false;
    private float extinguishSpeed = 1f;
    private Vector3 originalScale;
    private ParticleSystem[] childParticleSystems;

    void Start()
    {
        originalScale = transform.localScale;
        childParticleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (isBeingExtinguished)
        {
            // Gradually scale down the object
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * extinguishSpeed);

            // Gradually reduce emission rates of all child particle systems
            foreach (var ps in childParticleSystems)
            {
                var emission = ps.emission;
                emission.rateOverTime = Mathf.Lerp(emission.rateOverTime.constant, 0, Time.deltaTime * extinguishSpeed);
            }

            // Disable the object when fully extinguished
            if (transform.localScale.magnitude <= 0.05f)
            {
                gameObject.SetActive(false);
                isBeingExtinguished = false;
            }
        }
    }

    public void Extinguish()
    {
        isBeingExtinguished = true;
    }
}