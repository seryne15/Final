using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAnimContr : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        System.Threading.Thread.Sleep(1000);//5sec
        anim.Play("Punch Combo");
    }
}
