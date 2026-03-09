using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;
using Mirror;


public class CameraExtraControll :Mirror.NetworkBehaviour
{

    private CinemachineOrbitalFollow orbitalFollow;
    private CinemachineOrbitalFollow orbital;

    private CinemachineCamera cinemachineCamera;


    // S

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 50f;
    void Start()
    {
        orbitalFollow = GetComponent<CinemachineOrbitalFollow>();
        cinemachineCamera = GetComponent<CinemachineCamera>();
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    private void Awake()
    {
        orbitalFollow = GetComponent<CinemachineOrbitalFollow>();

    }


    // Update is called once per frame
    void Update()
    {

    }

    public void CameraZoomInAndOut()
    {
       

    }
}
