using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ImageState
{
    Wait, Run, Skip, Praise, Stan
}

public class PlayerImageAnimation : MonoBehaviour
{
    [SerializeField] Image PlayerImage;
    
    [SerializeField] Sprite Wait;
    [SerializeField] Sprite Run;
    [SerializeField] Sprite Skip;
    [SerializeField] Sprite Praise;
    [SerializeField] Sprite Stan;

    void Start()
    {
        SetImage(ImageState.Wait, Vector3.zero);
    }

    public void SetImage(ImageState imageState, Vector3 velocity) 
    {
        switch (imageState)
        {
            case ImageState.Wait:
                PlayerImage.sprite = Wait;
                break;
            case ImageState.Run:
                PlayerImage.sprite = Run;
                break;
            case ImageState.Skip:
                PlayerImage.sprite = Skip;
                break;
            case ImageState.Praise:
                PlayerImage.sprite = Praise;
                break;
            case ImageState.Stan:
                PlayerImage.sprite = Stan;
                break;
        }

        if(velocity != Vector3.zero) PlayerImage.transform.rotation = Quaternion.Euler(0, velocity.x > 0 ? 180 : 0, 0);
    }
}
