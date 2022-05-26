using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigunShot : MonoBehaviour
{

    public AudioSource source;
    public AudioClip clip;
   
    public GatlingGun gg ;
    
void Start(){

}
    // Update is called once per frame
    void Update()
    {
        
       if(gg.canFire){
       source.PlayOneShot(clip);
       
       } 
    }
}
