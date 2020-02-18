using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	Animator playerAnim;
	Rigidbody2D playerRb;

	// --- bool variables for checking things ---
	private bool isFacingRight = true;
	private bool isWalking;
	private bool isGrounded;
	private bool isTouchingWall;
	private bool isWallSliding;
	private bool canJump;
	// ------------------------------------------
	
	// --- Player stuff ---
	private float inputDirection;
	[SerializeField]
	private float movementSpeed = 10.0f;
	[SerializeField]
	private float jumpForce = 16.0f;
	[SerializeField]
	private int amountOfJumps = 1;
	private int amountOfJumpsLeft;
	// ------------------------------------------

	// --- check variables ---
	[SerializeField]
	private LayerMask whatIsGround;
	[SerializeField]
	private Transform groundCheck;
	[SerializeField]
	private float groundCheckRadius;
	[SerializeField]
	private Transform wallCheck;
	[SerializeField]
	private float wallCheckDistance;
	[SerializeField]
	private float wallSlidingSpeed;
	[SerializeField]
	private float movementForceInAir;
	[SerializeField]
	private float airDragMultiplier = 0.95f;
	[SerializeField]
	private float variableJumpHeightMultiplayer = 0.5f;
	// ------------------------------------------

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
		CheckIfWallSliding();
	}
	void FixedUpdate()
	{
		ApplyMovement();
		CheckSurroundings();
	}

	// --- This method will check every input from the player ---
	private void CheckInput()
	{
		inputDirection = Input.GetAxisRaw("Horizontal");

		if(Input.GetButtonDown("Jump"))
		{
			Jump();
		}

		if(Input.GetButtonUp("Jump"))
		{
			playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y * variableJumpHeightMultiplayer);
		}
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
		if(playerRb.velocity.x > 0.1 || playerRb.velocity.x < -0.1)
		{
			isWalking = true;
		}
		else
		{
			isWalking = false;
		}
	}
	private void CheckSurroundings()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
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
	private void CheckIfWallSliding()
	{
		if(isTouchingWall && !isGrounded && playerRb.velocity.y < 0)
		{
			isWallSliding = true;
		}
		else
		{
			isWallSliding = false;
		}
	}

	private void ApplyMovement()
	{
		if(isGrounded)
		{
			playerRb.velocity = new Vector2(movementSpeed * inputDirection, playerRb.velocity.y);
		}
		else if(!isGrounded && !isWallSliding && (inputDirection != 0))
		{
			Vector2 forceToAdd = new Vector2(movementForceInAir * inputDirection, 0);
			playerRb.AddForce(forceToAdd);

			if(Mathf.Abs(playerRb.velocity.x) > movementSpeed)
			{
				playerRb.velocity = new Vector2(movementSpeed * inputDirection, playerRb.velocity.y);
			}
		}
		else if(!isGrounded && !isWallSliding && inputDirection == 0)
		{
			playerRb.velocity = new Vector2(playerRb.velocity.x * airDragMultiplier, playerRb.velocity.y);
		}

		if(isWallSliding)
		{
			if(playerRb.velocity.y < -wallSlidingSpeed)
			{
				playerRb.velocity = new Vector2(playerRb.velocity.x, -wallSlidingSpeed);
			}
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
		playerAnim.SetBool("IsWallSliding", isWallSliding);
	}

	private void Flip()
	{
		if(!isWallSliding)
		{
			isFacingRight = !isFacingRight;
			transform.Rotate(0.0f, 180.0f, 0.0f);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
		Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
	}
}
