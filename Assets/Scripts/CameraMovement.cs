using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform target;
	public float smoothing;
	public Vector2 maxPosition;
	public Vector2 minPosition;

	void LateUpdate()
	{
		if (this.transform.position != target.position)
		{
			Vector3 offset = new Vector3(target.position.x, target.position.y, transform.position.z);

			offset.x = Mathf.Clamp(offset.x, minPosition.x, maxPosition.x);
			offset.y = Mathf.Clamp(offset.y, minPosition.y, maxPosition.y);

			transform.position = Vector3.Lerp(transform.position, offset, smoothing);
		}
	}
}
