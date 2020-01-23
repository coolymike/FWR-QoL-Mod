using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class PlayerRagdollMode : MonoBehaviour
{
	// Token: 0x060000FD RID: 253 RVA: 0x00002E8A File Offset: 0x0000108A
	private void Awake()
	{
		PlayerRagdollMode.playerSettings = base.GetComponent<PlayerSettings>();
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00002E97 File Offset: 0x00001097
	private void Start()
	{
		PlayerRagdollMode.playerSettings.simulatedRagdoll.OnRagdollToggle += this.ToggledRagdoll;
		LevelManager.OnWinStateChange += this.OnWinStateChange;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00002EC5 File Offset: 0x000010C5
	private void OnDestroy()
	{
		PlayerRagdollMode.playerSettings.simulatedRagdoll.OnRagdollToggle -= this.ToggledRagdoll;
		LevelManager.OnWinStateChange -= this.OnWinStateChange;
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00002EF3 File Offset: 0x000010F3
	private void Update()
	{
		this.RagdollToggleConditions();
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00002EFB File Offset: 0x000010FB
	private void OnWinStateChange(LevelManager.WinState winState)
	{
		if (winState != LevelManager.WinState.None && !PlayerRagdollMode.playerSettings.simulatedRagdoll.RagdollModeEnabled)
		{
			PlayerRagdollMode.playerSettings.simulatedRagdoll.RagdollModeEnabled = true;
		}
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0000B6FC File Offset: 0x000098FC
	private void ToggledRagdoll(bool RagdollModeEnabled)
	{
		if (RagdollModeEnabled)
		{
			if (this.loseOnRagdoll)
			{
				LevelManager.instance.winState = LevelManager.WinState.Lose;
			}
			this.canResetFromRagdoll = false;
			base.Invoke("EnableCanResetFromRagdoll", this.resetFromRagdollTime);
			GameData.stats.ragdollCount++;
			return;
		}
		this.canResetToRagdoll = false;
		base.Invoke("EnableCanResetToRagdoll", this.resetToRagdollTime);
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00002F21 File Offset: 0x00001121
	private void EnableCanResetFromRagdoll()
	{
		this.canResetFromRagdoll = true;
	}

	// Token: 0x06000104 RID: 260 RVA: 0x00002F2A File Offset: 0x0000112A
	private void EnableCanResetToRagdoll()
	{
		this.canResetToRagdoll = true;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0000B764 File Offset: 0x00009964
	private void RagdollToggleConditions()
	{
		if (InputManager.ToggleRagdollMode())
		{
			if (!this.canResetFromRagdoll && PlayerRagdollMode.playerSettings.simulatedRagdoll.RagdollModeEnabled)
			{
				return;
			}
			if (!this.canResetToRagdoll && !PlayerRagdollMode.playerSettings.simulatedRagdoll.RagdollModeEnabled)
			{
				return;
			}
			if (!this.allowRagdollToggle)
			{
				return;
			}
			if (PlayerSettings.disablePlayerControlls)
			{
				return;
			}
			if (LevelManager.StatusIsWinOrLose(true) && !this.allowRagdollToggleOnWinOrLose)
			{
				return;
			}
			PlayerRagdollMode.playerSettings.simulatedRagdoll.RagdollModeEnabled = !PlayerRagdollMode.playerSettings.simulatedRagdoll.RagdollModeEnabled;
		}
	}

	// Token: 0x04000176 RID: 374
	public static PlayerSettings playerSettings;

	// Token: 0x04000177 RID: 375
	[Header("Ragdoll Toggle Settings")]
	public bool allowRagdollToggle = true;

	// Token: 0x04000178 RID: 376
	public bool allowRagdollToggleOnWinOrLose = true;

	// Token: 0x04000179 RID: 377
	[Space]
	public bool loseOnRagdoll;

	// Token: 0x0400017A RID: 378
	[Space]
	private bool canResetFromRagdoll = true;

	// Token: 0x0400017B RID: 379
	private bool canResetToRagdoll = true;

	// Token: 0x0400017C RID: 380
	public float resetFromRagdollTime = 0.3f;

	// Token: 0x0400017D RID: 381
	public float resetToRagdollTime = 0.3f;

	// Token: 0x0400017E RID: 382
	[Header("Spawn Point")]
	public GameObject spawnPointObject;

	// Token: 0x0400017F RID: 383
	private GameObject placedSpawnPointObject;

	// Token: 0x04000180 RID: 384
	private float setSpawnTimerCooldown = 2f;
}
