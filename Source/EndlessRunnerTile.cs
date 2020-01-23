using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C1 RID: 193
public class EndlessRunnerTile : MonoBehaviour
{
	// Token: 0x060003CC RID: 972 RVA: 0x00005356 File Offset: 0x00003556
	private void Update()
	{
		if (this.playerSetTrigger)
		{
			this.RemoveOnDistanceReach();
			if (this.willCollapse && !this.collapsed)
			{
				base.StartCoroutine(this.CollapseRoutine());
			}
		}
	}

	// Token: 0x060003CD RID: 973 RVA: 0x00005383 File Offset: 0x00003583
	private IEnumerator CollapseRoutine()
	{
		this.collapsed = true;
		this.isFalling = true;
		CameraController.TriggerCameraTremor(PlayerSettings.instance.mainCamera);
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.8f, 1f));
		CameraController.TriggerCameraTremor(PlayerSettings.instance.mainCamera);
		this.isFalling = false;
		yield break;
	}

	// Token: 0x060003CE RID: 974 RVA: 0x00005392 File Offset: 0x00003592
	private void FixedUpdate()
	{
		if (this.isFalling)
		{
			this.Fall();
		}
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00016D08 File Offset: 0x00014F08
	private void Fall()
	{
		this.moveSpeed += Time.deltaTime * 0.2f;
		base.transform.Translate(new Vector3(0f, -1f, 0f) * this.moveSpeed, Space.World);
		base.transform.Rotate(this.rotateAngle * (this.moveSpeed * 0.5f), Space.Self);
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00016D7C File Offset: 0x00014F7C
	private void RemoveOnDistanceReach()
	{
		this.playerDistance = Vector3.Distance(PlayerSettings.instance.simulatedRagdoll.bodyElements.GetJoint(RagdollBodyElements.Joint.Core).transform.position, this.tileEnd.position);
		if (this.playerDistance > this.distance * 0.4f && !this.isFalling)
		{
			this.isFalling = true;
		}
		if (this.playerDistance > this.distance)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x000053A2 File Offset: 0x000035A2
	private void OnTriggerEnter(Collider other)
	{
		if (Tag.Compare(other.transform, Tag.Tags.Player) && !this.playerSetTrigger)
		{
			this.playerSetTrigger = true;
		}
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00016DFC File Offset: 0x00014FFC
	private void OnEnable()
	{
		this.rotateAngle = new Vector3(UnityEngine.Random.Range(-2.5f, -0.6f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-0.5f, 0.5f));
		this.willCollapse = (UnityEngine.Random.Range(0, 4) == 0);
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x000053C1 File Offset: 0x000035C1
	private void OnDisable()
	{
		this.playerSetTrigger = false;
		this.moveSpeed = 0f;
		this.isFalling = false;
		this.willCollapse = false;
		this.collapsed = false;
	}

	// Token: 0x04000529 RID: 1321
	public Transform tileEnd;

	// Token: 0x0400052A RID: 1322
	public bool playerSetTrigger;

	// Token: 0x0400052B RID: 1323
	private bool willCollapse;

	// Token: 0x0400052C RID: 1324
	private bool collapsed;

	// Token: 0x0400052D RID: 1325
	private readonly float distance = 150f;

	// Token: 0x0400052E RID: 1326
	public bool isFalling;

	// Token: 0x0400052F RID: 1327
	private float moveSpeed;

	// Token: 0x04000530 RID: 1328
	private Vector3 rotateAngle;

	// Token: 0x04000531 RID: 1329
	private float playerDistance;
}
