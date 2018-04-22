using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OffScreenTracker : MonoBehaviour
{
    private static OffScreenTracker _instance;

    public static OffScreenTracker Instance
    {
        get { return _instance; }
    }
    public GameObject ArrowPrefab;
    public float TrackerPaddingX = 0.50f;
    public float TrackerPaddingY = 0.50f;
    public float CenterOffsetX = 1f;
    public float CenterOffsetY = 1f;
    public float LowTrackerPaddingX = 0.50f;
    public float LowTrackerPaddingY = 0.50f;
    public float LowCenterOffsetX = 1f;
    public float LowCenterOffsetY = 1f;


    private Camera _camera;
    private readonly List<ObjectTracker> _trackableObjects = new List<ObjectTracker>();

    void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    void Start()
    {
        _camera = Camera.main;
    }



    public void AddTrackableObject(GameObject obj, Color colour)
    {
        var tracker = Instantiate(ArrowPrefab);
        tracker.transform.SetParent(transform);
        tracker.GetComponent<Image>().color = colour;

        var control = tracker.GetComponent<ObjectTracker>();
        control.Target = obj.transform;
            

        _trackableObjects.Add(control);
    }

    public void RemoveTrackableObject(GameObject obj)
    {
        _trackableObjects.RemoveAll(o => o.Target == obj.transform);
    }

    void Update()
    {

        var paddingX = TrackerPaddingX;
        var paddingY = TrackerPaddingY;
        var offsetX = CenterOffsetX;
        var offsetY = CenterOffsetY;


        if (_camera.aspect < 1.4f)
        {
            paddingX = LowTrackerPaddingX;
            paddingY = LowTrackerPaddingY;
            offsetX = LowCenterOffsetX;
            offsetY = LowCenterOffsetY;
        }


        _trackableObjects.RemoveAll(o => o == null);

        foreach (var o in _trackableObjects.Where(o => o.Target == null))
            Destroy(o.gameObject);

        _trackableObjects.RemoveAll(o => o == null);

        foreach (var tracker in _trackableObjects)
        {
            if (tracker.Target == null || tracker.Target.gameObject == null)
                continue;

            var screenPoint = _camera.WorldToScreenPoint(tracker.Target.position);

            if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.y > 0 && screenPoint.x < Screen.width &&
                screenPoint.y < Screen.height)
            {
                tracker.gameObject.SetActive(false);
            }
            else
            {
                tracker.gameObject.SetActive(true);

                if (screenPoint.z < 0)
                    screenPoint *= -1;

                var screenCenter = new Vector3(Screen.width + offsetX, Screen.height + offsetY, 0) / 2;

                screenPoint -= screenCenter;

                var angle = Mathf.Atan2(screenPoint.y, screenPoint.x);
                angle -= 90 * Mathf.Deg2Rad;

                var cos = Mathf.Cos(angle);
                var sin = Mathf.Sin(angle);

                screenPoint = screenCenter + new Vector3(sin * 150, cos * 150, 0);

                var m = cos / sin;

                var screenBounds = new Vector3(screenCenter.x * paddingX, screenCenter.y * paddingY, screenCenter.z); 

                screenPoint = cos > 0
                    ? new Vector3(screenBounds.y / m, screenBounds.y, 0)
                    : new Vector3(-screenBounds.y / m, -screenBounds.y, 0);

                if(screenPoint.x > screenBounds.x)
                    screenPoint = new Vector3(screenBounds.x, screenBounds.x * m, 0);
                else if(screenPoint.x < -screenBounds.x)
                    screenPoint = new Vector3(-screenBounds.x, -screenBounds.x * m, 0);

                screenPoint += screenCenter;

                var rect = tracker.GetComponent<RectTransform>();
                rect.transform.position = screenPoint;
                rect.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

            }
        }
    }
}
