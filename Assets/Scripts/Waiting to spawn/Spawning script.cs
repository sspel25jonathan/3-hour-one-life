using UnityEngine;

public class Spawningscript : MonoBehaviour
{
    public int playerCount = 0;
    Object playerPrefab;
    

    
    void Update()
    {
        
    }

    void AdamAndEveSpawn()
    {
       if (playerCount == 0)
       {
           Instantiate(playerPrefab, transform.position, transform.rotation);
       }
       else if (playerCount == 1)
       {
           
       }
    }
}
