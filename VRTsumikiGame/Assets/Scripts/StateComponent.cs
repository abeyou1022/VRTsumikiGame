using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateComponent : MonoBehaviour
{
    public Parts part;
    bool state = false; //Sign用
    GameObject colliding; //Grasp2用

    public GameObject GetColliding()
    {
        return colliding;
    }

    public void SetState(bool state)
    {
        this.state = state;
    }

    public bool GetState()
    {
        return state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GraspAble>() != null)
            colliding = other.gameObject;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<GraspAble>() != null)
            colliding = null;
    }
}

public enum Parts
{
    RIGHT_HAND,
    LEFT_HAND,
    RIGHT_FINGER,
    LEFT_FINGER,
}