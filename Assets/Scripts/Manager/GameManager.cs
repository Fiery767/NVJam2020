using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager Instance {
        get
        {
            if (m_instance == null)
            {
                Debug.LogError("GAME MANAGER DOES NOT EXIST... Something is very wrong.");
            }
            return m_instance;
        }
    }

    public PlayerController Character;

    private Vector3 SpawnPosition;

    // 0th scene in build settings is Master, Levels in build settings are in order
    private int m_currentLevel = 1;
    private bool m_isGameStarted = false;

    private void Awake()
    {
        m_instance = this;
        SpawnPosition = Character.transform.position;
    }

    public void StartGame()
    {
        if (m_isGameStarted) return;

        m_isGameStarted = true;
        m_currentLevel = 1;
        SceneManager.sceneLoaded += OnLevelLoaded;
        LoadNextLevel();
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        m_currentLevel++;

        Character.transform.position = SpawnPosition;
    }

    public void LoadNextLevel()
    {
        if (m_currentLevel > 1)
        {
            SceneManager.UnloadSceneAsync(m_currentLevel - 1);
        }

        SceneManager.LoadScene(m_currentLevel, LoadSceneMode.Additive);
    }
}
