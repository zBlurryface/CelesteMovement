using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTeleport : MonoBehaviour {

    public GameObject cinemachineVirtualCamera;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger) {
            cinemachineVirtualCamera.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger) {
            cinemachineVirtualCamera.SetActive(false);
        }


    }
}
