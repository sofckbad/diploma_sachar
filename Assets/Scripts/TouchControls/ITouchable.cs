using UnityEngine;


public interface ITouchable
{
    ITouchable StartTouch(Vector3 pos);
    void UpdateTouch(Vector3 pos);
    void EndTouch(Vector3 pos);
}