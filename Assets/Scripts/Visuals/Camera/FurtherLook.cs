using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurtherLook : MonoBehaviour
{
    public bool IsShifting = false;
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IsShifting = true;
        }
        else IsShifting = false;
    }
}
