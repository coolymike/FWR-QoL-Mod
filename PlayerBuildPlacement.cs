using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
[RequireComponent(typeof(PlayerBuildSystem))]
public class PlayerBuildPlacement : MonoBehaviour
{
	// Token: 0x060004CA RID: 1226
	private void Awake()
	{
		this.playerSettings = base.transform.root.GetComponent<PlayerSettings>();
		this.buildSystem = base.GetComponent<PlayerBuildSystem>();
		this.rotation = Quaternion.Euler(0f, 0f, 0f);
	}

	// Token: 0x060004CB RID: 1227
	private void Update()
	{
		if (LevelManager.BuildModeOn)
		{
			this.UpdateItemPlacement();
		}
	}

	// Token: 0x060004CC RID: 1228
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
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x, this.rotation.eulerAngles.y - 45f, this.rotation.eulerAngles.z);
		}
		if (InputManager.RotateObjectRight())
		{
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x, this.rotation.eulerAngles.y + rotationAmount, this.rotation.eulerAngles.z);
		}
		if (InputManager.RotateObjectXPositive())
		{
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x + rotationAmount, this.rotation.eulerAngles.y, this.rotation.eulerAngles.z);
		}
		if (InputManager.RotateObjectXNegative())
		{
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x - rotationAmount, this.rotation.eulerAngles.y, this.rotation.eulerAngles.z);
		}
		if (InputManager.RotateObjectZPositive())
		{
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x, this.rotation.eulerAngles.y, this.rotation.eulerAngles.z + rotationAmount);
		}
		if (InputManager.RotateObjectZNegative())
		{
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x, this.rotation.eulerAngles.y, this.rotation.eulerAngles.z - rotationAmount);
		}
		if (InputManager.SmallRotation())
		{
			rotationAmount = 1f;
		} else {
			rotationAmount = 45f;
		}
	}

	// Token: 0x060004CD RID: 1229
	public Vector3 PositionToGrid(Vector3 position, float offsetMultiplier = 1f)
	{
		return new Vector3(Mathf.Round(position.x / (PlayerBuildPlacement.gridSize * offsetMultiplier)) * PlayerBuildPlacement.gridSize * offsetMultiplier, Mathf.Max(Mathf.Round(position.y / (PlayerBuildPlacement.gridSize * offsetMultiplier)) * PlayerBuildPlacement.gridSize * offsetMultiplier, 0f), Mathf.Round(position.z / (PlayerBuildPlacement.gridSize * offsetMultiplier)) * PlayerBuildPlacement.gridSize * offsetMultiplier);
	}

	// Token: 0x060004CE RID: 1230
	public PlayerBuildPlacement()
	{
	}

	// Token: 0x060004CF RID: 1231
	static PlayerBuildPlacement()
	{
	}

	// Token: 0x0400064E RID: 1614
	private PlayerSettings playerSettings;

	// Token: 0x0400064F RID: 1615
	private PlayerBuildSystem buildSystem;

	// Token: 0x04000650 RID: 1616
	public static readonly float gridSize = 0.6f;

	// Token: 0x04000651 RID: 1617
	public static float boundsOffset = 0.2f;

	// Token: 0x04000652 RID: 1618
	private Vector3 aim;

	// Token: 0x04000653 RID: 1619
	public float distance;

	// Token: 0x04000654 RID: 1620
	public Vector3 position;

	// Token: 0x04000655 RID: 1621
	public Vector3 positionLerp;

	// Token: 0x04000656 RID: 1622
	public Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);

	// Token: 0x04000657 RID: 1623
	public Quaternion rotationLerp;

	// Token: 0x04000658 RID: 1624
	private readonly float positionSpeed = 26f;

	// Token: 0x04000659 RID: 1625
	private readonly float rotationSpeed = 16f;

	private float rotationAmount = 45f;
}
