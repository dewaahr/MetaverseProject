using UnityEngine;

public class ItemShooter : MonoBehaviour
{
    public ParticleSystem shotSmoke;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!shotSmoke.isPlaying)
                shotSmoke.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (shotSmoke.isPlaying)
                shotSmoke.Stop();
        }
        // }
        // void OnParticleCollision(GameObject other)
        // {
        //     FireController fire = other.GetComponentInParent<FireController>();
        //     if (fire != null && other.CompareTag("Fire"))
        //     {
        //         fire.Extinguish();
        //     }
        //     Debug.Log("Particle collision with: " + other.name);
        // }

    }
}
