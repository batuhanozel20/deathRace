using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MovementScript : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float rotationSpeed;
    
     void Start()
    {
       rb = GetComponent<Rigidbody>();
       }

    void Update()
    {
       
        float translation = Input.GetAxis("Vertical") * speed;
        
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

         if(rb.velocity.z>0.05 || rb.velocity.z<-0.05 ){
                
            }
    }
}
