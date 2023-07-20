using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private GameControl gameController;

    public void gotCalled()
    {
        Debug.Log("Game controller got called");
    }
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (false)
        {
            gameController.gotCalled();
        }
    }
}
