using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Timers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Linq;

public class RetourMenu : MonoBehaviour
{
    public bool slideChangeWithGestures = true;

   

    public List<Texture> slideTextures;
    public List<GameObject> horizontalSides;

    private bool isSpinning = false;
    private float slideWaitUntil;
    private Quaternion targetRotation;

    private GestureListener gestureListener;
    public GameObject loadingScreen;
    public Slider slider;
    public GameObject text;

    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer KeywordRecognizer;

    void Start()
    {
        // hide mouse cursor
        Cursor.visible = false;
        // anim = GetComponent<Animator>();


        // delay the first slide
        //slideWaitUntil = Time.realtimeSinceStartup + slideChangeAfterDelay;


        isSpinning = false;


        // get the gestures listener
        gestureListener = Camera.main.GetComponent<GestureListener>();
        
        keywordActions.Add("pause", Stop);
        keywordActions.Add("reprendre", Reprendre);
       // keywordActions.Add("quitter", Quitter);

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
                if (gestureListener.IsSwipeUp() )
                {                    
                    if (text.activeInHierarchy)
                    {
                        text.SetActive(false);
                        Time.timeScale = 1f;
                    }
                    else
                    {
                        
                        if(SceneManager.GetActiveScene().name == "stretching" || SceneManager.GetActiveScene().name == "HealthyFood")
                        {
                            text.SetActive(false);
                        }
                        else
                        {
                            text.SetActive(true);
                            Time.timeScale = 0f;
                        }
                    }
                        
                }
                else
                {
                    if (gestureListener.IsSwipeRight() && text.activeInHierarchy)
                    {
                        Time.timeScale = 1f;
                        LoadLevel("Menu");
                        DestroyImmediate(Camera.main.gameObject);
                        //Destroy(Camera.main.gameObject);
                    }
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
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void Stop()
    {
        Debug.Log("Stop");
        text.SetActive(true);
        Time.timeScale = 0f;
    }
    private void Reprendre()
    {
        text.SetActive(false);
        Time.timeScale = 1f;
    }
    //private void Quitter()
    //{
    //    Debug.Log("Quitter");
    //    Application.Quit();
    //}
}
