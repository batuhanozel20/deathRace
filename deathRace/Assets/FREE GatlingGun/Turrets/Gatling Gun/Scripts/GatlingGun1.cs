using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatlingGun1 : MonoBehaviour
{
    // target the gun will aim at
    Transform go_target;

    // Gameobjects need to control rotation and aiming
    public Transform go_baseRotation;
    public Transform go_GunBody;
    public Transform go_barrel;

    // Gun barrel rotation
    public float barrelRotationSpeed;
    float currentRotationSpeed;

    // Distance the turret can aim and fire from
    public float firingRange;

    // Particle system for the muzzel flash
    public ParticleSystem muzzelFlash;

    // Used to start and stop the turret firing
    public bool canFire = false;

    public float seconds;

    public GameObject enemyHit;
    public int enemyOnSight;

    public CarDamage CD;
    public CarDamage1 CD1;
    public CarDamage2 CD2;
    public CarDamage3 CD3;
    


    

    


  
        
   
    
  
    void Start()
    {
        
       // StartCoroutine(ExecuteAfterTime());
      
        
        this.GetComponent<SphereCollider>().radius =0;
       
        
    }

    void Update()
    {
        //enemyHit.
        if (canFire){
            if (enemyOnSight==1){
                CD.playerHP -= 10.0f*Time.deltaTime;
            }
            if (enemyOnSight==2){
                CD2.playerHP -= 10.0f*Time.deltaTime;
            }
            if (enemyOnSight==3){
                CD3.playerHP -= 10.0f*Time.deltaTime;
            }


        }
         AimAndFire();
         seconds= seconds-1.0f * Time.deltaTime;
          if (seconds<=0.0f){
         MinigunDeActivate();
 }



       //Debug.Log(seconds);

        


         
        
       
        
  
      
       
    }


    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position to show the firing range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, firingRange);
    }

    // Detect an Enemy, aim and fire
    public void OnTriggerEnter(Collider other)
    {
       
        
        if (other.gameObject.tag == "Enemy")
        {
            enemyHit = other.gameObject;
            if(enemyHit.name == "SportCar20_Driver_USA (1)"){
                //Debug.Log("AI 1");
                enemyOnSight = 1;
                //CD1.secondsForTakeDamage=5.0f;
            }
            else if(enemyHit.name == "SportCar20_Driver_USA AI (2)"){
                //Debug.Log("AI 2");
                enemyOnSight = 2;
                //CD2.secondsForTakeDamage=5.0f;
            }
            else if(enemyHit.name == "SportCar20_Driver_USA AI (3)"){
                //Debug.Log("AI 3");
                enemyOnSight = 3;
                //CD3.secondsForTakeDamage=5.0f;
            }
            
           
            go_target = other.transform;
            canFire = true; //Object Disable buraya yazılacak

             
               
        
           
        
        }
    }
    // Stop firing
   public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            canFire = false;
            
        }
    }

public void MinigunActivate(){
    seconds = 5.0f;
    this.GetComponent<SphereCollider>().radius = firingRange; 


    }
     
    

    
     
    



public void MinigunDeActivate(){
    
    this.GetComponent<SphereCollider>().radius =0;
}

 /*           IEnumerator ExecuteAfterTime()
 {
     MinigunActivate();

     yield return new WaitForSeconds(5);

     MinigunDeActivate();


    
 }*/
   
 

     void AimAndFire()
    {
        // Gun barrel rotation
        go_barrel.transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);

        // if can fire turret activates
        if (canFire)
        {
            // start rotation
            currentRotationSpeed = barrelRotationSpeed;

            // aim at enemy
            Vector3 baseTargetPostition = new Vector3(go_target.position.x, this.transform.position.y, go_target.position.z);
            Vector3 gunBodyTargetPostition = new Vector3(go_target.position.x, go_target.position.y, go_target.position.z);

            go_baseRotation.transform.LookAt(baseTargetPostition);
            go_GunBody.transform.LookAt(gunBodyTargetPostition);

            // start particle system 
            if (!muzzelFlash.isPlaying)
            {
                muzzelFlash.Play();
               
            }
        }
        else
        {
            // slow down barrel rotation and stop
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, 10 * Time.deltaTime);

            // stop the particle system
            if (muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop();
               
            }
        }
    }
}