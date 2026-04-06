using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HudCutscene : MonoBehaviour
{
    [Header("HUD")]
    public GameObject Hud;

    [Header("Config")]
    public bool StartOnAwake = false; // set false para iniciar apenas após eventos (ex.: pegar chave)

    public TextMeshProUGUI DialogueText;

    public TextMeshProUGUI NameText;

    public Image CharacterImage;

    public Image BackgroundImage;

    int CurrentLine = 0;

    [Header("História")]
    public DialogueLine[] Story;

    Coroutine TypingCoroutine;

    Coroutine BackgroundCoroutine;

    [Header("Eventos")]
    public UnityEvent OnDialogueStart;

    public UnityEvent OnDialogueEnd;

    [Header("Deploy")]
    public KeyPickup KeyPickupSource; // referência opcional para quem dispara a cutscene

    bool DialogueFinished;

    [Header("Troca de Cena")]
    string NextScene;

    [Header("Background Transition")]
    float DelayBeforeSceneChange = 5.5f;

    float BackgroundFadeDuration = 0.6f;

    public void TriggerCutscene()
    {
        CurrentLine = 0;
        
        DialogueFinished = false;

        if (Hud != null)
        {
            Hud.SetActive(true);
        }

        OnDialogueStart?.Invoke();

        ShowLine();
    }

    public void OnKeyCollected()
    {
        // método chamado por KeyPickup para iniciar cutscene
        TriggerCutscene();
    }

    void ShowLine()
    {
        if (CurrentLine >= Story.Length)
        {
            StartCoroutine(EndCutsceneRoutine());

            return;
        }

        DialogueLine Line = Story[CurrentLine];

        NameText.text = Line.SpeakerName;

        if (Line.CharacterSprite != null)
        {
            CharacterImage.sprite = Line.CharacterSprite;
        }

        if (Line.BackgroundSprite != null &&
        BackgroundImage.sprite != Line.BackgroundSprite)
        {
            if (BackgroundCoroutine != null)
            {
                StopCoroutine(BackgroundCoroutine);
            }

            BackgroundCoroutine = StartCoroutine(TransitionBackground(Line.BackgroundSprite));
        }

        if (TypingCoroutine != null)
        {
            StopCoroutine(TypingCoroutine);
        }

        TypingCoroutine = StartCoroutine(TypeText(Line.Dialogue));
    }

    public void NextLine()
    {
        CurrentLine++;

        ShowLine();
    }

    IEnumerator TypeText(string Text)
    {
        DialogueText.text = "";

        foreach (char C in Text)
        {
            DialogueText.text += C;

            yield return new WaitForSeconds(0.03f);
        }

        TypingCoroutine = null;
    }

    IEnumerator TransitionBackground(Sprite NewBackground)
    {
        float T = 0.0f;

        Color C = BackgroundImage.color;

        while (T < BackgroundFadeDuration)
        {
            T += Time.deltaTime;

            C.a = Mathf.Lerp(1.0f, 0.0f, T / BackgroundFadeDuration);

            BackgroundImage.color = C;
            
            yield return null;
        }

        BackgroundImage.sprite = NewBackground;

        T = 0.0f;

        while (T < BackgroundFadeDuration)
        {
            T += Time.deltaTime;

            C.a = Mathf.Lerp(0.0f, 1.0f, T / BackgroundFadeDuration);

            BackgroundImage.color = C;
            
            yield return null;
        }

        BackgroundCoroutine = null;
    }

    IEnumerator EndCutsceneRoutine()
    {
        DialogueFinished = true;

        OnDialogueEnd.Invoke();

        Hud.SetActive(false);

        yield return new WaitForSeconds(DelayBeforeSceneChange);

        if (!string.IsNullOrEmpty(NextScene))
        {
            SceneManager.LoadScene(NextScene);
        }
    }
}

[System.Serializable]
public class DialogueLine
{
    public string SpeakerName;

    [TextArea(9, 18)]
    public string Dialogue;

    public Sprite CharacterSprite;

    public Sprite BackgroundSprite;
}