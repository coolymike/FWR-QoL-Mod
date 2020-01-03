using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
[RequireComponent(typeof(PlayerBuildSystem))]
public partial class PlayerBuildPlacement : MonoBehaviour
{
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
			this.rotation = Quaternion.Euler(this.rotation.eulerAngles.x, this.rotation.eulerAngles.y - rotationAmount, this.rotation.eulerAngles.z);
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
			this.rotationAmount = 1f;
			return;
		}
		this.rotationAmount = 45f;
	}
}
