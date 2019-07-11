using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class FinDePartie : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI BravoText;

    public static int score = 0;
    public static bool gagner = false;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = -1;
        Application.targetFrameRate = 50;

    }

    // Start is called before the first frame update
    IEnumerator Start()
    {

        scoreText.SetText("<sup> <#50aaff>Score "+ score +"</color></sup>");

        if (!gagner)
            BravoText.text = "<size=100%>V</size >ous <size=100%>A</size>vez <size=100%>E</size>choué !";
        

        yield return new WaitForSeconds(10.0f);

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        DestroyImmediate(Camera.main.gameObject);
    }

}
