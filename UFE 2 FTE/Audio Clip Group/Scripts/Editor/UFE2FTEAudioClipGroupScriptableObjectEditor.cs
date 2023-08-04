#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UFE2FTE
{
	[CustomEditor(typeof(UFE2FTEAudioClipGroupScriptableObject))]
	public class UFE2FTEAudioClipGroupScriptableObjectEditor : Editor
	{
        [SerializeField]
		private List<AudioSource> audioClipGroupAudioSourceList;
		private List<AudioSource> GetAudioClipGroupAudioSourceList()
        {
			if (audioClipGroupAudioSourceList == null)
            {
				audioClipGroupAudioSourceList = new List<AudioSource>();
            }

			int count = audioClipGroupAudioSourceList.Count - 1;
			for (int i = count; i >= 0; i--)
			{
				if (audioClipGroupAudioSourceList[i] != null)
                {
					continue;
                }

				audioClipGroupAudioSourceList.RemoveAt(i);
			}

			return audioClipGroupAudioSourceList;
        }


		private void OnDisable()
		{
			DestroyAllInspectorAudioSources();
		}

		private void OnDestroy()
		{
			DestroyAllInspectorAudioSources();
		}

		private void SetInspectorAudioSourceList()
		{
			if (audioClipGroupOptionsArrayTotalAudioClips <= 0)
            {
				DestroyAllInspectorAudioSources();
            }
			else
            {
				for (int i = 0; i < audioClipGroupOptionsArrayTotalAudioClips; i++)
				{
					if (GetAudioClipGroupAudioSourceList().Count < audioClipGroupOptionsArrayTotalAudioClips)
					{
						AudioSource newAudioSource = EditorUtility.CreateGameObjectWithHideFlags("Audio Clip Group Audio Source " + GetAudioClipGroupAudioSourceList().Count, HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();

						newAudioSource.gameObject.AddComponent<UFE2FTEAudioClipGroupScriptableObjectInspectorAudioSource>();

						GetAudioClipGroupAudioSourceList().Add(newAudioSource);				
					}
					if (GetAudioClipGroupAudioSourceList().Count > audioClipGroupOptionsArrayTotalAudioClips)
					{
						int count = GetAudioClipGroupAudioSourceList().Count - 1;						
						for (int a = count; a >= audioClipGroupOptionsArrayTotalAudioClips; a--)
						{
							DestroyImmediate(audioClipGroupAudioSourceList[a].gameObject);
						}

						GetAudioClipGroupAudioSourceList();
					}
				}
			}
		}

		private void StopAllInspectorAudioSources()
		{
			int count = GetAudioClipGroupAudioSourceList().Count;
			for (int i = 0; i < count; i++)
			{
				audioClipGroupAudioSourceList[i].Stop();
			}
		}

		private void DestroyAllInspectorAudioSources()
        {
			int count = GetAudioClipGroupAudioSourceList().Count - 1;
			for (int i = count; i >= 0; i--)
			{
				DestroyImmediate(audioClipGroupAudioSourceList[i].gameObject);
			}

			GetAudioClipGroupAudioSourceList();
		}

		private void PlayAudioClipGroupOptions(UFE2FTEAudioClipGroupScriptableObject.AudioClipGroupOptions audioClipGroupOptions)
        {
			if (audioClipGroupOptions == null)
            {
				return;			
			}

			PlayAudioClipOptions(audioClipGroupOptions.audioClipOptionsArray);
        }

		private void PlayAudioClipGroupOptions(UFE2FTEAudioClipGroupScriptableObject.AudioClipGroupOptions[] audioClipGroupOptionsArray)
		{
			if (audioClipGroupOptionsArray == null)
			{
				return;		
			}

			int length = audioClipGroupOptionsArray.Length;
			for (int i = 0; i < length; i++)
			{
				PlayAudioClipOptions(audioClipGroupOptionsArray[i].audioClipOptionsArray);
			}
		}

		private void PlayAudioClipOptions(UFE2FTEAudioClipGroupScriptableObject.AudioClipOptions audioClipOptions)
        {
			if (audioClipOptions == null)
			{
				return;	
			}

			PlayAudioClip(
				audioClipOptions.audioClip,
			    audioClipOptions.ignoreListenerPause,
			    GetRandomVolumeUFE(audioClipOptions.minVolume, audioClipOptions.maxVolume),
			    GetRandomPitch(audioClipOptions.minPitch, audioClipOptions.maxPitch));
		}

		private void PlayAudioClipOptions(UFE2FTEAudioClipGroupScriptableObject.AudioClipOptions[] audioClipOptionsArray)
		{
			if (audioClipOptionsArray == null)
			{
				return;		
			}

			int length = audioClipOptionsArray.Length;
			for (int i = 0; i < length; i++)
			{
				PlayAudioClipOptions(audioClipOptionsArray[i]);
			}
		}

		private void PlayAudioClip(AudioClip audioClip, bool ignoreListenerPause, float volume, float pitch)
        {
			int count = GetAudioClipGroupAudioSourceList().Count;
			for (int i = 0; i < count; i++)
			{
				if (audioClipGroupAudioSourceList[i] == null
                    || audioClipGroupAudioSourceList[i].isPlaying == true)
                {
					continue;
                }

				audioClipGroupAudioSourceList[i].clip = audioClip;

				audioClipGroupAudioSourceList[i].ignoreListenerPause = ignoreListenerPause;

				audioClipGroupAudioSourceList[i].volume = volume;

				audioClipGroupAudioSourceList[i].pitch = pitch;

				audioClipGroupAudioSourceList[i].Play();

				break;
			}
		}

		private SerializedProperty audioClipGroupPlayMode;
		private SerializedProperty audioClipGroupOptionsArray;
		private int audioClipGroupOptionsArrayIndex = 0;
		private int audioClipGroupOptionsArrayTotalAudioClips = 0;

		public override void OnInspectorGUI()
		{
			//DrawDefaultInspector();

			GUIStyle defaultCenteredGUIStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };

			serializedObject.Update();

			if (audioClipGroupPlayMode == null)
			{
				audioClipGroupPlayMode = serializedObject.FindProperty("audioClipGroupPlayMode");
			}

			if (audioClipGroupPlayMode != null)
            {
				EditorGUILayout.PropertyField(audioClipGroupPlayMode);
			}

			if (audioClipGroupOptionsArray == null)
            {
				audioClipGroupOptionsArray = serializedObject.FindProperty("audioClipGroupOptionsArray");
			}

		    if (audioClipGroupOptionsArray != null)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Total Audio Clip Groups: " + audioClipGroupOptionsArray.arraySize, defaultCenteredGUIStyle);
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Total Audio Clips: " + audioClipGroupOptionsArrayTotalAudioClips, defaultCenteredGUIStyle);
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("Play Audio Clip Group Aescending"))
				{
					StopAllInspectorAudioSources();

					if (audioClipGroupOptionsArray.arraySize <= 0)
                    {
						return;
                    }

					if (audioClipGroupOptionsArrayIndex < 0)
					{
						audioClipGroupOptionsArrayIndex = audioClipGroupOptionsArray.arraySize - 1;
					}

					if (audioClipGroupOptionsArrayIndex > audioClipGroupOptionsArray.arraySize - 1)
					{
						audioClipGroupOptionsArrayIndex = 0;
					}

					PlayAudioClipGroupOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipGroupOptions)GetTargetObjectOfProperty(audioClipGroupOptionsArray.GetArrayElementAtIndex(audioClipGroupOptionsArrayIndex)));

					audioClipGroupOptionsArrayIndex--;
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Play Audio Clip Group Descending"))
                {
                    StopAllInspectorAudioSources();

                    if (audioClipGroupOptionsArray.arraySize <= 0)
                    {
						return;
                    }

					if (audioClipGroupOptionsArrayIndex < 0)
					{
						audioClipGroupOptionsArrayIndex = audioClipGroupOptionsArray.arraySize - 1;
					}

					if (audioClipGroupOptionsArrayIndex > audioClipGroupOptionsArray.arraySize - 1)
					{
						audioClipGroupOptionsArrayIndex = 0;
					}

					PlayAudioClipGroupOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipGroupOptions)GetTargetObjectOfProperty(audioClipGroupOptionsArray.GetArrayElementAtIndex(audioClipGroupOptionsArrayIndex)));

					audioClipGroupOptionsArrayIndex++;
				}
                EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("Play Audio Clip Group Random"))
				{
					StopAllInspectorAudioSources();

					if (audioClipGroupOptionsArray.arraySize <= 0)
                    {
						return;
                    }

					int randomAudioClipGroupOptionsArrayIndex = UnityEngine.Random.Range(0, audioClipGroupOptionsArray.arraySize);

					PlayAudioClipGroupOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipGroupOptions)GetTargetObjectOfProperty(audioClipGroupOptionsArray.GetArrayElementAtIndex(randomAudioClipGroupOptionsArrayIndex)));
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("Play Audio Clip Group Random With Exclusion"))
				{
					StopAllInspectorAudioSources();

					if (audioClipGroupOptionsArray.arraySize <= 0)
                    {
						return;
                    }

					int randomAudioClipGroupOptionsArrayIndex = RandomWithExclusion(0, audioClipGroupOptionsArray.arraySize);
			
					if (randomAudioClipGroupOptionsArrayIndex > audioClipGroupOptionsArray.arraySize - 1)
                    {
						randomAudioClipGroupOptionsArrayIndex = 0;
					}

					PlayAudioClipGroupOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipGroupOptions)GetTargetObjectOfProperty(audioClipGroupOptionsArray.GetArrayElementAtIndex(randomAudioClipGroupOptionsArrayIndex)));
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("Play All Audio Clip Groups: " + audioClipGroupOptionsArray.arraySize + " Audio Clips: " + audioClipGroupOptionsArrayTotalAudioClips))
				{
					StopAllInspectorAudioSources();

					if (audioClipGroupOptionsArray.arraySize <= 0)
                    {
						return;
                    }

					PlayAudioClipGroupOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipGroupOptions[])GetTargetObjectOfProperty(audioClipGroupOptionsArray));
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("Stop All Audio Clips: " + audioClipGroupOptionsArrayTotalAudioClips))
				{
					StopAllInspectorAudioSources();
				}
				EditorGUILayout.EndHorizontal();

				audioClipGroupOptionsArrayTotalAudioClips = 0;

				for (int i = 0; i < audioClipGroupOptionsArray.arraySize; i++)
				{
					int audioClipGroupOptionsArrayIndexedTotalAudioClips = 0;

					SerializedProperty audioClipOptionsArray = audioClipGroupOptionsArray.GetArrayElementAtIndex(i).FindPropertyRelative("audioClipOptionsArray");

					if (audioClipOptionsArray != null)
                    {
						for (int a = 0; a < audioClipOptionsArray.arraySize; a++)
                        {
							SerializedProperty audioClip = audioClipOptionsArray.GetArrayElementAtIndex(a).FindPropertyRelative("audioClip");

							int audioClipOptionsArrayIndexedTotalAudioClips = 0;

							if (audioClip != null)
                            {
								if (audioClip.objectReferenceValue != null)
								{
									audioClipGroupOptionsArrayTotalAudioClips++;

									audioClipGroupOptionsArrayIndexedTotalAudioClips++;
								}

								for (int b = 0; b < audioClipOptionsArray.arraySize; b++)
                                {
									if (audioClipOptionsArray.GetArrayElementAtIndex(b).FindPropertyRelative("audioClip").objectReferenceValue == null)
                                    {
										continue;
                                    }

									audioClipOptionsArrayIndexedTotalAudioClips++;
								}
                            }

							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("Audio Clip Group " + i + " Audio Clip " + a, defaultCenteredGUIStyle);
							EditorGUILayout.EndHorizontal();

							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField("Audio Clip Group " + i + " Total Audio Clips: " + audioClipOptionsArrayIndexedTotalAudioClips, defaultCenteredGUIStyle);
							EditorGUILayout.EndHorizontal();

							EditorGUILayout.PropertyField(audioClipOptionsArray.GetArrayElementAtIndex(a));
							audioClipOptionsArray.GetArrayElementAtIndex(a).isExpanded = true;

							EditorGUILayout.BeginHorizontal();
							if (GUILayout.Button("Play Audio Clip Group " + i +" Audio Clip " + a))
							{
								StopAllInspectorAudioSources();

								PlayAudioClipOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipOptions)GetTargetObjectOfProperty(audioClipOptionsArray.GetArrayElementAtIndex(a)));
							}
							EditorGUILayout.EndHorizontal();

							EditorGUILayout.BeginHorizontal();
							if (GUILayout.Button("Play All Audio Clip Group " + i + " Audio Clips: " + audioClipOptionsArrayIndexedTotalAudioClips))
							{
								StopAllInspectorAudioSources();

								PlayAudioClipOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipOptions[])GetTargetObjectOfProperty(audioClipOptionsArray));
							}
							EditorGUILayout.EndHorizontal();

							EditorGUILayout.BeginHorizontal();
							if (GUILayout.Button("Stop All Audio Clips: " + audioClipGroupOptionsArrayTotalAudioClips))
							{
								StopAllInspectorAudioSources();
							}
							EditorGUILayout.EndHorizontal();

							EditorGUILayout.BeginHorizontal();
							if (GUILayout.Button("Duplicate Group " + i + " Audio Clip " + a))
							{
								audioClipOptionsArray.InsertArrayElementAtIndex(a);
							}

							if (GUILayout.Button("Delete Group " + i + " Audio Clip " + a))
							{
								audioClipOptionsArray.DeleteArrayElementAtIndex(a);
							}
							EditorGUILayout.EndHorizontal();

							EditorGUILayout.BeginHorizontal();
							if (GUILayout.Button("Move Group " + i + " Audio Clip " + a + " Up"))
							{
								audioClipOptionsArray.MoveArrayElement(a, a - 1);
							}

							if (GUILayout.Button("Move Group " + i + " Audio Clip " + a + " Down"))
							{
								audioClipOptionsArray.MoveArrayElement(a, a + 1);
							}
							EditorGUILayout.EndHorizontal();
						}
                    }

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("Add Audio Clip Group " + i + " Audio Clip", GUILayout.Height(40)))
					{
						audioClipOptionsArray.arraySize += 1;
					}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("Audio Clip Group " + i, defaultCenteredGUIStyle);
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("Audio Clip Group " + i + " Total Audio Clips: " + audioClipGroupOptionsArrayIndexedTotalAudioClips, defaultCenteredGUIStyle);
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("Play All Audio Clip Group " + i + " Audio Clips: " + audioClipGroupOptionsArrayIndexedTotalAudioClips))
					{
						StopAllInspectorAudioSources();

						PlayAudioClipOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipOptions[])GetTargetObjectOfProperty(audioClipOptionsArray));
					}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("Play Audio Clip Group Aescending"))
					{
						StopAllInspectorAudioSources();

						if (audioClipGroupOptionsArray.arraySize <= 0)
                        {
							return;
                        }

						if (audioClipGroupOptionsArrayIndex < 0)
						{
							audioClipGroupOptionsArrayIndex = audioClipGroupOptionsArray.arraySize - 1;
						}

						if (audioClipGroupOptionsArrayIndex > audioClipGroupOptionsArray.arraySize - 1)
						{
							audioClipGroupOptionsArrayIndex = 0;
						}

						PlayAudioClipGroupOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipGroupOptions)GetTargetObjectOfProperty(audioClipGroupOptionsArray.GetArrayElementAtIndex(audioClipGroupOptionsArrayIndex)));

						audioClipGroupOptionsArrayIndex--;
					}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("Play Audio Clip Group Descending"))
					{
						StopAllInspectorAudioSources();

						if (audioClipGroupOptionsArray.arraySize <= 0)
                        {
							return;
                        }

						if (audioClipGroupOptionsArrayIndex < 0)
						{
							audioClipGroupOptionsArrayIndex = audioClipGroupOptionsArray.arraySize - 1;
						}

						if (audioClipGroupOptionsArrayIndex > audioClipGroupOptionsArray.arraySize - 1)
						{
							audioClipGroupOptionsArrayIndex = 0;
						}

						PlayAudioClipGroupOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipGroupOptions)GetTargetObjectOfProperty(audioClipGroupOptionsArray.GetArrayElementAtIndex(audioClipGroupOptionsArrayIndex)));

						audioClipGroupOptionsArrayIndex++;
					}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("Play Audio Clip Group Random"))
					{
						StopAllInspectorAudioSources();

						if (audioClipGroupOptionsArray.arraySize <= 0)
                        {
							return;
                        }

						int randomAudioClipGroupOptionsArrayIndex = UnityEngine.Random.Range(0, audioClipGroupOptionsArray.arraySize);

						PlayAudioClipGroupOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipGroupOptions)GetTargetObjectOfProperty(audioClipGroupOptionsArray.GetArrayElementAtIndex(randomAudioClipGroupOptionsArrayIndex)));
					}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("Play Audio Clip Group Random With Exclusion"))
					{
						StopAllInspectorAudioSources();

						if (audioClipGroupOptionsArray.arraySize <= 0)
                        {
							return;
                        }

						int randomAudioClipGroupOptionsArrayIndex = RandomWithExclusion(0, audioClipGroupOptionsArray.arraySize);

						if (randomAudioClipGroupOptionsArrayIndex > audioClipGroupOptionsArray.arraySize - 1)
						{
							randomAudioClipGroupOptionsArrayIndex = 0;
						}

						PlayAudioClipGroupOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipGroupOptions)GetTargetObjectOfProperty(audioClipGroupOptionsArray.GetArrayElementAtIndex(randomAudioClipGroupOptionsArrayIndex)));
					}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("Play All Audio Clip Groups: " + audioClipGroupOptionsArray.arraySize + " Audio Clips: " + audioClipGroupOptionsArrayTotalAudioClips))
					{
						StopAllInspectorAudioSources();

						if (audioClipGroupOptionsArray.arraySize <= 0)
                        {
							return;
                        }

						PlayAudioClipGroupOptions((UFE2FTEAudioClipGroupScriptableObject.AudioClipGroupOptions[])GetTargetObjectOfProperty(audioClipGroupOptionsArray));
					}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("Stop All Audio Clips: " + audioClipGroupOptionsArrayTotalAudioClips))
					{
						StopAllInspectorAudioSources();
					}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("Duplicate Audio Clip Group " + i))
					{
						audioClipGroupOptionsArray.InsertArrayElementAtIndex(i);
					}

					if (GUILayout.Button("Delete Audio Clip Group " + i))
					{
						audioClipGroupOptionsArray.DeleteArrayElementAtIndex(i);
					}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("Move Audio Clip Group " + i + " Up"))
					{
						audioClipGroupOptionsArray.MoveArrayElement(i, i - 1);
					}

					if (GUILayout.Button("Move Audio Clip Group " + i + " Down"))
					{
						audioClipGroupOptionsArray.MoveArrayElement(i, i + 1);
					}
					EditorGUILayout.EndHorizontal();
				}

				EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("Add Audio Clip Group " + audioClipGroupOptionsArray.arraySize, GUILayout.Height(40)))
				{
					audioClipGroupOptionsArray.arraySize += 1;
				}
				EditorGUILayout.EndHorizontal();
			}

			serializedObject.ApplyModifiedProperties();

			SetInspectorAudioSourceList();
		}

		private static float GetRandomVolumeUFE(float minVolume, float maxVolume)
		{
			if (maxVolume > UFE.GetSoundFXVolume())
			{
				float volumeDifference = minVolume - maxVolume;

				minVolume = UFE.GetSoundFXVolume() + volumeDifference;

				maxVolume = UFE.GetSoundFXVolume();
			}

			return UnityEngine.Random.Range(minVolume, maxVolume);
		}

		private static float GetRandomPitch(float minPitch, float maxPitch)
		{
			return UnityEngine.Random.Range(minPitch, maxPitch);
		}

		int excludeLastRandNum;
		bool firstRun = true;
		int RandomWithExclusion(int min, int max)
		{
			int result;
			//Don't exclude if this is first run.
			if (firstRun)
			{
				//Generate normal random number
				result = UnityEngine.Random.Range(min, max);
				excludeLastRandNum = result;
				firstRun = false;
				return result;
			}

			//Not first run, exclude last random number with -1 on the max
			result = UnityEngine.Random.Range(min, max - 1);
			//Apply +1 to the result to cancel out that -1 depending on the if statement
			result = (result < excludeLastRandNum) ? result : result + 1;
			excludeLastRandNum = result;
			return result;
		}

		/// <summary>
		/// Gets the object the property represents.
		/// </summary>
		/// <param name="prop"></param>
		/// <returns></returns>
		private static object GetTargetObjectOfProperty(UnityEditor.SerializedProperty prop)
		{
			var path = prop.propertyPath.Replace(".Array.data[", "[");
			object obj = prop.serializedObject.targetObject;
			var elements = path.Split('.');
			foreach (var element in elements)
			{
				if (element.Contains("["))
				{
					var elementName = element.Substring(0, element.IndexOf("["));
					var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
					obj = GetValue_Imp(obj, elementName, index);
				}
				else
				{
					obj = GetValue_Imp(obj, element);
				}
			}
			return obj;
		}

		private static object GetValue_Imp(object source, string name)
		{
			if (source == null)
				return null;
			var type = source.GetType();

			while (type != null)
			{
				var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
				if (f != null)
					return f.GetValue(source);

				var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
				if (p != null)
					return p.GetValue(source, null);

				type = type.BaseType;
			}
			return null;
		}

		private static object GetValue_Imp(object source, string name, int index)
		{
			var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
			if (enumerable == null) return null;
			var enm = enumerable.GetEnumerator();
			//while (index-- >= 0)
			//    enm.MoveNext();
			//return enm.Current;

			for (int i = 0; i <= index; i++)
			{
				if (!enm.MoveNext()) return null;
			}
			return enm.Current;
		}
	}
}
#endif