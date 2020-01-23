using System;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class ResetPlacement : MonoBehaviour
{
	// Token: 0x0600048F RID: 1167 RVA: 0x00018E44 File Offset: 0x00017044
	private void Awake()
	{
		this.startPosition = base.transform.position;
		this.startRotation = base.transform.rotation;
		this.itemContainer = base.transform.root.GetComponent<BuildItemContainer>();
		if (this.resetRigidbody)
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
		}
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00005C1B File Offset: 0x00003E1B
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00005C2E File Offset: 0x00003E2E
	private void OnBuildModeToggle(bool buildModeOn)
	{
		if (!this.resetOnBuildMode)
		{
			return;
		}
		if (!buildModeOn)
		{
			return;
		}
		if (this.itemContainer == null || (this.itemContainer != null && !this.itemContainer.isSelectedObject))
		{
			this.Reset();
		}
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x00005C6C File Offset: 0x00003E6C
	private void OnDisable()
	{
		this.Reset();
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x00005C74 File Offset: 0x00003E74
	private void Reset()
	{
		this.ResetTransform();
		this.ResetRigidbody();
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x00018EB0 File Offset: 0x000170B0
	private void ResetTransform()
	{
		base.transform.position = (this.specifyResetPlacement ? this.resetPosition : this.startPosition);
		base.transform.rotation = (this.specifyResetPlacement ? this.resetRotation : this.startRotation);
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x00018F00 File Offset: 0x00017100
	private void ResetRigidbody()
	{
		if (!this.resetRigidbody || this.rigidbody == null)
		{
			return;
		}
		this.rigidbody.velocity = new Vector3(0f, 0f, 0f);
		this.rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x04000609 RID: 1545
	public bool resetOnDisable = true;

	// Token: 0x0400060A RID: 1546
	public bool resetOnBuildMode = true;

	// Token: 0x0400060B RID: 1547
	public bool resetRigidbody = true;

	// Token: 0x0400060C RID: 1548
	[Space]
	public bool specifyResetPlacement;

	// Token: 0x0400060D RID: 1549
	public Vector3 resetPosition;

	// Token: 0x0400060E RID: 1550
	public Quaternion resetRotation;

	// Token: 0x0400060F RID: 1551
	private Vector3 startPosition;

	// Token: 0x04000610 RID: 1552
	private Quaternion startRotation;

	// Token: 0x04000611 RID: 1553
	private Rigidbody rigidbody;

	// Token: 0x04000612 RID: 1554
	private BuildItemContainer itemContainer;
}
