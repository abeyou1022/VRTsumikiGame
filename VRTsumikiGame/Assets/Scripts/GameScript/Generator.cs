using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] GameObject block;
    [SerializeField] GameObject spawn;
    [SerializeField] GameObject particle;
    public List<GameObject> list = new List<GameObject>();
    public bool playing;
    private float hue;

    // Start is called before the first frame update
    void Start()
    {
        playing = true;
        hue = 0;
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(list[0].transform.position.x);
        if(playing){
            if (Judge()) Generate();
        }
    }

    bool Judge()
    {
        foreach(GameObject block in list){
            //Debug.Log(block.transform.position.x);
            if (!block.GetComponent<Rigidbody>().IsSleeping() || block.transform.position.x > 0)
            //if (block.transform.position.x > 0)
            {
                return false;
            }
        }
        Debug.Log("静止");
        return true;
    }

    void Generate()
    {
        GameObject obj = Instantiate(block, spawn.transform.position, Quaternion.identity);
        obj.GetComponent<Renderer>().material.SetFloat("_Hue", hue);
        list.Add(obj);
        hue += 0.1f;
        if (ScoreText.state) ScoreText.score++;
    }

    public void Exp()
    {
        foreach(GameObject block in list)
        {
            GameObject exp = Instantiate(particle, block.transform.position, Quaternion.identity);
            Destroy(exp, 3f);
            Destroy(block);
        }
    }
}
