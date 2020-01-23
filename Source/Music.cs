using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000058 RID: 88
public class Music : MonoBehaviour
{
	// Token: 0x060001B7 RID: 439 RVA: 0x0000E750 File Offset: 0x0000C950
	private void Awake()
	{
		LevelManager.OnWinStateChange += this.OnWinStateChange;
		SceneManager.sceneLoaded += this.OnSceneLoaded;
		base.InvokeRepeating("PlayLevelMusic", 20f, 200f);
		this.mainMusicSource.clip = this.transitionMusic;
		this.mainMusicSource.Play();
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00003A1A File Offset: 0x00001C1A
	private void OnDestroy()
	{
		LevelManager.OnWinStateChange -= this.OnWinStateChange;
		SceneManager.sceneLoaded -= this.OnSceneLoaded;
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x0000E7B0 File Offset: 0x0000C9B0
	private void OnWinStateChange(LevelManager.WinState winState)
	{
		if (winState == LevelManager.WinState.None)
		{
			return;
		}
		if (winState != LevelManager.WinState.Win)
		{
			if (winState == LevelManager.WinState.Lose)
			{
				this.winLoseSource.clip = this.youLoseMusic;
			}
		}
		else
		{
			this.winLoseSource.clip = this.youWinMusic;
		}
		this.winLoseSource.Play();
		base.Invoke("StopMainMusic", 3f);
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00003A3E File Offset: 0x00001C3E
	private void StopMainMusic()
	{
		this.mainMusicSource.Stop();
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000E80C File Offset: 0x0000CA0C
	private void Update()
	{
		if (LevelManager.StatusIsWinOrLose(false))
		{
			this.mainMusicSource.volume = Mathf.MoveTowards(this.mainMusicSource.volume, 0f, Time.unscaledDeltaTime * this.transitionSpeed);
			this.winLoseSource.volume = Mathf.MoveTowards(this.winLoseSource.volume, 1f, Time.unscaledDeltaTime * this.transitionSpeed);
			return;
		}
		this.mainMusicSource.volume = Mathf.MoveTowards(this.mainMusicSource.volume, 1f, Time.unscaledDeltaTime * this.transitionSpeed);
		this.winLoseSource.volume = Mathf.MoveTowards(this.winLoseSource.volume, 0f, Time.unscaledDeltaTime * this.transitionSpeed);
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000E8D4 File Offset: 0x0000CAD4
	private void PlayLevelMusic()
	{
		if (this.mainMusicSource.isPlaying || this.winLoseSource.isPlaying || LevelManager.StatusIsWinOrLose(true))
		{
			return;
		}
		if (SceneManager.GetActiveScene().name.ToLower() == "sandbox")
		{
			switch (UnityEngine.Random.Range(0, 3))
			{
			case 0:
				this.mainMusicSource.clip = this.sandboxMusic;
				break;
			case 1:
				this.mainMusicSource.clip = this.transitionMusic;
				break;
			case 2:
				this.mainMusicSource.clip = this.gameplayMusic;
				break;
			}
			this.mainMusicSource.Play();
			return;
		}
		if (SceneManager.GetActiveScene().name.ToLower().Contains("minigame"))
		{
			int num = UnityEngine.Random.Range(0, 2);
			if (num != 0)
			{
				if (num == 1)
				{
					this.mainMusicSource.clip = this.gameplayMusic;
				}
			}
			else
			{
				this.mainMusicSource.clip = this.mainThemeMusic;
			}
			this.mainMusicSource.Play();
		}
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00003A4B File Offset: 0x00001C4B
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (UnityEngine.Random.Range(3, 5) == 3)
		{
			this.PlayLevelMusic();
		}
	}

	// Token: 0x0400027A RID: 634
	public AudioSource mainMusicSource;

	// Token: 0x0400027B RID: 635
	public AudioSource winLoseSource;

	// Token: 0x0400027C RID: 636
	[Header("Music Tracks")]
	public AudioClip mainThemeMusic;

	// Token: 0x0400027D RID: 637
	public AudioClip gameplayMusic;

	// Token: 0x0400027E RID: 638
	public AudioClip sandboxMusic;

	// Token: 0x0400027F RID: 639
	public AudioClip transitionMusic;

	// Token: 0x04000280 RID: 640
	public AudioClip youWinMusic;

	// Token: 0x04000281 RID: 641
	public AudioClip youLoseMusic;

	// Token: 0x04000282 RID: 642
	private LevelManager levelManager;

	// Token: 0x04000283 RID: 643
	private float transitionSpeed = 0.6f;
}
