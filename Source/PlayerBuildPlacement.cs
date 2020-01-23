using System;
using UnityEngine;

// Token: 0x020000F3 RID: 243
[RequireComponent(typeof(PlayerBuildSystem))]
public class PlayerBuildPlacement : MonoBehaviour
{
	// Token: 0x060004DF RID: 1247 RVA: 0x00005FBA File Offset: 0x000041BA
	private void Awake()
	{
		this.playerSettings = base.transform.root.GetComponent<PlayerSettings>();
		this.buildSystem = base.GetComponent<PlayerBuildSystem>();
		this.rotation = Quaternion.Euler(0f, 0f, 0f);
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x00005FF8 File Offset: 0x000041F8
	private void Update()
	{
		if (InputManager.GridToggle())
		{
			PlayerBuildPlacement.gridSize = 0.6f;
		}
		else
		{
			PlayerBuildPlacement.gridSize = 1E-07f;
		}
		if (LevelManager.BuildModeOn)
		{
			this.UpdateItemPlacement();
		}
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x0001A48C File Offset: 0x0001868C
	private void UpdateItemPlacement()
	{
		if (this.buildSystem.selectedBuildItemVariant == null)
		{
			return;
		}
		this.distance = Mathf.Clamp(this.buildSystem.selectedBuildItemVariant.bounds.magnitude, 3f, 6f);
		this.aim = this.playerSettings.mainCamera.transform.position;
		this.aim -= new Vector3(0f, this.buildSystem.selectedBuildItemVariant.bounds.y * 0.3f, 0f);
		this.aim += this.playerSettings.mainCamera.transform.forward * this.distance * 1.5f;
		this.position = this.PositionToGrid(this.aim, (float)this.buildSystem.selectedBuildItemVariant.gridPlacementMultiplier);
		this.positionLerp = Vector3.Lerp(this.positionLerp, this.position, this.positionSpeed * Time.deltaTime);
		this.rotationLerp = Quaternion.Slerp(this.rotationLerp, this.rotation, this.rotationSpeed * Time.deltaTime);
		if (InputManager.RotateObjectLeft())
		{
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x, this.rotation.eulerAngles.y - this.rotationAmount, this.rotation.eulerAngles.z);
		}
		if (InputManager.RotateObjectRight())
		{
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x, this.rotation.eulerAngles.y + this.rotationAmount, this.rotation.eulerAngles.z);
		}
		if (InputManager.RotateObjectXPositive())
		{
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x + this.rotationAmount, this.rotation.eulerAngles.y, this.rotation.eulerAngles.z);
		}
		if (InputManager.RotateObjectXNegative())
		{
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x - this.rotationAmount, this.rotation.eulerAngles.y, this.rotation.eulerAngles.z);
		}
		if (InputManager.RotateObjectZPositive())
		{
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x, this.rotation.eulerAngles.y, this.rotation.eulerAngles.z + this.rotationAmount);
		}
		if (InputManager.RotateObjectZNegative())
		{
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x, this.rotation.eulerAngles.y, this.rotation.eulerAngles.z - this.rotationAmount);
		}
		if (InputManager.SmallRotation())
		{
			this.rotationAmount = 45f;
			return;
		}
		this.rotationAmount = 1f;
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x0001A7A0 File Offset: 0x000189A0
	public Vector3 PositionToGrid(Vector3 position, float offsetMultiplier = 1f)
	{
		if (Input.GetKeyDown(InputManager.YPosToggleKey))
		{
			this.SavedCoord.y = Mathf.Max(Mathf.Round(position.y / (PlayerBuildPlacement.gridSize * offsetMultiplier)) * PlayerBuildPlacement.gridSize * offsetMultiplier, 0f);
		}
		if (Input.GetKeyDown(InputManager.XPosToggleKey))
		{
			this.SavedCoord.x = Mathf.Round(position.x / (PlayerBuildPlacement.gridSize * offsetMultiplier)) * PlayerBuildPlacement.gridSize * offsetMultiplier;
		}
		if (Input.GetKeyDown(InputManager.ZPosToggleKey))
		{
			this.SavedCoord.z = Mathf.Round(position.z / (PlayerBuildPlacement.gridSize * offsetMultiplier)) * PlayerBuildPlacement.gridSize * offsetMultiplier;
		}
		this.FinalCoord.y = Mathf.Max(Mathf.Round(position.y / (PlayerBuildPlacement.gridSize * offsetMultiplier)) * PlayerBuildPlacement.gridSize * offsetMultiplier, 0f);
		this.FinalCoord.x = Mathf.Round(position.x / (PlayerBuildPlacement.gridSize * offsetMultiplier)) * PlayerBuildPlacement.gridSize * offsetMultiplier;
		this.FinalCoord.z = Mathf.Round(position.z / (PlayerBuildPlacement.gridSize * offsetMultiplier)) * PlayerBuildPlacement.gridSize * offsetMultiplier;
		if (!InputManager.YPosToggle())
		{
			this.FinalCoord.y = this.SavedCoord.y;
		}
		if (!InputManager.XPosToggle())
		{
			this.FinalCoord.x = this.SavedCoord.x;
		}
		if (!InputManager.ZPosToggle())
		{
			this.FinalCoord.z = this.SavedCoord.z;
		}
		return new Vector3(this.FinalCoord.x, this.FinalCoord.y, this.FinalCoord.z);
	}

	// Token: 0x04000663 RID: 1635
	private PlayerSettings playerSettings;

	// Token: 0x04000664 RID: 1636
	private PlayerBuildSystem buildSystem;

	// Token: 0x04000665 RID: 1637
	public static float gridSize = 0.6f;

	// Token: 0x04000666 RID: 1638
	public static float boundsOffset = 0.2f;

	// Token: 0x04000667 RID: 1639
	private Vector3 aim;

	// Token: 0x04000668 RID: 1640
	public float distance;

	// Token: 0x04000669 RID: 1641
	public Vector3 position;

	// Token: 0x0400066A RID: 1642
	public Vector3 positionLerp;

	// Token: 0x0400066B RID: 1643
	public Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);

	// Token: 0x0400066C RID: 1644
	public Quaternion rotationLerp;

	// Token: 0x0400066D RID: 1645
	private readonly float positionSpeed = 26f;

	// Token: 0x0400066E RID: 1646
	private readonly float rotationSpeed = 16f;

	// Token: 0x0400066F RID: 1647
	private float rotationAmount = 45f;

	// Token: 0x04000670 RID: 1648
	private Vector3 FinalCoord;

	// Token: 0x04000671 RID: 1649
	private Vector3 SavedCoord;
}
