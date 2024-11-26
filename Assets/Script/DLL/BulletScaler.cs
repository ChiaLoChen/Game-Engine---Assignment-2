using UnityEngine;
using System.Runtime.InteropServices;

public class BulletScaler : MonoBehaviour
{
    // Import the ScaleBullet function from the C++ plugin (DLL)
    [DllImport("CourseProject_Plugins", CallingConvention = CallingConvention.Cdecl)]
    private static extern void ScaleBullet(float scaleFactor);

    // Public variable to hold the bullet prefab reference
    public GameObject bulletPrefab;

    // Bullet scaling factor
    private float scaleFactor = 2.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right-click (Button 1)
        {
            // When right-click is detected, scale the bullet using the C++ plugin
            ScaleBullet(scaleFactor);
        }
    }

    // Optional: Spawn and scale the bullet if needed
    public void SpawnAndScaleBullet(Vector3 position)
    {
        if (bulletPrefab != null)
        {
            // Instantiate the bullet prefab and apply the scaling
            GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
            bullet.transform.localScale *= scaleFactor;
        }
        else
        {
            Debug.LogError("Bullet prefab is not assigned!");
        }
    }
}