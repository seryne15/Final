using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

public class Scoring : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float mainTimer;
    [SerializeField] private TextMeshProUGUI info;

    public static float timer;
    private int TimerSpeed = 1;
    private int score = 0;
    int j = 0;
    private bool doOnce = false;
    public static int att = 0;

    bool re = true;
    private string giantString;
    public string[] registeredUsers;
    public List<string> scores = new List<string>();
    public List<string> cpts = new List<string>();
    int total;
    string data = "{ ";
    int scorePredit;
    string username;
    bool niv1 = true;
    string table = "Stretching";
    bool rej;
    bool bl = false;

    private void Awake()
    {
        Enter.nbr = 0;
        score = 0;
    }

    IEnumerator Start()
    {
        timer = 0;
        timerText.text = "00:00";

        username = UserConnection.username;

        //recuperer toutes les parties du user
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("tablePost", table);

        WWW users = new WWW("http://localhost/ReadAllCardio.php", form);
        yield return users;

        giantString = users.text;
        if (giantString == "") // la premeiere partie du user
        {
            //UnityEngine.Debug.Log("vide");
            niv1 = true;
            timerText.text = "01 :00";
            timer = 60;
        }
        else
        {
            //UnityEngine.Debug.Log("rempli");
            registeredUsers = giantString.Split(';');

            string pattern = @"score = (.*)\|cpt = (.*)";

            for (int j = 0; j < registeredUsers.Length; j++)
            {
                Match m = Regex.Match(registeredUsers[j], pattern);
                scores.Add(m.Groups[1].Value);
                cpts.Add(m.Groups[2].Value);
            }

            //le nbr de parties jouées
            // UnityEngine.Debug.Log(giantString);
            print(registeredUsers.Length - 2);
            total = int.Parse(cpts[registeredUsers.Length - 2]);
            //UnityEngine.Debug.Log(total);
            if (total < 5)
            {
                niv1 = true;
                switch (total)
                {
                    case 1: //partie 2
                        timerText.text = "02:00";
                        timer = 120;
                        break;
                    case 2:  //partie 3
                        timerText.text = "03:30H";
                        timer = 210;
                        break;
                    case 3:  //partie 4
                        timerText.text = "03:00";
                        timer = 180;
                        break;
                    case 4: //partie 5
                        timerText.text = "04:00";
                        timer = 240;
                        break;
                }
            }
            else
            {
                // total ==> max cpt
                //s'il n'a tjr pas gagné la derniere partie , il doit la rejouer
                WWWForm form2 = new WWWForm();
                form2.AddField("usernamePost", username);
                form2.AddField("cptPost", total.ToString());
                form2.AddField("tablePost", table);

                WWW win = new WWW("http://localhost/RecupScore.php", form2);
                yield return win;

                giantString = win.text;
                if (giantString == "F")
                {
                    //il rejoue la partie
                    //UnityEngine.Debug.Log("il rejoue la partie");
                    rej = true;
                    int scoreDPNP = int.Parse(scores[registeredUsers.Length - 2]);
                    scorePredit = scoreDPNP;
                    //UnityEngine.Debug.Log("le score  :  " + scorePredit);
                    niv1 = false;
                }
                else
                {
                    //predir le score de la partie
                    // UnityEngine.Debug.Log("predir le score de la partie");
                    niv1 = false;
                    // ********************************premiere partie d'un niveau*************************************************
                    if (total % 5 == 0)
                    {
                        int scoreDPNP = int.Parse(scores[registeredUsers.Length - 2]); //score de la derniere partie du niveau precedent
                        scorePredit = scoreDPNP + 10;
                        //UnityEngine.Debug.Log("le score predit est 2 :  " + scorePredit);
                    }
                    else
                    {//*******partie 2, 3, 4, 5*******************************************************

                        //*************************************remplir la var data*****************************************************
                        int i = 1;
                        while (i <= total)
                        {
                            data = data + "'Niveau " + (i / 5 + 1) + "': {";
                            while (i <= total && (i % 5 > 0))
                            {
                                if (i == total)
                                { data = data + "'partie " + (i % 5) + "': " + scores[i - 1] + "   "; }
                                else { data = data + "'partie " + (i % 5) + "': " + scores[i - 1] + ",   "; }

                                i++;
                            }

                            if (i < total + 1 && i % 5 == 0)
                                data = data + "'partie 5': " + scores[i - 1] + " },";
                            else data = data + "}";
                            i++;
                        }
                        data = data + "}";
                        //************************************************************************* pyt**************************************
                        var nbr = (total / 5) + 1;
                        var psi = new ProcessStartInfo();
                        psi.FileName = @"C:\Users\dell\AppData\Local\Programs\Python\Python35-32\python";
                        var script = @"C:\Users\dell\Desktop\ESSAISYS.py";

                        psi.Arguments = $"\"{script}\" \"{data}\" \"{nbr}\"";
                        psi.UseShellExecute = false;
                        psi.CreateNoWindow = true;
                        psi.RedirectStandardOutput = true;
                        psi.RedirectStandardError = true;

                        var errors = "";
                        var results = "";

                        using (var process = Process.Start(psi))
                        {
                            errors = process.StandardError.ReadToEnd();
                            results = process.StandardOutput.ReadToEnd();

                        }
                        //UnityEngine.Debug.Log("ERRORS");
                        //UnityEngine.Debug.Log(errors);
                        //UnityEngine.Debug.Log("RESULTS");
                        //UnityEngine.Debug.Log(results);
                        //****************Recuperer le score predit pour cette partie
                        scorePredit = int.Parse(results);
                        //UnityEngine.Debug.Log("le score predit est  :  " + scorePredit);
                    }
                    niv1 = false;
                }
                timer = scorePredit * (0.85f);
                timerText.text = "00:00";
            }

        }
        score = 0;
    }


    // Update is called once per frame
    void Update()
    {
        score = Enter.nbr * 2;
        //scoreText.text = (Enter.nbr * 8).ToString();
        scoreText.text = score.ToString();
        if (Seconds() == 30)
        {
            mainTimer -= 30;
        }

        if (timer > 0.0f && bl == false)
        {
            timer -= Time.deltaTime * TimerSpeed;
            string minutes = Mathf.Floor((timer % 3600) / 60).ToString("00");
            if (Mathf.Floor((timer % 3600) / 60) < 0)
                minutes = "00";
            string seconds = (timer % 60).ToString("00");
            if (seconds == "60")
                seconds = (timer % 60 - 1).ToString("00");
            timerText.text = minutes + ":" + seconds;
            if (minutes == "00" && seconds == "00" && j < 5)
            {
                doOnce = false;
                re = false;
                bl = true;
                //**********************************

                if (niv1 == false)
                {
                    if (rej == true)
                    {
                        if (score < scorePredit)
                        {
                            //UnityEngine.Debug.Log("game over");
                            FinDePartie.gagner = false;
                        }
                        else
                        {
                            //UnityEngine.Debug.Log("BRAVO !!");
                            RegisterModif(username, scoreText.text, total.ToString(), table);
                            FinDePartie.gagner = true;
                            //UnityEngine.Debug.Log("hayaaaaaaa");
                        }
                    }
                    else
                    {
                        if (score < scorePredit)
                        {
                            score = scorePredit;
                            //UnityEngine.Debug.Log("game over");
                            Register(username, score.ToString(), table, "F");
                            FinDePartie.gagner = false;
                        }
                        else
                        {
                            //UnityEngine.Debug.Log("BRAVO !!");
                            Register(username, scoreText.text, table, "T");
                            FinDePartie.gagner = true;
                        }

                    }

                }
                else
                {
                    RegisterNiv1(username, scoreText.text, table);
                    if (score == 0)
                        FinDePartie.gagner = false;
                    else
                        FinDePartie.gagner = true;
                }
                FinDePartie.score = score;
                SceneManager.LoadScene("FinDePartie", LoadSceneMode.Single);
                DestroyImmediate(Camera.main.gameObject);
            }
            else if (j == 5)
                info.text = "FIN!!";
        }
        else if (timer <= 0.0f && !doOnce)
        {
            doOnce = true;
        }
    }

    private int Seconds()
    {
        return (int)(mainTimer - timer);
    }

    public void Register(string username, string score, string table, string win)
    {
        WWWForm form = new WWWForm();

        form.AddField("usernamePost", username);
        form.AddField("scorePost", score);
        form.AddField("tablePost", table);
        form.AddField("winPost", win);

        WWW register = new WWW("http://localhost/InsertCardio.php", form);

    }

    public void RegisterNiv1(string username, string score, string table)
    {
        WWWForm form = new WWWForm();

        form.AddField("usernamePost", username);
        form.AddField("scorePost", score);
        form.AddField("tablePost", table);


        WWW register = new WWW("http://localhost/InsertCardioNiv1.php", form);

    }

    public void RegisterModif(string username, string score, string cpt, string table)
    {
        WWWForm form = new WWWForm();

        form.AddField("usernamePost", username);
        form.AddField("scorePost", score);
        form.AddField("cptPost", cpt);
        form.AddField("tablePost", table);

        WWW register = new WWW("http://localhost/ModifCardio.php", form);

    }

    


}
