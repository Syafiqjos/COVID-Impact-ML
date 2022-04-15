using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool isEnable = true;
    public Transform target;
    public float lerpRatio = 0.1f;
    public Rect observationBorder = new Rect();

    private void LateUpdate()
    {
        if (isEnable)
        {
            if (target)
            {
                Vector3 pos = Vector3.Lerp(transform.position, target.position, lerpRatio);
                pos.z = transform.position.z;

                pos.x = Mathf.Clamp(pos.x, observationBorder.x, observationBorder.x + observationBorder.width);
                pos.y = Mathf.Clamp(pos.y, observationBorder.y, observationBorder.y + observationBorder.height);

                transform.position = pos;
            }
        }
    }
}
