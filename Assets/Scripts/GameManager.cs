using UnityEngine;
using System.Collections;

public class GameManager
{
    public int level = 0;
    private int levelState = 0;
    private int levelProgress = 0;
    public bool running = true;
    public bool pauseGame = true;
    public Timer timer;

    public void Init()
    {
        timer = new Timer();
    }
    public void IncreaseCurrentLevelProgress()
    {
        levelProgress += 1;
    }
    private void NextLevel()
    {
        level += 1;
    }
    public void Update(Main main)
    {
        if(level == 0)
        {
            if (levelState == 0)
            {
                if (timer.Get() >= 5.0f)
                {
                    timer.Reset();
                    pauseGame = false;
                }
            }
            if (levelState == 1)
            {
                if (levelProgress > 20)
                {
                    timer.Reset();
                    pauseGame = true;
                }
            }
            if (levelState == 2)
            {
                if (timer.Get() >= 5.0f)
                {
                    timer.Reset();
                    NextLevel();
                }
            }
        }
    }
}
