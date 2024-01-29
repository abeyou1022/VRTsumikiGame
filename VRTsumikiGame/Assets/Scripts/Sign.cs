using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour //掌の球にアタッチする
{
    //public GameObject[] fingers = new GameObject[5];
    Parts hand;
    private bool[] signs = new bool[5];

    //public GameObject text;
    //string hand = "";

    GameObject window;

    void Start()
    {
        for (int i = 0; i < signs.Length; i++) signs[i] = false; //初期化
        hand = gameObject.GetComponent<Hand>().part;
        //window = GameObject.Find("window4n");
    }

    void Update()
    {
        /*
                switch (HandSign()) //あとでEnumに書き換えたい
                {
                    case 1:
                        Debug.Log(hand+"グー");
                        //hand = "グー";  //3Dテキストオブジェクト用
                        break;
                    case 2:
                        Debug.Log(hand + "チョキ");
                        //hand = "チョキ";
                        break;
                    case 3:
                        Debug.Log(hand + "パー");
                        //hand = "パー";
                        break;
                    default:
                        Debug.Log(hand+"その他");
                        //hand = "その他";
                        break;
                }
                //text.GetComponent<TextMesh>().text = hand;
        */

        /*ウィンドウメニューの表示
         * 
        if (HandSign() == global::HandSign.INDEX)
        {
            window.SetActive(true);
        }
        //else window.SetActive(false); //もしかしたら, 無効にしてもwindowのColliderは動作し続けるかも...
        */
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<StateComponent>() != null)
        {
            StateComponent comp = other.GetComponent<StateComponent>();
            if (comp.part == Parts.RIGHT_FINGER || comp.part == Parts.LEFT_FINGER)
                other.GetComponent<StateComponent>().SetState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<StateComponent>() != null)
        {
            StateComponent comp = other.GetComponent<StateComponent>();
            if (comp.part == Parts.RIGHT_FINGER || comp.part == Parts.LEFT_FINGER)
                other.GetComponent<StateComponent>().SetState(false);
        }
    }


    public HandSign HandSign() //1はグー, 2はチョキ, 3はパー, 0はその他
    {
        Hand hand = gameObject.GetComponent<Hand>();


        //for (int i = 0; i < fingers.Length; i++) signs[i] = fingers[i].GetComponent<StateComponent>().State; //更新
        signs = hand.Sign(); //更新

        //親指(0)は参照しない
        if (signs[1] && signs[2] && signs[3] && signs[4]) return global::HandSign.ROCK;
        else if (!signs[1] && !signs[2] && signs[3] && signs[4]) return global::HandSign.SCISSOURS;
        else if (!signs[1] && !signs[2] && !signs[3] && !signs[4]) return global::HandSign.PAPER;
        else if (!signs[1] && signs[2] && signs[3] && signs[4]) return global::HandSign.INDEX;

        return global::HandSign.OTHERS;
    }
}

public enum HandSign
{
    ROCK,   //グー
    SCISSOURS, //チョキ
    PAPER,  //パー
    OTHERS, //その他

    INDEX,  //人差し指
}