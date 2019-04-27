using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class switchScene : MonoBehaviour, IPointerClickHandler
{
    public int scene;
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(scene);
        GameManager.setMainMenu();
    }
}
