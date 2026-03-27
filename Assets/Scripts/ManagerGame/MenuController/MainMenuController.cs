using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour, IPointerClickHandler
{
    [Header("UI")]
    public Button StartButton;
    
    public TextMeshProUGUI InfoText;

    public TextMeshProUGUI SettingsPanelText;

    public GameObject SettingsPanel;

    readonly KeyCode R = KeyCode.R;

    [Header("Scene Name")]
    public string SceneToLoad = "Outside";

    void Start()
    {
        if (InfoText != null && InfoText.tag == "Info")
        {
            InfoText.text = "Pressione R para começar";

            if (Input.GetKeyDown(R))
            {
                LoadScene();
            }
        }

        if (SettingsPanelText != null &&
        SettingsPanelText.tag == "SettingsText")
        {
            SettingsPanelText.text = "Clique para abrir as configurações";

            ToggleSettingsPanel();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadScene();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleSettingsPanel();
    }

    void ToggleSettingsPanel()
    {
        if (SettingsPanel != null)
        {
            SettingsPanel.SetActive(!SettingsPanel.activeSelf);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}

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