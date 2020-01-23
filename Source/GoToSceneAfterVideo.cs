using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

// Token: 0x020000D5 RID: 213
public class GoToSceneAfterVideo : MonoBehaviour
{
	// Token: 0x06000445 RID: 1093 RVA: 0x00018298 File Offset: 0x00016498
	private void Start()
	{
		if (!File.Exists(Application.persistentDataPath + "\\mod_settings.txt"))
		{
			base.StartCoroutine(this.SceneChangeRoutine());
			base.StartCoroutine(this.BackupSceneChangeRoutineWithoutVideo());
			return;
		}
		string[] array = File.ReadAllText(Application.persistentDataPath + "\\mod_settings.txt").Split(new char[]
		{
			'\n'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				':'
			});
			if (array2[0] == "SkipIntroMovie")
			{
				if (array2[1] == "true")
				{
					this.SkipMovie = true;
				}
				else
				{
					this.SkipMovie = false;
				}
			}
		}
		if (this.SkipMovie)
		{
			SceneManager.LoadScene(this.sceneID);
			return;
		}
		base.StartCoroutine(this.SceneChangeRoutine());
		base.StartCoroutine(this.BackupSceneChangeRoutineWithoutVideo());
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x000058B3 File Offset: 0x00003AB3
	private IEnumerator SceneChangeRoutine()
	{
		yield return new WaitUntil(() => this.videoPlayer.isPrepared && !this.videoPlayer.isPlaying && !this.isLoadingScene);
		this.isLoadingScene = true;
		yield return new WaitForSecondsRealtime(1f);
		SceneManager.LoadScene(this.sceneID);
		yield return null;
		yield break;
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x000058C2 File Offset: 0x00003AC2
	private IEnumerator BackupSceneChangeRoutineWithoutVideo()
	{
		yield return new WaitForSecondsRealtime(16f);
		if (this.isLoadingScene)
		{
			yield break;
		}
		this.isLoadingScene = true;
		yield return new WaitForSecondsRealtime(1f);
		SceneManager.LoadScene(this.sceneID);
		yield return null;
		yield break;
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x000058D1 File Offset: 0x00003AD1
	private void Update()
	{
		if (this.isLoadingScene)
		{
			this.loadingTextGroup.alpha = Mathf.MoveTowards(this.loadingTextGroup.alpha, 1f, 5f * Time.unscaledDeltaTime);
		}
	}

	// Token: 0x040005B5 RID: 1461
	public int sceneID;

	// Token: 0x040005B6 RID: 1462
	public VideoPlayer videoPlayer;

	// Token: 0x040005B7 RID: 1463
	private bool isLoadingScene;

	// Token: 0x040005B8 RID: 1464
	public CanvasGroup loadingTextGroup;

	// Token: 0x040005B9 RID: 1465
	private bool SkipMovie;
}
