using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private Transform Player;
    private Camera mainCamera;
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = mousePos + Player.transform.position - mainCamera.transform.position; // Change cursor position relative to player
        Vector3 perpendicular = Vector3.Cross(Player.transform.position - mousePos, Vector3.forward);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }
}
