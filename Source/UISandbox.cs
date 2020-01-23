using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000090 RID: 144
public class UISandbox : MonoBehaviour
{
	// Token: 0x060002EB RID: 747 RVA: 0x0000488D File Offset: 0x00002A8D
	private void Start()
	{
		this.RefreshWorldButtonList();
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00013E78 File Offset: 0x00012078
	public void RefreshWorldButtonList()
	{
		for (int i = 0; i < this.spawnedButtons.Count; i++)
		{
			UnityEngine.Object.Destroy(this.spawnedButtons[i].gameObject);
		}
		this.spawnedButtons.Clear();
		this.worldFileLocations = FileManager.GetWorldFileDirectories();
		for (int k = 0; k < this.worldFileLocations.Count; k++)
		{
			this.worldButton = UnityEngine.Object.Instantiate<GameObject>(this.sandboxButtonPrefab, this.content).GetComponent<UISandboxWorldButton>();
			this.spawnedButtons.Add(this.worldButton.gameObject);
			this.worldButton.gameObject.SetActive(true);
			this.worldButton.buttonTitle.text = FileManager.GetWorldName(this.worldFileLocations[k]);
			int j = k;
			this.worldButton.mainButton.onClick.AddListener(delegate
			{
				this.LoadWorld(this.worldFileLocations[j]);
			});
			this.worldButton.moreInfoButton.onClick.AddListener(delegate
			{
				this.EditWorld(this.worldFileLocations[j]);
			});
		}
	}

	// Token: 0x060002ED RID: 749 RVA: 0x00004895 File Offset: 0x00002A95
	public void LoadWorld(string worldFileLocation)
	{
		WorldData.worldFileLocation = worldFileLocation;
		WorldData.loadWorldOnStart = true;
		this.sceneChangeRemote.ChangeScene("sandbox");
	}

	// Token: 0x060002EE RID: 750 RVA: 0x000048B3 File Offset: 0x00002AB3
	public void EditWorld(string fileLocation)
	{
		this.worldInfoMenu.worldFileLocation = fileLocation;
		this.worldInfoMenu.UpdateWorldInfo(fileLocation, FileManager.GetWorldName(fileLocation));
	}

	// Token: 0x04000445 RID: 1093
	public SceneChangeRemote sceneChangeRemote;

	// Token: 0x04000446 RID: 1094
	public GameObject sandboxButtonPrefab;

	// Token: 0x04000447 RID: 1095
	public UIWorldInfo worldInfoMenu;

	// Token: 0x04000448 RID: 1096
	public Transform content;

	// Token: 0x04000449 RID: 1097
	private UISandboxWorldButton worldButton;

	// Token: 0x0400044A RID: 1098
	private List<string> worldFileLocations = new List<string>();

	// Token: 0x0400044B RID: 1099
	private List<GameObject> spawnedButtons = new List<GameObject>();
}
