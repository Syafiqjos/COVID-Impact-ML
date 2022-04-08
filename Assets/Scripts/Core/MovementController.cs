using System.Collections;
using System.Collections.Generic;
using UnityEngine;
							 
public class MovementController : MonoBehaviour
{
	[SerializeField]
	private float movementSpeed;
	
	private Rigidbody2D rigidbody2d;
	
    void Awake()
    {
		rigidbody2d = GetComponent<Rigidbody2D>();
    }
	
	public void Move(Vector2 direction) {
        Vector2 normalizedDirection = direction.normalized;

		rigidbody2d.velocity = new Vector2(normalizedDirection.x * movementSpeed, normalizedDirection.y * movementSpeed);
	}
}
