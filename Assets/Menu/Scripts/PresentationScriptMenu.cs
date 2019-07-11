using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class PresentationScriptMenu : MonoBehaviour
{
    public bool slideChangeWithGestures = true;
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
    public GameObject loadingScreen;
    public Slider slider;
    private GestureListener gestureListener;
    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer KeywordRecognizer;

    void Start()
    {
        // hide mouse cursor
        Cursor.visible = false;

        // calculate max slides and textures
        maxSides = horizontalSides.Count;
        maxTextures = slideTextures.Count;


        targetRotation = transform.rotation;
        isSpinning = false;

        tex = 0;
        side = 0;

        if (horizontalSides[side] && horizontalSides[side].GetComponent<Renderer>())
        {
            horizontalSides[side].GetComponent<Renderer>().material.mainTexture = slideTextures[tex];
        }

        // get the gestures listener
        gestureListener = Camera.main.GetComponent<GestureListener>();

        keywordActions.Add("commencer", Commencer);
        keywordActions.Add("quitter", Quitter);

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
                //if (gestureListener.IsSwipeLeft() || gestureListener.IsPush())
                if (gestureListener.IsSwipeLeft())
                    RotateToNext();
                //else if (gestureListener.IsSwipeRight() || gestureListener.IsPush())
                else if (gestureListener.IsSwipeRight())
                    RotateToPrevious();
                else if (gestureListener.IsClick())
                {
                    switch (tex)
                    {
                        case 0:
                            LoadLevel("Box 1");
                            DestroyImmediate(Camera.main.gameObject);
                            break;
                        case 1:
                            LoadLevel("stretching");
                            DestroyImmediate(Camera.main.gameObject);
                            break;
                        case 2:
                            LoadLevel("Fitness");
                            DestroyImmediate(Camera.main.gameObject);
                            break;
                        case 3:
                            LoadLevel("HealthyFood");
                            DestroyImmediate(Camera.main.gameObject);
                            break;
                    }

                }

            }
        }
        else
        {
            // spin the presentation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, spinSpeed * Time.deltaTime);

            // check if transform reaches the target rotation. If yes - stop spinning
            float deltaTargetX = Mathf.Abs(targetRotation.eulerAngles.x - transform.rotation.eulerAngles.x);
            float deltaTargetY = Mathf.Abs(targetRotation.eulerAngles.y - transform.rotation.eulerAngles.y);

            if (deltaTargetX < 1f && deltaTargetY < 1f)
            {
                isSpinning = false;
            }
        }
    }


    private void RotateToNext()
    {
        // set the next texture slide
        tex = (tex + 1) % maxTextures;

        if (!isBehindUser)
        {
            side = (side + 1) % maxSides;
        }
        else
        {
            if (side <= 0)
                side = maxSides - 1;
            else
                side -= 1;
        }

        if (horizontalSides[side] && horizontalSides[side].GetComponent<Renderer>())
        {
            horizontalSides[side].GetComponent<Renderer>().material.mainTexture = slideTextures[tex];
        }

        // rotate the presentation
        float yawRotation = !isBehindUser ? 360f / maxSides : -360f / maxSides;
        Vector3 rotateDegrees = new Vector3(0f, yawRotation, 0f);
        targetRotation *= Quaternion.Euler(rotateDegrees);
        isSpinning = true;
    }


    private void RotateToPrevious()
    {
        // set the previous texture slide
        if (tex <= 0)
            tex = maxTextures - 1;
        else
            tex -= 1;

        if (!isBehindUser)
        {
            if (side <= 0)
                side = maxSides - 1;
            else
                side -= 1;
        }
        else
        {
            side = (side + 1) % maxSides;
        }

        if (horizontalSides[side] && horizontalSides[side].GetComponent<Renderer>())
        {
            horizontalSides[side].GetComponent<Renderer>().material.mainTexture = slideTextures[tex];
        }

        // rotate the presentation
        float yawRotation = !isBehindUser ? -360f / maxSides : 360f / maxSides;
        Vector3 rotateDegrees = new Vector3(0f, yawRotation, 0f);
        targetRotation *= Quaternion.Euler(rotateDegrees);
        isSpinning = true;
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

    private void Commencer()
    {
        Debug.Log("Commencer");
        switch (tex)
        {
            case 0:
                LoadLevel("Box 1");
                DestroyImmediate(Camera.main.gameObject);
                break;
            case 1:
                LoadLevel("stretching");
                DestroyImmediate(Camera.main.gameObject);
                break;
            case 2:
                LoadLevel("Fitness");
                DestroyImmediate(Camera.main.gameObject);  
                break;
            case 3:
                LoadLevel("HealthyFood");
                DestroyImmediate(Camera.main.gameObject);
                break;
        }
        Time.timeScale = 1f;
    }

    private void Quitter()
    {
        Debug.Log("Quitter");
        Application.Quit();
    }

}
