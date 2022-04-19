using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLocking : MonoBehaviour
{
    [SerializeField] private int fps = 30;

    private void Awake() {
        Application.targetFrameRate = fps;
    }
}
