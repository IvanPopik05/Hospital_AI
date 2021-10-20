//using BestHTTP;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Networking;
//using UnityEngine.UI;

//public class FirebaseEmulator : MonoBehaviour
//{
//    public string FirebaseGameName = "battleofminds";
//    public string NextSceneToLoad = "DataLoadingScreen";
//    public GameObject AuthView;
//    public Text logText;

//    private int registerdUsersCount = 0;
//    private int usersCount = 2;

   

//    public void SignAsUser(int id)
//    {
//        if(id == 1)
//            FirebaseManager.Instance.Auth.SetLocalId("fyx1hktpSHRmwkPLEylnAnIXuOK2");
//        else
//            FirebaseManager.Instance.Auth.SetLocalId("DS8I8LYxqoP5XumyhHcvyw7YTfV2");

//        GoToNextScene();
//    }

//    private void Awake()
//    {
//        if (FirebaseProjectConfigurations.PROJECT_BUILD != ProjectBuildType.Emulator)
//            Destroy(gameObject);
//    }

//    private IEnumerator Start()
//    {
//        Debug.Log("Start of emulator");
//        if (!AuthView.activeInHierarchy)
//            AuthView.SetActive(true);

//        PlayerPrefs.DeleteAll();

//        while (!(FirebaseManager.Instance.AllManagersInitialized || FirebaseProjectConfigurations.Initialized))
//            yield return null;

//        //edit. Add registration and auth in updated project
//        /* Creating first user data */
//        CreateNewUserInPlatform("fyx1hktpSHRmwkPLEylnAnIXuOK2", "User 1", "Surname", 2, (user) =>
//        {
//            CreateNewUserGameData(user, () =>
//            {
//                Debug.Log($"Success creating user {user.Id} game data");
//            }, () =>
//            {
//                Debug.LogError($"Failed creating user {user.Id} game data");
//            });

//            registerdUsersCount++;
//        }, (e) => { Debug.LogError($"Failed create user. Message: {e.Message}"); });


//        /* Creating second user data */
//        CreateNewUserInPlatform("DS8I8LYxqoP5XumyhHcvyw7YTfV2", "User 2", "Surname", 2, (user) =>
//        {
//            CreateNewUserGameData(user, () =>
//            {
//                Debug.Log($"Success creating user {user.Id} game data");
//            }, () =>
//            {
//                Debug.LogError($"Failed creating user {user.Id} game data");
//            });

//            registerdUsersCount++;
//        }, (e) => { Debug.LogError($"Failed create user. Message: {e.Message}"); });

//        CreateTestUsers(8);

//        while (registerdUsersCount < usersCount)
//            yield return null;

//        logText.text += "\r\nUsers created";
//    }
//    private void CreateNewUserInPlatform(string userId, string name, string surname, int @class, Action<PlatformUserData> onReady, Action<Exception> onFailed)
//    {
//        PlatformUserData platformUserData = new PlatformUserData(userId, name, surname, $"class{@class}", null);

//        FirebaseManager.Instance.Database.GetObject<PlatformUserData>($"{FirebaseProjectConfigurations.PLATFORM_DATABASE_ROOT_PATH}", $"allUsers/{userId}", (data) =>
//        {
//            if (data == null)
//            {
//                //create in platform allUsers
//                FirebaseManager.Instance.Functions.CallCloudFunctionPostObject("CreateNewUserInPlatform", platformUserData, null, (statusCode) => onReady(platformUserData), onFailed);
//            }
//            else
//            {
//                //don't create in platform. already exists
//                onReady(platformUserData);
//            }
//        }, (exception) =>
//        {
//            Debug.LogError($"Exception while downloading user {userId} platform data. Message - {exception.Message}");
//            GameAnalyticsSDK.GameAnalytics.NewErrorEvent(GameAnalyticsSDK.GAErrorSeverity.Error, $"Exception while downloading user {userId} platform data. Message - {exception.Message}");
//            onFailed(exception);
//        });

//    }
//    private void CreateNewUserGameData(PlatformUserData pud, Action onCreated, Action onFailed)
//    {
//        FirebaseManager.Instance.Database.GetJson($"users/{pud.Id}", (data) =>
//        {
//            if (data == null || data == "null")
//            {
//                Dictionary<string, string> args = new Dictionary<string, string>() { { "game", FirebaseGameName } };
//                FirebaseManager.Instance.Functions.CallCloudFunctionPostObject<PlatformUserData>("CreateNewUserGameData", pud, args, (statusCode) => onCreated(), (exception) => onFailed());
//            }
//        }, (exception) =>
//        {
//            Debug.LogError($"Exception while downloading user {pud.Id} game data. Message - {exception.Message}");
//            GameAnalyticsSDK.GameAnalytics.NewErrorEvent(GameAnalyticsSDK.GAErrorSeverity.Error, $"Exception while downloading user {pud.Id} game data. Message - {exception.Message}");
//            onFailed();
//        });
//    }

//    //edit. update
//    private void CreateTestUsers(int count)
//    {
//        for (int i = 3; i < 3+count; i++)
//        {
//            CreateNewUserInPlatform("TestUser"+i, "User "+i, "Surname", 2, (user) =>
//            {
//                CreateNewUserGameData(user, () =>
//                {
//                    Debug.Log($"Success creating user {user.Id} game data");
//                }, () =>
//                {
//                    Debug.LogError($"Failed creating user {user.Id} game data");
//                });

//            }, (e) => { Debug.LogError($"Failed create user. Message: {e.Message}"); });
//        }
//    }
//    private void GoToNextScene()
//    {
//        UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneToLoad);
//    }
//}

//public class DZGamesPlatformUser
//{
//    public string userId;
//    public string name;
//    public string surname;
//    public int userClass;

//    public DZGamesPlatformUser(string userId, string name, string surname, int userClass)
//    {
//        this.userId = userId;
//        this.name = name;
//        this.surname = surname;
//        this.userClass = userClass;
//    }
//}
