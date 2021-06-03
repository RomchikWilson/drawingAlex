using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private bool isRotate = false;
    [SerializeField] private float forceRotation = default;

    private void OnEnable()
    {
        PlayerController.FreezePlayerAction += SetRotate;
    }

    private void OnDisable()
    {
        PlayerController.FreezePlayerAction -= SetRotate;
    }

    void Update()
    {
        if (isRotate)
        {
            transform.Rotate(0, 0, forceRotation * Time.deltaTime, Space.Self);
        }
    }

    private void SetRotate(bool _rotate)
    {
        isRotate = !_rotate;
    }
}
