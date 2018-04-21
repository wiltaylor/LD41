using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceContainer : MonoBehaviour
{
    public Sprite FullImage;
    public Sprite EmptyImage;

    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
        SetContainer(false);
    }

    public void SetContainer(bool full)
    {
        try
        {
            _image.sprite = full ? FullImage : EmptyImage;
        }
        catch
        {
            //skip errors
        }
    }
}
