using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBox : MonoBehaviour
{
    public GameObject objet;
    //public static int score = 0;
    public static int score = -1;
    
    private void OnTriggerEnter()
    {
        
        score++;
        //Debug.Log("SCORE  :  "+score);
    }

    public int getScore()
    {
        return score;
    }
}
