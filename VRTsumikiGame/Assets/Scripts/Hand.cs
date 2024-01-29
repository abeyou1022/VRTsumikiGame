using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Parts part;
    public GameObject[] fingers = new GameObject[5]; //インスペクターから参照できるようにしないとインスタンス化されていないオブジェクトのためエラーがでる
    public GameObject inset;
    public bool i;

    public string ss = "hello";
/*
    public void SetFingers(GameObject[] f)
    {
        fingers = f;
        //Debug.Log("Hands:"+fingers[2]);
        insetBool = inset.GetComponent<PreventInset>().Isinset();
    }
*/
    public bool[] Sign()
    {
        bool[] state = new bool[fingers.Length];

        for (int i = 0; i < state.Length; i++)
            state[i] = fingers[i].GetComponent<StateComponent>().GetState();

        return state;
    }

    public bool Target(GameObject target)
    {
        //int clear = 0;
        for (int i = 1; i < fingers.Length; i++) //親指を除く
        {
            if (fingers[i].GetComponent<StateComponent>().GetColliding() != target) return false;
            //clear++;
        }

        //if (clear >= 3) return true;

        return true;
    }

    public bool IsInset()
    {
        return inset.GetComponent<PreventInset>().GetInset();
    }

}
