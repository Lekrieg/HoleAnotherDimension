using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPortalManager : MonoBehaviour
{
	Camera mainCamera, rendererCamera;

	private bool isOverworld;
	public LayerMask whatIsGround;

	void Awake()
	{
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		rendererCamera = GameObject.Find("Camera").GetComponent<Camera>();

		// whatIsGround = LayerMask.NameToLayer("Overworld");

		isOverworld = false;

		Physics2D.IgnoreLayerCollision(0, 9);
		whatIsGround = 1 << 8;
	}

	public void ToggleWorlds()
	{
		if (isOverworld)
		{
			mainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Overworld");
			mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("Underworld"));
			rendererCamera.cullingMask |= 1 << LayerMask.NameToLayer("Underworld");
			rendererCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("Overworld"));

			Physics2D.IgnoreLayerCollision(0, 8, false);
			Physics2D.IgnoreLayerCollision(0, 9);

			whatIsGround = 1 << 8;

			isOverworld = false;
		}
		else
		{
			mainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Underworld");
			mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("Overworld"));
			rendererCamera.cullingMask |= 1 << LayerMask.NameToLayer("Overworld");
			rendererCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("Underworld"));

			Physics2D.IgnoreLayerCollision(0, 9, false);
			Physics2D.IgnoreLayerCollision(0, 8);

			whatIsGround = 1 << 9;

			isOverworld = true;
		}
	}
}
