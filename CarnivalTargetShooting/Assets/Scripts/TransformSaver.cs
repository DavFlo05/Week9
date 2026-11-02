using UnityEngine;

public class TransformSaver : MonoBehaviour, ISaveable
{
    [SerializeField] private string _saveID = "Player";

    public string SaveID => _saveID;

    public TransformSaveData GetSaveData()
    {
        // Capture this object's current Transform.
        TransformSaveData data = new TransformSaveData();

        data.saveID = _saveID;
        data.position = transform.position;
        data.rotation = transform.rotation;
        data.scale = transform.localScale;

        return data;
    }

    public void LoadFromData(TransformSaveData data)
    {
        // Restore the saved Transform.
        transform.position = data.position;
        transform.rotation = data.rotation;
        transform.localScale = data.scale;
    }
}

