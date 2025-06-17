using UnityEngine;

public class PemadamSc : MonoBehaviour
{

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Terdeteksi mengenai: " + other.name);
        if (other.CompareTag("Fire"))
        {
            FireController fire = other.GetComponentInParent<FireController>();
            if (fire != null)
            {
                fire.Extinguish();
                // Debug.Log("Api berhasil dipadamkan.");
            }

        }
    }

}
