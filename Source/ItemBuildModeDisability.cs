using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class ItemBuildModeDisability : MonoBehaviour
{
	// Token: 0x060004DA RID: 1242 RVA: 0x0001A17C File Offset: 0x0001837C
	private void Start()
	{
		this.monoBehaviours = this.item.GetComponentsInChildren<MonoBehaviour>();
		this.colliders = this.item.GetComponentsInChildren<Collider>();
		this.meshColliders = this.item.GetComponentsInChildren<MeshCollider>();
		this.wheelColliders = this.item.GetComponentsInChildren<WheelCollider>();
		this.rigidbodies = this.item.GetComponentsInChildren<Rigidbody>();
		this.i = 0;
		while (this.i < this.rigidbodies.Length)
		{
			if (!this.rigidbodies[this.i].isKinematic)
			{
				this.nonKinematicRigidbodies.Add(this.rigidbodies[this.i]);
			}
			this.i++;
		}
		if (this.buildItemContainer.isSelectedObject)
		{
			for (int i = 0; i < this.monoBehaviours.Length; i++)
			{
				if (this.monoBehaviours[i] != null)
				{
					this.monoBehaviours[i].enabled = false;
				}
			}
		}
		if (!this.buildItemContainer.isSelectedObject)
		{
			LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
		}
		this.OnBuildModeToggle(LevelManager.BuildModeOn);
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x00005F8B File Offset: 0x0000418B
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x00005F9E File Offset: 0x0000419E
	private void OnBuildModeToggle(bool buildModeOn)
	{
		this.ToggleItemRigidbodyAttributes(buildModeOn);
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0001A298 File Offset: 0x00018498
	private void ToggleItemRigidbodyAttributes(bool enable)
	{
		this.i = 0;
		while (this.i < this.nonKinematicRigidbodies.Count)
		{
			this.nonKinematicRigidbodies[this.i].isKinematic = enable;
			this.i++;
		}
		if (this.disableColliders)
		{
			this.i = 0;
			while (this.i < this.colliders.Length)
			{
				if (this.colliders[this.i].GetInstanceID() != base.gameObject.GetInstanceID())
				{
					this.colliders[this.i].enabled = !enable;
				}
				this.i++;
			}
			this.i = 0;
			while (this.i < this.meshColliders.Length)
			{
				this.meshColliders[this.i].enabled = !enable;
				this.i++;
			}
			this.i = 0;
			while (this.i < this.wheelColliders.Length)
			{
				this.wheelColliders[this.i].enabled = !enable;
				this.i++;
			}
			return;
		}
		this.i = 0;
		while (this.i < this.colliders.Length)
		{
			if (this.colliders[this.i].GetInstanceID() != base.gameObject.GetInstanceID() && this.colliders[this.i].isTrigger)
			{
				this.colliders[this.i].enabled = !enable;
			}
			this.i++;
		}
		this.i = 0;
		while (this.i < this.meshColliders.Length)
		{
			if (this.meshColliders[this.i].isTrigger)
			{
				this.meshColliders[this.i].enabled = !enable;
			}
			this.i++;
		}
	}

	// Token: 0x04000659 RID: 1625
	public GameObject item;

	// Token: 0x0400065A RID: 1626
	public BuildItemContainer buildItemContainer;

	// Token: 0x0400065B RID: 1627
	public bool disableColliders;

	// Token: 0x0400065C RID: 1628
	private int i;

	// Token: 0x0400065D RID: 1629
	private MonoBehaviour[] monoBehaviours;

	// Token: 0x0400065E RID: 1630
	private Collider[] colliders;

	// Token: 0x0400065F RID: 1631
	private MeshCollider[] meshColliders;

	// Token: 0x04000660 RID: 1632
	private WheelCollider[] wheelColliders;

	// Token: 0x04000661 RID: 1633
	private Rigidbody[] rigidbodies;

	// Token: 0x04000662 RID: 1634
	private List<Rigidbody> nonKinematicRigidbodies = new List<Rigidbody>();
}
