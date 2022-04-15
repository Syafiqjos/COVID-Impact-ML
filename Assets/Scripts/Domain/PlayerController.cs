using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool enableController = true;
    public MovementController controller;
    public RaycastCatcher distanceMeasurer;

    private void Update()
    {
        ControlMovement();
    }

    private void ControlMovement() {
        if (enableController)
        {
            float mx = Input.GetAxisRaw("Horizontal");
            float my = Input.GetAxisRaw("Vertical");

            controller.Move(new Vector2(mx, my));
        } else
        {
            controller.Move(new Vector2(0, 0));
        }
    }
}
