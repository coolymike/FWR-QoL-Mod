using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012E RID: 302
public class Tutorial : MonoBehaviour
{
	// Token: 0x0600068E RID: 1678 RVA: 0x0001D6D0 File Offset: 0x0001B8D0
	private void Start()
	{
		base.StartCoroutine(this.TutorialRoutine());
		this.dummyDannyCollisionDetection.OnCollision += this.DannyOnCollision;
		TutorialPlatformJumpZone.JumpedOnPlatform += this.JumpedOnPlatform;
		if (!Tutorial.TutorialUnlocked)
		{
			this.playerController.enableMovement = false;
			this.playerController.enableJumping = false;
			this.playerController.enableFlying = false;
			this.playerGrabbing.enabled = false;
			this.playerRagdollMode.enabled = false;
			this.playerTeleport.allowSetSpawn = true;
			this.playerBuildSystem.enabled = false;
			this.menuManagerPlayerInput.enableBuildItemMenu = false;
		}
		this.tutorialText.text = "";
		this.tutorialCanvasGroup.alpha = 0f;
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0000749D File Offset: 0x0000569D
	private void OnDestroy()
	{
		this.dummyDannyCollisionDetection.OnCollision -= this.DannyOnCollision;
		TutorialPlatformJumpZone.JumpedOnPlatform -= this.JumpedOnPlatform;
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x000074C7 File Offset: 0x000056C7
	private void Update()
	{
		this.tutorialCanvasGroup.alpha = Mathf.MoveTowards(this.tutorialCanvasGroup.alpha, (float)(this.displayText ? 1 : 0), Time.deltaTime * 2f);
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x000074FC File Offset: 0x000056FC
	private void JumpedOnPlatform()
	{
		this.jumpedOnPlatform = true;
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x00007505 File Offset: 0x00005705
	private void DannyOnCollision(Transform hitObject, Vector3 velocityImpact)
	{
		this.dannyLanded = true;
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x0000750E File Offset: 0x0000570E
	private IEnumerator TutorialRoutine()
	{
		this.dummyDannyVoice.clip = this.TutorialVoice1;
		this.dummyDannyVoice.Play();
		yield return new WaitUntil(() => this.dannyLanded);
		this.playerTeleport.allowSetSpawn = false;
		this.dummyDannyVoice.clip = this.TutorialVoice2;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		if (Tutorial.TutorialUnlocked)
		{
			this.dummyDannyVoice.clip = this.TutorialVoice3V2;
		}
		else
		{
			this.dummyDannyVoice.clip = this.TutorialVoice3V1;
		}
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		this.dummyDannyVoice.clip = this.TutorialVoice4;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		this.playerController.enableMovement = true;
		if (Application.isMobilePlatform)
		{
			this.tutorialText.text = "DRAG the LEFT side of the SCREEN to MOVE\nDRAG the RIGHT side of the SCREEN to LOOK";
		}
		else
		{
			this.tutorialText.text = string.Concat(new string[]
			{
				"PRESS [",
				GameData.controls.moveForward.ToString().ToUpper(),
				"], [",
				GameData.controls.moveBackward.ToString().ToUpper(),
				"], [",
				GameData.controls.moveLeft.ToString().ToUpper(),
				"], OR [",
				GameData.controls.moveRight.ToString().ToUpper(),
				"]"
			});
		}
		this.displayText = true;
		yield return new WaitUntil(() => InputManager.Move(true).magnitude > 0f);
		yield return new WaitForSeconds(2f);
		yield return new WaitUntil(() => InputManager.Move(true).magnitude > 0f);
		this.displayText = false;
		this.dummyDannyVoice.clip = this.TutorialVoice5;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		this.playerController.enableJumping = true;
		if (Application.isMobilePlatform)
		{
			this.tutorialText.text = "TAP the JUMP icon";
		}
		else
		{
			this.tutorialText.text = "PRESS [" + GameData.controls.jump.ToString().ToUpper() + "] TO JUMP";
		}
		this.displayText = true;
		yield return new WaitUntil(() => this.jumpedOnPlatform);
		this.displayText = false;
		yield return new WaitForSeconds(0.5f);
		if (!Application.isMobilePlatform)
		{
			this.dummyDannyVoice.clip = this.TutorialVoice6;
			this.dummyDannyVoice.Play();
			yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
			this.playerGrabbing.enabled = true;
			this.tutorialText.text = "AIM & HOLD [LEFT MOUSE BUTTON] TO GRAB";
			this.displayText = true;
			yield return new WaitUntil(() => this.playerGrabbing.Grabbing && this.playerGrabbing.focusRigidbody.transform.root.GetInstanceID() == this.dummyDannyVoice.transform.root.GetInstanceID());
			this.displayText = false;
			if (Tutorial.TutorialUnlocked)
			{
				this.dummyDannyVoice.clip = this.TutorialVoice7P2;
			}
			else
			{
				this.dummyDannyVoice.clip = this.TutorialVoice7P1;
			}
			this.dummyDannyVoice.Play();
			yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
			yield return new WaitUntil(() => !this.playerGrabbing.Grabbing);
			yield return new WaitForSeconds(1f);
		}
		this.dummyDannyVoice.clip = this.TutorialVoice8;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		this.playerRagdollMode.enabled = true;
		if (Application.isMobilePlatform)
		{
			this.tutorialText.text = "TAP the RAGDOLL button to RAGDOLL";
		}
		else
		{
			this.tutorialText.text = "PRESS [" + GameData.controls.toggleRagdoll.ToString().ToUpper() + "] TO RAGDOLL";
		}
		this.displayText = true;
		yield return new WaitUntil(() => this.playerSettings.simulatedRagdoll.ragdollModeEnabled);
		this.displayText = false;
		this.playerRagdollMode.enabled = false;
		yield return new WaitForSeconds(1.2f);
		this.dummyDannyVoice.clip = this.TutorialVoice9;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		this.playerTeleport.allowSetSpawn = true;
		this.playerRagdollMode.enabled = true;
		this.displayText = true;
		if (Application.isMobilePlatform)
		{
			this.tutorialText.text = "TAP the RAGDOLL button to DE-RAGDOLL";
		}
		else
		{
			this.tutorialText.text = "PRESS [" + GameData.controls.toggleRagdoll.ToString().ToUpper() + "] TO DE-RAGDOLL";
		}
		yield return new WaitUntil(() => !this.playerSettings.simulatedRagdoll.ragdollModeEnabled);
		yield return new WaitForSeconds(0.5f);
		this.displayText = false;
		this.dummyDannyVoice.clip = this.TutorialVoice10;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		this.displayText = true;
		if (Application.isMobilePlatform)
		{
			this.tutorialText.text = "TAP the SET SPAWN button\n(Looks like a target)";
		}
		else
		{
			this.tutorialText.text = "PRESS [" + GameData.controls.setSpawn.ToString().ToUpper() + "] TO SET SPAWN";
		}
		yield return new WaitUntil(() => InputManager.SetSpawn());
		this.displayText = false;
		yield return new WaitForSeconds(0.5f);
		this.dummyDannyVoice.clip = this.TutorialVoice11;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		if (GameData.stats.XPToLevel(GameData.stats.XP) >= GameData.stats.maxLevel)
		{
			this.collisionPointsGoal = 0;
		}
		else
		{
			this.collisionPointsGoal = GameData.stats.XP + 20;
		}
		this.displayText = true;
		this.tutorialText.text = "Go hurt yourself, lol.";
		yield return new WaitForSeconds(2.5f);
		this.displayText = false;
		yield return new WaitForSeconds(2.5f);
		yield return new WaitUntil(() => this.collisionPointsGoal < GameData.stats.XP);
		this.dummyDannyVoice.clip = this.TutorialVoice12;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		this.displayText = true;
		if (Application.isMobilePlatform)
		{
			this.tutorialText.text = "You're on your own pal.";
		}
		else
		{
			this.tutorialText.text = "PRESS [" + GameData.controls.toggleBuildMode.ToString().ToUpper() + "] TO TOGGLE BUILD MODE";
		}
		if (Application.isMobilePlatform)
		{
			yield return new WaitUntil(() => Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended);
		}
		else
		{
			yield return new WaitUntil(() => Input.anyKeyDown);
		}
		this.displayText = false;
		this.crisisSoundSource.Play();
		yield return new WaitForSeconds(0.5f);
		this.dummyDannyVoice.clip = this.TutorialVoice13;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		this.playerBuildSystem.enabled = true;
		this.displayText = true;
		if (Application.isMobilePlatform)
		{
			this.tutorialText.text = "It's the button on the BOTTOM LEFT";
		}
		else
		{
			this.tutorialText.text = "PRESS [" + GameData.controls.toggleBuildMode.ToString().ToUpper() + "] TO NOT CAUSE AN EXISTENTIAL CRISIS";
		}
		yield return new WaitUntil(() => InputManager.ToggleBuildMode());
		this.displayText = false;
		this.dummyDannyVoice.clip = this.TutorialVoice14;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		this.menuManagerPlayerInput.enableBuildItemMenu = true;
		this.displayText = true;
		if (Application.isMobilePlatform)
		{
			this.tutorialText.text = "TAP the button ABOVE the BUILD MODE button";
		}
		else
		{
			this.tutorialText.text = "PRESS [" + GameData.controls.toggleBuildItemMenu.ToString().ToUpper() + "] TO TOGGLE BUILD ITEM MENU";
		}
		yield return new WaitUntil(() => this.playerBuildSystem.placeholder != null);
		this.displayText = false;
		this.dummyDannyVoice.clip = this.TutorialVoice15;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		this.playerBuildSystem.enabled = true;
		this.displayText = true;
		if (Application.isMobilePlatform)
		{
			this.tutorialText.text = "TAP or HOLD the PLUS button";
		}
		else
		{
			this.tutorialText.text = "PRESS [LEFT MOUSE BUTTON] TO PLACE";
		}
		yield return new WaitUntil(() => WorldData.instance.data.items.Count > 0);
		this.displayText = false;
		this.dummyDannyVoice.clip = this.TutorialVoice16;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		this.displayText = true;
		if (Application.isMobilePlatform)
		{
			this.tutorialText.text = "TAP or HOLD the MINUS button";
		}
		else
		{
			this.tutorialText.text = "PRESS [RIGHT MOUSE BUTTON] TO REMOVE";
		}
		yield return new WaitUntil(() => WorldData.instance.data.items.Count < 1);
		this.displayText = false;
		this.dummyDannyVoice.clip = this.TutorialVoice17;
		this.dummyDannyVoice.Play();
		yield return new WaitWhile(() => this.dummyDannyVoice.isPlaying);
		this.sceneChangeRemote.ChangeSceneDirectly("MainMenu");
		Debug.Log("Done");
		yield return null;
		yield break;
	}

	// Token: 0x04000784 RID: 1924
	public Text tutorialText;

	// Token: 0x04000785 RID: 1925
	public CanvasGroup tutorialCanvasGroup;

	// Token: 0x04000786 RID: 1926
	private bool displayText;

	// Token: 0x04000787 RID: 1927
	public SceneChangeRemote sceneChangeRemote;

	// Token: 0x04000788 RID: 1928
	[Header("Player")]
	public PlayerSettings playerSettings;

	// Token: 0x04000789 RID: 1929
	public PlayerBuildSystem playerBuildSystem;

	// Token: 0x0400078A RID: 1930
	public PlayerController playerController;

	// Token: 0x0400078B RID: 1931
	public PlayerGrabbing playerGrabbing;

	// Token: 0x0400078C RID: 1932
	public PlayerRagdollMode playerRagdollMode;

	// Token: 0x0400078D RID: 1933
	public PlayerTeleport playerTeleport;

	// Token: 0x0400078E RID: 1934
	public UIMenuManagerPlayerInput menuManagerPlayerInput;

	// Token: 0x0400078F RID: 1935
	[Header("Danny")]
	public AudioSource dummyDannyVoice;

	// Token: 0x04000790 RID: 1936
	public RagdollCollisionDetection dummyDannyCollisionDetection;

	// Token: 0x04000791 RID: 1937
	[Header("Danny Voice Lines")]
	public AudioClip TutorialVoice1;

	// Token: 0x04000792 RID: 1938
	private bool playedTutorialVoice1;

	// Token: 0x04000793 RID: 1939
	public AudioClip TutorialVoice2;

	// Token: 0x04000794 RID: 1940
	private bool playedTutorialVoice2;

	// Token: 0x04000795 RID: 1941
	public AudioClip TutorialVoice3V1;

	// Token: 0x04000796 RID: 1942
	public AudioClip TutorialVoice3V2;

	// Token: 0x04000797 RID: 1943
	public AudioClip TutorialVoice4;

	// Token: 0x04000798 RID: 1944
	public AudioClip TutorialVoice5;

	// Token: 0x04000799 RID: 1945
	public AudioClip TutorialVoice6;

	// Token: 0x0400079A RID: 1946
	public AudioClip TutorialVoice7P1;

	// Token: 0x0400079B RID: 1947
	public AudioClip TutorialVoice7P2;

	// Token: 0x0400079C RID: 1948
	public AudioClip TutorialVoice8;

	// Token: 0x0400079D RID: 1949
	public AudioClip TutorialVoice9;

	// Token: 0x0400079E RID: 1950
	public AudioClip TutorialVoice10;

	// Token: 0x0400079F RID: 1951
	public AudioClip TutorialVoice11;

	// Token: 0x040007A0 RID: 1952
	public AudioClip TutorialVoice12;

	// Token: 0x040007A1 RID: 1953
	public AudioClip TutorialVoice13;

	// Token: 0x040007A2 RID: 1954
	public AudioClip TutorialVoice14;

	// Token: 0x040007A3 RID: 1955
	public AudioClip TutorialVoice15;

	// Token: 0x040007A4 RID: 1956
	public AudioClip TutorialVoice16;

	// Token: 0x040007A5 RID: 1957
	public AudioClip TutorialVoice17;

	// Token: 0x040007A6 RID: 1958
	public AudioSource crisisSoundSource;

	// Token: 0x040007A7 RID: 1959
	private bool dannyLanded;

	// Token: 0x040007A8 RID: 1960
	private bool jumpedOnPlatform;

	// Token: 0x040007A9 RID: 1961
	private int collisionPointsGoal;

	// Token: 0x040007AA RID: 1962
	public static bool TutorialUnlocked;
}
