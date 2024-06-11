using UnityEngine;

public class ScreamerActive : MonoBehaviour
{
    public GameObject monstr;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monstr.SetActive(true);
        }
    }
}
