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
    }
}
