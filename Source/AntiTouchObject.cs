using System;
using UnityEngine;

// Token: 0x020000EB RID: 235
public class AntiTouchObject : MonoBehaviour
{
	// Token: 0x060004AA RID: 1194 RVA: 0x00019650 File Offset: 0x00017850
	private void Start()
	{
		AntiTouchObject.ComponentDistributionMethod componentDistributionMethod = this.distributionMethod;
		if (componentDistributionMethod == AntiTouchObject.ComponentDistributionMethod.ApplyToChild)
		{
			this.CopyComponentToChild();
			return;
		}
		if (componentDistributionMethod != AntiTouchObject.ComponentDistributionMethod.ApplyToChildren)
		{
			return;
		}
		this.CopyComponentToChildren();
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0001967C File Offset: 0x0001787C
	private void CopyComponentToChild()
	{
		Collider componentInChildren = base.GetComponentInChildren<Collider>();
		if (componentInChildren == null)
		{
			return;
		}
		this.CopyVariablesToScript(componentInChildren.gameObject.AddComponent<AntiTouchObject>());
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x000196B4 File Offset: 0x000178B4
	private void CopyComponentToChildren()
	{
		foreach (Collider collider in base.GetComponentsInChildren<Collider>())
		{
			this.CopyVariablesToScript(collider.gameObject.AddComponent<AntiTouchObject>());
		}
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x000196F4 File Offset: 0x000178F4
	private void CopyVariablesToScript(AntiTouchObject script)
	{
		if (script == null)
		{
			return;
		}
		script.triggerCollisions = this.triggerCollisions;
		script.colliderCollisions = this.colliderCollisions;
		script.badForPlayer = this.badForPlayer;
		script.badForSmartRagdolls = this.badForSmartRagdolls;
		script.specifiedSmartRagdolls = this.specifiedSmartRagdolls;
		script.antiTouchInBuildMode = this.antiTouchInBuildMode;
		script.surpassCooldown = this.surpassCooldown;
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00005D87 File Offset: 0x00003F87
	private void OnTriggerEnter(Collider collision)
	{
		if (!this.triggerCollisions)
		{
			return;
		}
		AntiTouchSystem.ObjectTouchedSomething(this, collision.transform);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00005D9E File Offset: 0x00003F9E
	private void OnCollisionEnter(Collision collision)
	{
		if (!this.colliderCollisions)
		{
			return;
		}
		AntiTouchSystem.ObjectTouchedSomething(this, collision.transform);
	}

	// Token: 0x04000636 RID: 1590
	public AntiTouchObject.ComponentDistributionMethod distributionMethod;

	// Token: 0x04000637 RID: 1591
	[Header("Collision Detection")]
	public bool triggerCollisions;

	// Token: 0x04000638 RID: 1592
	public bool colliderCollisions = true;

	// Token: 0x04000639 RID: 1593
	[Header("Settings")]
	public bool badForPlayer = true;

	// Token: 0x0400063A RID: 1594
	[Space]
	public bool badForSmartRagdolls = true;

	// Token: 0x0400063B RID: 1595
	public SmartRagdollController.Logic[] specifiedSmartRagdolls = new SmartRagdollController.Logic[0];

	// Token: 0x0400063C RID: 1596
	[Space]
	public bool antiTouchInBuildMode;

	// Token: 0x0400063D RID: 1597
	public bool surpassCooldown;

	// Token: 0x020000EC RID: 236
	public enum ComponentDistributionMethod
	{
		// Token: 0x0400063F RID: 1599
		None,
		// Token: 0x04000640 RID: 1600
		ApplyToChild,
		// Token: 0x04000641 RID: 1601
		ApplyToChildren
	}
}
