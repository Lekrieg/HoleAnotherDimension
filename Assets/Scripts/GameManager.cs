using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[SerializeField]
	Camera mainCamera, rendererCamera;

	private bool isOverworld;
	public LayerMask whatIsGround;

	void Awake()
	{
		whatIsGround = LayerMask.NameToLayer("Overworld");
		instance = this;

		isOverworld = false;

		Physics2D.IgnoreLayerCollision(0, 9);
		whatIsGround = 1 << 8;
	}
    /*
	se: Entrei no portal
		ativa o layer underworld da main camera e desativa o overworld

		desativa o underworld do underworldRenderer e ativa o overworld

		troca a layer do personagem de acordo com o necessario

	*/
	public void ToggleWorlds()
	{
		if(isOverworld)
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
