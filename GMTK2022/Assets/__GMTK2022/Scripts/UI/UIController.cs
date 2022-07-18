using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using GMTK2022.Core;

namespace GMTK2022.UI
{
    public class UIController : MonoBehaviour
    {
        public GameObject[] UIGameObjects;
        public GameObject dialogue;

        public bool hintActive = false;

        public TextMeshProUGUI dialogueText;
        public float textSpeed = .5f;

        private Coroutine _coroutine;

        public void Awake()
        {
            for (int i = 0; i < UIGameObjects.Length; i++)
            {
                UIGameObjects[i].SetActive(hintActive);
            }
        }

        public void ToggleHint()
        {
            hintActive = !hintActive;
            for (int i = 0; i < UIGameObjects.Length; i++)
            {
                UIGameObjects[i].SetActive(hintActive);
            }
        }


        public void ShowDialogue(string line, Action action = null)
        {
            dialogue.SetActive(true);
            dialogueText.text = "";
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(WriteText(line, action));
        }

        private IEnumerator WriteText(string line, Action action = null)
        { 
            foreach (char c in line.ToCharArray())
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
            dialogue.SetActive(false);
            dialogueText.text = "";
            action?.Invoke();
        }

        public void Restart()
        {
            GM.Level.ReloadScene();
        }
    }
}
