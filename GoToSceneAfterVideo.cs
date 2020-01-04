using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

// Token: 0x020000D3 RID: 211
public class GoToSceneAfterVideo : MonoBehaviour
{
	// Token: 0x06000438 RID: 1080
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

	// Token: 0x06000439 RID: 1081
	private IEnumerator SceneChangeRoutine()
	{
		yield return new WaitUntil(() => this.videoPlayer.isPrepared && !this.videoPlayer.isPlaying && !this.isLoadingScene);
		this.isLoadingScene = true;
		yield return new WaitForSecondsRealtime(1f);
		SceneManager.LoadScene(this.sceneID);
		yield return null;
		yield break;
	}

	// Token: 0x0600043A RID: 1082
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

	// Token: 0x0600043B RID: 1083
	private void Update()
	{
		if (this.isLoadingScene)
		{
			this.loadingTextGroup.alpha = Mathf.MoveTowards(this.loadingTextGroup.alpha, 1f, 5f * Time.unscaledDeltaTime);
		}
	}

	// Token: 0x040005A9 RID: 1449
	public int sceneID;

	// Token: 0x040005AA RID: 1450
	public VideoPlayer videoPlayer;

	// Token: 0x040005AB RID: 1451
	private bool isLoadingScene;

	// Token: 0x040005AC RID: 1452
	public CanvasGroup loadingTextGroup;

	// Token: 0x04000AF6 RID: 2806
	private bool SkipMovie;
}
