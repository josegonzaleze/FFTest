using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerTurn { X_Turn, O_Turn };
public enum Square { X, O, B };
public enum GameStatus { X_Win, O_Win, NotOver };

public struct Coord
{
    public int X;
    public int Y;
}

public struct Moves
{
    public bool Left;
    public bool Right;
    public bool Forward;
}

public class GameController : MonoBehaviour
{

    const int WIDTH = 8;
    const int LENGTH = 8;
    const int INIT_PLAYER_START_ROWS = 2;
    private Square[,] GameBoard;
    private GameObject currentPiece;
    private GameObject currentBlock;
    private PlayerTurn turn;

    // Use this for initialization
    void Start()
    {
        GameBoard = new Square[WIDTH, LENGTH];
        resetBoard();
        turn = PlayerTurn.O_Turn;
        //printBoard();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void resetBoard()
    {
        //Set the X's and O's on their ends of the board
        for (int i = 0; i < INIT_PLAYER_START_ROWS; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                GameBoard[i, j] = Square.X;
                GameBoard[LENGTH - i - 1, j] = Square.O;
            }
        }
        //Set the blanks in the middle
        for (int i = INIT_PLAYER_START_ROWS; i < LENGTH - 2; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                GameBoard[i, j] = Square.B;
            }
        }
    }

    //This function tells you all of the possible moves a piece can make, using the global struct Moves
    public Moves GetPossibleMoves(GameObject Piece)
    {

        Coord current = CoordinateTranslator(Piece.GetComponent<SelectPiece>().GetLocation());
        Moves possibleMoves;
        Coord newCoord;

        //Initializing all to false, if they turn out to be true they will be switched.
        possibleMoves.Left = false;
        possibleMoves.Right = false;
        possibleMoves.Forward = false;


        if (Piece.tag == "Red")
        {
            //Check Left
            newCoord.X = current.X - 1;
            newCoord.Y = current.Y - 1;
            if (IsValidCoord(newCoord))
            {
                if (isB(newCoord) || isX(newCoord))
                {
                    possibleMoves.Left = true;
                }
            }

            //Check Right
            newCoord.X = current.X + 1;
            newCoord.Y = current.Y - 1;
            if (IsValidCoord(newCoord))
            {
                if (isB(newCoord) || isX(newCoord))
                {
                    possibleMoves.Right = true;
                }
            }

            //Check forward
            newCoord.X = current.X;
            newCoord.Y = current.Y - 1;
            if (IsValidCoord(newCoord))
            {
                if (isB(newCoord))
                {
                    possibleMoves.Forward = true;
                }
            }
        }
        else
        {
            //Check Left
            newCoord.X = current.X + 1;
            newCoord.Y = current.Y + 1;
            if (IsValidCoord(newCoord))
            {
                if (isB(newCoord) || isO(newCoord))
                {
                    possibleMoves.Left = true;
                }
            }

            //Check Right
            newCoord.X = current.X - 1;
            newCoord.Y = current.Y + 1;
            if (IsValidCoord(newCoord))
            {
                if (isB(newCoord) || isO(newCoord))
                {
                    possibleMoves.Right = true;
                }
            }

            //Check forward
            newCoord.X = current.X;
            newCoord.Y = current.Y + 1;
            if (IsValidCoord(newCoord))
            {
                if (isB(newCoord))
                {
                    possibleMoves.Forward = true;
                }
            }
        }


        return possibleMoves;
    }
    public void SelectedPiece(GameObject piece)
    {
        currentPiece = piece;
    }
    public void SelectedBlock(GameObject block)
    {
        currentBlock = block;
        if (currentPiece != null)
        {       
            string pieceLocation = currentPiece.GetComponent<SelectPiece>().GetLocation();
            string blockLocation = currentBlock.GetComponent<SelectBlock>().GetLocation();
            if (IsValidCoord(blockLocation) && CheckGameStatus() == GameStatus.NotOver)
            {

                if (currentPiece.tag == "Red" && turn == PlayerTurn.O_Turn)
                {

                    if (blockLocation[0] == pieceLocation[0] - 1 && blockLocation[1] == pieceLocation[1] + 1) //Move left
                    {

                        moveDiagLeft(currentPiece.GetComponent<SelectPiece>().GetLocation(), currentPiece, currentBlock);
                        currentPiece = null;
                    }
                    else if (blockLocation[0] == pieceLocation[0] + 1 && blockLocation[1] == pieceLocation[1] + 1)//Move right
                    {

                        moveDiagRight(currentPiece.GetComponent<SelectPiece>().GetLocation(), currentPiece, currentBlock);                        
                        currentPiece = null;
                    }
                    else if (blockLocation[1] == pieceLocation[1] + 1)
                    {

                        moveForward(currentPiece.GetComponent<SelectPiece>().GetLocation(), currentPiece, currentBlock);
                        currentPiece = null;
                    }
                }
                else if (currentPiece.tag =="Blue" && turn == PlayerTurn.X_Turn)
                {

                    if (blockLocation[0] == pieceLocation[0] + 1 && blockLocation[1] == pieceLocation[1] - 1) //Move left
                    {

                        moveDiagLeft(currentPiece.GetComponent<SelectPiece>().GetLocation(), currentPiece, currentBlock);
                        currentPiece = null;
                    }
                    else if (blockLocation[0] == pieceLocation[0] - 1 && blockLocation[1] == pieceLocation[1] - 1)//Move right
                    {

                        moveDiagRight(currentPiece.GetComponent<SelectPiece>().GetLocation(), currentPiece, currentBlock);
                        currentPiece = null;
                    }
                    else if (blockLocation[1] == pieceLocation[1] - 1)
                    {
                        moveForward(currentPiece.GetComponent<SelectPiece>().GetLocation(), currentPiece, currentBlock);
                        currentPiece = null;
                    }
                }
            }
        }
    }
    public bool isX(Coord current)
    {
        if (GameBoard[current.Y, current.X] == Square.X)
        {
            return true;
        }
        return false;
    }
    public bool isO(Coord current)
    {
        if (GameBoard[current.Y, current.X] == Square.O)
        {
            return true;
        }
        return false;
    }
    public bool isB(Coord current)
    {
        if (GameBoard[current.Y, current.X] == Square.B)
        {
            return true;
        }
        return false;
    }
    public bool IsValidCoord(Coord CurrentLocation)
    {
        if (CurrentLocation.X >= 0 && CurrentLocation.X < 8 && CurrentLocation.Y >= 0 && CurrentLocation.Y < 8)
        {
            return true;
        }
        return false;
    }
    public bool IsValidCoord(string CurrentLocation)
    {
        Coord current = CoordinateTranslator(CurrentLocation);
        if (current.X >= 0 && current.X < 8 && current.Y >= 0 && current.Y < 8)
        {
            return true;
        }
        return false;
    }
    private Coord CoordinateTranslator(string location)
    {
        Coord result;
        result.X = location[0] - 65;
        result.Y = 8 - location[1] + 48;
        return result;
    }
    private static string ParseBlockName(string name)
    {
        string result = name.Substring(name.IndexOf('(') + 1, name.IndexOf(')') - 5);
        return result;
    }
    public bool moveDiagLeft(string location, GameObject piece, GameObject block)
    {
        Coord current = CoordinateTranslator(location);
        Coord newCoord;



        if (turn == PlayerTurn.X_Turn)
        {
            if (!isX(current))
            {
                return false;
            }
            newCoord.X = current.X + 1;
            newCoord.Y = current.Y + 1;

            if (!IsValidCoord(newCoord))
                return false;

            if (isB(newCoord) || isO(newCoord))
            {
                
                if (isO(newCoord))
                {
                    TakePiece();
                }
                GameBoard[newCoord.Y, newCoord.X] = Square.X;
                GameBoard[current.Y, current.X] = Square.B;
                piece.GetComponent<SelectPiece>().MoveLeft();
                piece.GetComponent<SelectPiece>().SetCurrentBlock(block);
                block.GetComponent<SelectBlock>().SetCurrentPiece(piece);
                turn = PlayerTurn.O_Turn;
                return true;
            }
        }
        else
        {
            if (!isO(current))
            {
                return false;
            }
            newCoord.X = current.X - 1;
            newCoord.Y = current.Y - 1;

            if (!IsValidCoord(newCoord))
                return false;

            if (isB(newCoord) || isX(newCoord))
            {
                if (isX(newCoord))
                {
                    TakePiece();
                }
                GameBoard[newCoord.Y, newCoord.X] = Square.O;
                GameBoard[current.Y, current.X] = Square.B;
                piece.GetComponent<SelectPiece>().MoveLeft();
                piece.GetComponent<SelectPiece>().SetCurrentBlock(block);
                block.GetComponent<SelectBlock>().SetCurrentPiece(piece);
                turn = PlayerTurn.X_Turn;
                return true;
            }
        }
        return false;
    }

    //This would be from the players perspective! (Assuming they are sitting behind their side of the board)
    public bool moveDiagRight(string location, GameObject piece, GameObject block)
    {
        Coord current = CoordinateTranslator(location);
        Coord newCoord;

        if (turn == PlayerTurn.X_Turn)
        {
            if (!isX(current))
            {
                return false;
            }
            newCoord.X = current.X - 1;
            newCoord.Y = current.Y + 1;

            if (!IsValidCoord(newCoord))
                return false;

            if (isB(newCoord) || isO(newCoord))
            {
                if (isO(newCoord))
                {
                    TakePiece();
                }
                GameBoard[newCoord.Y, newCoord.X] = Square.X;
                GameBoard[current.Y, current.X] = Square.B;
                piece.GetComponent<SelectPiece>().MoveRight();
                piece.GetComponent<SelectPiece>().SetCurrentBlock(block);
                block.GetComponent<SelectBlock>().SetCurrentPiece(piece);
                turn = PlayerTurn.O_Turn;
                return true;
            }
        }
        else
        {
            if (!isO(current))
            {
                return false;
            }
            newCoord.X = current.X + 1;
            newCoord.Y = current.Y - 1;

            if (!IsValidCoord(newCoord))
                return false;

            if (isB(newCoord) || isX(newCoord))
            {
                if (isX(newCoord))
                {
                    TakePiece();
                }
                GameBoard[newCoord.Y, newCoord.X] = Square.O;
                GameBoard[current.Y, current.X] = Square.B;
                piece.GetComponent<SelectPiece>().MoveRight();
                piece.GetComponent<SelectPiece>().SetCurrentBlock(block);
                block.GetComponent<SelectBlock>().SetCurrentPiece(piece);
                turn = PlayerTurn.X_Turn;
                return true;
            }
        }
        return false;
    }

    //This would be from the players perspective! (Assuming they are sitting behind their side of the board)
    public bool moveForward(string location, GameObject piece, GameObject block)
    {
        Coord current = CoordinateTranslator(location);
        Coord newCoord;

        if (turn == PlayerTurn.X_Turn)
        {
            if (!isX(current))
            {
                return false;
            }
            newCoord.X = current.X;
            newCoord.Y = current.Y + 1;

            if (!IsValidCoord(newCoord))
                return false;

            if (isB(newCoord))
            {
                GameBoard[newCoord.Y, newCoord.X] = Square.X;
                GameBoard[current.Y, current.X] = Square.B;
                piece.GetComponent<SelectPiece>().MoveForward();
                piece.GetComponent<SelectPiece>().SetCurrentBlock(block);
                block.GetComponent<SelectBlock>().SetCurrentPiece(piece);
                turn = PlayerTurn.O_Turn;
                return true;
            }
        }
        else
        {
            if (!isO(current))
            {
                return false;
            }
            newCoord.X = current.X;
            newCoord.Y = current.Y - 1;

            if (!IsValidCoord(newCoord))
                return false;

            if (isB(newCoord))
            {
                GameBoard[newCoord.Y, newCoord.X] = Square.O;
                GameBoard[current.Y, current.X] = Square.B;
                piece.GetComponent<SelectPiece>().MoveForward();
                piece.GetComponent<SelectPiece>().SetCurrentBlock(block);
                block.GetComponent<SelectBlock>().SetCurrentPiece(piece);
                turn = PlayerTurn.X_Turn;
                return true;
            }
        }
        return false;
    }

    private void TakePiece()
    {
        Debug.Log("Destroy");
        GameObject.Destroy(currentBlock.GetComponent<SelectBlock>().GetCurrentPiece());
    }

    public GameStatus CheckGameStatus()
    {
        for (int i = 0; i < 8; i++)
        {
            if (GameBoard[LENGTH - 1, i] == Square.X)
                return GameStatus.X_Win;
            else if (GameBoard[0, i] == Square.O)
                return GameStatus.O_Win;
        }
        return GameStatus.NotOver;
    }
}
