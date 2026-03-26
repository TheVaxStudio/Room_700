using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class VNManager : MonoBehaviour
{
    // UI References - Assign in Inspector
    public TextMeshProUGUI DialogueText;

    public TextMeshProUGUI SpeakerNameText;

    public Image BackgroundImage;

    public Image CharacterPortrait;

    public Button[] ChoiceButtons;

    public AudioSource BgmSource;

    public AudioSource SfxSource;

    public HudCutscene HudScene;

    // Data Structures
    Queue<string> DialogueQueue = new Queue<string>();
    
    Dictionary<string, object> StoryVariables =
    new Dictionary<string, object>();
    
    bool IsDisplayingText = false;

    // Dialogue Management
    public void StartDialogue(string[] Lines)
    {
        DialogueQueue.Clear();

        foreach (string Line in Lines)
        {
            DialogueQueue.Enqueue(Line);
        }

        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (DialogueQueue.Count > 0 && !IsDisplayingText)
        {
            string Line = DialogueQueue.Dequeue();

            StartCoroutine(DisplayText(Line));
        }
    }

    IEnumerator DisplayText(string Line)
    {
        IsDisplayingText = true;

        DialogueText.text = "";

        // Parse line: Assume format "Speaker: Text" or just "Text"
        string[] Parts = Line.Split(new char[] { ':' }, 2);

        if (Parts.Length > 1)
        {
            SpeakerNameText.text = Parts[0].Trim();
        
            Line = Parts[1].Trim();
        }

        else
        {
            SpeakerNameText.text = "";
        }

        // Typewriter effect
        foreach (char C in Line)
        {
            DialogueText.text += C;
        
            yield return new WaitForSeconds(0.05f); // Adjust speed
        }
        
        IsDisplayingText = false;
    }

    // Choice System
    public void ShowChoices(string[] Choices,
    System.Action<int> OnChoiceSelected)
    {
        HideChoices();
        
        for (int I = 0; I < Mathf.Min(ChoiceButtons.Length,
        Choices.Length); I++)
        {
            ChoiceButtons[I].gameObject.SetActive(true);

            ChoiceButtons[I].GetComponentInChildren<TextMeshProUGUI>().
            text = Choices[I];

            int Index = I;

            ChoiceButtons[I].onClick.RemoveAllListeners();
            
            ChoiceButtons[I].onClick.AddListener(() => 
            {
                OnChoiceSelected(Index);
            
                HideChoices();
            });
        }
    }

    public void HideChoices()
    {
        foreach (Button Btn in ChoiceButtons)
        {
            Btn.gameObject.SetActive(false);
        }
    }

    // Visual Elements
    public void ChangeBackground(Sprite Background)
    {
        if (BackgroundImage != null)
        {
            BackgroundImage.sprite = Background;
        }
    }

    public void ShowCharacter(Sprite Portrait, Vector2 Position)
    {
        if (CharacterPortrait != null)
        {
            CharacterPortrait.sprite = Portrait;

            CharacterPortrait.rectTransform.anchoredPosition = Position;
            
            CharacterPortrait.gameObject.SetActive(true);
        }
    }

    public void HideCharacter()
    {
        if (CharacterPortrait != null)
        {
            CharacterPortrait.gameObject.SetActive(false);
        }
    }

    // Audio Management
    public void PlayBGM(AudioClip Bgm, bool Loop = true)
    {
        if (BgmSource != null)
        {
            BgmSource.clip = Bgm;

            BgmSource.loop = Loop;
            
            BgmSource.Play();
        }
    }

    public void StopBGM()
    {
        if (BgmSource != null)
        {
            BgmSource.Stop();
        }
    }

    public void PlaySFX(AudioClip Sfx)
    {
        if (SfxSource != null)
        {
            SfxSource.PlayOneShot(Sfx);
        }
    }

    public void PlayVoice(AudioClip Voice)
    {
        // Assuming voice uses sfxSource or separate
        PlaySFX(Voice);
    }

    // Story Variables
    public void SetVariable(string Key, object Value)
    {
        StoryVariables[Key] = Value;
    }

    public object GetVariable(string Key)
    {
        return StoryVariables.ContainsKey(Key) 
        ? StoryVariables[Key] : null;
    }

    public bool CheckCondition(string Key, object Value)
    {
        return StoryVariables.ContainsKey(Key) 
        && StoryVariables[Key].Equals(Value);
    }

    // Save/Load System (Simplified - uses PlayerPrefs)
    public void SaveGame(int Slot)
    {
        string SaveKey = "VN_Save_" + Slot;
        // Save current state - this is basic, expand as needed
        PlayerPrefs.SetString(SaveKey + "_Dialogue",
        string.Join("|", DialogueQueue.ToArray()));

        PlayerPrefs.SetString(SaveKey + "_Variables",
        SerializeVariables());
        
        PlayerPrefs.Save();
    }

    public void LoadGame(int Slot)
    {
        string SaveKey = "VN_Save_" + Slot;

        if (PlayerPrefs.HasKey(SaveKey + "_Dialogue"))
        {
            string[] Lines = PlayerPrefs.GetString(SaveKey 
            + "_Dialogue").Split('|');

            StartDialogue(Lines);
        
            DeserializeVariables(PlayerPrefs.GetString(SaveKey 
            + "_Variables"));
        }
    }

    string SerializeVariables()
    {
        // Simple serialization - improve for complex types
        List<string> Serialized = new List<string>();

        foreach (var Kvp in StoryVariables)
        {
            Serialized.Add(Kvp.Key + ":" + Kvp.Value.ToString());
        }
        
        return string.Join(";", Serialized);
    }

    void DeserializeVariables(string Data)
    {
        StoryVariables.Clear();

        string[] Pairs = Data.Split(';');
        
        foreach (string Pair in Pairs)
        {
            string[] Kv = Pair.Split(':');
        
            if (Kv.Length == 2)
            {
                StoryVariables[Kv[0]] = Kv[1]; 
                // Assume string, cast as needed
            }
        }
    }

    // Transitions and Effects
    public void FadeIn(Image Target, float Duration)
    {
        StartCoroutine(Fade(Target, 0.0f, 1.0f, Duration));
    }

    public void FadeOut(Image Target, float Duration)
    {
        StartCoroutine(Fade(Target, 1.0f, 0.0f, Duration));
    }

    IEnumerator Fade(Image Target, float StartAlpha,
    float EndAlpha, float Duration)
    {
        Color Color = Target.color;

        Color.a = StartAlpha;
        
        Target.color = Color;

        float Timer = 0.0f;
        
        while (Timer < Duration)
        {
            Timer += Time.deltaTime;

            Color.a = Mathf.Lerp(StartAlpha,
            EndAlpha, Timer / Duration);

            Target.color = Color;

            yield return null;
        }

        Color.a = EndAlpha;
        
        Target.color = Color;
    }

    public void ShakeScreen(float Duration, float Intensity)
    {
        StartCoroutine(Shake(transform, Duration, Intensity));
    }

     IEnumerator Shake(Transform Target,
     float Duration, float Intensity)
    {
        Vector3 OriginalPos = Target.localPosition;

        float Timer = 0.0f;

        while (Timer < Duration)
        {
            Timer += Time.deltaTime;

            Target.localPosition = OriginalPos 
            + Random.insideUnitSphere * Intensity;

            yield return null;
        }
        
        Target.localPosition = OriginalPos;
    }

    // Scene Management
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    // Auto-Advance (for demo)
    public void EnableAutoAdvance(float Delay)
    {
        StartCoroutine(AutoAdvance(Delay));
    }

    IEnumerator AutoAdvance(float Delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(Delay);

            if (!IsDisplayingText && DialogueQueue.Count > 0)
            {
                ShowNextLine();
            }

            else if (DialogueQueue.Count == 0)
            {
                break;
            }
        }
    }

    // Input Handling
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsDisplayingText)
        {
            HudScene.enabled = true;

            HudScene.gameObject.SetActive(true);

            ShowNextLine();
        }
    }
}