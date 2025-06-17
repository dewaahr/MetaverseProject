using UnityEngine;

public class FireController : MonoBehaviour
{
    [Header("Visual Api")]
    public ParticleSystem fireParticles;
    public float extinguishRate = 0.5f;

    [Header("Overlap Cek Player")]
    public float detectionRadius = 1f;  // Radius deteksi api
    public LayerMask playerLayer;       // Layer untuk player
    public bool showDebugSphere = true; // Tampilkan debug sphere di editor

    private bool isBeingExtinguished = false;
    private bool isExtinguished = false;

    private GameManager gameManager;

    void Start()
    {
        if (fireParticles == null)
            fireParticles = GetComponent<ParticleSystem>();

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
            Debug.LogError("GameManager not found in the scene.");
    }

    void Update()
    {
        if (!isExtinguished)
        {
            CekPlayerKenaApi();
        }

        if (isBeingExtinguished)
        {
            var main = fireParticles.main;
            float size = main.startSize.constant - extinguishRate * Time.deltaTime;
            size = Mathf.Max(size, 0.1f);
            main.startSize = new ParticleSystem.MinMaxCurve(size);

            if (size <= 0.1f)
            {
                fireParticles.Stop();
                isBeingExtinguished = false;
                isExtinguished = true;
                gameObject.SetActive(false); // Optional: hilangkan api dari scene
            }
        }
    }

    void CekPlayerKenaApi()
    {
        // Gunakan Physics.OverlapSphere untuk mendeteksi pemain di radius tertentu
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Debug.Log("Player terkena api! Game Over.");
                gameManager?.GameOver(); // Langsung panggil GameOver
                break; // Keluar dari loop setelah mendeteksi pemain
            }
        }
    }

    public void Extinguish()
    {
        if (!isExtinguished)
        {
            isBeingExtinguished = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!showDebugSphere) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
