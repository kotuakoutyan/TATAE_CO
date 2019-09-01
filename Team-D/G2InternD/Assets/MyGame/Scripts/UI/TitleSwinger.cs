using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSwinger : MonoBehaviour
{
    private RectTransform RectTransform;

    private bool MoveRight = false;
    [SerializeField] float SwingSpeed = 1.0f;
    [SerializeField] float SwingWidth = 5.0f;

    void Start()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (MoveRight && RectTransform.eulerAngles.z < 360f - SwingWidth && RectTransform.eulerAngles.z > 180f) MoveRight = !MoveRight;
        else if (!MoveRight && RectTransform.eulerAngles.z > SwingWidth && RectTransform.eulerAngles.z < 180f) MoveRight = !MoveRight; 
        RectTransform.rotation *= Quaternion.Euler(0, 0, SwingSpeed * (MoveRight ? -1.0f : 1.0f) * Time.deltaTime); 
	}
}
