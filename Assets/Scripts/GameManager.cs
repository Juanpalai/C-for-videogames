using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameSate
{
    menu, 
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    //initialize the Game State

    public GameSate currentGameState = GameSate.menu;

    public static GameManager instance;

    PlayerController controller;

    private void Awake()
    {
        if(instance == null) //verification that the instance has not been declared before.
        {
            instance = this;
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
       controller = GameObject.Find("Player").GetComponent<PlayerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && currentGameState != GameSate.inGame)
        {
            StarGame();
        }
    }

    //Funtion for Star the game
    public void StarGame()
    {
        SetGameState(GameSate.inGame);
        
    }

    //Funtion for Finish the game
    public void GameOver()
    {
        SetGameState(GameSate.gameOver);
    }
     //Funtion for Back to the menu of the game
    public void BackToMenu()
    {
        SetGameState(GameSate.menu);
    }

    private void SetGameState(GameSate newGameState)
    {
        if(newGameState==GameSate.menu)
        {
            //TODO: place the menu logic
        } else if(newGameState == GameSate.inGame)
        {
            //TODO: The scene must be set to play
            LevelManager.instance.RemoveAllLevelBlock();
            LevelManager.instance.GenerateInicalBlock();
            controller.Stargame();
        }else if(newGameState == GameSate.gameOver)
        {
            //TODO: The scene must be set to game over
        }

        this.currentGameState = newGameState;
    }    
}
