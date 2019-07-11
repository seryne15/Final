using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeTriger : MonoBehaviour
{
    public GameObject objet;
    public static int i = 0;
    private void OnTriggerEnter()
    {
        // i++;
        //Debug.Log(i);
        if( scoringFitness.score > 0)
            scoringFitness.score = scoringFitness.score-1;

    }
}
