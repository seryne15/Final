using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Enter : MonoBehaviour
{
    public GameObject objet1;
    public GameObject objet2;
    public static int cpt = 0;
    int cpt_prec = 0;
    public static int nbr = 0;
    public static bool b = false;
    bool dkhelt = false;

    IEnumerator SomeCoroutine()
    {
        if (CubeLeft.j == cubeRight.i && cubeRight.i > cpt && dkhelt ==false)
        {
            dkhelt = true;
            //nbr++;
            if (cpt - cpt_prec > 1)
            {
                cpt = cpt_prec + 1;
                print("aa");
            }
            if (cpt > 5)
                cpt = 0;
            print(cpt);
            cpt++;
            yield return new WaitForSeconds(2);

            switch (cpt)
            {
                case 1:
                    objet1.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    objet1.gameObject.transform.position = new Vector3(13.2f, 10, -44.19f);

                    objet2.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    objet2.gameObject.transform.position = new Vector3(15.2f, 10, -44.19f);
                    cpt_prec = cpt;

                    break;
                case 2:
                    objet1.gameObject.GetComponent<Renderer>().material.color = Color.green;
                    objet1.gameObject.transform.position = new Vector3(13.2f, 4.17f, -44.19f);

                    objet2.gameObject.GetComponent<Renderer>().material.color = Color.green;
                    objet2.gameObject.transform.position = new Vector3(15.2f, 4.17f, -44.19f);
                    cpt_prec = cpt;
                    break;
                case 3:
                    objet1.gameObject.GetComponent<Renderer>().material.color = Color.black;
                    objet1.gameObject.transform.position = new Vector3(11.5f, 8, -44.19f);

                    objet2.gameObject.GetComponent<Renderer>().material.color = Color.black;
                    objet2.gameObject.transform.position = new Vector3(12.5f, 8, -44.19f);
                    cpt_prec = cpt;
                    break;
                case 4:
                    objet1.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
                    objet1.gameObject.transform.position = new Vector3(16.5f, 8, -44.19f);

                    objet2.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
                    objet2.gameObject.transform.position = new Vector3(17.5f, 8, -44.19f);
                    cpt_prec = cpt;
                    break;
                case 5:
                    objet1.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                    objet1.gameObject.transform.position = new Vector3(11.94f, 9.84f, -44.19f);

                    objet2.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                    objet2.gameObject.transform.position = new Vector3(16.57f, 9.79f, -44.19f);
                    cpt_prec = 0;
                    cpt = 0;
                    cubeRight.i = 0;
                    CubeLeft.j = 0;
                    break;
            }
            nbr++;
            dkhelt = false;
        }
    }

    void App()
    {
        print("sissi");
        StartCoroutine(SomeCoroutine());
    }

    void Start()
    {
        InvokeRepeating("App", 0.0f, 1.0f);
    }

    //void Update()
    //{
    //     //StartCoroutine(SomeCoroutine());
    //    //if (CubeLeft.j == cubeRight.i && cubeRight.i > cpt)
    //    //{

    //    //    //nbr++;
    //    //    print(cpt);
    //    //    cpt++;

    //    //    switch (cpt)
    //    //    {
    //    //        case 1:
    //    //            objet1.gameObject.GetComponent<Renderer>().material.color = Color.red;
    //    //            objet1.gameObject.transform.position = new Vector3(13.2f, 10, -44.19f);

    //    //            objet2.gameObject.GetComponent<Renderer>().material.color = Color.red;
    //    //            objet2.gameObject.transform.position = new Vector3(15.2f, 10, -44.19f);

    //    //            break;
    //    //        case 2:
    //    //            objet1.gameObject.GetComponent<Renderer>().material.color = Color.green;
    //    //            objet1.gameObject.transform.position = new Vector3(13.2f, 4.17f, -44.19f);

    //    //            objet2.gameObject.GetComponent<Renderer>().material.color = Color.green;
    //    //            objet2.gameObject.transform.position = new Vector3(15.2f, 4.17f, -44.19f);

    //    //            break;
    //    //        case 3:
    //    //            objet1.gameObject.GetComponent<Renderer>().material.color = Color.black;
    //    //            objet1.gameObject.transform.position = new Vector3(11.5f, 8, -44.19f);

    //    //            objet2.gameObject.GetComponent<Renderer>().material.color = Color.black;
    //    //            objet2.gameObject.transform.position = new Vector3(12.5f, 8, -44.19f);

    //    //            break;
    //    //        case 4:
    //    //            objet1.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
    //    //            objet1.gameObject.transform.position = new Vector3(16.5f, 8, -44.19f);

    //    //            objet2.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
    //    //            objet2.gameObject.transform.position = new Vector3(17.5f, 8, -44.19f);

    //    //            break;
    //    //        case 5:
    //    //            objet1.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
    //    //            objet1.gameObject.transform.position = new Vector3(11.94f, 9.84f, -44.19f);

    //    //            objet2.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
    //    //            objet2.gameObject.transform.position = new Vector3(16.57f, 9.79f, -44.19f);
    //    //            cpt = 0;
    //    //            cubeRight.i = 0;
    //    //            CubeLeft.j = 0;
    //    //            break;
    //    //    }
    //    //    nbr++;

    //    //}
    //}
}
