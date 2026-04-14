using System;
using UnityEngine;
using UnityEngine.InputSystem;


//Bijhorende oefeningen
//1. Voeg de "J" knop toe aan de bindings om te kunnen springen

//2. Geef een blokje een rigidbody en laat hem springen als je op jump drukt

// Zorg dat het blok je niet draait als hij ergens tegenaan loopt (rigidbody constraints)

//3. Zorg dat je blokje pas weer kan springen als hij de grond heeft geraakt

//4. Beweeg het blokje meerdere richtingen op met de moveAction

//5. Zorg dat ook "i","k", "j" en "l" als bindings werken voor de movement van player 1


//Huiswerk
// 
//!!!!1.Voeg een tweede blok met rigidbody toe en zorg dat hiervoor een player 2 map gemaakt wordt waarmee je kunt lopen en springen WERKT NOG NIET
//2. Zorg dat de blokjes hoger springen als de jump langer ingehouden wordt. Zorg wel voor een cap.. 
// 


public class InputPlayer : MonoBehaviour
{
    [SerializeField] private InputActionAsset input;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float jumpForcePerSec = 300f;
    [SerializeField] private float maxForce = 1000f;

    [SerializeField] private string mapName = "";
    private InputAction jumpAction;
    private InputAction moveAction;
    private Vector2 _movement;

    private Rigidbody rb;
    private float jumpTimer = 0f;
    private float jumpForce = 0f;
    void Awake()
    {
        //let op dat je een instance van de juiste map moet ophalen 

        InputActionMap map = input.FindActionMap(mapName);
        jumpAction = map.FindAction("Jump");
        moveAction = map.FindAction("Move");

        rb = transform.GetComponent<Rigidbody>();

    }

    void OnEnable()
    {
        input.FindActionMap("" + mapName).Enable();

    }
    void OnDisable()
    {
        input.FindActionMap("" + mapName).Disable();


    }
    // Update is called once per frame
    void Update()
    {


        CheckInput();

    }

    private void CheckInput()
    {
        if (jumpAction.WasPressedThisFrame())
        {
            jumpTimer = 0;
            //Debug.Log("was pressed this frame");
        }
        else if (jumpAction.IsPressed())
        {
            jumpTimer += Time.deltaTime;
            //Debug.Log("down");
        }
        else if (jumpAction.WasReleasedThisFrame())
        {
            //Debug.Log("was released this frame");
            jumpForce = jumpTimer * jumpForcePerSec + 200f;
            Jump(jumpForce);


        }
        transform.Translate(
            new Vector3(
                moveAction.ReadValue<Vector2>().x * Time.deltaTime * moveSpeed,
                0f,
                moveAction.ReadValue<Vector2>().y * Time.deltaTime * moveSpeed),
                Space.World);

    }
    private void Jump(float force)
    {
        if (force > maxForce) force = maxForce;
        rb.AddForce(Vector3.up * force);

    }
}
