using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]

    [SerializeField] ParticleSystem speedUpParticleSystem;

    [Space(10)]

    [Header("FOV Variables")]

    [SerializeField] float minFOV = 25.0f;
    [SerializeField] float maxFOV = 120.0f;

    [SerializeField] float zoomDuration = 0.5f;
    [SerializeField] float zoomSpeedModifier = 5f;


    CinemachineCamera cinemachineCamera;

    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));

        if (speedAmount > 0.0f)
        {
            speedUpParticleSystem.Play();
        }
    }

    IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + (speedAmount * zoomSpeedModifier), minFOV, maxFOV);

        float elapsedTime = 0.0f;

        while (elapsedTime < zoomDuration) 
        {
            float t = elapsedTime / zoomDuration;

            elapsedTime += Time.deltaTime;

            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);

            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV;
        
    }
}
