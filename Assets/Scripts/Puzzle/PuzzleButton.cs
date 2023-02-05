using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : Interactable
{
    [SerializeField] private int digit;
    public GameObject puzzleInstance;
    private PuzzleManager puzzleScript;
    private bool animButtonPress;
    [SerializeField] public GameObject button;



    // Start is called before the first frame update
    void Start()
    {
        puzzleScript = puzzleInstance.GetComponent<PuzzleManager>();
    }


    protected override void Interact()
    {
        if (puzzleScript.puzzleSolved)
            return;

        StartCoroutine(InputDigit());

    }

    IEnumerator InputDigit()
    {
        //play button press animation
        animButtonPress = !animButtonPress;
        button.GetComponent<Animator>().SetBool("isPressed", animButtonPress);


        yield return new WaitForSeconds(0.6f);
        puzzleScript.input[0] = puzzleScript.input[1];
        puzzleScript.input[1] = puzzleScript.input[2];
        puzzleScript.input[2] = puzzleScript.input[3];
        puzzleScript.input[3] = digit;

        yield return new WaitForSeconds(0.4f);
        animButtonPress = !animButtonPress;
        button.GetComponent<Animator>().SetBool("isPressed", animButtonPress);

        bool inputMatch = puzzleScript.solution[0] == puzzleScript.input[0] && puzzleScript.solution[1] == puzzleScript.input[1]
                    && puzzleScript.solution[2] == puzzleScript.input[2] && puzzleScript.solution[3] == puzzleScript.input[3];

        if (inputMatch && !puzzleScript.puzzleSolved)
        {
            puzzleScript.puzzleSolved = true;
            //open door if puzzle is solved
            puzzleScript.doorOpen = !puzzleScript.doorOpen;
            puzzleScript.door.GetComponent<Animator>().SetBool("isOpen", puzzleScript.doorOpen);
            //Debug.Log("puzzle solved");
        }
    }
}