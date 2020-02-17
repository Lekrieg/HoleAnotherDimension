using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	Rigidbody2D playerRb;

	private bool isFacingRight = true;

	private float inputDirection;
	[SerializeField]
	private float movementSpeed = 10.0f;
	[SerializeField]
	private float jumpForce = 16.0f;

	void Start()
	{
		playerRb = GetComponent<Rigidbody2D>();
	}
	void Update()
	{
		CheckInput();
		CheckMovementDirection();
	}
	void FixedUpdate()
	{
		ApplyMovement();
	}

	// This method will check every input from the player
	private void CheckInput()
	{
		inputDirection = Input.GetAxisRaw("Horizontal");

		if(Input.GetButtonDown("Jump"))
		{
			Jump();
		}
	}
	private void ApplyMovement()
	{
		playerRb.velocity = new Vector2(movementSpeed * inputDirection, playerRb.velocity.y);
	}
	private void CheckMovementDirection()
	{
		if(isFacingRight && inputDirection < 0)
		{
			Flip();
		}
		else if(!isFacingRight && inputDirection > 0)
		{
			Flip();
		}
	}
	private void Jump()
	{
		playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
	}

	private void Flip()
	{
		isFacingRight = !isFacingRight;

		transform.Rotate(0.0f, 180.0f, 0.0f);
	}
}
