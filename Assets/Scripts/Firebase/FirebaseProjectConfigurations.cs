using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ProjectBuildType
{
    Debug,
    Emulator,
    Release
}

public class FirebaseProjectConfigurations : MonoBehaviour
{
    public ProjectBuildType Build;
    public int RealtimeDatabasePort = 9001;
    public int CloudFunctionsPort = 5001;
    public int AuthPort = 9099;
    public string NextSceneToLoad;

    public static ProjectBuildType PROJECT_BUILD { get => projectBuild; }
    public static bool Initialized { get => initialized; }
    public static string PROJECT_API_KEY { get => GetProjectApiKey(); }
    public static string PROJECT_ID { get => GetProjectId(); }
    public static string REALTIME_DATABASE_ROOT_PATH { get => GetRealtimeDatabaseRootPath(); }
    public static string PLATFORM_DATABASE_ROOT_PATH { get => GetPlatformDatabaseRootPath(); }

    public static int REALTIME_DATABASE_PORT { get => realtimeDatabasePort; }
    public static int CLOUD_FUNCTIONS_PORT { get => cloudFunctionsPort; }
    public static int AUTH_PORT { get => authPort; }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        projectBuild = Build;

        realtimeDatabasePort = RealtimeDatabasePort;
        cloudFunctionsPort = CloudFunctionsPort;
        authPort = AuthPort;

        initialized = true;


        if (!string.IsNullOrEmpty(NextSceneToLoad))
            UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneToLoad);
    }

    public static string GetDatabaseUrlByPath(string path)
    {
        switch (projectBuild)
        {
            case ProjectBuildType.Debug:
                return realtime_database_root_path_release + "/" + path + ".json";
            case ProjectBuildType.Release:
                return realtime_database_root_path_release + "/" + path + ".json";
            case ProjectBuildType.Emulator:
                return platform_database_root_path_emulator + "/" + path + ".json" + "?ns=" + project_id_emulator;
            default:
                return null;
        }
    }

    private static ProjectBuildType projectBuild = ProjectBuildType.Debug;
    private static bool initialized = false;
    private static int realtimeDatabasePort = 9000;
    private static int cloudFunctionsPort = 5001;
    private static int authPort = 9099;

    private const string project_api_key_release = "<Your-Api-Key>"; //enter your project api key
    private const string project_api_key_debug = "<Your-Api-Key>";
    private const string project_api_key_emulator = "project_api_key";

    private const string project_id_release = "<Your-Project-Id>"; //enter your project id
    private const string project_id_debug = "<Your-Project-Id>";
    private const string project_id_emulator = "dzgamesdebugproject";

    private const string realtime_database_root_path_release = "<Database-URL>"; //enter your project database root url
    private const string realtime_database_root_path_debug = "<Database-URL>";
    private const string realtime_database_root_path_emulator = "http://localhost:9000/battleofminds";

    private const string platform_database_root_path_release = "<Database-URL>"; //enter your project database root url
    private const string platform_database_root_path_debug = "<Database-URL>";
    private const string platform_database_root_path_emulator = "http://localhost:9000";
 
    private static string GetProjectApiKey()
    {
        switch (projectBuild)
        {
            case ProjectBuildType.Debug:
                return project_api_key_debug;
            case ProjectBuildType.Release:
                return project_api_key_release;
            case ProjectBuildType.Emulator:
                return project_api_key_emulator;
            default:
                return null;
        }
    }
    private static string GetProjectId()
    {
        switch (projectBuild)
        {
            case ProjectBuildType.Debug:
                return project_id_debug;
            case ProjectBuildType.Release:
                return project_id_release;
            case ProjectBuildType.Emulator:
                return project_id_emulator;
            default:
                return null;
        }
    }
    private static string GetRealtimeDatabaseRootPath()
    {
        switch (projectBuild)
        {
            case ProjectBuildType.Debug:
                return realtime_database_root_path_debug;
            case ProjectBuildType.Release:
                return realtime_database_root_path_release;
            case ProjectBuildType.Emulator:
                return $"http://localhost:{realtimeDatabasePort}/";
            default:
                return null;
        }
    }
    private static string GetPlatformDatabaseRootPath()
    {
        switch (projectBuild)
        {
            case ProjectBuildType.Debug:
                return platform_database_root_path_debug;
            case ProjectBuildType.Release:
                return platform_database_root_path_release;
            case ProjectBuildType.Emulator:
                return platform_database_root_path_emulator;
            default:
                return null;
        }
    }

}
