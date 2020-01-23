using System;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class UIAnimate : MonoBehaviour
{
	// Token: 0x0600031F RID: 799 RVA: 0x00004B5D File Offset: 0x00002D5D
	private void Start()
	{
		this.startPosition = base.transform.localPosition;
		this.startEularRotation = base.transform.localEulerAngles;
		this.finalAnimationPosition = this.animateToPosition;
	}

	// Token: 0x06000320 RID: 800 RVA: 0x00004B8D File Offset: 0x00002D8D
	private void Update()
	{
		if (this.skipUpdate > 2)
		{
			this.RandomShake();
			this.AnimateReveal();
			this.ApplyTransformations();
			return;
		}
		this.skipUpdate++;
	}

	// Token: 0x06000321 RID: 801 RVA: 0x00004BB9 File Offset: 0x00002DB9
	private void ApplyTransformations()
	{
		base.transform.localPosition = this.finalAnimationPosition + this.finalRandomPosition;
		base.transform.localEulerAngles = this.startEularRotation + this.finalRandomRotation;
	}

	// Token: 0x06000322 RID: 802 RVA: 0x00014BAC File Offset: 0x00012DAC
	private void AnimateReveal()
	{
		if (!this.playBackwards)
		{
			this.finalAnimationPosition = Vector3.Lerp(this.finalAnimationPosition, this.startPosition, this.revealSpeed * Time.unscaledDeltaTime);
			return;
		}
		this.finalAnimationPosition = Vector3.Lerp(this.finalAnimationPosition, this.animateToPosition, this.revealSpeed * Time.unscaledDeltaTime);
	}

	// Token: 0x06000323 RID: 803 RVA: 0x00014C08 File Offset: 0x00012E08
	private void RandomShake()
	{
		this.perlinNoise.x = Mathf.PerlinNoise(Time.realtimeSinceStartup * this.randomPositionSpeed + 0.2f, 0f) * 2f - 1f;
		this.perlinNoise.y = Mathf.PerlinNoise(Time.realtimeSinceStartup * this.randomPositionSpeed + 0.5f, 0f) * 2f - 1f;
		this.perlinNoise.z = Mathf.PerlinNoise(Time.realtimeSinceStartup * this.randomRotationSpeed, 0f) * 2f - 1f;
		this.finalRandomRotation = new Vector3(0f, 0f, this.perlinNoise.z * this.randomRotationAmount);
		this.finalRandomPosition = new Vector3(this.perlinNoise.x * this.randomPositionAmount, this.perlinNoise.y * this.randomPositionAmount, 0f);
	}

	// Token: 0x04000483 RID: 1155
	private Vector3 startPosition;

	// Token: 0x04000484 RID: 1156
	private Vector3 startEularRotation;

	// Token: 0x04000485 RID: 1157
	[Header("Random Shake")]
	public float randomPositionSpeed;

	// Token: 0x04000486 RID: 1158
	public float randomPositionAmount;

	// Token: 0x04000487 RID: 1159
	[Space]
	public float randomRotationSpeed;

	// Token: 0x04000488 RID: 1160
	public float randomRotationAmount;

	// Token: 0x04000489 RID: 1161
	private Vector3 perlinNoise;

	// Token: 0x0400048A RID: 1162
	[Header("Reveal")]
	public Vector3 animateToPosition;

	// Token: 0x0400048B RID: 1163
	public float revealSpeed = 1f;

	// Token: 0x0400048C RID: 1164
	public bool playBackwards;

	// Token: 0x0400048D RID: 1165
	private Vector3 finalAnimationPosition;

	// Token: 0x0400048E RID: 1166
	private Vector3 finalRandomPosition;

	// Token: 0x0400048F RID: 1167
	private Vector3 finalRandomRotation;

	// Token: 0x04000490 RID: 1168
	private int skipUpdate;
}
