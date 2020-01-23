using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class TouchInput : MonoBehaviour
{
	// Token: 0x060005C2 RID: 1474 RVA: 0x00006CBB File Offset: 0x00004EBB
	public void Shift(bool isOn)
	{
		InputManager.TouchInput.shift = isOn;
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x00006CC3 File Offset: 0x00004EC3
	public void Place(bool isOn)
	{
		InputManager.TouchInput.place = isOn;
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00006CCB File Offset: 0x00004ECB
	public void Remove(bool isOn)
	{
		InputManager.TouchInput.remove = isOn;
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x00006CD3 File Offset: 0x00004ED3
	public void Jumping(bool isOn)
	{
		InputManager.TouchInput.jumping = isOn;
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x00006CDB File Offset: 0x00004EDB
	public void VehicleA(bool isOn)
	{
		InputManager.TouchInput.vehicleA = isOn;
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x00006CE3 File Offset: 0x00004EE3
	public void VehicleB(bool isOn)
	{
		InputManager.TouchInput.vehicleB = isOn;
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x00006CEB File Offset: 0x00004EEB
	public void Jump()
	{
		base.StartCoroutine(this.JumpRoutine());
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x00006CFA File Offset: 0x00004EFA
	private IEnumerator JumpRoutine()
	{
		InputManager.TouchInput.jump = true;
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		InputManager.TouchInput.jump = false;
		yield break;
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x00006D02 File Offset: 0x00004F02
	public void ToggleRagdoll()
	{
		base.StartCoroutine(this.ToggleRagdollRoutine());
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x00006D11 File Offset: 0x00004F11
	private IEnumerator ToggleRagdollRoutine()
	{
		InputManager.TouchInput.toggleRagdoll = true;
		yield return new WaitForEndOfFrame();
		InputManager.TouchInput.toggleRagdoll = false;
		yield break;
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x00006D19 File Offset: 0x00004F19
	public void RotateRight()
	{
		base.StartCoroutine(this.RotateRightRoutine());
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x00006D28 File Offset: 0x00004F28
	private IEnumerator RotateRightRoutine()
	{
		InputManager.TouchInput.rotateRight = true;
		yield return new WaitForEndOfFrame();
		InputManager.TouchInput.rotateRight = false;
		yield break;
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x00006D30 File Offset: 0x00004F30
	public void RotateLeft()
	{
		base.StartCoroutine(this.RotateLeftRoutine());
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x00006D3F File Offset: 0x00004F3F
	private IEnumerator RotateLeftRoutine()
	{
		InputManager.TouchInput.rotateLeft = true;
		yield return new WaitForEndOfFrame();
		InputManager.TouchInput.rotateLeft = false;
		yield break;
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x00006D47 File Offset: 0x00004F47
	public void ToggleBuildMode()
	{
		base.StartCoroutine(this.ToggleBuildModeRoutine());
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x00006D56 File Offset: 0x00004F56
	private IEnumerator ToggleBuildModeRoutine()
	{
		InputManager.TouchInput.toggleBuildMode = true;
		yield return new WaitForEndOfFrame();
		InputManager.TouchInput.toggleBuildMode = false;
		yield break;
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x00006D5E File Offset: 0x00004F5E
	public void SetSpawn()
	{
		base.StartCoroutine(this.SetSpawnRoutine());
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x00006D6D File Offset: 0x00004F6D
	private IEnumerator SetSpawnRoutine()
	{
		InputManager.TouchInput.setSpawn = true;
		yield return new WaitForEndOfFrame();
		InputManager.TouchInput.setSpawn = false;
		yield break;
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x00006D75 File Offset: 0x00004F75
	public void ToggleSlowMotion()
	{
		base.StartCoroutine(this.ToggleSlowMotionRoutine());
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x00006D84 File Offset: 0x00004F84
	private IEnumerator ToggleSlowMotionRoutine()
	{
		InputManager.TouchInput.toggleSlowMotion = true;
		yield return new WaitForEndOfFrame();
		InputManager.TouchInput.toggleSlowMotion = false;
		yield break;
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x00006D8C File Offset: 0x00004F8C
	public void ToggleBuildItemMenu()
	{
		base.StartCoroutine(this.ToggleBuildItemMenuRoutine());
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00006D9B File Offset: 0x00004F9B
	private IEnumerator ToggleBuildItemMenuRoutine()
	{
		InputManager.TouchInput.toggleBuildItemMenu = true;
		yield return new WaitForEndOfFrame();
		InputManager.TouchInput.toggleBuildItemMenu = false;
		yield break;
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x00006DA3 File Offset: 0x00004FA3
	public void ToggleEscape()
	{
		base.StartCoroutine(this.ToggleEscapeRoutine());
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x00006DB2 File Offset: 0x00004FB2
	private IEnumerator ToggleEscapeRoutine()
	{
		InputManager.TouchInput.escape = true;
		yield return new WaitForEndOfFrame();
		InputManager.TouchInput.escape = false;
		yield break;
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x00006DBA File Offset: 0x00004FBA
	public void Interact()
	{
		base.StartCoroutine(this.InteractRoutine());
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x00006DC9 File Offset: 0x00004FC9
	private IEnumerator InteractRoutine()
	{
		InputManager.TouchInput.interact = true;
		yield return new WaitForEndOfFrame();
		InputManager.TouchInput.interact = false;
		yield break;
	}
}
