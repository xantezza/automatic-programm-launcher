using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

namespace Editor
{
    public class PopupCreator : EditorWindow
    {
        public const string addressablesGroupName = "PopupCreating";
        public const string popupBasePrefabName = "PopupBase";
        public const string popupsRootPrefabName = "Popups";

        [MenuItem("Utils/UI/Popups/CreatePopup")]
        private static void ShowWindow()
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            var group = settings.FindGroup(x => x.Name == addressablesGroupName);

            var popupBase = (GameObject)group.entries.First(x => x.MainAsset.name == popupBasePrefabName).MainAsset;
            var popupsRoot = (GameObject)group.entries.First(x => x.MainAsset.name == popupsRootPrefabName).MainAsset;

            var popupBaseInstance = PrefabUtility.InstantiatePrefab(popupBase) as GameObject;
            var popupsRootInstance = PrefabUtility.InstantiatePrefab(popupsRoot) as GameObject;

            popupBaseInstance.transform.parent = popupsRootInstance.transform;
            PrefabUtility.UnpackPrefabInstance(popupBaseInstance, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            PrefabUtility.ApplyPrefabInstance(popupsRootInstance, InteractionMode.AutomatedAction);
            AssetDatabase.SaveAssets();
            DestroyImmediate(popupsRootInstance);
        }
    }
}