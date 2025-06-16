using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepClimber : MonoBehaviour
{
    public Transform rayStart;         // Posisi depan kaki karakter
    public float rayDistance;          // Jarak raycast ke depan
    public float stepHeight;           // Tinggi maksimum anak tangga
    public float stepSmooth;           // Seberapa halus karakter naik

    void FixedUpdate()
    {
        // Ray dari bawah (untuk deteksi halangan)
        RaycastHit hitLower;
        Vector3 lowerRayOrigin = rayStart.position + Vector3.up * 0.05f;
        if (Physics.Raycast(lowerRayOrigin, transform.forward, out hitLower, rayDistance))
        {
            // Ray dari atas (cek apakah bisa dinaiki)
            RaycastHit hitUpper;
            Vector3 upperRayOrigin = rayStart.position + Vector3.up * stepHeight;
            if (!Physics.Raycast(upperRayOrigin, transform.forward, out hitUpper, rayDistance))
            {
                // Naikkan karakter sedikit
                transform.position += new Vector3(0, stepSmooth, 0);
            }
        }
    }

    // Draw Gizmos to visualize raycasts
    void OnDrawGizmos()
    {
        if (rayStart == null) return;

        // Lower ray (for detecting obstacles)
        Gizmos.color = Color.red;
        Vector3 lowerRayOrigin = rayStart.position + Vector3.up * 0.05f;
        Gizmos.DrawLine(lowerRayOrigin, lowerRayOrigin + transform.forward * rayDistance);

        // Upper ray (for checking step clearance)
        Gizmos.color = Color.green;
        Vector3 upperRayOrigin = rayStart.position + Vector3.up * stepHeight;
        Gizmos.DrawLine(upperRayOrigin, upperRayOrigin + transform.forward * rayDistance);
    }
}