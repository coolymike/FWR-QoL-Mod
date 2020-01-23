using System;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class PlayerTeleport : MonoBehaviour
{
	// Token: 0x0600010A RID: 266 RVA: 0x00002F8A File Offset: 0x0000118A
	private void Awake()
	{
		this.playerSettings.simulatedRagdoll.OnRagdollToggle += this.Teleport;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x0000B840 File Offset: 0x00009A40
	private void Start()
	{
		if (WorldData.instance != null && WorldData.instance.autoSave)
		{
			this.savedControllerPosition = WorldData.instance.data.player.spawnPosition;
			this.savedControllerRotation = WorldData.instance.data.player.spawnRotation;
			this.Teleport(false);
		}
		this.SetSpawn();
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00002FA8 File Offset: 0x000011A8
	private void OnDestroy()
	{
		this.playerSettings.simulatedRagdoll.OnRagdollToggle -= this.Teleport;
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00002FC6 File Offset: 0x000011C6
	private void Update()
	{
		this.SetSpawnConditions();
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00002FCE File Offset: 0x000011CE
	private void EnableSpawning()
	{
		this.canSpawn = true;
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0000B8A8 File Offset: 0x00009AA8
	private void SetSpawnConditions()
	{
		if (InputManager.SetSpawn())
		{
			if (!this.canSpawn)
			{
				return;
			}
			if (WorldData.instance != null && WorldData.instance.allowWorldSave && WorldData.instance.data.settings.lockSpawn)
			{
				return;
			}
			if (!this.playerSettings.playerController.IsGrounded)
			{
				return;
			}
			if (this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
			{
				return;
			}
			if (!this.allowSetSpawn)
			{
				return;
			}
			if (PlayerSettings.disablePlayerControlls)
			{
				return;
			}
			if (LevelManager.StatusIsWinOrLose(true))
			{
				return;
			}
			this.canSpawn = false;
			base.Invoke("EnableSpawning", this.setSpawnTimerCooldown);
			this.SetSpawn();
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000B958 File Offset: 0x00009B58
	private void SetSpawn()
	{
		this.savedControllerPosition = this.playerSettings.playerController.transform.position;
		this.savedControllerRotation = this.playerSettings.playerCameraController.lookRotation;
		if (WorldData.instance != null)
		{
			WorldData.instance.data.player.spawnPosition = this.savedControllerPosition;
			WorldData.instance.data.player.spawnRotation = this.savedControllerRotation;
		}
		this.SpawnParticleEffect();
		this.SpawnArrowEffect();
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000B9E8 File Offset: 0x00009BE8
	private void Teleport(bool ragdollModeEnabled)
	{
		if (ragdollModeEnabled)
		{
			return;
		}
		this.playerSettings.playerController.controller.enabled = false;
		this.playerSettings.playerController.transform.position = this.savedControllerPosition;
		this.playerSettings.playerController.controller.enabled = true;
		this.playerSettings.playerCameraController.lookRotation = this.savedControllerRotation;
		this.playerSettings.simulatedRagdoll.ResetRagdoll();
		this.coreTeleportPosition = new Vector3(this.playerSettings.playerController.transform.position.x, this.playerSettings.playerController.transform.position.y + 1.3f, this.playerSettings.playerController.transform.position.z);
		this.playerSettings.simulatedRagdoll.bodyElements.GetJoint(RagdollBodyElements.Joint.Core).transform.position = this.coreTeleportPosition;
		this.SpawnParticleEffect();
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00002FD7 File Offset: 0x000011D7
	private void SpawnParticleEffect()
	{
		UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate<GameObject>(this.setSpawnEffect, this.savedControllerPosition, this.playerSettings.playerController.transform.rotation), 4f);
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00003009 File Offset: 0x00001209
	private void SpawnArrowEffect()
	{
		if (this.placedSpawnArrow != null)
		{
			UnityEngine.Object.Destroy(this.placedSpawnArrow);
		}
		this.placedSpawnArrow = UnityEngine.Object.Instantiate<GameObject>(this.spawnArrowPrefab, this.savedControllerPosition, Quaternion.Euler(Vector3.zero));
	}

	// Token: 0x0400018E RID: 398
	public PlayerSettings playerSettings;

	// Token: 0x0400018F RID: 399
	public GameObject spawnArrowPrefab;

	// Token: 0x04000190 RID: 400
	private GameObject placedSpawnArrow;

	// Token: 0x04000191 RID: 401
	public GameObject setSpawnEffect;

	// Token: 0x04000192 RID: 402
	private Vector3 savedControllerPosition;

	// Token: 0x04000193 RID: 403
	private Vector3 savedControllerRotation;

	// Token: 0x04000194 RID: 404
	[Header("Properties")]
	public bool allowSetSpawn = true;

	// Token: 0x04000195 RID: 405
	private bool canSpawn = true;

	// Token: 0x04000196 RID: 406
	private float setSpawnTimerCooldown = 2f;

	// Token: 0x04000197 RID: 407
	private Vector3 coreTeleportPosition;
}
