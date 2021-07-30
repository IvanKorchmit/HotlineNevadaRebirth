using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGradient : MonoBehaviour
{
    public Gradient gradient;
    private Camera mainCamera;
    [SerializeField] private float currentState = 0;
    [SerializeField] private float time;
    private bool isGoingDown = false;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if(!isGoingDown) // Checks if it's true, then increases the time of gradient.
        {
            currentState += Time.deltaTime;
            if(currentState >= time)
            {
                isGoingDown = true;
            }
        }
        else // Or else decreases so the time goes down and it creates an infinite loop
        {
            currentState -= Time.deltaTime;
            if (currentState <= 0) // If it reaches zero, the loop repeats and time goes up.
            {
                isGoingDown = false;
            }
        }
        mainCamera.backgroundColor = gradient.Evaluate(currentState / time); // Applies color to current state
    }
}
