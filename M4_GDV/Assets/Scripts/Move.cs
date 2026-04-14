using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAsset;
    [SerializeField] private string mapName;
    [SerializeField] private float speed = 10f;
    private InputActionMap map;
    private InputAction moveAction;
    private Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        map = inputAsset.FindActionMap(mapName);
        moveAction = map.FindAction("Move");
    }
    void OnEnable()
    {
        map.Enable();
    }
    void OnDisable()
    {
        map.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        movement = moveAction.ReadValue<Vector2>();
        transform.Translate(new Vector3(
            movement.x * Time.deltaTime * speed,        //x as
            0f,                                         //y as
            movement.y * Time.deltaTime * speed         //z as
        ));
    }
}
