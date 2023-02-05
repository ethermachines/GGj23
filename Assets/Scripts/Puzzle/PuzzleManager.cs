using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int[] input;
    public int[] solution;
    public bool doorOpen;
    [SerializeField] public GameObject door;

    public bool puzzleSolved = false;
}
