using System;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class Hammer : MonoBehaviour
{
	// Token: 0x06000220 RID: 544 RVA: 0x00003E96 File Offset: 0x00002096
	private void Start()
	{
		this.startQuat = base.transform.rotation;
		this.rb = this.swingObject.GetComponent<Rigidbody>();
		this.startRotation = this.swingObject.localEulerAngles.z;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x000107AC File Offset: 0x0000E9AC
	private void FixedUpdate()
	{
		if (!LevelManager.BuildModeOn)
		{
			this.rotationTime += Time.deltaTime;
			if (this.swingAround)
			{
				this.swing -= Time.deltaTime * this.swingSpeed * this.startRotation;
			}
			else
			{
				this.swing = Mathf.Cos(this.rotationTime * this.swingSpeed) * this.startRotation;
			}
			this.rb.MoveRotation(this.startQuat * Quaternion.Euler(0f, 0f, this.swing));
			return;
		}
		this.rotationTime = 0f;
		this.swing = this.startRotation;
		this.swingObject.localEulerAngles = new Vector3(0f, 0f, this.startRotation);
	}

	// Token: 0x04000310 RID: 784
	public Transform swingObject;

	// Token: 0x04000311 RID: 785
	public bool swingAround;

	// Token: 0x04000312 RID: 786
	public float swingSpeed = 4f;

	// Token: 0x04000313 RID: 787
	private float rotationTime;

	// Token: 0x04000314 RID: 788
	private float startRotation;

	// Token: 0x04000315 RID: 789
	private float swing;

	// Token: 0x04000316 RID: 790
	private Quaternion startQuat;

	// Token: 0x04000317 RID: 791
	private Rigidbody rb;
}
