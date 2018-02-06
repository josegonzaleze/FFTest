using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPiece : MonoBehaviour
{

    public GameObject currentBlock;
    private GameObject selectedBlock;
    private Material defaultMaterial;
    private string currentBlockString;
    private GameObject gameBoard;

    struct Coord
    {
        public int X;
        public int Y;
    }

    // Use this for initialization
    void Start()
    {
        gameBoard = GameObject.Find("GameBoard");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurrentBlock(GameObject block)
    {
        currentBlock = block;
    }

    public string GetLocation()
    {
        return currentBlockString;
    }

    private void OnMouseDown()
    {
        gameBoard.GetComponent<GameController>().SelectedPiece(gameObject);
    }

    private void OnMouseEnter()
    {
        selectedBlock = currentBlock;

        currentBlockString = ParseBlockName(currentBlock.name);
        HighlightAvailable();
    }

    private void OnMouseExit()
    {

        Unhighlight(currentBlockString);
    }

    public void MoveLeft()
    {
        Vector3 newPosition = gameObject.transform.position;
        if (gameObject.tag == "Red")
        {
            newPosition.z += 0.5f;
            newPosition.x -= 0.5f;
            gameObject.transform.position = newPosition;
            int x = currentBlockString[0];
            x -= 1;
            int y = currentBlockString[1];
            y += 1;
            string temp = "" + x + y;
            currentBlockString = temp;
        }
        else
        {
            newPosition.z -= 0.5f;
            newPosition.x += 0.5f;
            gameObject.transform.position = newPosition;
            int x = currentBlockString[0];
            x += 1;
            int y = currentBlockString[1];
            y -= 1;
            string temp = "" + x + y;
            currentBlockString = temp;
        }
    }
    public void MoveRight()
    {
        Vector3 newPosition = gameObject.transform.position;
        if (gameObject.tag == "Red")
        {
            newPosition.z += 0.5f;
            newPosition.x += 0.5f;
            gameObject.transform.position = newPosition;
            int x = currentBlockString[0];
            x += 1;
            int y = currentBlockString[1];
            y += 1;
            string temp = "" + x + y;
            currentBlockString = temp;
        }
        else
        {
            newPosition.z -= 0.5f;
            newPosition.x -= 0.5f;
            gameObject.transform.position = newPosition;
            int x = currentBlockString[0];
            x -= 1;
            int y = currentBlockString[1];
            y -= 1;
            string temp = "" + x + y;
            currentBlockString = temp;
        }
    }
    public void MoveForward()
    {
        Vector3 newPosition = gameObject.transform.position;
        if (gameObject.tag == "Red")
        {
            newPosition.z += 0.5f;
            gameObject.transform.position = newPosition;
            int x = currentBlockString[0];
            int y = currentBlockString[1];
            y += 1;
            string temp = "" + x + y;
            currentBlockString = temp;
        }
        else
        {
            newPosition.z -= 0.5f;
            gameObject.transform.position = newPosition;
            int x = currentBlockString[0];
            int y = currentBlockString[1];
            y -= 1;
            string temp = "" + x + y;
            currentBlockString = temp;
        }
    }

    private void HighlightAvailable()
    {
        Moves possibleMoves = gameBoard.GetComponent<GameController>().GetPossibleMoves(gameObject);
        if (possibleMoves.Forward)
        {
            HighlightForwardBlock(currentBlockString);
        }
        if (possibleMoves.Left)
        {
            HighlightLeftBlock(currentBlockString);
        }
        if (possibleMoves.Right)
        {
            HighlightRightBlock(currentBlockString);
        }

    }
    private void HighlightForwardBlock(string currentBlock)
    {
        char x = currentBlock[0];
        char y = currentBlock[1];
        if (gameObject.tag == "Red")
        {
            y++;
        }
        else
        {
            y--;
        }
        if (gameBoard.GetComponent<GameController>().IsValidCoord("" + x + y))      //This is just sending the coordinate in a string
        {
            string forwardBlockString;
            forwardBlockString = "Cube(" + x + y + ")";
            GameObject forwardBlock = GameObject.Find(forwardBlockString);
            forwardBlock.GetComponent<Highlight>().HighlightBlock();
        }
    }
    private void HighlightLeftBlock(string currentBlock)
    {
        char x = currentBlock[0];
        char y = currentBlock[1];
        if (gameObject.tag == "Red")
        {
            y++;
            x--;
        }
        else
        {
            y--;
            x++;
        }
        if (gameBoard.GetComponent<GameController>().IsValidCoord("" + x + y))      //This is just sending the coordinate in a string
        {
            string leftBlockString;
            leftBlockString = "Cube(" + x + y + ")";
            GameObject leftBlock = GameObject.Find(leftBlockString);
            leftBlock.GetComponent<Highlight>().HighlightBlock();
        }
    }
    private void HighlightRightBlock(string currentBlock)
    {
        char x = currentBlock[0];
        char y = currentBlock[1];
        if (gameObject.tag == "Red")
        {
            y++;
            x++;
        }
        else
        {
            y--;
            x--;
        }
        if (gameBoard.GetComponent<GameController>().IsValidCoord("" + x + y))      //This is just sending the coordinate in a string
        {
            string rightBlockString;
            rightBlockString = "Cube(" + x + y + ")";
            GameObject rightBlock = GameObject.Find(rightBlockString);
            rightBlock.GetComponent<Highlight>().HighlightBlock();
        }
    }
    private void Unhighlight(string currentBlock)
    {
        UnHighlightForwardBlock(currentBlock);
        UnHighlightLeftBlock(currentBlock);
        UnHighlightRightBlock(currentBlock);

    }
    private void UnHighlightForwardBlock(string currentBlock)
    {
        char x = currentBlock[0];
        char y = currentBlock[1];
        if (gameObject.tag == "Red")
        {
            y++;
        }
        else
        {
            y--;
        }
        if (gameBoard.GetComponent<GameController>().IsValidCoord("" + x + y))      //This is just sending the coordinate in a string
        {
            string forwardBlockString;
            forwardBlockString = "Cube(" + x + y + ")";
            GameObject forwardBlock = GameObject.Find(forwardBlockString);
            forwardBlock.GetComponent<Highlight>().UnHighlightBlock();
        }
    }
    private void UnHighlightLeftBlock(string currentBlock)
    {
        char x = currentBlock[0];
        char y = currentBlock[1];
        if (gameObject.tag == "Red")
        {
            y++;
            x--;
        }
        else
        {
            y--;
            x++;
        }
        if (gameBoard.GetComponent<GameController>().IsValidCoord("" + x + y))      //This is just sending the coordinate in a string
        {
            string leftBlockString;
            leftBlockString = "Cube(" + x + y + ")";
            GameObject leftBlock = GameObject.Find(leftBlockString);
            leftBlock.GetComponent<Highlight>().UnHighlightBlock();
        }
    }
    private void UnHighlightRightBlock(string currentBlock)
    {
        char x = currentBlock[0];
        char y = currentBlock[1];
        if (gameObject.tag == "Red")
        {
            y++;
            x++;
        }
        else
        {
            y--;
            x--;
        }
        if (gameBoard.GetComponent<GameController>().IsValidCoord("" + x + y))      //This is just sending the coordinate in a string
        {
            string rightBlockString;
            rightBlockString = "Cube(" + x + y + ")";
            GameObject rightBlock = GameObject.Find(rightBlockString);
            rightBlock.GetComponent<Highlight>().UnHighlightBlock();
        }
    }
    private static string ParseBlockName(string name)
    {
        string result = name.Substring(name.IndexOf('(') + 1, name.IndexOf(')') - 5);
        
        return result;
    }
}