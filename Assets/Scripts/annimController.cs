using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class annimController : MonoBehaviour
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
        if (Input.GetKeyDown("1"))
        {
            anim.Play("Punch Combo");
            
        }
        if (Input.GetKeyDown("2"))
        {
            anim.Play("mixamo_com");
            System.Threading.Thread.Sleep(1000);//5sec
            anim.Play("Illegal Elbow Punch-1");

        }
    }
}
