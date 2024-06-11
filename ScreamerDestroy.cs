using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreamerDestroy : MonoBehaviour
{
    public GameObject monstr;
    public float delay = 5f; // Задержка перед переходом на MainMenu

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monstr.SetActive(false);
            Invoke("LoadMainMenu", delay); // Вызываем функцию через delay секунд
        }
    }

    void LoadMainMenu()
    {
        // Разблокируем курсор перед загрузкой MainMenu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu"); // Загружаем MainMenu
    }
}
