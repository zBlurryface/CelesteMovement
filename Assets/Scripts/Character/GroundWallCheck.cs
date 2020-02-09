using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWallCheck : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool onWall;

    [Space]

    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    [Header("Collision")]
    public float bottonSize;
    public float sideSize;

    [Space] 
    
    [Header("Originds")]
    public Vector2 bottonOrigin;
    public Vector2 bottonOrigin2;
    
    public Vector2 sideOrigin;
    public Vector2 sideOrigin2;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var charPosition = (Vector2)transform.position;
        onGround = Physics2D.Raycast(charPosition + bottonOrigin, Vector2.down, bottonSize, groundLayer) 
                   || Physics2D.Raycast( charPosition + bottonOrigin2, Vector2.down, bottonSize, groundLayer);
        onWall = Physics2D.Raycast(charPosition +  sideOrigin, Vector2.right, sideSize, groundLayer) 
                   || Physics2D.Raycast(charPosition +  sideOrigin2, Vector2.right, sideSize, groundLayer) 
                   || Physics2D.Raycast(charPosition +  sideOrigin, Vector2.left, sideSize, groundLayer) 
                   || Physics2D.Raycast(charPosition +  sideOrigin2, Vector2.left, sideSize, groundLayer);

        
        onRightWall = Physics2D.Raycast(charPosition + sideOrigin, Vector2.right, sideSize, groundLayer)
                      || Physics2D.Raycast(charPosition + sideOrigin2, Vector2.right, sideSize, groundLayer);
        onLeftWall = Physics2D.Raycast(charPosition +  sideOrigin, Vector2.left, sideSize, groundLayer) 
                      || Physics2D.Raycast(charPosition +  sideOrigin2, Vector2.left, sideSize, groundLayer);

        
        
        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawRay((Vector2)transform.position + bottonOrigin, Vector3.down * bottonSize);
        Gizmos.DrawRay((Vector2)transform.position + bottonOrigin2, Vector3.down * bottonSize);

        Gizmos.DrawRay((Vector2)transform.position + sideOrigin, Vector3.right * sideSize);
        Gizmos.DrawRay((Vector2)transform.position + sideOrigin2, Vector3.right * sideSize);

        Gizmos.DrawRay((Vector2)transform.position + sideOrigin, Vector3.left * sideSize);
        Gizmos.DrawRay((Vector2)transform.position + sideOrigin2, Vector3.left * sideSize);

    }








}
