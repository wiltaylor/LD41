using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMessageSystem : MonoBehaviour
{
    public Text MessageTextBox;

    private static TextMessageSystem _instance;

    private readonly Queue<MessageItem> _items = new Queue<MessageItem>();
    private float _currentTimeOut = 0f;

    public static TextMessageSystem Instance
    {
        get { return _instance; }
    }

    public bool isEmpty
    {
        get
        {
            if (_items.Count > 0)
                return false;

            return _currentTimeOut <= 0f;
        }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public void AddMessage(string message, float timeout, Color colour)
    {
        _items.Enqueue(new MessageItem{Message = message, TimeOut = timeout, Colour = colour});
    }

    public void Clear()
    {
        _items.Clear();
        MessageTextBox.text = "";
        _currentTimeOut = 0f;
    }

    void Update()
    {
        if (_currentTimeOut <= 0f)
        {
            if (_items.Count > 0)
            {
                var item = _items.Dequeue();

                MessageTextBox.text = item.Message;
                MessageTextBox.color = item.Colour;
                _currentTimeOut = item.TimeOut;
            }
            else
            {
                MessageTextBox.text = "";
            }


        }
        else
        {
            _currentTimeOut -= Time.deltaTime;
        }



    }





}
