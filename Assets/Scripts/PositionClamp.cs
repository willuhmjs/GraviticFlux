using UnityEngine;
using System.Collections;

public class PositionClamp
{
	float minX;
	float minY;

	float maxX;
	float maxY;

	public PositionClamp(float worldMinX, float worldMinY, float worldMaxX, float worldMaxY, Renderer r)
	{
		float halfSpriteWidth = r.bounds.size.x / 2.0f;
		float halfSpriteHeight = r.bounds.size.y / 2.0f;

		minX = worldMinX + halfSpriteWidth;
		minY = worldMinY + halfSpriteHeight;

		maxX = worldMaxX - halfSpriteWidth;
		maxY = worldMaxY - halfSpriteHeight;
	}

	public PositionClamp(float worldMinX, float worldMinY, float worldMaxX, float worldMaxY, Camera cam)
	{
		float halfCameraHeight = cam.orthographicSize;    
		float halfCameraWidth = halfCameraHeight * Screen.width / Screen.height;

		minX = worldMinX + halfCameraWidth;
		minY = worldMinY + halfCameraHeight;

		maxX = worldMaxX - halfCameraWidth;
		maxY = worldMaxY - halfCameraHeight;
	}

	public void movementLimiter(Vector3 targetPosition, Transform trans)
	{
		float clampedX = Mathf.Clamp(targetPosition.x,minX,maxX);
		float clampedY = Mathf.Clamp(targetPosition.y,minY,maxY);

		trans.position = new Vector3 (clampedX, clampedY, trans.position.z);
	}

}
