
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] private string _loadScene; //シーン名を記述

    public void SceneChange()
    {
        SceneManager.LoadScene(_loadScene);
    }
}
