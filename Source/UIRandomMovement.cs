using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class UIRandomMovement : MonoBehaviour
{
	// Token: 0x060003AD RID: 941 RVA: 0x00005257 File Offset: 0x00003457
	private void Start()
	{
		this.startPosition = base.gameObject.transform.localPosition;
		this.startRotation = base.gameObject.transform.localEulerAngles;
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00015F78 File Offset: 0x00014178
	private void Update()
	{
		this.randomRotation.z = Mathf.PerlinNoise(Time.realtimeSinceStartup * this.randomRotationSpeed, Time.realtimeSinceStartup * this.randomRotationSpeed) * 2f - 1f;
		this.randomPosition.x = Mathf.PerlinNoise(Time.realtimeSinceStartup * this.randomPositionSpeed + 0.2f, 0f) * 2f - 1f;
		this.randomPosition.y = Mathf.PerlinNoise(Time.realtimeSinceStartup * this.randomPositionSpeed + 0.5f, 0f) * 2f - 1f;
		if (this.enableRandomRotation)
		{
			base.gameObject.transform.localEulerAngles = new Vector3(base.gameObject.transform.eulerAngles.x, base.gameObject.transform.eulerAngles.y, this.startRotation.z + this.randomRotation.z * this.randomRotationAmount);
		}
		if (this.enableRandomPosition)
		{
			base.gameObject.transform.localPosition = new Vector3(this.startPosition.x + this.randomPosition.x * this.randomPositionAmount, this.startPosition.y + this.randomPosition.y * this.randomPositionAmount, this.startPosition.z);
		}
	}

	// Token: 0x040004F2 RID: 1266
	[Header("Position")]
	public bool enableRandomPosition;

	// Token: 0x040004F3 RID: 1267
	public float randomPositionSpeed;

	// Token: 0x040004F4 RID: 1268
	public float randomPositionAmount;

	// Token: 0x040004F5 RID: 1269
	private Vector3 startPosition;

	// Token: 0x040004F6 RID: 1270
	private Vector3 randomPosition;

	// Token: 0x040004F7 RID: 1271
	[Header("Rotation")]
	public bool enableRandomRotation;

	// Token: 0x040004F8 RID: 1272
	public float randomRotationSpeed;

	// Token: 0x040004F9 RID: 1273
	public float randomRotationAmount;

	// Token: 0x040004FA RID: 1274
	private Vector3 startRotation;

	// Token: 0x040004FB RID: 1275
	private Vector3 randomRotation;
}
