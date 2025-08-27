using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBaiVuaHoc : MonoBehaviour
{
    
    [SerializeField]
    private float raycasteDistance = 2f;
    public LayerMask LayerMask;

    private void Update()
    {
        Vector2 positionRaycaste = transform.position; 
        Vector2 directionOfRaycaste = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(positionRaycaste, directionOfRaycaste, raycasteDistance, LayerMask);

        if (hit.collider != null)
        {
            Debug.Log("da cham san");
        }
        else {
            Debug.Log("chua cham san");
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * raycasteDistance);
    }
}
