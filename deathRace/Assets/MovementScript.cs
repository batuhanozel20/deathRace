using UnityEngine;

public class MovementScript : MonoBehaviour
{
  public float speed;
  public Rigidbody rb;
  //public Transform from;
  //public Transform to;
  //private float timeCount = 0.0f;
  //public static Quaternion Slerp(Quaternion a, Quaternion b, float t){}
  

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        
    }
     void Update () {
         Vector3 pos = transform.position;
        var fwd= transform.forward*speed;
        var rgt= transform.right*speed;
      

         if (Input.GetKey ("w")) {
            
            for (int i = 1; i <=speed/(5/2); i++){
            rb.AddForce(fwd/(5/2));
            }
             
         }
         if (Input.GetKey ("s")) {
           
           for (int i = 1; i <=speed/(5/2); i++){
            rb.AddForce(-fwd/(5/2));
            }
             
         }
   
         if (Input.GetKey ("d")) {
             if(rb.velocity.z>0.05 || rb.velocity.z<-0.05 ){
            
            // pos.x += speed * Time.deltaTime;
             rb.AddForce(rgt/(5/2));
             transform.Rotate(0,2*speed*speed*Time.deltaTime,0,Space.Self);
             
             //transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, timeCount);
             //timeCount = timeCount + Time.deltaTime;
            
              }
         }
         if (Input.GetKey ("a")) {
               if(rb.velocity.z<-0.05 || rb.velocity.z>0.05){
             //pos.x -= speed * Time.deltaTime;
             rb.AddForce(-rgt/(5/2));
             transform.Rotate(0,-2*speed*speed*Time.deltaTime,0,Space.Self);

             //transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, timeCount);
             //timeCount = timeCount + Time.deltaTime;
             }
         }
             
 
         transform.position = pos;
         
         
     }
}
