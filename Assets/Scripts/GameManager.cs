using UnityEngine;
using System.Collections;

public class GameManager
{
    public int level = -1;
    private int levelState = 0;
    private int levelProgress = 0;
    public bool running = true;
    public bool pauseGame = true;
    public Timer timer;

    public void Init()
    {
        timer = new Timer();
    }
    public void IncreaseCurrentLevelProgress(int forLevel)
    {
        if (forLevel == level)
        {
            Debug.Log(level + ", " + levelState + ", " + levelProgress);
            levelProgress += 1;
        }
    }
    private void NextLevel(Main main)
    {
        level += 1;
        levelProgress = 0;
        levelState = 0;
        Debug.Log("Level " + level);
        main.interLevelPanel.FadeOut("Part " + (level + 1), 1);
        main.ResetMap();
    }
    public void Update(Main main)
    {
        if(level == -1)
        {
            pauseGame = true;
            main.interLevelPanel.Set("Part " + (level + 2), 1);
            if (timer.Get() >= 2.0f)
                NextLevel(main);
        }
        if (level == 0)
        {
            if (levelState == 0)
            {
                if (timer.Get() >= 5.0f)
                {
                    timer.Reset();
                    pauseGame = false;
                    levelState = 1;
                }
            }
            if (levelState == 1)
            {
                if (levelProgress > 20)
                {
                    timer.Reset();
                    pauseGame = true;
                    levelState = 2;
                }
            }
            if (levelState == 2)
            {
                if (timer.Get() >= 1.0f)
                {
                    timer.Reset();
                    pauseGame = true;
                    main.interLevelPanel.FadeIn("Part " + (level + 2), 1);
                    levelState = 3;
                }
            }
            if (levelState == 3)
            {
                if (timer.Get() >= 2.0f)
                {
                    timer.Reset();
                    NextLevel(main);
                }
            }
        }
        if (level == 1)
        {
            if (levelState == 0)
            {
                if (timer.Get() >= 5.0f)
                {
                    timer.Reset();
                    pauseGame = false;
                    levelState = 1;
                }
            }
            if (levelState == 1)
            {
                if (levelProgress > 20)
                {
                    timer.Reset();
                    pauseGame = true;
                    levelState = 2;
                }
            }
            if (levelState == 2)
            {
                if (timer.Get() >= 1.0f)
                {
                    timer.Reset();
                    pauseGame = true;
                    main.interLevelPanel.FadeIn("Part " + (level + 2), 1);
                    levelState = 3;
                }
            }
            if (levelState == 3)
            {
                if (timer.Get() >= 2.0f)
                {
                    timer.Reset();
                    //NextLevel(main);
                }
            }
        }
    }
}
