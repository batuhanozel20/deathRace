using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class CarDamage1 : MonoBehaviour 
{
    public float hits;
    public float maxhits;
    public GameObject CarSmoke;
    public GameObject CarFire;
	public float maxMoveDelta = 1.0f; // maximum distance one vertice moves per explosion (in meters)
	public float maxCollisionStrength = 50.0f;
	public float YforceDamp = 0.1f; // 0.0 - 1.0
	public float demolutionRange = 0.5f;
	public float impactDirManipulator = 0.0f;
	public MeshFilter[] MeshList;
    public AudioSource Crash;

	private MeshFilter[] meshfilters;
	private float sqrDemRange;

    public int repairNum=0;
    public GatlingGun1 gg ;


    public float playerHP=100.0f;

    public static bool isDead;

    public TextMeshProUGUI playerHPText;

    public int crashing=0;

    public float seconds=0.0f;

    public float secondsForTakeDamage = 0.0f;


    //Save Vertex Data
    private struct permaVertsColl
    {
        public Vector3[] permaVerts;
    }
    private permaVertsColl[] originalMeshData;
    int i;


  
public void TakeDamage(float damageAmount){
    playerHP-=damageAmount;
    
}

public void countToFive(){
 seconds=5.0f;
}

	public void Start()
	{   
        playerHP=100.0f;
        isDead=false;
       

        if(MeshList.Length>0)
        	meshfilters = MeshList;
        else
        	meshfilters = GetComponentsInChildren<MeshFilter>();
        	
        sqrDemRange = demolutionRange*demolutionRange;

        LoadOriginalMeshData();

    }
 
    private void Update()
    {
        if(playerHP<=0.0f){
        isDead=true;
        GetComponent<Rigidbody>().velocity =Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity =Vector3.zero;
            CarFire.SetActive(true);
        }
        Debug.Log(isDead);
        if (50.0f > playerHP)
        {
            CarSmoke.SetActive(true);
        }
        secondsForTakeDamage = secondsForTakeDamage-1.0f*Time.deltaTime;
        if (secondsForTakeDamage>=0.0f){
            TakeDamage(10.0f*Time.deltaTime);
        }
        seconds= seconds-1.0f * Time.deltaTime;
          if(crashing==1 && seconds<=0){
              TakeDamage(10*Time.deltaTime);
          }

        playerHPText.text= ""+playerHP;
  
       
      //Debug.Log(repairNum);
       if (Input.GetKeyDown(KeyCode.R) && repairNum>0) {
          Repair();
          repairNum=0;

        }
    }

    void LoadOriginalMeshData()
    {
        originalMeshData = new permaVertsColl[meshfilters.Length];
        for( i = 0; i < meshfilters.Length; i++)
        {
            originalMeshData[i].permaVerts = meshfilters[i].mesh.vertices;
        }
    }
    
    public void Repair()
    {
        for (int i = 0; i < meshfilters.Length; i++)
        {
            meshfilters[i].mesh.vertices = originalMeshData[i].permaVerts;
            meshfilters[i].mesh.RecalculateNormals();
            meshfilters[i].mesh.RecalculateBounds();
            CarSmoke.SetActive(false);
            playerHP=100;
        }
    }
    
public void OnTriggerEnter (Collider other){
    if(other.GetComponent<Collider>().tag=="Finish"){
    SceneManager.LoadScene("YouLose");
    }
    if(other.GetComponent<Collider>().tag=="Ground" && seconds < -5.0f){
    playerHP=-1.0f;
    }

    if(other.GetComponent<Collider>().tag=="Enemy"){
    crashing=1;
    }

    if(other.GetComponent<Collider>().tag=="Minigun"){
     gg.MinigunActivate();
    
    countToFive();
    
    
      } 
    if(other.CompareTag("Repair")){
         
          Repair();
 
      } 

}
public void OnTriggerExit(Collider other){

    if (other.gameObject.tag == "Enemy")
        {
            crashing=0;
            
        }
}
	
    public void OnCollisionEnter( Collision collision ) 
	{
    
           
        Crash.Play();
        
        
       
		Vector3 colRelVel = collision.relativeVelocity;
		colRelVel.y *= YforceDamp;
		
		Vector3 colPointToMe = transform.position - collision.contacts[0].point;

		// Dot = angle to collision point, frontal = highest damage, strip = lowest damage
		float colStrength = colRelVel.magnitude * Vector3.Dot(collision.contacts[0].normal, colPointToMe.normalized);

		OnMeshForce( collision.contacts[0].point, Mathf.Clamp01(colStrength/maxCollisionStrength) );

	}
	
	// if called by SendMessage(), we only have 1 param
	public void OnMeshForce( Vector4 originPosAndForce )
	{
       
        OnMeshForce( (Vector3)originPosAndForce, originPosAndForce.w );
	}

	public void OnMeshForce( Vector3 originPos, float force )
	{
      
        // force should be between 0.0 and 1.0
        force = Mathf.Clamp01(force);
        
        for(int j=0; j<meshfilters.Length; ++j)
        {
        	Vector3 [] verts = meshfilters[j].mesh.vertices;

        	for (int i=0;i<verts.Length;++i)
        	{
        		Vector3 scaledVert = Vector3.Scale( verts[i], transform.localScale );						
        		Vector3 vertWorldPos = meshfilters[j].transform.position + (meshfilters[j].transform.rotation * scaledVert);
                Vector3 originToMeDir = vertWorldPos - originPos;
                Vector3 flatVertToCenterDir = transform.position - vertWorldPos;
                flatVertToCenterDir.y = 0.0f;
                   
                // 0.5 - 1 => 45� to 0�  / current vertice is nearer to exploPos than center of bounds
                if( originToMeDir.sqrMagnitude < sqrDemRange ) //dot > 0.8f )
                {
                	float dist = Mathf.Clamp01(originToMeDir.sqrMagnitude/sqrDemRange);
                    float moveDelta = force * (1.0f-dist) * maxMoveDelta;

					Vector3 moveDir = Vector3.Slerp(originToMeDir, flatVertToCenterDir, impactDirManipulator).normalized * moveDelta;

                    verts[i] += Quaternion.Inverse(transform.rotation)*moveDir;
                }

        	}
       
        	meshfilters[j].mesh.vertices = verts;
        	meshfilters[j].mesh.RecalculateBounds();
        }
	} 
}
