using UnityEngine;
using System.Collections;

public class GameManager
{
    public int level = -1;
    public int levelState = 0;
    public int levelProgress = 0;
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
            Debug.Log("Increase " + level + ", " + levelState + ", " + levelProgress);
            levelProgress += 1;
        }
    }
    private void NextLevel(Main main)
    {
        level += 1;
        levelState = 0;
        levelProgress = 0;
        Debug.Log("Level " + level);
        main.interLevelPanel.FadeOut("Part " + (level + 1), 1);
        main.ResetMap();
    }
    public void Update(Main main)
    {
        if (level == -3)
        {
            pauseGame = true;
            main.genevieve.animToPlay = 8;
            timer.Reset();
            levelState = 0;
        }
        if (level == -2)
        {
            if (levelState == 0)
            {
                if (timer.Get() > 3.0f)
                {
                    pauseGame = true;
                    main.interLevelPanel.FadeIn("You died", 1);
                    main.genevieve.animToPlay = 8;
                    levelState += 1;
                }
            }
        }
        if (level == -1)
        {
            pauseGame = true;
            main.interLevelPanel.Set("Part " + (level + 2), 1);
            if (timer.Get() >= 2.0f)
                NextLevel(main);
        }
        if (level >= 0 && level <= 5)
        {
            UpdateLevel(main);
        }
        if (level == 6)
        {
            pauseGame = true;
            main.interLevelPanel.Set("Win!", 1);
        }
    }
    private float[] durationBeforeSalleSound = new float[] { 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f };
    private float[] durationBeforeRunning = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
    private float[] durationBeforeDrop = new float[] { 88.0f, 61.0f, 46.0f, 18.0f, 15.0f, 23.0f };
    private float[] durationBeforeDeath = new float[] { 100.0f, 80.0f, 70.0f, 140.0f, 130.0f, 135.0f };
    private float[] durationBeforeEnd = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
    private float[] durationInterLevel = new float[] { 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f };
    private float[] progressNeeded = new float[] { 20, 20, 1, 5, 1, 1 };
    private void UpdateLevel(Main main)
    {
        if (levelState == 0)
        {
            timer.Reset();
            pauseGame = true;
            levelState += 1;

            main.mamySource.clip = main.audioClips[level * 2];
            main.mamySource.volume = 0.2f;
            main.mamySource.Play();
        }
        if (levelState == 1)
        {
            if (timer.Get() >= durationBeforeSalleSound[level])
            {
                timer.Reset();
                pauseGame = true;
                levelState += 1;

                main.salle.clip = main.audioClips[level * 2 + 1];
                main.salle.volume = 0.2f;
                main.salle.Play();

                main.genevieve.sat = false;
            }
        }
        if (levelState == 2)
        {
            if (timer.Get() >= durationBeforeRunning[level])
            {
                timer.Reset();
                pauseGame = false;
                levelState += 1;
            }
        }
        if (levelState == 3)
        {
            if (timer.Get() >= durationBeforeDeath[level] - durationBeforeRunning[level])
            {
                levelState = 10;
                level = -2;
                timer.Reset();
            }
            else
            {
                if (timer.Get() >= durationBeforeDrop[level] - durationBeforeRunning[level])
                {
                    Debug.Log("DROP");
                    main.salle.volume = 1.0f;
                }
                else
                    Debug.Log(timer.Get() + ", " + (durationBeforeDrop[level] - durationBeforeRunning[level]));
                if (levelProgress >= progressNeeded[level])
                {
                    timer.Reset();
                    pauseGame = true;
                    levelState += 1;

                    main.salle.Stop();
                }
            }
        }
        if (levelState == 4)
        {
            if (timer.Get() >= durationBeforeEnd[level])
            {
                timer.Reset();
                pauseGame = true;
                main.interLevelPanel.FadeIn("Part " + (level + 2), 1);
                levelState += 1;
            }
        }
        if (levelState == 5)
        {
            if (timer.Get() >= durationInterLevel[level])
            {
                timer.Reset();
                NextLevel(main);
            }
        }
    }
}
