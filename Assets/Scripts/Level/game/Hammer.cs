using System;
using UnityEngine;


public class Hammer : MonoBehaviour, ITouchable
{
    #region Fields

    [SerializeField] private float touchRadius;
    [SerializeField] private float bubbleTriggerRadius;
    [SerializeField] private Transform startPos;

    public event Action<Bubble> OnHammerOnBubbleActivated;


    #endregion



    #region Methods

    public void Initialize()
    {
        TouchInputHandler.Instance.Register(this);

    }


    public void Deinitialize()
    {
        TouchInputHandler.Instance.Unregister(this);
    }

    #endregion



    #region ITouchable

    public ITouchable StartTouch(Vector3 pos)
    {
        pos.Set(pos.x, pos.y, transform.position.z);
        if ((pos - transform.position).magnitude < touchRadius)
        {
            transform.position = pos;
            return this;
        }

        return null;
    }


    public void UpdateTouch(Vector3 pos)
    {
        pos.Set(pos.x, pos.y, transform.position.z);
        transform.position = pos;
    }


    public void EndTouch(Vector3 pos)
    {
        var col = Physics2D.OverlapCircle(transform.position, bubbleTriggerRadius);

        if (col != null)
        {
            var bubble = col.GetComponent<Bubble>();
            if (bubble != null)
            {
                OnHammerOnBubbleActivated?.Invoke(bubble);
            }
        }

        transform.position = startPos.position;
    }

    #endregion
}