using System;
using UnityEngine;

// Token: 0x020000F2 RID: 242
[RequireComponent(typeof(PlayerBuildSystem))]
public class PlayerBuildPlacement : MonoBehaviour
{
	// Token: 0x060004D9 RID: 1241 RVA: 0x00005F7F File Offset: 0x0000417F
	private void Awake()
	{
		this.playerSettings = base.transform.root.GetComponent<PlayerSettings>();
		this.buildSystem = base.GetComponent<PlayerBuildSystem>();
		this.rotation = Quaternion.Euler(0f, 0f, 0f);
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00005FBD File Offset: 0x000041BD
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

	// Token: 0x060004DB RID: 1243 RVA: 0x0001A158 File Offset: 0x00018358
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

	// Token: 0x060004DC RID: 1244 RVA: 0x0001A46C File Offset: 0x0001866C
	public Vector3 PositionToGrid(Vector3 position, float offsetMultiplier = 1f)
	{
		return new Vector3(Mathf.Round(position.x / (PlayerBuildPlacement.gridSize * offsetMultiplier)) * PlayerBuildPlacement.gridSize * offsetMultiplier, Mathf.Max(Mathf.Round(position.y / (PlayerBuildPlacement.gridSize * offsetMultiplier)) * PlayerBuildPlacement.gridSize * offsetMultiplier, 0f), Mathf.Round(position.z / (PlayerBuildPlacement.gridSize * offsetMultiplier)) * PlayerBuildPlacement.gridSize * offsetMultiplier);
	}

	// Token: 0x0400065C RID: 1628
	private PlayerSettings playerSettings;

	// Token: 0x0400065D RID: 1629
	private PlayerBuildSystem buildSystem;

	// Token: 0x0400065E RID: 1630
	public static float gridSize = 0.6f;

	// Token: 0x0400065F RID: 1631
	public static float boundsOffset = 0.2f;

	// Token: 0x04000660 RID: 1632
	private Vector3 aim;

	// Token: 0x04000661 RID: 1633
	public float distance;

	// Token: 0x04000662 RID: 1634
	public Vector3 position;

	// Token: 0x04000663 RID: 1635
	public Vector3 positionLerp;

	// Token: 0x04000664 RID: 1636
	public Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);

	// Token: 0x04000665 RID: 1637
	public Quaternion rotationLerp;

	// Token: 0x04000666 RID: 1638
	private readonly float positionSpeed = 26f;

	// Token: 0x04000667 RID: 1639
	private readonly float rotationSpeed = 16f;

	// Token: 0x04000668 RID: 1640
	private float rotationAmount = 45f;
}
