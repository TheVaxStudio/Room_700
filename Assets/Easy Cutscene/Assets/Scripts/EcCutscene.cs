using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using HisaGames.TransformSetting;
using HisaGames.CutsceneManager;
using HisaGames.Props;
using HisaGames.Character;

namespace HisaGames.Cutscene
{
    public class EcCutscene : MonoBehaviour
    {
        [System.Serializable]
        public class CharacterData
        {
            [Tooltip("Name of the character.")]
            public string name;

            [Tooltip("Identifier for the initial transform configuration.")]
            public string initialTransformID;

            [Tooltip("Identifier for the final transform configuration.")]
            public string finalTransformID;

            [Tooltip("Name of the sprite to use for this character.")]
            public string spriteString;
        }

        [System.Serializable]
        public class PropsData
        {
            [Tooltip("Name of the prop.")]
            public string name;

            [Tooltip("Target position of the prop in the scene.")]
            public Vector3 position;

            [Tooltip("Time speed for the prop to fade in and become visible.")]
            public float fadeSpeed;
        }

        [System.Serializable]
        public class CSUnityEvent : UnityEvent
        {
            
        }

        [System.Serializable]
        public class CutsceneData
        {
            [Header("Cutscene Data")]
            [Tooltip("Name of the cutscene.")]
            public string name;

            [Tooltip("Array of character data for the cutscene.")]
            public CharacterData[] charactersData;

            [Tooltip("Array of props data for the cutscene.")]
            public PropsData[] propsData;

            [Tooltip("Name displayed in the chat.")]
            public string nameString;

            [Tooltip("Chat text displayed during the cutscene.")]
            [TextArea] public string chatString;

            [Header("Cutscene Event")]
            [Tooltip("Event triggered before the cutscene starts.")]
            public CSUnityEvent cutscenePreEvent;

            [Tooltip("Event triggered after the cutscene ends.")]
            public CSUnityEvent cutscenePostEvent;
        }

        [SerializeField]
        [Tooltip("Array of cutscene data for the sequence.")]
        CutsceneData[] cutsceneData;

        [Tooltip("Index of the currently active cutscene.")]
        public int currentID;

        [Header("Cutscene Settings")]
        [HideInInspector]
        [Tooltip("Timer for auto-playing the next cutscene.")]
        float autoplayTime;

        [Header("Other Settings (no need to change)")]
        [SerializeField]
        [Tooltip("Text field for character names.")]
        Text charaNameText;

        [SerializeField]
        [Tooltip("Text field for chat text.")]
        Text chatText;

        [Tooltip("Full string of the chat text for the current cutscene.")]
        string chatTextString;

        [Tooltip("Delay timer between typing each character in the chat.")]
        float typingTimer;

        [Tooltip("Indicates whether typing animation is currently active.")]
        bool startTyping;

        [Tooltip("Array of active props data for the current cutscene.")]
        PropsData[] activePropsData;

        public void StartCutscene()
        {
            currentID = 0;

            autoplayTime = EcCutsceneManager.instance.autoplayTime;

            startTyping = false;
            
            typingTimer = EcCutsceneManager.instance.chatTypingDelay;

            PlayCutscene();
        }

        void PlayCutscene()
        {
            InvokePreEvent();
            
            PlayChatTypingAnimation();

            ShowingCurrentCharacters();

            ClearPreviousProps();

            ShowingCurrentProps();
        }

        void InvokePreEvent()
        {
            if (cutsceneData[currentID].cutscenePreEvent != null)
            {
                cutsceneData[currentID].cutscenePreEvent.Invoke();
            }
        }

        void InvokePostEvent()
        {
            if (cutsceneData[currentID].cutscenePostEvent != null)
            {
                cutsceneData[currentID].cutscenePostEvent.Invoke();
            }
        }

        void PlayChatTypingAnimation()
        {
            chatText.text = "";

            chatTextString = cutsceneData[currentID].chatString;

            startTyping = true;

            charaNameText.text = cutsceneData[currentID].nameString;

            if (charaNameText.text == "")
            {
                charaNameText.transform.parent.gameObject.SetActive(false);
            }

            else
            {
                charaNameText.transform.parent.gameObject.SetActive(true);
            }
        }

        void ShowingCurrentCharacters()
        {
            for (int i = 0; i < cutsceneData[currentID].charactersData.Length; i++)
            {
                CharacterData tempCharaData = cutsceneData[currentID].charactersData[i];

                EcCharacter character =
                EcCutsceneManager.instance.getCharacterObject(tempCharaData.name);

                if (character != null)
                {
                    character.transform.gameObject.SetActive(true);

                    if (tempCharaData.initialTransformID != "" && tempCharaData.finalTransformID != "")
                    {
                        EcTransformSetting charaInitialTransform =
                        EcCutsceneManager.instance.getCharaTransformSetting
                        (tempCharaData.initialTransformID);

                        EcTransformSetting charaFinalTransform
                        = EcCutsceneManager.instance.getCharaTransformSetting
                        (tempCharaData.finalTransformID);

                        if (charaInitialTransform != null)
                        {
                            character.transform.localRotation =
                            Quaternion.Euler(charaInitialTransform.rotation);

                            character.transform.localScale = charaInitialTransform.scale;
                            
                            character.transform.position = charaInitialTransform.position;

                            if (charaFinalTransform != null)
                            {
                                character.SetCharacterMove(charaFinalTransform.position);
                            }
                        }

                        else
                        {
                            Debug.Log("There are no transform setting named with "
                            + tempCharaData.initialTransformID);
                        }
                    }

                    if (tempCharaData.spriteString != "")
                    {
                        character.ChangeSpriteByName(tempCharaData.spriteString);
                    }

                    if (character.name == cutsceneData[currentID].nameString)
                    {
                        character.Sp.color = Color.white;
                    }

                    else
                    {
                        character.Sp.color = Color.gray;
                    }
                }

                else
                {
                    Debug.Log("There are no characters named with " + tempCharaData.name);
                }
            }
        }

        void ClearPreviousProps()
        {
            if (activePropsData != null)
            {
                for (int i = 0; i < activePropsData.Length; i++)
                {
                    EcProps props = EcCutsceneManager.instance.getPropObject(activePropsData[i].name);

                    if (props != null)
                    {
                        props.transform.gameObject.SetActive(false);
                    }

                    else
                    {
                        Debug.Log("There are no properties named with " + activePropsData[i].name);
                    }
                }
            }
        }

        void ShowingCurrentProps()
        {
            activePropsData = cutsceneData[currentID].propsData;

            for (int i = 0; i < activePropsData.Length; i++)
            {
                EcProps props = EcCutsceneManager.instance.getPropObject(activePropsData[i].name);

                if (props != null)
                {
                    props.transform.gameObject.SetActive(true);

                    props.transform.position = activePropsData[i].position;

                    props.transform.localScale = Vector3.zero;
                    
                    props.SetVisibility(activePropsData[i].fadeSpeed);
                }

                else
                {
                    Debug.Log("There are no properties named with " + activePropsData[i].name);
                }
            }
        }

        void StartTypingAnimation(Text chatText, string stringResult)
        {
            typingTimer -= Time.deltaTime;

            if (typingTimer <= 0)
            {
                if (chatText.text != stringResult || chatText.text.Length < stringResult.Length)
                {
                    chatText.text += stringResult[chatText.text.Length];

                    typingTimer = EcCutsceneManager.instance.chatTypingDelay;
                }

                else
                {
                    startTyping = false;
                }
            }
        }

        public void PlayNextCutscene()
        {
            if (chatText.text == chatTextString)
            {
                InvokePostEvent();

                if (currentID < cutsceneData.Length - 1)
                {
                    currentID += 1;

                    autoplayTime = EcCutsceneManager.instance.autoplayTime;
                    
                    PlayCutscene();
                }

                else
                {
                    Debug.Log("Cutscene finished");

                    EcCutsceneManager.instance.closeCutscenes();
                }
            }

            else
            {
                chatText.text = chatTextString;

                startTyping = false;

                typingTimer = 0;
            }
        }

        void AutoPlayingCutscene()
        {
            float temp = EcCutsceneManager.instance.autoplayTime;

            if (temp >= 0 && chatText.text == chatTextString)
            {
                autoplayTime -= Time.deltaTime;

                if (autoplayTime <= 0)
                {
                    PlayNextCutscene();
                }
            }
        }
    }
}