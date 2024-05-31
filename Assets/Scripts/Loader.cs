using UnityEngine.SceneManagement;

public static class Loader
{

    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene

    }

    public static Scene targetSence;

    public static void Load(Scene targetScene)
    {
        Loader.targetSence = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());

    }

    public static void LoaderCallback()
    {

        SceneManager.LoadScene(targetSence.ToString());
    }
}
