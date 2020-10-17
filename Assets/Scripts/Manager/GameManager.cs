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
    public NarratorEngine Narrator;
    public GameObject DeadBodyPrefab;

    private Vector3 SpawnPosition;
    private List<GameObject> CurrentLevelDeadBodies;

    private int m_totalDeaths = 0;

    // 0th scene in build settings is Master, Levels in build settings are in order
    private int m_currentLevel = 1;
    private bool m_isGameStarted = false;

    private void Awake()
    {
        m_instance = this;
        SpawnPosition = Character.transform.position;
        CurrentLevelDeadBodies = new List<GameObject>();
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

        ClearAllBodies();
    }

    public void ResetLevel()
    {
        Narrator.ClearBodies();
        Character.transform.position = SpawnPosition;
    }

    public void PlayerDie()
    {
        var deadBody = Instantiate(DeadBodyPrefab, Character.transform.position, Quaternion.identity);
        CurrentLevelDeadBodies.Add(deadBody);
        m_totalDeaths++;

        Character.transform.position = SpawnPosition;
    }

    public List<GameObject> GetDeadBodies()
    {
        return CurrentLevelDeadBodies;
    }

    public void ClearAllBodies()
    {
        foreach (var body in CurrentLevelDeadBodies)
        {
            Destroy(body);
        }
        CurrentLevelDeadBodies.Clear();
    }
}
