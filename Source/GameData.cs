using System;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class GameData : MonoBehaviour
{
	// Token: 0x06000538 RID: 1336 RVA: 0x00006250 File Offset: 0x00004450
	private void Awake()
	{
		GameData.LoadEverything();
		FileManager.CreateWorldDirectory();
	}

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x06000539 RID: 1337 RVA: 0x0001B900 File Offset: 0x00019B00
	// (remove) Token: 0x0600053A RID: 1338 RVA: 0x0001B934 File Offset: 0x00019B34
	public static event GameData.ChangeHandler OnVolumeChanged;

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x0600053B RID: 1339 RVA: 0x0001B968 File Offset: 0x00019B68
	// (remove) Token: 0x0600053C RID: 1340 RVA: 0x0001B99C File Offset: 0x00019B9C
	public static event GameData.ChangeHandler OnSettingsChanged;

	// Token: 0x14000011 RID: 17
	// (add) Token: 0x0600053D RID: 1341 RVA: 0x0001B9D0 File Offset: 0x00019BD0
	// (remove) Token: 0x0600053E RID: 1342 RVA: 0x0001BA04 File Offset: 0x00019C04
	public static event GameData.ChangeHandler OnCheatsChanged;

	// Token: 0x14000012 RID: 18
	// (add) Token: 0x0600053F RID: 1343 RVA: 0x0001BA38 File Offset: 0x00019C38
	// (remove) Token: 0x06000540 RID: 1344 RVA: 0x0001BA6C File Offset: 0x00019C6C
	public static event GameData.ChangeHandler OnStyleChanged;

	// Token: 0x14000013 RID: 19
	// (add) Token: 0x06000541 RID: 1345 RVA: 0x0001BAA0 File Offset: 0x00019CA0
	// (remove) Token: 0x06000542 RID: 1346 RVA: 0x0001BAD4 File Offset: 0x00019CD4
	public static event GameData.ChangeHandler OnLevelUpgrade;

	// Token: 0x14000014 RID: 20
	// (add) Token: 0x06000543 RID: 1347 RVA: 0x0001BB08 File Offset: 0x00019D08
	// (remove) Token: 0x06000544 RID: 1348 RVA: 0x0001BB3C File Offset: 0x00019D3C
	public static event GameData.ChangeHandler OnXPUpdate;

	// Token: 0x06000545 RID: 1349 RVA: 0x0000625C File Offset: 0x0000445C
	public static void SaveSettings()
	{
		PlayerPrefs.SetString(GameData.prefSettings, JsonUtility.ToJson(GameData.settings));
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00006272 File Offset: 0x00004472
	public static void LoadSettings()
	{
		GameData.settings = JsonUtility.FromJson<GameData.Settings>(PlayerPrefs.GetString(GameData.prefSettings, JsonUtility.ToJson(new GameData.Settings())));
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00006292 File Offset: 0x00004492
	public static void ResetSettings()
	{
		GameData.settings = new GameData.Settings();
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x0000629E File Offset: 0x0000449E
	public static void SaveControls()
	{
		PlayerPrefs.SetString(GameData.prefControls, JsonUtility.ToJson(GameData.controls));
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x000062B4 File Offset: 0x000044B4
	public static void LoadControls()
	{
		GameData.controls = JsonUtility.FromJson<GameData.Controls>(PlayerPrefs.GetString(GameData.prefControls, JsonUtility.ToJson(new GameData.Controls())));
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x000062D4 File Offset: 0x000044D4
	public static void ResetControls()
	{
		GameData.controls = new GameData.Controls();
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x000062E0 File Offset: 0x000044E0
	public static void SaveHighScores()
	{
		PlayerPrefs.SetString(GameData.prefHighScores, JsonUtility.ToJson(GameData.highScores));
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x000062F6 File Offset: 0x000044F6
	public static void LoadHighScores()
	{
		GameData.highScores = JsonUtility.FromJson<GameData.HighScores>(PlayerPrefs.GetString(GameData.prefHighScores, JsonUtility.ToJson(new GameData.HighScores())));
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00006316 File Offset: 0x00004516
	public static void ResetHighScores()
	{
		GameData.highScores = new GameData.HighScores();
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x00006322 File Offset: 0x00004522
	public static void SaveCheats()
	{
		PlayerPrefs.SetString(GameData.prefCheats, JsonUtility.ToJson(GameData.cheats));
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x00006338 File Offset: 0x00004538
	public static void LoadCheats()
	{
		GameData.cheats = JsonUtility.FromJson<GameData.Cheats>(PlayerPrefs.GetString(GameData.prefCheats, JsonUtility.ToJson(new GameData.Cheats())));
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00006358 File Offset: 0x00004558
	public static void ResetCheats()
	{
		GameData.cheats = new GameData.Cheats();
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x00006364 File Offset: 0x00004564
	public static void SaveStats()
	{
		PlayerPrefs.SetString(GameData.prefStats, JsonUtility.ToJson(GameData.stats));
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x0000637A File Offset: 0x0000457A
	public static void LoadStats()
	{
		GameData.stats = JsonUtility.FromJson<GameData.Stats>(PlayerPrefs.GetString(GameData.prefStats, JsonUtility.ToJson(new GameData.Stats())));
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x0000639A File Offset: 0x0000459A
	public static void ResetStats()
	{
		GameData.stats = new GameData.Stats();
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x000063A6 File Offset: 0x000045A6
	public static void SaveStyling()
	{
		PlayerPrefs.SetString(GameData.prefStyling, JsonUtility.ToJson(GameData.styling));
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x000063BC File Offset: 0x000045BC
	public static void LoadStyling()
	{
		GameData.styling = JsonUtility.FromJson<GameData.Styling>(PlayerPrefs.GetString(GameData.prefStyling, JsonUtility.ToJson(new GameData.Styling())));
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x000063DC File Offset: 0x000045DC
	public static void ResetStyling()
	{
		GameData.styling = new GameData.Styling();
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x000063E8 File Offset: 0x000045E8
	public static void ResetEverything()
	{
		PlayerPrefs.DeleteAll();
		GameData.ResetControls();
		GameData.ResetSettings();
		GameData.ResetStyling();
		GameData.ResetHighScores();
		GameData.ResetCheats();
		GameData.ResetStats();
		GameData.collisionPoints = 0;
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x00006413 File Offset: 0x00004613
	public static void LoadEverything()
	{
		GameData.LoadControls();
		GameData.LoadSettings();
		GameData.LoadStyling();
		GameData.LoadCheats();
		GameData.LoadStats();
		GameData.LoadHighScores();
	}

	// Token: 0x040006AA RID: 1706
	private static string prefSettings = "Settings";

	// Token: 0x040006AB RID: 1707
	public static GameData.Settings settings = new GameData.Settings();

	// Token: 0x040006AC RID: 1708
	private static string prefControls = "Controls";

	// Token: 0x040006AD RID: 1709
	public static GameData.Controls controls = new GameData.Controls();

	// Token: 0x040006AE RID: 1710
	private static string prefCheats = "Cheats";

	// Token: 0x040006AF RID: 1711
	public static GameData.Cheats cheats = new GameData.Cheats();

	// Token: 0x040006B0 RID: 1712
	private static string prefCollisionPoints = "CollisionPoints";

	// Token: 0x040006B1 RID: 1713
	public static int collisionPoints = 0;

	// Token: 0x040006B2 RID: 1714
	private static string prefStyling = "Styling";

	// Token: 0x040006B3 RID: 1715
	public static GameData.Styling styling = new GameData.Styling();

	// Token: 0x040006B4 RID: 1716
	private static string prefHighScores = "HighScores";

	// Token: 0x040006B5 RID: 1717
	public static GameData.HighScores highScores = new GameData.HighScores();

	// Token: 0x040006B6 RID: 1718
	private static string prefStats = "Stats";

	// Token: 0x040006B7 RID: 1719
	public static GameData.Stats stats = new GameData.Stats();

	// Token: 0x02000102 RID: 258
	// (Invoke) Token: 0x0600055C RID: 1372
	public delegate void ChangeHandler();

	// Token: 0x02000103 RID: 259
	public class Settings
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00006433 File Offset: 0x00004633
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000643B File Offset: 0x0000463B
		public float MusicVolume
		{
			get
			{
				return this.musicVolume;
			}
			set
			{
				if (this.musicVolume == value)
				{
					return;
				}
				this.musicVolume = value;
				GameData.ChangeHandler onVolumeChanged = GameData.OnVolumeChanged;
				if (onVolumeChanged == null)
				{
					return;
				}
				onVolumeChanged();
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000645D File Offset: 0x0000465D
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00006465 File Offset: 0x00004665
		public float SoundFXVolume
		{
			get
			{
				return this.soundFXVolume;
			}
			set
			{
				if (this.soundFXVolume == value)
				{
					return;
				}
				this.soundFXVolume = value;
				GameData.ChangeHandler onVolumeChanged = GameData.OnVolumeChanged;
				if (onVolumeChanged == null)
				{
					return;
				}
				onVolumeChanged();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00006487 File Offset: 0x00004687
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0000648F File Offset: 0x0000468F
		public float VoicesVolume
		{
			get
			{
				return this.voicesVolume;
			}
			set
			{
				if (this.voicesVolume == value)
				{
					return;
				}
				this.voicesVolume = value;
				GameData.ChangeHandler onVolumeChanged = GameData.OnVolumeChanged;
				if (onVolumeChanged == null)
				{
					return;
				}
				onVolumeChanged();
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x000064B1 File Offset: 0x000046B1
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x000064B9 File Offset: 0x000046B9
		public bool DisplayHUD
		{
			get
			{
				return this.displayHUD;
			}
			set
			{
				if (this.displayHUD == value)
				{
					return;
				}
				this.displayHUD = value;
				GameData.ChangeHandler onSettingsChanged = GameData.OnSettingsChanged;
				if (onSettingsChanged == null)
				{
					return;
				}
				onSettingsChanged();
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x000064DB File Offset: 0x000046DB
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x000064E3 File Offset: 0x000046E3
		public bool CameraShake
		{
			get
			{
				return this.cameraShake;
			}
			set
			{
				if (this.cameraShake == value)
				{
					return;
				}
				this.cameraShake = value;
				GameData.ChangeHandler onSettingsChanged = GameData.OnSettingsChanged;
				if (onSettingsChanged == null)
				{
					return;
				}
				onSettingsChanged();
			}
		}

		// Token: 0x040006B8 RID: 1720
		[SerializeField]
		private float musicVolume;

		// Token: 0x040006B9 RID: 1721
		[SerializeField]
		private float soundFXVolume;

		// Token: 0x040006BA RID: 1722
		[SerializeField]
		private float voicesVolume;

		// Token: 0x040006BB RID: 1723
		[SerializeField]
		private bool displayHUD = true;

		// Token: 0x040006BC RID: 1724
		[SerializeField]
		private bool glowAndBloom = true;

		// Token: 0x040006BD RID: 1725
		[SerializeField]
		private bool cameraShake = true;
	}

	// Token: 0x02000104 RID: 260
	public class Controls
	{
		// Token: 0x040006BE RID: 1726
		public KeyCode moveForward = KeyCode.W;

		// Token: 0x040006BF RID: 1727
		public KeyCode moveBackward = KeyCode.S;

		// Token: 0x040006C0 RID: 1728
		public KeyCode moveLeft = KeyCode.A;

		// Token: 0x040006C1 RID: 1729
		public KeyCode moveRight = KeyCode.D;

		// Token: 0x040006C2 RID: 1730
		public KeyCode jump = KeyCode.Space;

		// Token: 0x040006C3 RID: 1731
		public KeyCode toggleRagdoll = KeyCode.R;

		// Token: 0x040006C4 RID: 1732
		public KeyCode setSpawn = KeyCode.T;

		// Token: 0x040006C5 RID: 1733
		public KeyCode interact = KeyCode.E;

		// Token: 0x040006C6 RID: 1734
		public KeyCode toggleBuildMode = KeyCode.Tab;

		// Token: 0x040006C7 RID: 1735
		public KeyCode toggleBuildItemMenu = KeyCode.F;

		// Token: 0x040006C8 RID: 1736
		public KeyCode rotateItemLeft = KeyCode.Q;

		// Token: 0x040006C9 RID: 1737
		public KeyCode rotateItemRight = KeyCode.E;

		// Token: 0x040006CA RID: 1738
		public KeyCode removeLastItem = KeyCode.X;

		// Token: 0x040006CB RID: 1739
		public KeyCode toggleSlowMotion = KeyCode.Z;

		// Token: 0x040006CC RID: 1740
		public KeyCode zoom = KeyCode.LeftControl;

		// Token: 0x040006CD RID: 1741
		public KeyCode switchCamera = KeyCode.C;

		// Token: 0x040006CE RID: 1742
		public KeyCode generateCameraAngle = KeyCode.Tab;

		// Token: 0x040006CF RID: 1743
		public float scrollSpeed = 4f;

		// Token: 0x040006D0 RID: 1744
		public Vector2 lookSensitivity = new Vector2(0.55f, 0.5f);

		// Token: 0x040006D1 RID: 1745
		public bool invertLookX;

		// Token: 0x040006D2 RID: 1746
		public bool invertLookY;
	}

	// Token: 0x02000105 RID: 261
	public class HighScores
	{
		// Token: 0x040006D3 RID: 1747
		public bool roadCrossNewSave = true;

		// Token: 0x040006D4 RID: 1748
		public float roadCrossTime;

		// Token: 0x040006D5 RID: 1749
		public int roadCrossRagdollCount;

		// Token: 0x040006D6 RID: 1750
		public bool fortInvasionNewSave = true;

		// Token: 0x040006D7 RID: 1751
		public int fortInvasionRagdollCount;

		// Token: 0x040006D8 RID: 1752
		public int fortInvasionBadEntered;

		// Token: 0x040006D9 RID: 1753
		public int fortInvasionGoodEntered;
	}

	// Token: 0x02000106 RID: 262
	public class Cheats
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00006538 File Offset: 0x00004738
		public bool UnlockEverything
		{
			get
			{
				return Application.isEditor || this.unlockEverything;
			}
		}

		// Token: 0x040006DA RID: 1754
		public bool slowMotionInPlayMode;

		// Token: 0x040006DB RID: 1755
		public bool activeRagdoll = true;

		// Token: 0x040006DC RID: 1756
		public bool ignoreBuildCollisionCheck;

		// Token: 0x040006DD RID: 1757
		[SerializeField]
		private bool unlockEverything;

		// Token: 0x040006DE RID: 1758
		[SerializeField]
		private bool allowSpecialSkinsForAIRagdolls;
	}

	// Token: 0x02000107 RID: 263
	public class Styling
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00006558 File Offset: 0x00004758
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00006560 File Offset: 0x00004760
		public RagdollStylePack.SkinType Skin
		{
			get
			{
				return this.skin;
			}
			set
			{
				if (this.skin == value)
				{
					return;
				}
				this.skin = value;
				GameData.ChangeHandler onStyleChanged = GameData.OnStyleChanged;
				if (onStyleChanged == null)
				{
					return;
				}
				onStyleChanged();
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00006582 File Offset: 0x00004782
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0000658A File Offset: 0x0000478A
		public RagdollStylePack.FaceType Face
		{
			get
			{
				return this.face;
			}
			set
			{
				if (this.face == value)
				{
					return;
				}
				this.face = value;
				GameData.ChangeHandler onStyleChanged = GameData.OnStyleChanged;
				if (onStyleChanged == null)
				{
					return;
				}
				onStyleChanged();
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000065AC File Offset: 0x000047AC
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x000065B4 File Offset: 0x000047B4
		public WorldStyle.ColorStyle FloorColor
		{
			get
			{
				return this.floorColor;
			}
			set
			{
				if (this.floorColor == value)
				{
					return;
				}
				this.floorColor = value;
				GameData.ChangeHandler onStyleChanged = GameData.OnStyleChanged;
				if (onStyleChanged == null)
				{
					return;
				}
				onStyleChanged();
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x000065D6 File Offset: 0x000047D6
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x000065DE File Offset: 0x000047DE
		public WorldStyle.ColorStyle BlockColor
		{
			get
			{
				return this.blockColor;
			}
			set
			{
				if (this.blockColor == value)
				{
					return;
				}
				this.blockColor = value;
				GameData.ChangeHandler onStyleChanged = GameData.OnStyleChanged;
				if (onStyleChanged == null)
				{
					return;
				}
				onStyleChanged();
			}
		}

		// Token: 0x040006DF RID: 1759
		[SerializeField]
		private RagdollStylePack.SkinType skin = RagdollStylePack.SkinType.Grey;

		// Token: 0x040006E0 RID: 1760
		[SerializeField]
		private RagdollStylePack.FaceType face = RagdollStylePack.FaceType.Idle;

		// Token: 0x040006E1 RID: 1761
		[SerializeField]
		private WorldStyle.ColorStyle floorColor = WorldStyle.ColorStyle.Grey;

		// Token: 0x040006E2 RID: 1762
		[SerializeField]
		private WorldStyle.ColorStyle blockColor;
	}

	// Token: 0x02000108 RID: 264
	public class Stats
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000661E File Offset: 0x0000481E
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00006626 File Offset: 0x00004826
		public int Level
		{
			get
			{
				return this.level;
			}
			set
			{
				if (this.level == value)
				{
					return;
				}
				this.level = value;
				GameData.ChangeHandler onLevelUpgrade = GameData.OnLevelUpgrade;
				if (onLevelUpgrade == null)
				{
					return;
				}
				onLevelUpgrade();
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00006648 File Offset: 0x00004848
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00006650 File Offset: 0x00004850
		public int XP
		{
			get
			{
				return this.xp;
			}
			set
			{
				if (this.Level == this.maxLevel)
				{
					return;
				}
				this.xp = value;
				GameData.ChangeHandler onXPUpdate = GameData.OnXPUpdate;
				if (onXPUpdate != null)
				{
					onXPUpdate();
				}
				this.Level = this.XPToLevel(this.xp);
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000668A File Offset: 0x0000488A
		public int XPToLevel(int xp)
		{
			return Mathf.FloorToInt(Mathf.Pow((float)xp / 6f, 0.333333343f));
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000066A3 File Offset: 0x000048A3
		public int LevelToXP(int lvl)
		{
			return lvl * lvl * lvl * 6;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001BCC8 File Offset: 0x00019EC8
		public float LevelCompletePercentage()
		{
			if (this.Level == this.maxLevel)
			{
				return 1f;
			}
			return ((float)this.XP - (float)this.LevelToXP(this.Level)) / ((float)this.LevelToXP(this.Level + 1) - (float)this.LevelToXP(this.Level));
		}

		// Token: 0x040006E3 RID: 1763
		public readonly int maxLevel = 20;

		// Token: 0x040006E4 RID: 1764
		[SerializeField]
		private int level;

		// Token: 0x040006E5 RID: 1765
		[SerializeField]
		private int xp;

		// Token: 0x040006E6 RID: 1766
		public bool isFirstStartUp = true;

		// Token: 0x040006E7 RID: 1767
		public int ragdollCount;

		// Token: 0x040006E8 RID: 1768
		public int collisions;

		// Token: 0x040006E9 RID: 1769
		public int spawnsSet;
	}
}
