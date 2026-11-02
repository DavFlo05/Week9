using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private TransformSaver _playerSaver;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private TargetSpawner _targetSpawner;

    private string _jsonPath;
    private string _binaryPath;

    private void Awake()
    {
        // Store save files in Unity's persistent data folder.
        _jsonPath = Path.Combine(
            Application.persistentDataPath, "positions.json");

        _binaryPath = Path.Combine(
            Application.persistentDataPath, "score.dat");
    }

    private void Update()
    {
        // New Input System keyboard controls.
        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            SaveGame();
        }

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        // Capture the player and all existing ducks.
        GameSaveData data = new GameSaveData();

        data.player = _playerSaver.GetSaveData();

        Target[] targets = FindObjectsByType<Target>(
            FindObjectsSortMode.None);

        data.ducks = new DuckSaveData[targets.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            data.ducks[i] = targets[i].GetSaveData();
        }

        // Save positions and duck configurations as JSON.
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_jsonPath, json);

        // Save the score separately in binary format.
        using (BinaryWriter writer =
            new BinaryWriter(File.Open(_binaryPath, FileMode.Create)))
        {
            writer.Write(_scoreManager.Score);
        }

        Debug.Log("Game saved!");
        Debug.Log(_jsonPath);
        Debug.Log(_binaryPath);
    }

    public void LoadGame()
    {
        // Both files must exist before loading.
        if (!File.Exists(_jsonPath) ||
            !File.Exists(_binaryPath))
        {
            Debug.LogWarning("No saved game found.");
            return;
        }

        try
        {
            string json = File.ReadAllText(_jsonPath);
            GameSaveData data =
                JsonUtility.FromJson<GameSaveData>(json);

            if (data == null || data.player == null ||
                data.ducks == null)
            {
                Debug.LogWarning("Invalid save data.");
                return;
            }

            int savedScore;

            using (BinaryReader reader =
                new BinaryReader(File.Open(_binaryPath, FileMode.Open)))
            {
                savedScore = reader.ReadInt32();
            }

            // Remove existing ducks before restoring saved ones.
            Target[] currentTargets = FindObjectsByType<Target>(
                FindObjectsSortMode.None);

            foreach (Target target in currentTargets)
            {
                target.gameObject.SetActive(false);
                Destroy(target.gameObject);
            }

            // Restore the player's Transform.
            _playerSaver.LoadFromData(data.player);

            // Recreate each saved duck.
            foreach (DuckSaveData duck in data.ducks)
            {
                _targetSpawner.RestoreTarget(duck);
            }

            // Restore the score and update its UI.
            _scoreManager.SetScore(savedScore);

            Debug.Log("Game loaded!");
        }
        catch (Exception exception)
        {
            Debug.LogError("Failed to load game: " +
                exception.Message);
        }
    }
}

