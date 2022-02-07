using System.Collections.Generic;
using UnityEngine;


public class TouchInputHandler : MonoBehaviour
{
    #region Fields

    [SerializeField] private Camera cam;
    private static TouchInputHandler instance;

    private List<ITouchable> touchListeners;
    private ITouchable currentTouch;

    #endregion



    #region Properties

    public static TouchInputHandler Instance => instance;

    #endregion



    #region Methods

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
        touchListeners = new List<ITouchable>();
    }


    private void Update()
    {
        #if UNITY_EDITOR
            UpdateMouse();
        #elif UNITY_ANDROID
            UpdateTouch();
        #endif
    }


    private void UpdateTouch()
    {
        if (Input.touches.Length > 0)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (currentTouch == null)
                    {
                        ITouchable t = null;

                        for (int i = 0; i < touchListeners.Count; i++)
                        {
                            t = touchListeners[i].StartTouch(ScreenToWorldPoint(touch.position));
                            if (t != null)
                            {
                                break;
                            }
                        }

                        if (t != null)
                        {
                            currentTouch = t;
                        }
                    }

                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (currentTouch != null)
                    {
                        currentTouch.UpdateTouch(ScreenToWorldPoint(touch.position));
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (currentTouch != null)
                    {
                        currentTouch.EndTouch(ScreenToWorldPoint(touch.position));
                        currentTouch = null;
                    }

                    break;
            }
        }
    }


    private void UpdateMouse()
    {
        var touch = Input.mousePosition;

        if (Input.GetMouseButtonDown(0) && currentTouch == null)
        {
            ITouchable t = null;

            for (int i = 0; i < touchListeners.Count; i++)
            {
                t = touchListeners[i].StartTouch(ScreenToWorldPoint(touch));
                if (t != null)
                {
                    break;
                }
            }

            if (t != null)
            {
                currentTouch = t;
            }
        }

        if (Input.GetMouseButton(0) && currentTouch != null)
        {
            currentTouch.UpdateTouch(ScreenToWorldPoint(touch));
        }

        if (Input.GetMouseButtonUp(0) && currentTouch != null)
        {
            currentTouch.EndTouch(ScreenToWorldPoint(touch));
            currentTouch = null;
        }
    }


    private Vector3 ScreenToWorldPoint(Vector3 pos)
    {
        pos.Set(pos.x, pos.y, 100);
        var result = cam.ScreenToWorldPoint(pos);
        return result;
    }


    public void Register(ITouchable obj)
    {
        touchListeners?.Add(obj);
    }


    public void Unregister(ITouchable obj)
    {
        touchListeners?.Remove(obj);
    }

    #endregion
}