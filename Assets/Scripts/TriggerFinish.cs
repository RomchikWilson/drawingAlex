using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerController.RestartPlayerAction?.Invoke();
            PlayerController.FreezePlayerAction?.Invoke(true);
        }
    }
}
