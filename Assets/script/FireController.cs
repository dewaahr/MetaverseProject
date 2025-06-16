using UnityEngine;

public class FireController : MonoBehaviour
{
    private bool isBeingExtinguished = false;
    private float extinguishSpeed = 1f;
    private Vector3 originalScale;
    private ParticleSystem[] childParticleSystems;

    void Start()
    {

    }

    void Update()
    {
        if (isBeingExtinguished)
        {
            // transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * extinguishSpeed);

            // foreach (var ps in childParticleSystems)
            // {
            //     var emission = ps.emission;
            //     emission.rateOverTime = Mathf.Lerp(emission.rateOverTime.constant, 0, Time.deltaTime * extinguishSpeed);
            // }

            // if (transform.localScale.magnitude <= 0.05f)
            // {
            //     gameObject.SetActive(false);
            //     isBeingExtinguished = false;
            // }
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * extinguishSpeed);
            if (transform.localScale.magnitude <= 0.05f)
            {
                // gameObject.SetActive(false);
                isBeingExtinguished = false;
                Destroy(gameObject);
            }



            // gameObject.SetActive(false);
        }
    }

    public void Extinguish()
    {
        isBeingExtinguished = true;
    }
}