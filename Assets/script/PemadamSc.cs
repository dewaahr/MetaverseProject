using UnityEngine;

public class PemadamSc : MonoBehaviour
{

    void OnParticleCollision(GameObject other)
    {

        Debug.Log("Particle collision with: " + other.name);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            FireController fire = other.GetComponentInParent<FireController>();
            if (fire != null)
            {
                fire.Extinguish();
            }
        }
    }
}
