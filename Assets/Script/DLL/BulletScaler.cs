using System;
using UnityEngine;
using System.Runtime.InteropServices;

public class BulletScaler : MonoBehaviour
{
    // Import the ScaleBullet function from the C++ plugin (DLL)
    [DllImport("CourseProject_Plugins", CallingConvention = CallingConvention.Cdecl)]
    private static extern void ScaleBullet(float scaleFactor);

    public GameObject bulletPrefab;

    private float scaleFactor = 2.0f;

    void Update()
    {
        try
        {
            if (Input.GetMouseButtonDown(1)) // Right-click
            {
                ScaleBullet(scaleFactor); // Call the DLL function
            }
        }
        catch (DllNotFoundException ex)
        {
            Debug.LogError("DLL not found: " + ex.Message);
        }
        catch (EntryPointNotFoundException ex)
        {
            Debug.LogError("Function not found in DLL: " + ex.Message);
        }
    }

    public void SpawnAndScaleBullet(Vector3 position)
    {
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
            bullet.transform.localScale *= scaleFactor;
        }
        else
        {
            Debug.LogError("Bullet prefab is not assigned!");
        }
    }
}