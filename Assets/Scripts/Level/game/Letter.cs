using System;
using TMPro;
using UnityEngine;


public class Letter : MonoBehaviour, ITouchable
{
    #region Fields

    [SerializeField] private TMP_Text text;
    [SerializeField] private float touchRadius;
    [SerializeField] private float bubbleTriggerRadius;

    private Vector3 startPos;
    
    public static event Action OnChanged;
    
    private char letter;

    #endregion



    #region Unity Lifecycle

    private void OnEnable()
    {
        TouchInputHandler.Instance.Register(this);
    }


    private void OnDisable()
    {
        TouchInputHandler.Instance.Unregister(this);
    }

    #endregion



    #region Methods

    public void Initialize(char c, Vector3 position)
    {
        text.text = c.ToString();
        letter = c;
        startPos = position;
        transform.position = position;
    }


    public void Deinitialize()
    {
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
                if (bubble.SetLetter(letter))
                {
                    gameObject.SetActive(false);
                }
            }
        }
        
        transform.position = startPos;
        OnChanged?.Invoke();
    }

    #endregion
}