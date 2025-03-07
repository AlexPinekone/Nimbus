using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
	public static CameraManager instance;
	
	[SerializeField] private CinemachineVirtualCamera[] _allVirtualCamera;
	
	[Header("Controls for lerping the Y Damping during player jump/fall")]
	[SerializeField] private float _fallPanAmount = 0.25f;
	[SerializeField] private float _fallPanTime = 0.35f;
	public float _fallSpeedYDampingChangeThreshold = -15f;
	
	public bool IsLerpingYDamping {get; private set;}
	public bool LerpedFromPlayerFalling {get; set;}
	
	private Coroutine _lerpYPanCoroutine;
	
	private CinemachineVirtualCamera _currentCamera;
	private CinemachineFramingTransposer _framingTransposer;
	
	private float _moreYPanAmount;
	
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		
		for (int i = 0; i < _allVirtualCamera.Length; i++)
		{
			if (_allVirtualCamera[i].enabled)
			{
				//set all current active camera
				_currentCamera = _allVirtualCamera[i];
				
				//set the framer transposer
				_framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
			}
		}
		//Set the YDamping so it's based on the inspector value
		_moreYPanAmount = _framingTransposer.m_YDamping;
	}
	
	#region Lerp the Y Damping
	
	public void LerpYDamping(bool isPlayerFalling)
	{
		_lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
	}
	
	private IEnumerator LerpYAction(bool isPlayerFalling)
	{
		IsLerpingYDamping = true;
		
		//grab the starting damping amount
		float startDampAmount = _framingTransposer.m_YDamping;
		float endDampAmount = 0f;
		
		//Determine the end Damping amount
		if(isPlayerFalling)
		{
			endDampAmount = _fallPanAmount;
			LerpedFromPlayerFalling = true;
		}
		else
		{
			endDampAmount = _moreYPanAmount;
		}
		//Lerp pan amount
		float elapsedTime = 0f;
		while(elapsedTime < _fallPanTime)
		{
			elapsedTime += Time.deltaTime;
			
			float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / _fallPanTime));
			_framingTransposer.m_YDamping = lerpedPanAmount;
			
			yield return null;
		}
		
		IsLerpingYDamping = false;
	}
	
	#endregion
}
