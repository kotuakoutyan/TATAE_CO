using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerawork : MonoBehaviour
{
    [SerializeField] Transform Player1;
    [SerializeField] Transform Player2;

    [SerializeField] Vector3 Offset = new Vector3(0, 8, -12);
    [SerializeField] float MoveSpeed = 1.0f;

    [SerializeField] float Ground_Weight;
    private Vector3 Ground_Center;
    private Vector3 Position_Previous;

    void Start()
    {
        var ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<Renderer>().bounds;
        Ground_Center = ground.center;
    }

    void Update ()
    {
        var midPoint = (Player1.position + Player2.position + Ground_Center * Ground_Weight) / (2 + Ground_Weight);
        transform.position = Vector3.Lerp(transform.position, midPoint + Offset, MoveSpeed * Time.deltaTime);
        Position_Previous = transform.position;
	}
}
