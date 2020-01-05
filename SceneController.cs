using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000122 RID: 290
public class SceneController : MonoBehaviour
{
	// Token: 0x06000647 RID: 1607 RVA: 0x000070C4 File Offset: 0x000052C4
	private void Update()
	{
		if (!SceneController.isLoadingNextScene)
		{
			base.StartCoroutine(this.LoadSceneAsyncRoutine(SceneController.nextSceneToLoad));
		}
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x000070DF File Offset: 0x000052DF
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

	// Token: 0x06000649 RID: 1609 RVA: 0x000070F5 File Offset: 0x000052F5
	public bool IsSceneReady()
	{
		return this.asyncOperation != null && this.asyncOperation.progress >= 0.9f;
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x00007116 File Offset: 0x00005316
	public void ActivateLoadedScene()
	{
		this.asyncOperation.allowSceneActivation = true;
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x00007124 File Offset: 0x00005324
	public static int GetSceneIndex(string sceneName)
	{
		if (!SceneController.sceneIndex.ContainsKey(sceneName.ToLower()))
		{
			Debug.LogError("Scene Name not availible");
			return 0;
		}
		return SceneController.sceneIndex[sceneName.ToLower()];
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x00007154 File Offset: 0x00005354
	public static void LoadScene(string sceneName)
	{
		SceneController.nextSceneToLoad = SceneController.GetSceneIndex(sceneName);
		SceneController.isLoadingNextScene = false;
		SceneController.loadSceneManually = true;
		SceneManager.LoadScene(SceneController.GetSceneIndex("loadingscreen"));
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0000717C File Offset: 0x0000537C
	public static void LoadScene(int sceneIndex)
	{
		SceneController.nextSceneToLoad = sceneIndex;
		SceneController.isLoadingNextScene = false;
		SceneController.loadSceneManually = true;
		SceneManager.LoadScene(SceneController.GetSceneIndex("loadingscreen"));
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0000719F File Offset: 0x0000539F
	public static void LoadSceneDirectly(string sceneName)
	{
		SceneController.nextSceneToLoad = SceneController.GetSceneIndex(sceneName);
		SceneController.isLoadingNextScene = false;
		SceneController.loadSceneManually = false;
		SceneManager.LoadScene(SceneController.GetSceneIndex("loadingscreen"));
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x000071C7 File Offset: 0x000053C7
	public static void LoadSceneDirectly(int sceneIndex)
	{
		SceneController.nextSceneToLoad = sceneIndex;
		SceneController.isLoadingNextScene = false;
		SceneController.loadSceneManually = false;
		SceneManager.LoadScene(SceneController.GetSceneIndex("loadingscreen"));
	}

	// Token: 0x0400073C RID: 1852
	public float loadingProgress;

	// Token: 0x0400073D RID: 1853
	private AsyncOperation asyncOperation;

	// Token: 0x0400073E RID: 1854
	private static int nextSceneToLoad;

	// Token: 0x0400073F RID: 1855
	private static bool isLoadingNextScene;

	// Token: 0x04000740 RID: 1856
	private static bool loadSceneManually;

	// Token: 0x04000741 RID: 1857
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
