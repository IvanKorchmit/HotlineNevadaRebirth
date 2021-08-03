using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private Transform cursor;
    private void Start()
    {
        cursor = GameObject.Find("Cursor").transform;
    }
    private void Update()
    {
        Vector3 mousePosition = cursor.position;
        Vector3 perpendicular = Vector3.Cross(transform.position - mousePosition, Vector3.forward);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }
}
