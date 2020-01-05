using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

// Token: 0x020000D4 RID: 212
public class GoToSceneAfterVideo : MonoBehaviour
{
	// Token: 0x0600043F RID: 1087 RVA: 0x00017F74 File Offset: 0x00016174
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

	// Token: 0x06000440 RID: 1088 RVA: 0x00005878 File Offset: 0x00003A78
	private IEnumerator SceneChangeRoutine()
	{
		yield return new WaitUntil(() => this.videoPlayer.isPrepared && !this.videoPlayer.isPlaying && !this.isLoadingScene);
		this.isLoadingScene = true;
		yield return new WaitForSecondsRealtime(1f);
		SceneManager.LoadScene(this.sceneID);
		yield return null;
		yield break;
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00005887 File Offset: 0x00003A87
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

	// Token: 0x06000442 RID: 1090 RVA: 0x00005896 File Offset: 0x00003A96
	private void Update()
	{
		if (this.isLoadingScene)
		{
			this.loadingTextGroup.alpha = Mathf.MoveTowards(this.loadingTextGroup.alpha, 1f, 5f * Time.unscaledDeltaTime);
		}
	}

	// Token: 0x040005AE RID: 1454
	public int sceneID;

	// Token: 0x040005AF RID: 1455
	public VideoPlayer videoPlayer;

	// Token: 0x040005B0 RID: 1456
	private bool isLoadingScene;

	// Token: 0x040005B1 RID: 1457
	public CanvasGroup loadingTextGroup;

	// Token: 0x040005B2 RID: 1458
	private bool SkipMovie;
}
