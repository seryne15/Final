using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Timers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PresentationScript : MonoBehaviour 
{  
    private bool b=false;
    public GameObject avatar;
    public Animator anim;
    public bool slideChangeWithGestures = true;
    public bool slideChangeWithKeys = true;
    public float spinSpeed = 5;

   

    public List<Texture> slideTextures;
    public List<GameObject> horizontalSides;

    // if the presentation cube is behind the user (true) or in front of the user (false)
    public bool isBehindUser = false;

    private int maxSides = 0;
    private int maxTextures = 0;
    private int side = 0;
    private int tex = 0;
    private bool isSpinning = false;
    private float slideWaitUntil;
    private Quaternion targetRotation;

    private GestureListener gestureListener;

    float time;
    float timeInit;    
    float tick= 2f;
    float x,y,z;
    float xr, yr, zr;
    public GameObject text;
    public EnterBox scriptA;
    public GameObject loadingScreen;
    public Slider slider;

    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer KeywordRecognizer;

    void Start()
    {
        // hide mouse cursor
        Cursor.visible = false;
        anim = GetComponent<Animator>();


       


        isSpinning = false;


        // get the gestures listener
        gestureListener = Camera.main.GetComponent<GestureListener>();
        x = avatar.transform.position.x;
        y = avatar.transform.position.y;
        z = avatar.transform.position.z;

        xr = avatar.transform.rotation.x;
        yr = avatar.transform.rotation.y;
        zr = avatar.transform.rotation.z;

        keywordActions.Add("pause", Stop);
        keywordActions.Add("reprendre", Reprendre);

        KeywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        KeywordRecognizer.OnPhraseRecognized += OnKeywordsRecognised;
        KeywordRecognizer.Start();
    }

    void Update()
    {        
        // dont run Update() if there is no user
        KinectManager kinectManager = KinectManager.Instance;
       

        if (!isSpinning)
        {

            if (slideChangeWithGestures && gestureListener)
            {
                time = (int)Time.time;
                if(b == true && time-timeInit==2)
                {                    
                    b = false;
                    //anim.Play("mixamo_com");
                    avatar.GetComponent<Animator>().Play("Idle");
                    avatar.gameObject.transform.position = new Vector3(x, y, z);
                    //transform.eulerAngles = new Vector3(xr, yr, zr);  pr remettre l avatar à sa position initiale
                    avatar.gameObject.transform.eulerAngles = new Vector3(xr, yr, zr);
                   
                }
                if (gestureListener.IsPush() && !text.activeInHierarchy)
                {
                    time = (int)Time.time;
                    timeInit = time;
                    b = true;
                    avatar.GetComponent<Animator>().Play("Punch Combo");

                }
                else
                {
                    //if (gestureListener.IsSwipeUp() )
                    //{
                    //    if(text.activeInHierarchy)
                    //    {
                    //        text.SetActive(false);
                    //        Time.timeScale = 1f;
                    //    }
                    //    else
                    //    {
                    //        text.SetActive(true);
                    //        Time.timeScale = 0f;
                    //    }
                            
                    //}
                    //else
                    //{
                        if (gestureListener.IsSwipeRight() && text.activeInHierarchy)
                        {
                            Time.timeScale = 1f;
                            LoadLevel("Menu");
                            DestroyImmediate(Camera.main.gameObject);
                        }
                    //}
                }
                
            }

            
        }
        

    }
    void LoadLevel(string SceneName)
    {
        StartCoroutine(LoadAsynchronously(SceneName));
    }
    IEnumerator LoadAsynchronously(string SceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }

    private void OnKeywordsRecognised(PhraseRecognizedEventArgs args)
    {
        keywordActions[args.text].Invoke();
    }

    private void Stop()
    {
        text.SetActive(true);
        Time.timeScale = 0f;
    }
    private void Reprendre()
    {
        text.SetActive(false);
        Time.timeScale = 1f;
    }

}
