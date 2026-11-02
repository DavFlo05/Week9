public interface ISaveable
{
    string SaveID { get; }

    TransformSaveData GetSaveData();

    void LoadFromData(TransformSaveData data);
}

