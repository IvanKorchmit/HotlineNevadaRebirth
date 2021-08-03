using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Shake : MonoBehaviour
{
    private static Camera mainCamera;
    private const float POWER = 0.1f;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    public static void ShakeCamera (float duration)
    {
        for (float i = 0; i < duration; i += Time.deltaTime) 
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + Random.Range(-POWER, POWER), mainCamera.transform.position.y + Random.Range(-POWER, POWER), mainCamera.transform.position.z);
        }
    }
}
