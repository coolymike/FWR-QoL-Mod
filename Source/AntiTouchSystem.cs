using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class AntiTouchSystem : MonoBehaviour
{
	// Token: 0x060004B1 RID: 1201 RVA: 0x00005DDE File Offset: 0x00003FDE
	private void Awake()
	{
		AntiTouchSystem.reference = base.GetComponent<AntiTouchSystem>();
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00019760 File Offset: 0x00017960
	public static void PlayerTouchedRagdoll(PlayerSettings playerSettings, Transform ragdoll)
	{
		if (!Tag.Compare(ragdoll, Tag.Tags.Ragdoll))
		{
			return;
		}
		if (LevelManager.BuildModeOn)
		{
			return;
		}
		if (!playerSettings.ragdollsCanTouchThisPlayer)
		{
			AntiTouchSystem.KillSmartRagdoll(ragdoll);
		}
		AntiTouchSystem.antiTouchScript = ragdoll.root.GetComponent<AntiTouchObject>();
		if (AntiTouchSystem.antiTouchScript == null)
		{
			return;
		}
		if ((AntiTouchSystem.antiTouchScript.antiTouchInBuildMode || !AntiTouchSystem.antiTouchScript.antiTouchInBuildMode) && AntiTouchSystem.antiTouchScript.badForPlayer)
		{
			AntiTouchSystem.KillPlayer(playerSettings);
		}
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x000197D8 File Offset: 0x000179D8
	public static void ObjectTouchedSomething(AntiTouchObject antiTouchScript, Transform touchedObject)
	{
		if (antiTouchScript.antiTouchInBuildMode || (!antiTouchScript.antiTouchInBuildMode && !LevelManager.BuildModeOn))
		{
			if (antiTouchScript.badForPlayer && Tag.Compare(touchedObject, Tag.Tags.Player))
			{
				AntiTouchSystem.KillPlayer(touchedObject.transform.root.GetComponent<PlayerSettings>());
			}
			if (antiTouchScript.badForSmartRagdolls && Tag.Compare(touchedObject, Tag.Tags.Ragdoll))
			{
				if (antiTouchScript.specifiedSmartRagdolls.Length != 0)
				{
					for (int i = 0; i < antiTouchScript.specifiedSmartRagdolls.Length; i++)
					{
						if (SmartRagdollController.RagdollHasLogic(touchedObject, antiTouchScript.specifiedSmartRagdolls[i]))
						{
							AntiTouchSystem.KillSmartRagdoll(touchedObject);
							return;
						}
					}
					return;
				}
				AntiTouchSystem.KillSmartRagdoll(touchedObject);
			}
		}
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00005DEB File Offset: 0x00003FEB
	public static void KillPlayer(PlayerSettings playerSettings)
	{
		if (playerSettings == null)
		{
			return;
		}
		if (playerSettings.simulatedRagdoll.RagdollModeEnabled || playerSettings.simulatedRagdoll.ragdollInCooldown)
		{
			return;
		}
		playerSettings.simulatedRagdoll.RagdollModeEnabled = true;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00019870 File Offset: 0x00017A70
	public static void KillSmartRagdoll(Transform ragdoll)
	{
		SmartRagdollController componentInChildren = ragdoll.root.GetComponentInChildren<SmartRagdollController>();
		if (componentInChildren == null)
		{
			return;
		}
		if (componentInChildren.simulatedRagdoll == null)
		{
			return;
		}
		componentInChildren.simulatedRagdoll.RagdollModeEnabled = true;
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x000198B0 File Offset: 0x00017AB0
	public static AntiTouchObject AddAntiTouchComponent(GameObject _antiTouchObject, bool _addToTriggerCollider = false)
	{
		if (_antiTouchObject.transform.root.GetComponentInChildren<AntiTouchObject>() == null)
		{
			foreach (Collider collider in _antiTouchObject.transform.root.GetComponentsInChildren<Collider>())
			{
				if (collider.isTrigger && _addToTriggerCollider)
				{
					return collider.gameObject.AddComponent<AntiTouchObject>();
				}
				if (!collider.isTrigger && !_addToTriggerCollider)
				{
					return collider.gameObject.AddComponent<AntiTouchObject>();
				}
			}
		}
		return null;
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00005E1E File Offset: 0x0000401E
	public static void SetAntiTouchValues(AntiTouchObject _antiTouchSettings, bool _badForPlayer, bool _badforSmartRagdolls, bool _deathOnTouch, bool? _ragdollsCanTouchPlayer = null)
	{
		if (_antiTouchSettings != null)
		{
			_antiTouchSettings.badForPlayer = _badForPlayer;
			_antiTouchSettings.badForSmartRagdolls = _badforSmartRagdolls;
			if (AntiTouchSystem.reference != null && _ragdollsCanTouchPlayer != null)
			{
				AntiTouchSystem.reference.ragdollsCanTouchPlayer = _ragdollsCanTouchPlayer.Value;
			}
		}
	}

	// Token: 0x04000642 RID: 1602
	public static AntiTouchSystem reference;

	// Token: 0x04000643 RID: 1603
	[Header("Player Settings")]
	public Dictionary<int, bool> playersTouched = new Dictionary<int, bool>();

	// Token: 0x04000644 RID: 1604
	public bool ragdollsCanTouchPlayer;

	// Token: 0x04000645 RID: 1605
	private static AntiTouchObject antiTouchScript;
}
