using System;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static event EventHandler OnLoadNewScen;

    public enum Scene
    {
        MainMenuScene,
        Level_01,
        Level_02,
        Level_03,
        Level_04,
        Level_05,
        Level_06,
        Level_07,
        Level_08,
        Level_09,
        Level_10,
        LoadingScene,
        GameUI,
        GameCamera,
        WinScene,
    }

    private static Scene _targetScene;

    public static void Load(Scene targetScene)
    {
        _targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadSceneAsync(_targetScene.ToString());

        if (_targetScene != Scene.MainMenuScene && _targetScene != Scene.WinScene)
        {
            SceneManager.LoadSceneAsync(Scene.GameUI.ToString(), LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(Scene.GameCamera.ToString(), LoadSceneMode.Additive);
        }

        OnLoadNewScen?.Invoke(null, EventArgs.Empty);
    }

    public static Scene GetCurrentTargetScene()
    {
        return _targetScene;
    }
}
