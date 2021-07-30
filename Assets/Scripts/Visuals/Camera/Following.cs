using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    public GameObject target;
    private Camera mainCamera;
    private Transform cursor;
    private FurtherLook furtherLook;
    private void Start() // Caching objects
    {
        // target = GameObject.Find("Player");
        mainCamera = Camera.main;
        furtherLook = mainCamera.GetComponent<FurtherLook>();
        cursor = GameObject.Find("Cursor").transform;
    }
    private void FixedUpdate()
    {
        
        if(!furtherLook.IsShifting)
        {
            mainCamera.transform.position = Vector2.Lerp(mainCamera.transform.position, target.transform.position, 0.1f);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10);
        }
        else
        {
            mainCamera.transform.position = Vector2.Lerp(mainCamera.transform.position, cursor.transform.position, 0.1f);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10);
        }
    }
}
