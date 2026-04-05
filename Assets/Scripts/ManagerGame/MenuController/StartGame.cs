using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [Header("UI")]
    public Button StartButton;

    public Button QuitButton;
    
    public Button LanguageButton;

    public TextMeshProUGUI StartButtonText;

    public TextMeshProUGUI QuitButtonText;

    public TextMeshProUGUI LanguageButtonText;
    
    public TextMeshProUGUI InfoText;

    [Header("Scene")]
    string SceneName = "Outside";

    readonly KeyCode R = KeyCode.R;

    enum Language
    {
        PTBR,
        EN
    }

    Language CurrentLanguage = Language.PTBR;

    public void StartGameScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    void QuitGame()
    {
        Application.Quit();

        Debug.Log("O jogo foi fechado!");
    }

    void ChangeLanguage()
    {
        CurrentLanguage = (CurrentLanguage == Language.PTBR) ? Language.EN : Language.PTBR;

        ApplyLanguage();
    }

    void ApplyLanguage()
    {
        if (CurrentLanguage == Language.PTBR)
        {
            StartButtonText.text = "Iniciar";

            QuitButtonText.text = "Sair";

            LanguageButtonText.text = "Idioma: Português";

            InfoText.text = "Pressione R para iniciar";
        }

        else
        {
            StartButtonText.text = "Start";

            QuitButtonText.text = "Quit";

            LanguageButtonText.text = "Language: English";
            
            InfoText.text = "Press R to Start";
        }
    }
}