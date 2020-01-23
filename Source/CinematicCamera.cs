using System;
using UnityEngine;

// Token: 0x0200007B RID: 123
public class CinematicCamera : MonoBehaviour
{
	// Token: 0x0600027A RID: 634 RVA: 0x0000428F File Offset: 0x0000248F
	private void Awake()
	{
		this.playerSettings = base.transform.root.GetComponent<PlayerSettings>();
		this.generateCamera = true;
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00012330 File Offset: 0x00010530
	private void Update()
	{
		if (this.playerSettings.mainCamera.target != CameraController.Target.Cinematic)
		{
			return;
		}
		this.targetPosition = this.playerSettings.simulatedRagdoll.bodyElements.AverageJointPosition();
		if (!this.setWinLoseShot && LevelManager.StatusIsWinOrLose(true))
		{
			this.generateCamera = true;
			this.setWinLoseShot = true;
		}
		else if (!LevelManager.StatusIsWinOrLose(true))
		{
			this.setWinLoseShot = false;
		}
		if (InputManager.NewCameraAngle() && !LevelManager.StatusIsWinOrLose(true) && this.playerSettings.mainCamera.target == CameraController.Target.Cinematic)
		{
			this.generateCamera = true;
		}
		while (this.generateCamera && !LevelManager.GamePaused)
		{
			this.GenerateCamera();
			this.iterations++;
			if (this.iterations > this.iterationLimit)
			{
				this.generateCamera = false;
				this.iterations = 0;
			}
		}
	}

	// Token: 0x0600027C RID: 636 RVA: 0x000042AE File Offset: 0x000024AE
	private void FixedUpdate()
	{
		if (this.playerSettings.mainCamera.target != CameraController.Target.Cinematic)
		{
			return;
		}
		this.SetCamera();
	}

	// Token: 0x0600027D RID: 637 RVA: 0x00012408 File Offset: 0x00010608
	private void SetCamera()
	{
		this.cameraTargetLookRotation = Quaternion.Lerp(Quaternion.Euler(this.cameraTargetLookRotation), Quaternion.LookRotation(this.targetPosition - base.transform.position), Time.deltaTime * 10f).eulerAngles;
		this.cameraTargetLookRotation.z = 0f;
		base.transform.eulerAngles = this.cameraTargetLookRotation;
		this.cameraTargetPosition.x = this.cameraFinalStartPosition.x + (Mathf.Sin(Time.time * this.dollySpeed) + 1f) * 0.5f * (this.cameraFinalEndPosition.x - this.cameraFinalStartPosition.x);
		this.cameraTargetPosition.y = this.cameraFinalStartPosition.y + (Mathf.Sin(Time.time * this.dollySpeed) + 1f) * 0.5f * (this.cameraFinalEndPosition.y - this.cameraFinalStartPosition.y);
		this.cameraTargetPosition.z = this.cameraFinalStartPosition.z + (Mathf.Sin(Time.time * this.dollySpeed) + 1f) * 0.5f * (this.cameraFinalEndPosition.z - this.cameraFinalStartPosition.z);
		base.transform.position = this.cameraTargetPosition;
	}

	// Token: 0x0600027E RID: 638 RVA: 0x00012570 File Offset: 0x00010770
	private void GenerateCamera()
	{
		this.focalLength = this.focalLengths[UnityEngine.Random.Range(0, this.focalLengths.Length)];
		this.dollyLength = UnityEngine.Random.Range(this.dollyLengthRange.x, this.dollyLengthRange.y);
		this.dollySpeed = UnityEngine.Random.Range(this.dollySpeedRange.x, this.dollySpeedRange.y);
		this.chosenDollyAxis = UnityEngine.Random.Range(0, 3);
		this.dollyOnX = (this.chosenDollyAxis == 1);
		this.dollyOnY = (this.chosenDollyAxis == 2);
		this.dollyOnZ = (this.chosenDollyAxis == 3);
		this.cameraStartPosition.x = this.targetPosition.x + this.dollyPlacementRangeXZ[UnityEngine.Random.Range(0, this.dollyPlacementRangeXZ.Length)];
		this.cameraStartPosition.y = this.targetPosition.y + this.dollyPlacementRangeY[UnityEngine.Random.Range(0, this.dollyPlacementRangeY.Length)];
		this.cameraStartPosition.z = this.targetPosition.z + this.dollyPlacementRangeXZ[UnityEngine.Random.Range(0, this.dollyPlacementRangeXZ.Length)];
		this.cameraEndPosition = this.cameraStartPosition;
		if (this.dollyOnX)
		{
			this.cameraEndPosition.x = this.cameraStartPosition.x + UnityEngine.Random.Range(this.dollyLengthRange.x, this.dollyLengthRange.y);
		}
		if (this.dollyOnY)
		{
			this.cameraEndPosition.y = this.cameraStartPosition.y + UnityEngine.Random.Range(this.dollyLengthRange.x, this.dollyLengthRange.y);
		}
		if (this.dollyOnZ)
		{
			this.cameraEndPosition.z = this.cameraStartPosition.z + UnityEngine.Random.Range(this.dollyLengthRange.x, this.dollyLengthRange.y);
		}
		if (this.CanSeeTarget(this.cameraStartPosition) && this.CanSeeTarget(this.cameraEndPosition))
		{
			this.cameraFinalStartPosition = this.cameraStartPosition;
			this.cameraFinalEndPosition = this.cameraEndPosition;
			base.transform.position = this.cameraStartPosition;
			this.cameraTargetLookRotation = Quaternion.LookRotation(this.targetPosition - base.transform.position).eulerAngles;
			this.generateCamera = false;
		}
	}

	// Token: 0x0600027F RID: 639 RVA: 0x000127C8 File Offset: 0x000109C8
	private bool CanSeeTarget(Vector3 startPosition)
	{
		this.direction = (this.targetPosition - startPosition).normalized;
		Debug.DrawRay(startPosition, this.direction, Color.green, 4f);
		return !Physics.Linecast(startPosition, this.targetPosition, this.layerMask);
	}

	// Token: 0x040003AE RID: 942
	private PlayerSettings playerSettings;

	// Token: 0x040003AF RID: 943
	private Vector3 targetPosition;

	// Token: 0x040003B0 RID: 944
	private bool generateCamera;

	// Token: 0x040003B1 RID: 945
	private readonly int iterationLimit = 20;

	// Token: 0x040003B2 RID: 946
	private int iterations;

	// Token: 0x040003B3 RID: 947
	[Header("Camera Settings")]
	public float focalLength;

	// Token: 0x040003B4 RID: 948
	private float[] focalLengths = new float[]
	{
		12f,
		16f,
		18f,
		24f,
		32f,
		35f,
		50f,
		60f,
		80f,
		100f,
		120f
	};

	// Token: 0x040003B5 RID: 949
	private Vector3 cameraTargetLookRotation;

	// Token: 0x040003B6 RID: 950
	[Header("Dolly Settings")]
	private Vector3 cameraTargetPosition;

	// Token: 0x040003B7 RID: 951
	[Header("Dolly Settings")]
	private Vector3 cameraEndPosition;

	// Token: 0x040003B8 RID: 952
	[Header("Dolly Settings")]
	private Vector3 cameraStartPosition;

	// Token: 0x040003B9 RID: 953
	[Header("Dolly Settings")]
	private Vector3 cameraFinalStartPosition;

	// Token: 0x040003BA RID: 954
	[Header("Dolly Settings")]
	private Vector3 cameraFinalEndPosition;

	// Token: 0x040003BB RID: 955
	private float[] dollyPlacementRangeXZ = new float[]
	{
		-40f,
		-30f,
		-20f,
		-15f,
		-10f,
		-8f,
		-5f,
		-4f,
		4f,
		5f,
		8f,
		10f,
		15f,
		20f,
		30f,
		40f
	};

	// Token: 0x040003BC RID: 956
	private float[] dollyPlacementRangeY = new float[]
	{
		-1f,
		-0.7f,
		-0.5f,
		-0.2f,
		0f,
		0.5f,
		1f,
		2f,
		3f,
		4f,
		5f,
		8f,
		10f
	};

	// Token: 0x040003BD RID: 957
	public bool dollyOnX;

	// Token: 0x040003BE RID: 958
	public bool dollyOnY;

	// Token: 0x040003BF RID: 959
	public bool dollyOnZ;

	// Token: 0x040003C0 RID: 960
	private int chosenDollyAxis;

	// Token: 0x040003C1 RID: 961
	public float dollyLength;

	// Token: 0x040003C2 RID: 962
	public Vector2 dollyLengthRange = new Vector2(1f, 30f);

	// Token: 0x040003C3 RID: 963
	public float dollySpeed;

	// Token: 0x040003C4 RID: 964
	public Vector2 dollySpeedRange = new Vector2(0.05f, 1f);

	// Token: 0x040003C5 RID: 965
	private bool setWinLoseShot;

	// Token: 0x040003C6 RID: 966
	private RaycastHit hit;

	// Token: 0x040003C7 RID: 967
	private Vector3 direction;

	// Token: 0x040003C8 RID: 968
	private LayerMask layerMask = -26117;
}
