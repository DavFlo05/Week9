using System;
using UnityEngine;

[Serializable]
public class TransformSaveData
{
    public string saveID;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}

[Serializable]
public class DuckSaveData
{
    public TransformSaveData transform;
    public int type;
    public float speed;
    public int points;
    public float leftBoundary;
    public float rightBoundary;
    public float direction;
}

[Serializable]
public class GameSaveData
{
    public TransformSaveData player;
    public DuckSaveData[] ducks;
}

