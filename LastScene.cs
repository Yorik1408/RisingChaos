using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastScene : MonoBehaviour
{
    public Image[] images;
    private int currentIndex = 0;

    void Start()
    {
        // �������� ��� ����������� ����� �������
        for (int i = 1; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // ����������� ����������� ��� ������� �������
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
                // ���� �������� ��������� �����������, ��������� ����� MainGame
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}