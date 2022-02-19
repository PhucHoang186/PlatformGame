using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            if( i+1 > levelReached)
            {
                buttons[i].interactable = false;
            }
        }
    }

}
