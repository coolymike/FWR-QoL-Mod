using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008C RID: 140
public class UIMenuManager : MonoBehaviour
{
	// Token: 0x060002D3 RID: 723 RVA: 0x00013904 File Offset: 0x00011B04
	private void Awake()
	{
		UIMenuManager.reference.Add(this);
		for (int i = 0; i < this.menus.Length; i++)
		{
			this.menus[i].menu.menuManager = this;
			this.menus[i].menu.thisMenuID = this.menus[i].id;
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x0000475B File Offset: 0x0000295B
	private void OnDestroy()
	{
		UIMenuManager.reference.Remove(this);
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00004769 File Offset: 0x00002969
	private void Start()
	{
		if (this.autoOpenMainMenu)
		{
			this.ReturnToMainMenu();
		}
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00004779 File Offset: 0x00002979
	private void Update()
	{
		MouseControl.HideMouse(!UIMenuManager.AnyMenuActive());
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00013964 File Offset: 0x00011B64
	public void GoToMenu(int menuID)
	{
		for (int i = 0; i < this.menus.Length; i++)
		{
			this.menus[i].menu.Active = (this.menus[i].id == menuID);
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x000139A8 File Offset: 0x00011BA8
	public void CloseMenu(int menuID)
	{
		for (int i = 0; i < this.menus.Length; i++)
		{
			if (this.menus[i].id == menuID)
			{
				this.menus[i].menu.Active = false;
			}
		}
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x000139EC File Offset: 0x00011BEC
	public void CloseMenus(int[] menuIDs)
	{
		for (int i = 0; i < this.menus.Length; i++)
		{
			for (int j = 0; j < menuIDs.Length; j++)
			{
				if (this.menus[i].id == j && this.menus[i].menu.Active)
				{
					this.menus[i].menu.Active = false;
				}
			}
		}
	}

	// Token: 0x060002DA RID: 730 RVA: 0x00013A54 File Offset: 0x00011C54
	public int GetActiveMenu()
	{
		for (int i = 0; i < this.menus.Length; i++)
		{
			if (this.menus[i].menu.Active)
			{
				return i;
			}
		}
		return 999999999;
	}

	// Token: 0x060002DB RID: 731 RVA: 0x00013A90 File Offset: 0x00011C90
	public void ReturnToMainMenu()
	{
		for (int i = 0; i < this.menus.Length; i++)
		{
			this.menus[i].menu.Active = this.menus[i].isMainMenu;
		}
	}

	// Token: 0x060002DC RID: 732 RVA: 0x00013AD0 File Offset: 0x00011CD0
	public void CloseAllMenus()
	{
		for (int i = 0; i < this.menus.Length; i++)
		{
			this.menus[i].menu.Active = false;
		}
	}

	// Token: 0x060002DD RID: 733 RVA: 0x00013B04 File Offset: 0x00011D04
	public static bool AnyMenuActive()
	{
		for (int i = 0; i < UIMenuManager.reference.Count; i++)
		{
			for (int j = 0; j < UIMenuManager.reference[i].menus.Length; j++)
			{
				if (UIMenuManager.reference[i].menus[j].menu.Active)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04000431 RID: 1073
	public static List<UIMenuManager> reference = new List<UIMenuManager>();

	// Token: 0x04000432 RID: 1074
	public bool autoOpenMainMenu;

	// Token: 0x04000433 RID: 1075
	public UIMenuManager.Menu[] menus;

	// Token: 0x0200008D RID: 141
	[Serializable]
	public class Menu
	{
		// Token: 0x04000434 RID: 1076
		public UIMenu menu;

		// Token: 0x04000435 RID: 1077
		public int id;

		// Token: 0x04000436 RID: 1078
		public bool isMainMenu;
	}
}
