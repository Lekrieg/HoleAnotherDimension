using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	Animator playerAnim;
	Rigidbody2D playerRb;

	WorldPortalManager wpManager;

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
	[SerializeField]
	private float wallHopForce;
	[SerializeField]
	private float wallJumpForce;
	private int facingDirection = 1;
	// ------------------------------------------

	// --- Vectors that determine the direction that we jump off the walls ---
	[SerializeField]
	private Vector2 wallHopDirection;
	[SerializeField]
	private Vector2 wallJumpDirection;
	// ------------------------------------------

	// --- check variables ---
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

	private void Awake()
	{
		wpManager = GameObject.FindObjectOfType<WorldPortalManager>();
	}

	void Start()
	{
		playerAnim = GetComponent<Animator>();
		playerRb = GetComponent<Rigidbody2D>();

		amountOfJumpsLeft = amountOfJumps;

		wallHopDirection.Normalize();
		wallJumpDirection.Normalize();
	}
	void Update()
	{
		UpdateAnimations();
		if (!DialogueSystem.Instance.isInteracting)
		{
			CheckInput();
			CheckMovementDirection();
			CheckIfCanJump();
			CheckIfWallSliding();
		}
	}
	void FixedUpdate()
	{
		if(!DialogueSystem.Instance.isInteracting)
		{
			ApplyMovement();
			CheckSurroundings();
		}
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

		// Se eu estiver no range com objeto e eu puder interagir eu interajo
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
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, wpManager.whatIsGround);

		isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, wpManager.whatIsGround);
	}
	private void CheckIfCanJump()
	{
		if((isGrounded && playerRb.velocity.y <= 0) || isWallSliding)
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
		if(canJump && !isWallSliding)
		{
			playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
			amountOfJumpsLeft--;
		}
		else if(isWallSliding && inputDirection == 0 && canJump) // Wall hop
		{
			isWallSliding = false;
			amountOfJumpsLeft--;
			Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
			playerRb.AddForce(forceToAdd, ForceMode2D.Impulse);
		}
		else if((isWallSliding || isTouchingWall) && (inputDirection != 0) && canJump) // Wall jump
		{
			isWallSliding = false;
			amountOfJumpsLeft--;
			Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * inputDirection, wallJumpForce * wallJumpDirection.y);
			playerRb.AddForce(forceToAdd, ForceMode2D.Impulse);
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
			facingDirection *= -1;
			isFacingRight = !isFacingRight;
			transform.Rotate(0.0f, 180.0f, 0.0f);
		}
	}

	private void OnTriggerEnter2D(Collider2D colliderObject)
	{
		if(colliderObject.tag == "DimensionPortal")
		{
			wpManager.ToggleWorlds();
		}
		if(colliderObject.tag == "Door")
		{
			GameManager.instance.NextLevel();
			Debug.Log("Interacting with the door!");
		}
		if(colliderObject.tag == "Tombstone")
		{
			Debug.Log("Interacting with a tombstone!");
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
		Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
	}
}
