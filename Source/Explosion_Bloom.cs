using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
[AddComponentMenu("KriptoFX/Explosion_Bloom")]
[ImageEffectAllowedInSceneView]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class Explosion_Bloom : MonoBehaviour
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600002C RID: 44 RVA: 0x000022B5 File Offset: 0x000004B5
	public Shader shader
	{
		get
		{
			if (this.m_Shader == null)
			{
				this.m_Shader = Shader.Find("Hidden/KriptoFX/PostEffects/Explosion_Bloom");
			}
			return this.m_Shader;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600002D RID: 45 RVA: 0x000022DB File Offset: 0x000004DB
	public Material material
	{
		get
		{
			if (this.m_Material == null)
			{
				this.m_Material = Explosion_Bloom.CheckShaderAndCreateMaterial(this.shader);
			}
			return this.m_Material;
		}
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00008630 File Offset: 0x00006830
	public static bool IsSupported(Shader s, bool needDepth, bool needHdr, MonoBehaviour effect)
	{
		if (s == null || !s.isSupported)
		{
			Debug.LogWarningFormat("Missing shader for image effect {0}", new object[]
			{
				effect
			});
			return false;
		}
		if (!SystemInfo.supportsImageEffects)
		{
			Debug.LogWarningFormat("Image effects aren't supported on this device ({0})", new object[]
			{
				effect
			});
			return false;
		}
		if (needDepth && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			Debug.LogWarningFormat("Depth textures aren't supported on this device ({0})", new object[]
			{
				effect
			});
			return false;
		}
		if (needHdr && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
		{
			Debug.LogWarningFormat("Floating point textures aren't supported on this device ({0})", new object[]
			{
				effect
			});
			return false;
		}
		return true;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002302 File Offset: 0x00000502
	public static Material CheckShaderAndCreateMaterial(Shader s)
	{
		if (s == null || !s.isSupported)
		{
			return null;
		}
		return new Material(s)
		{
			hideFlags = HideFlags.DontSave
		};
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000030 RID: 48 RVA: 0x00002325 File Offset: 0x00000525
	public static bool supportsDX11
	{
		get
		{
			return SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders;
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000086C4 File Offset: 0x000068C4
	private void Awake()
	{
		this.m_Threshold = Shader.PropertyToID("_Threshold");
		this.m_Curve = Shader.PropertyToID("_Curve");
		this.m_PrefilterOffs = Shader.PropertyToID("_PrefilterOffs");
		this.m_SampleScale = Shader.PropertyToID("_SampleScale");
		this.m_Intensity = Shader.PropertyToID("_Intensity");
		this.m_BaseTex = Shader.PropertyToID("_BaseTex");
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002337 File Offset: 0x00000537
	private void OnEnable()
	{
		if (!Explosion_Bloom.IsSupported(this.shader, true, false, this))
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002350 File Offset: 0x00000550
	private void OnDisable()
	{
		if (this.m_Material != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Material);
		}
		this.m_Material = null;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00008734 File Offset: 0x00006934
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		bool isMobilePlatform = Application.isMobilePlatform;
		int num = source.width;
		int num2 = source.height;
		if (!this.settings.highQuality)
		{
			num /= 2;
			num2 /= 2;
		}
		RenderTextureFormat format = isMobilePlatform ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR;
		float num3 = Mathf.Log((float)num2, 2f) + this.settings.radius - 8f;
		int num4 = (int)num3;
		int num5 = Mathf.Clamp(num4, 1, 16);
		float thresholdLinear = this.settings.thresholdLinear;
		this.material.SetFloat(this.m_Threshold, thresholdLinear);
		float num6 = thresholdLinear * this.settings.softKnee + 1E-05f;
		Vector3 v = new Vector3(thresholdLinear - num6, num6 * 2f, 0.25f / num6);
		this.material.SetVector(this.m_Curve, v);
		bool flag = !this.settings.highQuality && this.settings.antiFlicker;
		this.material.SetFloat(this.m_PrefilterOffs, flag ? -0.5f : 0f);
		this.material.SetFloat(this.m_SampleScale, 0.5f + num3 - (float)num4);
		this.material.SetFloat(this.m_Intensity, Mathf.Max(0f, this.settings.intensity));
		RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, format);
		Graphics.Blit(source, temporary, this.material, this.settings.antiFlicker ? 1 : 0);
		RenderTexture renderTexture = temporary;
		for (int i = 0; i < num5; i++)
		{
			this.m_blurBuffer1[i] = RenderTexture.GetTemporary(renderTexture.width / 2, renderTexture.height / 2, 0, format);
			Graphics.Blit(renderTexture, this.m_blurBuffer1[i], this.material, (i == 0) ? (this.settings.antiFlicker ? 3 : 2) : 4);
			renderTexture = this.m_blurBuffer1[i];
		}
		for (int j = num5 - 2; j >= 0; j--)
		{
			RenderTexture renderTexture2 = this.m_blurBuffer1[j];
			this.material.SetTexture(this.m_BaseTex, renderTexture2);
			this.m_blurBuffer2[j] = RenderTexture.GetTemporary(renderTexture2.width, renderTexture2.height, 0, format);
			Graphics.Blit(renderTexture, this.m_blurBuffer2[j], this.material, this.settings.highQuality ? 6 : 5);
			renderTexture = this.m_blurBuffer2[j];
		}
		int num7 = 7;
		num7 += (this.settings.highQuality ? 1 : 0);
		this.material.SetTexture(this.m_BaseTex, source);
		Graphics.Blit(renderTexture, destination, this.material, num7);
		for (int k = 0; k < 16; k++)
		{
			if (this.m_blurBuffer1[k] != null)
			{
				RenderTexture.ReleaseTemporary(this.m_blurBuffer1[k]);
			}
			if (this.m_blurBuffer2[k] != null)
			{
				RenderTexture.ReleaseTemporary(this.m_blurBuffer2[k]);
			}
			this.m_blurBuffer1[k] = null;
			this.m_blurBuffer2[k] = null;
		}
		RenderTexture.ReleaseTemporary(temporary);
	}

	// Token: 0x0400003E RID: 62
	[SerializeField]
	public Explosion_Bloom.Settings settings = Explosion_Bloom.Settings.defaultSettings;

	// Token: 0x0400003F RID: 63
	[HideInInspector]
	[SerializeField]
	private Shader m_Shader;

	// Token: 0x04000040 RID: 64
	private Material m_Material;

	// Token: 0x04000041 RID: 65
	private const int kMaxIterations = 16;

	// Token: 0x04000042 RID: 66
	private RenderTexture[] m_blurBuffer1 = new RenderTexture[16];

	// Token: 0x04000043 RID: 67
	private RenderTexture[] m_blurBuffer2 = new RenderTexture[16];

	// Token: 0x04000044 RID: 68
	private int m_Threshold;

	// Token: 0x04000045 RID: 69
	private int m_Curve;

	// Token: 0x04000046 RID: 70
	private int m_PrefilterOffs;

	// Token: 0x04000047 RID: 71
	private int m_SampleScale;

	// Token: 0x04000048 RID: 72
	private int m_Intensity;

	// Token: 0x04000049 RID: 73
	private int m_BaseTex;

	// Token: 0x0200000F RID: 15
	[Serializable]
	public struct Settings
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000239F File Offset: 0x0000059F
		public float thresholdGamma
		{
			get
			{
				return Mathf.Max(0f, this.threshold);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000023B1 File Offset: 0x000005B1
		public float thresholdLinear
		{
			get
			{
				return Mathf.GammaToLinearSpace(this.thresholdGamma);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00008A4C File Offset: 0x00006C4C
		public static Explosion_Bloom.Settings defaultSettings
		{
			get
			{
				return new Explosion_Bloom.Settings
				{
					threshold = 2f,
					softKnee = 0f,
					radius = 7f,
					intensity = 0.7f,
					highQuality = true,
					antiFlicker = true
				};
			}
		}

		// Token: 0x0400004A RID: 74
		[Tooltip("Filters out pixels under this level of brightness.")]
		[SerializeField]
		public float threshold;

		// Token: 0x0400004B RID: 75
		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("Makes transition between under/over-threshold gradual.")]
		public float softKnee;

		// Token: 0x0400004C RID: 76
		[Tooltip("Changes extent of veiling effects in a screen resolution-independent fashion.")]
		[Range(1f, 7f)]
		[SerializeField]
		public float radius;

		// Token: 0x0400004D RID: 77
		[SerializeField]
		[Tooltip("Blend factor of the result image.")]
		public float intensity;

		// Token: 0x0400004E RID: 78
		[SerializeField]
		[Tooltip("Controls filter quality and buffer resolution.")]
		public bool highQuality;

		// Token: 0x0400004F RID: 79
		[SerializeField]
		[Tooltip("Reduces flashing noise with an additional filter.")]
		public bool antiFlicker;
	}
}
