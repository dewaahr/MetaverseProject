using UnityEngine;

public class PemadamSc : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire"))
        {
            FireController fire = other.GetComponent<FireController>();
            if (fire != null)
            {
                fire.Extinguish();
            }
        }
        Debug.Log("Particle collision with: " + other.name);
    }
}
