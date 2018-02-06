using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBlock : MonoBehaviour {

    private GameObject gameBoard;
    private string location;
    public GameObject currentPiece;

    // Use this for initialization
    void Start()
    {
        gameBoard = GameObject.Find("GameBoard");
        location = ParseBlockName(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetLocation()
    {
        return location;
    }

    private void OnMouseDown()
    {
        gameBoard.GetComponent<GameController>().SelectedBlock(gameObject);
    }

    private static string ParseBlockName(string name)
    {
        string result = name.Substring(name.IndexOf('(') + 1, name.IndexOf(')') - 5);
        return result;
    }

    public void SetCurrentPiece(GameObject piece)
    {
        currentPiece = piece;
    }

    public GameObject GetCurrentPiece()
    {
        return currentPiece;
    }
}
