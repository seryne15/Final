using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigerHF : MonoBehaviour
{
   

    void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.name)
            {
            case ("1"):
                ScorringHF.score++;
                break;
            case ("Chocolate"):
                ScorringHF.score--;
                break;
            case ("1(Clone)"):
                ScorringHF.score++;
                break;
            case ("Chocolate(Clone)"):
                ScorringHF.score--;
                break;
            case ("2(Clone)"):
                ScorringHF.score++;
                break;
            case ("3(Clone)"):
                ScorringHF.score++;
                break;
            case ("4(Clone)"):
                ScorringHF.score++;
                break;
            case ("5(Clone)"):
                ScorringHF.score++;
                break;
            case ("6(Clone)"):
                ScorringHF.score++;
                break;
            case ("7(Clone)"):
                ScorringHF.score++;
                break;
            case ("Cone(Clone)"):
                ScorringHF.score--;
                break;
            case ("CupCake(Clone)"):
                ScorringHF.score--;
                break;
            case ("Hamburger(Clone)"):
                ScorringHF.score--;
                break;
            case ("Muffin(Clone)"):
                ScorringHF.score--;
                break;
            case ("Donut(Clone)"):
                ScorringHF.score--;
                break;

        }
        Debug.Log("Score  " + ScorringHF.score);
        col.gameObject.GetComponent<BoxCollider>().enabled = false;
        Debug.Log(col.gameObject);
    }

}
