using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class RotatingObject : MonoBehaviour
{
	// Token: 0x0600025E RID: 606 RVA: 0x000114BC File Offset: 0x0000F6BC
	private void Update()
	{
		if (!LevelManager.BuildModeOn && this.enableRotation)
		{
			switch (this.rotateOnAxis)
			{
			case RotatingObject.RotationAngle.x:
				this.eulerAngleVelocity = Vector3.right;
				break;
			case RotatingObject.RotationAngle.y:
				this.eulerAngleVelocity = Vector3.up;
				break;
			case RotatingObject.RotationAngle.z:
				this.eulerAngleVelocity = Vector3.forward;
				break;
			}
			this.rotatingObject.transform.Rotate(this.eulerAngleVelocity * this.rotationSpeed * Time.deltaTime);
		}
	}

	// Token: 0x04000361 RID: 865
	public GameObject rotatingObject;

	// Token: 0x04000362 RID: 866
	public bool enableRotation;

	// Token: 0x04000363 RID: 867
	public RotatingObject.RotationAngle rotateOnAxis;

	// Token: 0x04000364 RID: 868
	public float rotationSpeed = 100f;

	// Token: 0x04000365 RID: 869
	private Vector3 eulerAngleVelocity;

	// Token: 0x02000076 RID: 118
	public enum RotationAngle
	{
		// Token: 0x04000367 RID: 871
		x,
		// Token: 0x04000368 RID: 872
		y,
		// Token: 0x04000369 RID: 873
		z
	}
}
