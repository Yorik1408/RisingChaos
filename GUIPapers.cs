/// <summary>
/// GUI papers.
/// Ставить на пустой игровой объект
/// Выводит на экран количество собранных записок и сообщение о победе
/// </summary>
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GUIPapers : MonoBehaviour
{

    public int Papers; // Количество записок
    public int TimeDown; // Время исчезновения надписи
    private float Timer; // Таймер
    public bool _visible; // Вывод количества записок
    private bool win; // Вывод победы
    private bool waitingForMainMenu = false; // Флаг ожидания перехода в главное меню

    // Use this for initialization
    void Start()
    {
        Papers = 0;
        _visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeDown != 0)
        {
            Timer = (float)TimeDown;
            TimeDown = 0;
        }

        // Таймер
        if (Timer > 0)
            Timer -= Time.deltaTime;

        // На всякий случай обнуляем результат
        if (Timer < 0)
            Timer = 0;

        // Если пауза выдержана, то убираем надпись
        if (Timer == 0)
        {
            _visible = false;
        }

        // Проверяем, если собрано все записки
        if (Papers == 10)
        {
            // Помечаем победу
            win = true;
            // Убираем надпись
            _visible = false;
            // Запускаем задержку перед переходом в главное меню
            if (!waitingForMainMenu)
            {
                StartCoroutine(WaitAndLoadMainMenu());
            }
        }
    }

    // Задержка перед переходом в главное меню
    IEnumerator WaitAndLoadMainMenu()
    {
        waitingForMainMenu = true;
        yield return new WaitForSeconds(3f); // Задержка в 3 секунды (можете изменить значение по желанию)
        LoadMainMenu(); // Функция для загрузки главного меню
    }

    // Функция для загрузки главного меню
    void LoadMainMenu()
    {
        // Пример кода для загрузки главного меню
        SceneManager.LoadScene("LastScene"); // Предполагается, что вы используете Unity SceneManager для загрузки сцен
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 25; // Устанавливаем размер шрифта для всех надписей
        GUI.skin.label.normal.textColor = Color.red; // Устанавливаем красный цвет для всех надписей

        if (_visible)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 3 - 50, 180, 30), "Papers       /10");
            GUI.Label(new Rect(Screen.width / 2 + 100, Screen.height / 3 - 50, 180, 30), Papers.ToString()); // Увеличиваем отступ для цифры
        }

        if (win)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 3 - 100, 200, 100), "Last note picked up", new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 50,
                normal = { textColor = Color.red }
            });
        }
    }
}
