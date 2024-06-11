using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastScene : MonoBehaviour
{
    public Image[] images;
    private int currentIndex = 0;

    void Start()
    {
        // Скрываем все изображения кроме первого
        for (int i = 1; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Переключаем изображения при нажатии клавиши
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentIndex < images.Length - 1)
            {
                images[currentIndex].gameObject.SetActive(false);
                currentIndex++;
                images[currentIndex].gameObject.SetActive(true);
            }
            else
            {
                // Если показано последнее изображение, загружаем сцену MainGame
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}