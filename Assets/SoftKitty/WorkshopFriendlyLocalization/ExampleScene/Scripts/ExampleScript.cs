using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoftKitty.WSFL
{
    public class ExampleScript : MonoBehaviour
    {
        #region Variables
        public Text LanguageName;
        public Text LanguageCount;
        public RectTransform DropDownRoot;
        private string[] Languages;
        private Dropdown LanguageDropDown;

        #endregion

        public void OnDropDownChange()
        {
            StartCoroutine(UpdateLanguageName());
            //The unity dropDown component update the label text 1 frame later after the value changed,
            //so we wait for one frame then switch the Language, otherwise the label's name will be out of sync;
        }

        IEnumerator UpdateLanguageName()
        {
            yield return 1;
            Localization.SelectedLanguage = LanguageDropDown.value;//Set the current Language to the value of DropDown box.
            //These are examples of using Localization.GetString() to replace the string in your code with translated text.
            LanguageName.text = Localization.GetString("Selected Language:") + Localization.GetString(Languages[Localization.SelectedLanguage].Replace(".txt", "").Replace("_", " "));
            LanguageCount.text = Localization.GetString("We offer ") + Languages.Length.ToString() + Localization.GetString(" selectable languages in the demo.");
        }

    }
}
