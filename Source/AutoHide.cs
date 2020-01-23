using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class AutoHide : MonoBehaviour
{
	// Token: 0x06000426 RID: 1062 RVA: 0x000057AD File Offset: 0x000039AD
	private void OnDisable()
	{
		base.CancelInvoke("Hide");
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x000057BA File Offset: 0x000039BA
	private void OnEnable()
	{
		base.Invoke("Hide", this.timeUntilHide);
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x000057CD File Offset: 0x000039CD
	private void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x000057DB File Offset: 0x000039DB
	public static AutoHide AddComponent(GameObject _object, float _time)
	{
		_object.AddComponent<AutoHide>();
		AutoHide component = _object.GetComponent<AutoHide>();
		component.timeUntilHide = _time;
		return component;
	}

	// Token: 0x04000599 RID: 1433
	public float timeUntilHide = 5f;
}
