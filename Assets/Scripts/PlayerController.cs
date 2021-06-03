using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isMove = false;
    [SerializeField] private float force = 0f;

    private Rigidbody rigidbody = default;
    private Vector3 startPosition = default;

    public static Action<bool> StartMovePlayerAction = default;
    public static Action<bool> FreezePlayerAction = default;
    public static Action RestartPlayerAction = default;

    private void OnEnable()
    {
        StartMovePlayerAction += StartMovePlayer;
        FreezePlayerAction += FreezePlayer;
        RestartPlayerAction += RestartPlayer;
    }

    private void OnDisable()
    {
        StartMovePlayerAction -= StartMovePlayer;
        FreezePlayerAction -= FreezePlayer;
        RestartPlayerAction -= RestartPlayer;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (isMove)
        {
            rigidbody.AddForce(-transform.right * force * Time.deltaTime, ForceMode.Force);
        }
    }

    private void StartMovePlayer(bool isMove)
    {
        this.isMove = isMove;
    }

    private void FreezePlayer(bool _freez)
    {
        rigidbody.isKinematic = _freez;
    }

    private void RestartPlayer()
    {
        transform.position = startPosition;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.velocity = Vector3.zero;
    }
}
