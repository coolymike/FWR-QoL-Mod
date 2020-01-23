using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class CameraShake : MonoBehaviour
{
	// Token: 0x06000273 RID: 627 RVA: 0x0000421E File Offset: 0x0000241E
	private void Start()
	{
		this.cameraStartRotation = base.transform.localEulerAngles;
	}

	// Token: 0x06000274 RID: 628 RVA: 0x00004231 File Offset: 0x00002431
	private void LateUpdate()
	{
		this.SparaticShake();
		this.Handheld();
		this.LandingShake();
		base.transform.localEulerAngles = this.handheldTotal + this.sparaticShakeTotal + this.landingShakeTotal;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x00011F70 File Offset: 0x00010170
	private void LandingShake()
	{
		if (this.startLandingShake)
		{
			this.lsTimer = this.landingShakeTime;
			this.lsSeed = UnityEngine.Random.value;
			this.startLandingShake = false;
		}
		this.lsTimer = Mathf.Lerp(this.lsTimer, 0f, Time.deltaTime * 2.3f);
		if (this.lsTimer > 0f)
		{
			this.cameraRotationOffset = new Vector3(this.cameraStartRotation.x + (Mathf.PerlinNoise(this.landingShakeSpeed * this.lsTimer, this.lsSeed * 2f) * 2f - 1f) * this.landingShakeAmount * this.lsTimer, 0f, this.cameraStartRotation.z + (Mathf.PerlinNoise(this.landingShakeSpeed * this.lsTimer, this.lsSeed * 10f) * 2f - 1f) * this.landingShakeAmount * 2f * this.lsTimer);
			this.landingShakeTotal = this.cameraRotationOffset;
		}
	}

	// Token: 0x06000276 RID: 630 RVA: 0x00012080 File Offset: 0x00010280
	private void SparaticShake()
	{
		if (this.startSparaticShake)
		{
			this.ssTimer = this.sparaticShakeTime;
			this.ssSeed = UnityEngine.Random.value;
			this.startSparaticShake = false;
		}
		this.ssTimer -= Time.deltaTime;
		if (this.ssTimer > 0f)
		{
			this.cameraRotationOffset = new Vector3(this.cameraStartRotation.x + (Mathf.PerlinNoise(this.sparaticShakeSpeed * this.ssTimer, this.ssSeed * 2f) * 2f - 1f) * this.sparaticShakeAmount * this.ssTimer, this.cameraStartRotation.y + (Mathf.PerlinNoise(this.sparaticShakeSpeed * this.ssTimer, this.ssSeed * 6f) * 2f - 1f) * this.sparaticShakeAmount * this.ssTimer, this.cameraStartRotation.z + (Mathf.PerlinNoise(this.sparaticShakeSpeed * this.ssTimer, this.ssSeed * 10f) * 2f - 1f) * this.sparaticShakeAmount * this.ssTimer);
			this.sparaticShakeTotal = this.cameraRotationOffset;
		}
	}

	// Token: 0x06000277 RID: 631 RVA: 0x000121B8 File Offset: 0x000103B8
	private void Handheld()
	{
		this.handheldInputAmount = Mathf.Clamp(Mathf.Abs(InputManager.Move(true).y) + Mathf.Abs(InputManager.Move(true).x) * 0.5f, 0f, 1f);
		this.shakeTime += Time.deltaTime * Mathf.Clamp(this.handheldInputAmount * 1.5f, 1f, 1.5f);
		this.cameraRotationOffset.x = this.HandheldShakeFormula(this.hShakeAmount + this.handheldInputAmount * 0.5f, 0.1f);
		this.cameraRotationOffset.y = this.HandheldShakeFormula(this.hShakeAmount + this.handheldInputAmount * 0.5f, 0.5f);
		this.cameraRotationOffset.z = this.HandheldShakeFormula(this.hShakeAmount + this.handheldInputAmount * 0.5f, 0.8f);
		this.handheldTotal = this.cameraStartRotation + this.cameraRotationOffset;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0000426C File Offset: 0x0000246C
	private float HandheldShakeFormula(float amount, float seed)
	{
		return (Mathf.PerlinNoise(this.hShakeSpeed * this.shakeTime, seed) * 2f - 1f) * amount;
	}

	// Token: 0x04000399 RID: 921
	private Vector3 cameraStartRotation;

	// Token: 0x0400039A RID: 922
	private Vector3 cameraRotationOffset;

	// Token: 0x0400039B RID: 923
	[Header("Handheld")]
	public float hShakeSpeed = 2f;

	// Token: 0x0400039C RID: 924
	public float hShakeAmount = 0.2f;

	// Token: 0x0400039D RID: 925
	private float shakeTime;

	// Token: 0x0400039E RID: 926
	[Header("Sparatic Shake")]
	public bool startSparaticShake;

	// Token: 0x0400039F RID: 927
	public float sparaticShakeSpeed = 5f;

	// Token: 0x040003A0 RID: 928
	public float sparaticShakeAmount = 15f;

	// Token: 0x040003A1 RID: 929
	public float sparaticShakeTime = 0.5f;

	// Token: 0x040003A2 RID: 930
	[Header("Landing Shake")]
	public bool startLandingShake;

	// Token: 0x040003A3 RID: 931
	public float landingShakeSpeed = 4f;

	// Token: 0x040003A4 RID: 932
	public float landingShakeAmount = 15f;

	// Token: 0x040003A5 RID: 933
	public float landingShakeTime = 0.6f;

	// Token: 0x040003A6 RID: 934
	private Vector3 sparaticShakeTotal;

	// Token: 0x040003A7 RID: 935
	private Vector3 handheldTotal;

	// Token: 0x040003A8 RID: 936
	private Vector3 landingShakeTotal;

	// Token: 0x040003A9 RID: 937
	private float lsTimer;

	// Token: 0x040003AA RID: 938
	private float lsSeed;

	// Token: 0x040003AB RID: 939
	private float ssTimer;

	// Token: 0x040003AC RID: 940
	private float ssSeed;

	// Token: 0x040003AD RID: 941
	private float handheldInputAmount;
}
