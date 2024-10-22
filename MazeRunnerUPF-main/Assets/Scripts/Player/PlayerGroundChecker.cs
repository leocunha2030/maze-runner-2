using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"OnTriggerEnter = {other.name}");
        if (other.CompareTag("Platform"))
        {
            transform.parent.SetParent(other.transform);
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log($"OnTriggerExit = {other.name}");
        if (other.CompareTag("Platform"))
        {
            transform.parent.SetParent(null);
        }
    }
} 