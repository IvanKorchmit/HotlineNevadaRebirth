using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private Transform cursor;
    private Transform visuals;
    private void Start()
    {
        cursor = GameObject.Find("Cursor").transform;
        visuals = transform.Find("Visual");
    }
    private void Update()
    {
        Vector3 mousePosition = cursor.position;
        Vector3 perpendicular = Vector3.Cross(transform.position - mousePosition, Vector3.forward);
        visuals.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }
}
