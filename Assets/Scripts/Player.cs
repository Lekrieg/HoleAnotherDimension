using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	Animator playerAnim;
	Rigidbody2D playerRb;

	private bool isFacingRight = true;
	private bool isWalking;
	private bool isGrounded;
	private bool canJump;

	private float inputDirection;
	[SerializeField]
	private float movementSpeed = 10.0f;
	[SerializeField]
	private float jumpForce = 16.0f;
	[SerializeField]
	private int amountOfJumps = 1;
	private int amountOfJumpsLeft;

	[SerializeField]
	private LayerMask whatIsGround;
	[SerializeField]
	private Transform groundCheck;
	[SerializeField]
	private float groundCheckRadius;

	void Start()
	{
		playerAnim = GetComponent<Animator>();
		playerRb = GetComponent<Rigidbody2D>();

		amountOfJumpsLeft = amountOfJumps;
	}
	void Update()
	{
		CheckInput();
		CheckMovementDirection();
		UpdateAnimations();
		CheckIfCanJump();
	}
	void FixedUpdate()
	{
		ApplyMovement();
		CheckSurroundings();
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

		Debug.Log(playerRb.velocity.x);
		if(playerRb.velocity.x != 0)
		{
			isWalking = true;
		}
		else
		{
			isWalking = false;
		}
	}
	private void Jump()
	{
		if(canJump)
		{
			playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
			amountOfJumpsLeft--;
		}
	}
	private void UpdateAnimations()
	{
		playerAnim.SetBool("IsWalking", isWalking);
		playerAnim.SetBool("IsGrounded", isGrounded);
		playerAnim.SetFloat("yVelocity", playerRb.velocity.y);
	}
	private void CheckSurroundings()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
	}
	private void CheckIfCanJump()
	{
		if(isGrounded && playerRb.velocity.y <= 0)
		{
			amountOfJumpsLeft = amountOfJumps;
		}
		
		if(amountOfJumpsLeft  <= 0)
		{
			canJump = false;
		}
		else
		{
			canJump = true;
		}
	}

	private void Flip()
	{
		isFacingRight = !isFacingRight;

		transform.Rotate(0.0f, 180.0f, 0.0f);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
	}
}
