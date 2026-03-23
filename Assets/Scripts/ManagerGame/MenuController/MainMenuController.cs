using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class LanguageData
{
    public string Code;

    public string Start;

    public string Quit;

    public string Language;

    public string Info;

    public string Settings;

    public string Back;

    public string Volume;
    
    public string Quality;
}

[System.Serializable]
public class LanguageRoot
{
    public List<LanguageData> Languages;
}


public class MainMenuController : MonoBehaviour
{
    [Header("UI")]
    public Button StartButton;
    
    public TextMeshProUGUI InfoText;

    [Header("Scene Name")]
    public string SceneToLoad = "Outside";

    void Start()
    {
        if (StartButton != null)
        {
            StartButton.onClick.AddListener(LoadScene);
        }

        if (InfoText != null)
        {
            InfoText.text = "Pressione R para começar";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}