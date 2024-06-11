/// <summary>
/// Trigger.
/// Ставить на триггер. Записка - дочерний объект у триггера
/// Уничтожает записку и прибавляет +1 к счету записок игрока
/// </summary>
using UnityEngine;

public class DestroyPapers : MonoBehaviour
{

    public GameObject Papers;
    public GameObject player;
    private bool trigger;
    private GUIPapers gp;

    void Awake()
    {
        Papers = GameObject.Find("Papers");
        player = GameObject.FindGameObjectWithTag("Player");
        gp = GameObject.Find("GUIPapers").GetComponent<GUIPapers>(); //инициализируем поле
    }

    void Update()
    {
        if (trigger)
        {
            if (gp != null)
            {
                gp.Papers++; // Увеличиваем количество собранных записок
                gp.TimeDown = 3;
                gp._visible = true;
            }
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = false;
        }
    }
}
