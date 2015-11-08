﻿using UnityEngine;
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
        levelState = 0;
        levelProgress = 0;
        Debug.Log("Level " + level);
        main.interLevelPanel.FadeOut("Part " + (level + 1), 1);
        main.ResetMap();
    }
    public void Update(Main main)
    {
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
    private float[] durationBeforeEnd = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
    private float[] durationInterLevel = new float[] { 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f };
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
                main.salle.volume = 0.5f;
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
            if (timer.Get() >= durationBeforeDrop[level] - durationBeforeRunning[level])
            {
                Debug.Log("DROP");
                main.salle.volume = 1.0f;
            }
            else
                Debug.Log(timer.Get() + ", " + (durationBeforeDrop[level] - durationBeforeRunning[level]));
            if (levelProgress > 20)
            {
                timer.Reset();
                pauseGame = true;
                levelState += 1;

                main.salle.Stop();
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
