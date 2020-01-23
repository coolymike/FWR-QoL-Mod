using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000124 RID: 292
public class SceneController : MonoBehaviour
{
	// Token: 0x06000656 RID: 1622 RVA: 0x00007135 File Offset: 0x00005335
	private void Update()
	{
		if (!SceneController.isLoadingNextScene)
		{
			base.StartCoroutine(this.LoadSceneAsyncRoutine(SceneController.nextSceneToLoad));
		}
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x00007150 File Offset: 0x00005350
	private IEnumerator LoadSceneAsyncRoutine(int sceneIndex)
	{
		SceneController.isLoadingNextScene = true;
		this.asyncOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
		this.asyncOperation.allowSceneActivation = !SceneController.loadSceneManually;
		while (!this.asyncOperation.isDone)
		{
			this.loadingProgress = Mathf.Clamp01(this.asyncOperation.progress / 0.9f);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x00007166 File Offset: 0x00005366
	public bool IsSceneReady()
	{
		return this.asyncOperation != null && this.asyncOperation.progress >= 0.9f;
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x00007187 File Offset: 0x00005387
	public void ActivateLoadedScene()
	{
		this.asyncOperation.allowSceneActivation = true;
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00007195 File Offset: 0x00005395
	public static int GetSceneIndex(string sceneName)
	{
		if (!SceneController.sceneIndex.ContainsKey(sceneName.ToLower()))
		{
			Debug.LogError("Scene Name not availible");
			return 0;
		}
		return SceneController.sceneIndex[sceneName.ToLower()];
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x000071C5 File Offset: 0x000053C5
	public static void LoadScene(string sceneName)
	{
		SceneController.nextSceneToLoad = SceneController.GetSceneIndex(sceneName);
		SceneController.isLoadingNextScene = false;
		SceneController.loadSceneManually = true;
		SceneManager.LoadScene(SceneController.GetSceneIndex("loadingscreen"));
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x000071ED File Offset: 0x000053ED
	public static void LoadScene(int sceneIndex)
	{
		SceneController.nextSceneToLoad = sceneIndex;
		SceneController.isLoadingNextScene = false;
		SceneController.loadSceneManually = true;
		SceneManager.LoadScene(SceneController.GetSceneIndex("loadingscreen"));
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x00007210 File Offset: 0x00005410
	public static void LoadSceneDirectly(string sceneName)
	{
		SceneController.nextSceneToLoad = SceneController.GetSceneIndex(sceneName);
		SceneController.isLoadingNextScene = false;
		SceneController.loadSceneManually = false;
		SceneManager.LoadScene(SceneController.GetSceneIndex("loadingscreen"));
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x00007238 File Offset: 0x00005438
	public static void LoadSceneDirectly(int sceneIndex)
	{
		SceneController.nextSceneToLoad = sceneIndex;
		SceneController.isLoadingNextScene = false;
		SceneController.loadSceneManually = false;
		SceneManager.LoadScene(SceneController.GetSceneIndex("loadingscreen"));
	}

	// Token: 0x04000751 RID: 1873
	public float loadingProgress;

	// Token: 0x04000752 RID: 1874
	private AsyncOperation asyncOperation;

	// Token: 0x04000753 RID: 1875
	private static int nextSceneToLoad;

	// Token: 0x04000754 RID: 1876
	private static bool isLoadingNextScene;

	// Token: 0x04000755 RID: 1877
	private static bool loadSceneManually;

	// Token: 0x04000756 RID: 1878
	private static Dictionary<string, int> sceneIndex = new Dictionary<string, int>
	{
		{
			"mainmenu",
			1
		},
		{
			"loadingscreen",
			2
		},
		{
			"sandbox",
			3
		},
		{
			"tutorial",
			4
		},
		{
			"mg-fortinvasion",
			5
		},
		{
			"mg-roadcross",
			6
		},
		{
			"e-thepit",
			7
		},
		{
			"e-prongwall",
			8
		},
		{
			"e-tower",
			9
		}
	};
}
