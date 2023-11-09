using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace UFE2FTE
{
	public class TMProFontReplacer : EditorWindow
	{
		private const string EditorPrefsKey = "Utilities.TMProFontReplacer";
		private const string MenuItemName = "Utilities/Replace TMProFonts...";

		private TMP_FontAsset _src;
		private TMP_FontAsset _dest;
		private bool _includePrefabs;

		[MenuItem(MenuItemName)]
		public static void DisplayWindow()
		{
			var window = GetWindow<TMProFontReplacer>(true, "Replace TMProFonts");
			var position = window.position;
			position.size = new Vector2(position.size.x, 151);
			position.center = new Rect(0f, 0f, Screen.currentResolution.width, Screen.currentResolution.height).center;
			window.position = position;
			window.Show();
		}

		public void OnEnable()
		{
			var path = EditorPrefs.GetString(EditorPrefsKey + ".src");
			if (path != string.Empty)
				_src = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(path) ?? Resources.GetBuiltinResource<TMP_FontAsset>(path);

			path = EditorPrefs.GetString(EditorPrefsKey + ".dest");
			if (path != string.Empty)
				_dest = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(path) ?? Resources.GetBuiltinResource<TMP_FontAsset>(path);

			_includePrefabs = EditorPrefs.GetBool(EditorPrefsKey + ".includePrefabs", false);
		}

		public void OnGUI()
		{
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PrefixLabel("Find:");
			_src = (TMP_FontAsset)EditorGUILayout.ObjectField(_src, typeof(TMP_FontAsset), false);

			EditorGUILayout.Space();
			EditorGUILayout.PrefixLabel("Replace with:");
			_dest = (TMP_FontAsset)EditorGUILayout.ObjectField(_dest, typeof(TMP_FontAsset), false);

			EditorGUILayout.Space();
			_includePrefabs = EditorGUILayout.ToggleLeft("Include Prefabs", _includePrefabs);
			if (EditorGUI.EndChangeCheck())
			{
				EditorPrefs.SetString(EditorPrefsKey + ".src", GetAssetPath(_src, "ttf"));
				EditorPrefs.SetString(EditorPrefsKey + ".dest", GetAssetPath(_dest, "ttf"));
				EditorPrefs.SetBool(EditorPrefsKey + ".includePrefabs", _includePrefabs);
			}

			GUI.color = Color.green;
			if (GUILayout.Button("Replace All", GUILayout.Height(EditorGUIUtility.singleLineHeight * 2f)))
			{
				ReplaceFonts(_src, _dest, _includePrefabs);
			}
			GUI.color = Color.white;
		}

		private static void ReplaceFonts(TMP_FontAsset src, TMP_FontAsset dest, bool includePrefabs)
		{
			var prefabMatches = 0;
			if (includePrefabs)
			{
				var prefabs =
					AssetDatabase.FindAssets("t:Prefab")
						.Select(guid => AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid)));
				foreach (var prefab in prefabs)
				{
					prefabMatches += ReplaceFonts(src, dest, prefab.GetComponentsInChildren<TextMeshProUGUI>(true));
					UnityEditor.EditorUtility.SetDirty(prefab);
				}
			}

			var sceneMatches = 0;
			for (var i = 0; i < SceneManager.sceneCount; i++)
			{
				var scene = SceneManager.GetSceneAt(i);
				var gos = new List<GameObject>(scene.GetRootGameObjects());
				foreach (var go in gos)
				{
					sceneMatches += ReplaceFonts(src, dest, go.GetComponentsInChildren<TextMeshProUGUI>(true));
				}
			}
			UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();

			if (includePrefabs)
			{
				Debug.LogFormat("Replaced {0} font(s), {1} in scenes, {2} in prefabs", sceneMatches + prefabMatches, sceneMatches, prefabMatches);
			}
			else
			{
				Debug.LogFormat("Replaced {0} font(s) in scenes", sceneMatches);
			}
		}

		private static int ReplaceFonts(TMP_FontAsset src, TMP_FontAsset dest, IEnumerable<TextMeshProUGUI> texts)
		{
			var matches = 0;
			var textsFiltered = src != null ? texts.Where(text => text.font == src) : texts;
			foreach (var text in textsFiltered)
			{
				text.font = dest;
				matches++;
			}
			return matches;
		}

		private static string GetAssetPath(Object assetObject, string defaultExtension)
		{
			var path = AssetDatabase.GetAssetPath(assetObject);
			if (path.StartsWith("Library/", System.StringComparison.InvariantCultureIgnoreCase))
				path = assetObject.name + "." + defaultExtension;
			return path;
		}
	}
}