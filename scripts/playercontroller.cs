using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Playercontroller : MonoBehaviour
{
  // for the behavior of the player object
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private int count;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent <Rigidbody>();
       count = 0;
       SetCountText();
       winTextObject.SetActive(false);
    }
    void OnMove(InputValue movementValue)
      {
        // moving the object by giving it little bit of the force and direction
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
      }
    void SetCountText()
    {
      // giving the count and the winner text after the player have collected all the rewards
      countText.text = "Count: " + count.ToString();
      if (count >= 16)
      {
        winTextObject.SetActive(true);
        
      }
    }
    void FixedUpdate()
   {
    Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
    // Applying the force to the player
    rb.AddForce(movement * speed);

   }
   private void OnCollisionEnter(Collision collision)
{
   if (collision.gameObject.CompareTag("Enemy"))
   {
       // Destroy the current object
       Destroy(gameObject); 
       // Update the winText to display "You Lose!"
       winTextObject.gameObject.SetActive(true);
       winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
   }
}
   private void OnTriggerEnter(Collider other)
   {
    if (other.gameObject.CompareTag("PickUp"))
    {
      // counting and adding the value of the rewards in the screen
     other.gameObject.SetActive(false);
     count = count + 1;
     SetCountText();
    }
   }
}
