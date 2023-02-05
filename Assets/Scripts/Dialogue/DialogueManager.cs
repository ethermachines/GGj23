using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using Ink.Runtime;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    private static DialogueManager instance;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject continueIcon;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; } //make it read only to outside scripts

    [SerializeField] private float typingSpeed;
    private Coroutine displayLineCoroutine;
    private bool canContinueNextLine = false;

    private bool submitButtonPressed = false;

    [SerializeField] private DialogueTrigger dialogueTrigger;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one instance of Singleton DialogueManager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetButtonDown("Submit"))
        {
            ContinueStory();
        }


    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        //Instantiate new story taking inkJSON text
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();

    }

    private void ExitDialogueMode() //IEnumerator
    {
        //yield return new WaitForSeconds(0.25f);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
            ExitDialogueMode();//StartCoroutine(ExitDialogueMode());
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }



}
