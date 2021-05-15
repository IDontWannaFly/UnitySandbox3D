using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{

    public LayerMask layers;

    private int colCount = 0;

    public bool IsGrounded(){
        return colCount > 0;
    }

    private void OnTriggerEnter(Collider other) {
        colCount++;
    }

    private void OnTriggerExit(Collider other) {
        colCount--;
    }
}
