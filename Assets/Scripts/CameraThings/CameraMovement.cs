using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform target;
	public float smoothing;

	void LateUpdate()
	{
		if (this.transform.position != target.position)
		{
			Vector3 offset = new Vector3(target.position.x, target.position.y, transform.position.z);

			offset.x = Mathf.Clamp(offset.x, GameManager.instance.cdPoints[GameManager.instance.door].minPosition.x, GameManager.instance.cdPoints[GameManager.instance.door].maxPosition.x);
			offset.y = Mathf.Clamp(offset.y, GameManager.instance.cdPoints[GameManager.instance.door].minPosition.y, GameManager.instance.cdPoints[GameManager.instance.door].maxPosition.y);

			transform.position = Vector3.Lerp(transform.position, offset, smoothing);
		}
	}
}
