using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000120 RID: 288
public class SceneChangeRemote : MonoBehaviour
{
	// Token: 0x0600063C RID: 1596 RVA: 0x00007073 File Offset: 0x00005273
	public void ChangeScene(string sceneName)
	{
		base.StartCoroutine(this.ChangeSceneRoutine(sceneName));
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x00007083 File Offset: 0x00005283
	public void ChangeSceneDirectly(string sceneName)
	{
		base.StartCoroutine(this.ChangeSceneDirectlyRoutine(sceneName));
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0001CA68 File Offset: 0x0001AC68
	public void ReloadCurrentScene()
	{
		SceneController.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x00007093 File Offset: 0x00005293
	public void ReloadCurrentSceneDirectly()
	{
		base.StartCoroutine(this.ReloadCurrentSceneDirectlyRoutine());
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x000070A2 File Offset: 0x000052A2
	private IEnumerator ReloadCurrentSceneDirectlyRoutine()
	{
		yield return new WaitForSecondsRealtime(this.waitDuration);
		SceneController.LoadSceneDirectly(SceneManager.GetActiveScene().buildIndex);
		yield return null;
		yield break;
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x000070B1 File Offset: 0x000052B1
	private IEnumerator ChangeSceneRoutine(string sceneName)
	{
		yield return new WaitForSecondsRealtime(this.waitDuration);
		SceneController.LoadScene(sceneName);
		yield return null;
		yield break;
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x000070C7 File Offset: 0x000052C7
	private IEnumerator ChangeSceneDirectlyRoutine(string sceneName)
	{
		yield return new WaitForSecondsRealtime(this.waitDuration);
		SceneController.LoadSceneDirectly(sceneName);
		yield return null;
		yield break;
	}

	// Token: 0x04000745 RID: 1861
	private readonly float waitDuration = 0.3f;
}
