using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;
[InitializeOnLoad]
public class SaveOnPlay
{
     static SaveOnPlay() {
        EditorApplication.playmodeStateChanged = () => {
            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                if (EditorSceneManager.GetActiveScene().isDirty) {
                    Debug.Log("Auto-Save Scene");
                    AssetDatabase.SaveAssets();
                    EditorSceneManager.SaveOpenScenes();
                 //   EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
            
            }
        };
    }
}
