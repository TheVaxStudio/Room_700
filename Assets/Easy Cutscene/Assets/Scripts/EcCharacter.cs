using System.Collections.Generic;
using UnityEngine;
using HisaGames.CutsceneManager;

namespace HisaGames.Character
{
    [System.Serializable]
    public class EcCharacter : MonoBehaviour
    {
        public enum CharacterState
        {
            StayInScene,
            Moving
        }

        [Header("State Settings")]
        [HideInInspector]
        [Tooltip("Current state of the character (StayInScene, MoveIn, MoveOut).")]
        public CharacterState CharState;

        [Tooltip("Target position for the character when moving.")]
        Vector3 TargetMovePosition;

        [Header("Sprite Settings")]
        [HideInInspector]
        [Tooltip("SpriteRenderer component for displaying character sprites.")]
        public SpriteRenderer Sp;

        [Tooltip("Array of sprites used for the character.")]
        public Sprite[] SpriteImages;

        [Tooltip("Dictionary to map sprite names to their corresponding sprites.")]
        Dictionary<string, Sprite> SpriteDictionary;

        void Awake()
        {
            SpriteDictionary = new Dictionary<string, Sprite>();

            foreach (var Sprite in SpriteImages)
            {
                SpriteDictionary[Sprite.name] = Sprite;
            }

            Sp = GetComponentInChildren<SpriteRenderer>();
        }

        public void ChangeSpriteByName(string spriteName)
        {
            if (Sp != null)
            {
                if (SpriteDictionary.TryGetValue(spriteName, out var newSprite))
                {
                    Sp.sprite = newSprite;
                }

                else
                {
                    Debug.LogWarning($"Sprite with name {spriteName} not found.");
                }
            }

            else
            {
                Debug.LogWarning("spriteRenderer is null.");
            }
        }

        public void CheckingCharacterState()
        {
            var Step = EcCutsceneManager.instance.characterTransitionSpeed * Time.deltaTime;

            switch (CharState)
            {
                case CharacterState.Moving:
                    transform.position = Vector3.MoveTowards(transform.position,
                    TargetMovePosition, Step);

                    if (TargetMovePosition == transform.position)
                    {
                        CharState = CharacterState.StayInScene;

                        Debug.Log("Play StayInScene");
                    }

                    break;

                case CharacterState.StayInScene:
                    break;
            }
        }

        public void SetCharacterMove(Vector3 TargetPosition)
        {
            TargetMovePosition = TargetPosition;

            if (transform.position != TargetMovePosition)
            {
                CharState = CharacterState.Moving;

                Debug.Log("Play Moving");
            }
        }
    }
}