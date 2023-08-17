using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UFE3D;
using FPLibrary;

namespace UFE2FTE
{
    public static class UFE2FTEHelperMethodsManager
    {
        #region Audio Methods

        public static float GetRandomVolumeUFE(float minVolume, float maxVolume)
        {
            if (maxVolume > UFE.GetSoundFXVolume())
            {
                float volumeDifference = minVolume - maxVolume;

                minVolume = UFE.GetSoundFXVolume() + volumeDifference;

                maxVolume = UFE.GetSoundFXVolume();
            }

            return UnityEngine.Random.Range(minVolume, maxVolume);
        }

        public static float GetRandomPitch(float minPitch, float maxPitch)
        {
            return UnityEngine.Random.Range(minPitch, maxPitch);
        }

        public static IEnumerator StartAudioSourceFade(AudioSource audioSource, float duration, float targetVolume)
        {
            if (audioSource != null)
            {
                float currentTime = 0;

                float start = audioSource.volume;

                while (currentTime < duration)
                {
                    currentTime += (float)UFE.fixedDeltaTime;

                    if (audioSource != null)
                    {
                        audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                    }

                    yield return null;
                }

                yield break;
            }
        }

        //StartCoroutine(UFE2FTEHelperMethodsManager.StartAudioSourceFade(AudioSource audioSource, float duration, float targetVolume));

        public static IEnumerator StartAudioMixerFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
        {
            if (audioMixer != null)
            {
                float currentTime = 0;

                float currentVol;

                audioMixer.GetFloat(exposedParam, out currentVol);

                currentVol = Mathf.Pow(10, currentVol / 20);

                float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

                while (currentTime < duration)
                {
                    currentTime += (float)UFE.fixedDeltaTime;

                    float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);

                    if (audioMixer != null)
                    {
                        audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
                    }

                    yield return null;
                }

                yield break;
            }
        }

        //StartCoroutine(UFE2FTEHelperMethodsManager.StartAudioMixerFade(AudioMixer audioMixer, string exposedParameter, float duration, float targetVolume));

        #endregion

        #region Button Press Methods

        public static ButtonPress GetButtonPressFromBlockType(BlockType blockType)
        {
            switch (blockType)
            {
                case BlockType.None: return ButtonPress.Back;

                case BlockType.HoldBack: return ButtonPress.Back;

                case BlockType.HoldButton1: return ButtonPress.Button1;

                case BlockType.HoldButton2: return ButtonPress.Button2;

                case BlockType.HoldButton3: return ButtonPress.Button3;

                case BlockType.HoldButton4: return ButtonPress.Button4;

                case BlockType.HoldButton5: return ButtonPress.Button5;

                case BlockType.HoldButton6: return ButtonPress.Button6;

                case BlockType.HoldButton7: return ButtonPress.Button7;

                case BlockType.HoldButton8: return ButtonPress.Button8;

                case BlockType.HoldButton9: return ButtonPress.Button9;

                case BlockType.HoldButton10: return ButtonPress.Button10;

                case BlockType.HoldButton11: return ButtonPress.Button11;

                case BlockType.HoldButton12: return ButtonPress.Button12;

                default: return ButtonPress.Back;
            }
        }

        #endregion

        #region Character Info Methods

        public static UFE3D.CharacterInfo GetCharacterInfoFromGlobalInfo(string characterName)
        {
            if (UFE.config == null)
            {
                return null;
            }

            int length = UFE.config.characters.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterName != UFE.config.characters[i].characterName)
                {
                    continue;
                }

                return UFE.config.characters[i];
            }

            return null;
        }

        #endregion

        #region GameObject Methods

        public static void SetGameObjectActive(GameObject gameObject, bool active)
        {
            if (gameObject == null)
            {
                return;
            }
        
            gameObject.SetActive(active);  
        }

        public static void SetGameObjectActive(GameObject[] gameObjectArray, bool active)
        {
            if (gameObjectArray == null)
            {
                return;
            }

            int length = gameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                SetGameObjectActive(gameObjectArray[i], active);
            }
        }

        public static void SpawnNetworkGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            UFE.SpawnGameObject(gameObject, Vector3.zero, Quaternion.identity, true, 0);   
        }

        public static void SpawnNetworkGameObject(GameObject[] gameObjectArray)
        {
            if (gameObjectArray == null)
            {
                return;
            }

            int length = gameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                SpawnNetworkGameObject(gameObjectArray[i]);
            }
        }

        #endregion

        #region Gauge Methods

        public static void SetLifePointsPercent(ControlsScript player, Fix64 percent)
        {
            if (player == null)
            {
                return;
            }

            player.currentLifePoints = player.myInfo.lifePoints * (percent / 100);

            if (player.currentLifePoints > player.myInfo.lifePoints)
            {
                player.currentLifePoints = player.myInfo.lifePoints;
            }
            else if (player.currentLifePoints < 0)
            {
                player.currentLifePoints = 0;
            }
        }

        public static void AddLifePointsPercent(ControlsScript player, Fix64 percent)
        {
            if (player == null)
            {
                return;
            }

            player.currentLifePoints += player.myInfo.lifePoints * (percent / 100);

            if (player.currentLifePoints > player.myInfo.lifePoints)
            {
                player.currentLifePoints = player.myInfo.lifePoints;
            }
            else if (player.currentLifePoints < 0)
            {
                player.currentLifePoints = 0;
            }
        }

        public static void SetGaugePointsPercent(ControlsScript player, GaugeId gaugeId, Fix64 percent)
        {
            if (player == null)
            {
                return;
            }

            player.currentGaugesPoints[(int)gaugeId] = player.myInfo.maxGaugePoints * (percent / 100);

            if (player.currentGaugesPoints[(int)gaugeId] > player.myInfo.maxGaugePoints)
            {
                player.currentGaugesPoints[(int)gaugeId] = player.myInfo.maxGaugePoints;
            }
            else if (player.currentGaugesPoints[(int)gaugeId] < 0)
            {
                player.currentGaugesPoints[(int)gaugeId] = 0;
            }
        }

        public static void AddGaugePointsPercent(ControlsScript player, GaugeId gaugeId, Fix64 percent)
        {
            if (player == null)
            {
                return;
            }

            player.currentGaugesPoints[(int)gaugeId] += player.myInfo.maxGaugePoints * (percent / 100);

            if (player.currentGaugesPoints[(int)gaugeId] > player.myInfo.maxGaugePoints)
            {
                player.currentGaugesPoints[(int)gaugeId] = player.myInfo.maxGaugePoints;
            }
            else if (player.currentGaugesPoints[(int)gaugeId] < 0)
            {
                player.currentGaugesPoints[(int)gaugeId] = 0;
            }
        }

        #endregion

        #region Hit Pause Methods

        public static void HitPause(ControlsScript player, Fix64 freezingTime)
        {
            if (player == null)
            {
                return;
            }

            player.HitPause(0);

            HitUnpause(player, freezingTime);
        }

        public static void HitUnpause(ControlsScript player, Fix64 freezingTime)
        {
            if (player == null)
            {
                return;
            }

            UFE.DelaySynchronizedAction(player.HitUnpause, freezingTime);
        }

        #endregion

        #region Move Info Methods

        public static void CastMoveByMoveName(ControlsScript player, string moveName, bool overrideCurrentMove = true, bool forceGrounded = false, bool castWarning = false)
        {
            if (player == null)
            {
                return;
            }

            player.CastMove(GetMoveInfoByMoveNameFromMoveInfoCollection(moveName, player.MoveSet.attackMoves), overrideCurrentMove, forceGrounded, castWarning);
        }

        public static MoveInfo GetMoveInfoByMoveNameFromMoveInfoCollection(string moveName, MoveInfo[] moveInfoArray)
        {
            if (moveInfoArray == null)
            {
                return null;      
            }

            int length = moveInfoArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (moveName != moveInfoArray[i].moveName)
                {
                    continue;
                }

                return moveInfoArray[i];
            }

            return null;
        }

        public static void KillAllMoves(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            player.storedMove = null;

            player.KillCurrentMove();
        }

        public static void KillAllMovesPlayer1()
        {
            if (UFE.GetPlayer1ControlsScript() == null)
            {
                return;
            }

            UFE.GetPlayer1ControlsScript().storedMove = null;

            UFE.GetPlayer1ControlsScript().KillCurrentMove();
        }

        public static void KillAllMovesPlayer2()
        {
            if (UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            UFE.GetPlayer2ControlsScript().storedMove = null;

            UFE.GetPlayer2ControlsScript().KillCurrentMove();
        }

        #endregion

        #region Move Set Script Methods

        public static void ChangeMoveStances(ControlsScript player, CombatStances newStance)
        {
            if (player == null)
            {
                return;
            }

            player.MoveSet.ChangeMoveStances(newStance);
        }

        #endregion

        #region Physics Methods

        public static void ForceGrounded(ControlsScript player, int timesToExecute = 1)
        {
            if (player == null)
            {
                return;
            }

            for (int i = 0; i < timesToExecute; i++)
            {
                player.Physics.ForceGrounded();
            }
        }

        public static void AddForce(ControlsScript player, FPVector forces)
        {
            if (player == null)
            {
                return;
            }

            player.Physics.AddForce(forces, player.GetDirection(), true);
        }

        public static void ResetForces(ControlsScript player, bool resetXForce, bool resetYForce, bool resetZForce)
        {
            if (player == null)
            {
                return;
            }

            player.Physics.ResetForces(resetXForce, resetYForce, resetZForce);
        }

        #endregion

        #region Position Methods

        public static void SetPlayerPosition(ControlsScript player, FPVector position)
        {
            if (player == null)
            {
                return;
            }

            player.worldTransform.position = position;
        }

        public static void SetPlayerRotation(ControlsScript player, FPQuaternion rotation)
        {
            if (player == null)
            {
                return;
            }

            player.worldTransform.rotation = rotation;
        }

        public static void ShakeCharacterPosition(ControlsScript player, FPVector shakeOffset)
        {
            if (player == null)
            {
                return;
            }

            FPVector offset = new FPVector(
                offset.x = RandomWithTwoPossibleOutcomes(-1, 1) * shakeOffset.x,
                offset.y = RandomWithTwoPossibleOutcomes(-1, 1) * shakeOffset.y,
                offset.z = RandomWithTwoPossibleOutcomes(-1, 1) * shakeOffset.z);

            player.localTransform.position = new FPVector(
                player.localTransform.position.x + offset.x,
                player.localTransform.position.y + offset.y,
                player.localTransform.position.z + offset.z);
        }

        public static void SetAllPlayersLeftCornerPosition(ControlsScript player, Fix64 cornerPositionXOffset)
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            UFE.GetPlayer1ControlsScript().ResetData(true);

            UFE.GetPlayer2ControlsScript().ResetData(true);

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    if (player == UFE.GetPlayer1ControlsScript())
                    {
                        SetPlayerPosition(
                            UFE.GetPlayer1ControlsScript(),
                            new FPVector(
                            UFE.GetStage()._leftBoundary + cornerPositionXOffset,
                            UFE.GetStage().position.y,
                            UFE.GetPlayer1ControlsScript().worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.GetPlayer2ControlsScript(),
                            new FPVector(
                            UFE.GetStage()._leftBoundary,
                            UFE.GetStage().position.y,
                            UFE.GetPlayer2ControlsScript().worldTransform.position.z));
                    }
                    else if (player == UFE.GetPlayer2ControlsScript())
                    {
                        SetPlayerPosition(
                            UFE.GetPlayer1ControlsScript(),
                            new FPVector(
                            UFE.GetStage()._leftBoundary,
                            UFE.GetStage().position.y,
                            UFE.GetPlayer1ControlsScript().worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.GetPlayer2ControlsScript(),
                            new FPVector(
                            UFE.GetStage()._leftBoundary + cornerPositionXOffset,
                            UFE.GetStage().position.y,
                            UFE.GetPlayer2ControlsScript().worldTransform.position.z));
                    }
                    break;

                case GameplayType._3DFighter:
                    break;

                case GameplayType._3DArena:
                    break;
            }
        }

        public static void SetAllPlayersRightCornerPosition(ControlsScript player, Fix64 cornerPositionXOffset)
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            UFE.GetPlayer1ControlsScript().ResetData(true);

            UFE.GetPlayer2ControlsScript().ResetData(true);

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    if (player == UFE.GetPlayer1ControlsScript())
                    {
                        SetPlayerPosition(
                            UFE.GetPlayer1ControlsScript(),
                            new FPVector(
                            UFE.GetStage()._rightBoundary - cornerPositionXOffset,
                            UFE.GetStage().position.y,
                            UFE.GetPlayer1ControlsScript().worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.GetPlayer2ControlsScript(),
                            new FPVector(
                            UFE.GetStage()._rightBoundary,
                            UFE.GetStage().position.y,
                            UFE.GetPlayer2ControlsScript().worldTransform.position.z));
                    }
                    else if (player == UFE.GetPlayer2ControlsScript())
                    {
                        SetPlayerPosition(
                            UFE.GetPlayer1ControlsScript(),
                            new FPVector(
                            UFE.GetStage()._rightBoundary,
                            UFE.GetStage().position.y,
                            UFE.GetPlayer1ControlsScript().worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.GetPlayer2ControlsScript(),
                            new FPVector(
                            UFE.GetStage()._rightBoundary - cornerPositionXOffset,
                            UFE.GetStage().position.y,
                            UFE.GetPlayer2ControlsScript().worldTransform.position.z));
                    }
                    break;

                case GameplayType._3DFighter:
                    break;

                case GameplayType._3DArena:
                    break;
            }
        }

        public static void ResetAllPlayersPosition()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            UFE.GetPlayer1ControlsScript().ResetData(true);

            UFE.GetPlayer2ControlsScript().ResetData(true);

            SetPlayerPosition(UFE.GetPlayer1ControlsScript(), UFE.config.roundOptions._p1XPosition);

            SetPlayerPosition(UFE.GetPlayer2ControlsScript(), UFE.config.roundOptions._p2XPosition);

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    break;

                case GameplayType._3DFighter:
                    UFE.GetPlayer1ControlsScript().LookAtTarget();

                    UFE.GetPlayer2ControlsScript().LookAtTarget();
                    break;

                case GameplayType._3DArena:
                    SetPlayerRotation(UFE.GetPlayer1ControlsScript(), FPQuaternion.Euler(UFE.config.roundOptions._p1XRotation));

                    SetPlayerRotation(UFE.GetPlayer2ControlsScript(), FPQuaternion.Euler(UFE.config.roundOptions._p2XRotation));
                    break;
            }
        }

        public static Vector3 GetCenterPositionFromHitBox(HitBox hitBox)
        {
            if (hitBox == null)
            {
                return new Vector3(0, 0, 0);
            }

            if (hitBox.shape == HitBoxShape.circle)
            {
                return new FPVector(hitBox.mappedPosition.x, hitBox.mappedPosition.y, hitBox.mappedPosition.z).ToVector();
            }
            else if (hitBox.shape == HitBoxShape.rectangle)
            {
                return new FPVector(hitBox.rect.center.x + hitBox.mappedPosition.x, hitBox.rect.center.y + hitBox.mappedPosition.y, hitBox.mappedPosition.z).ToVector();
            }

            return new Vector3(0, 0, 0);
        }

        #endregion

        #region Random Methods

        public static int RandomWithTwoPossibleOutcomes(int outcome1, int outcome2)
        {
            int randomOutcomeNumber = FPRandom.Range(0, 2);

            if (randomOutcomeNumber == 0)
            {
                return outcome1;
            }
            else
            {
                return outcome2;
            }
        }

        #endregion

        #region Stage Options Methods

        public static StageOptions GetStageOptionsFromGlobalInfo(string stageName)
        {
            int length = UFE.config.stages.Length;
            for (int i = 0; i < length; i++)
            {
                if (stageName != UFE.config.stages[i].stageName)
                {
                    continue;
                }

                return UFE.config.stages[i];
            }

            return null;
        }

        #endregion

        #region Save And Load State Methods

        public static void SaveState()
        {
            if (UFE.replayMode == null)
            {
                return;
            }

            UFE.replayMode.SaveState();
        }

        public static void LoadState()
        {
            if (UFE.replayMode == null
                || UFE.fluxCapacitor.savedState == null)
            {
                return;
            }

            UFE.replayMode.LoadState();
        }

        #endregion

        #region Training Mode Methods

        public static void SetAllPlayersTrainingModeLifeMode(LifeBarTrainingMode lifeBarTrainingMode)
        {
            UFE.config.trainingModeOptions.p1Life = lifeBarTrainingMode;

            UFE.config.trainingModeOptions.p2Life = lifeBarTrainingMode;
        }

        public static void SetAllPlayersTrainingModeGaugeMode(LifeBarTrainingMode lifeBarTrainingMode)
        {
            UFE.config.trainingModeOptions.p1Gauge = lifeBarTrainingMode;

            UFE.config.trainingModeOptions.p2Gauge = lifeBarTrainingMode;
        }

        #endregion

        #region UFE Controller Methods

        public static void PressAxis(UFEController controller, InputType inputType = InputType.HorizontalAxis, int axisValue = 0)
        {
            if (controller == null)
            {
                return;
            }

            foreach (InputReferences inputReference in controller.inputReferences)
            {
                if (controller.isCPU == false
                    && inputReference.inputType == inputType
                    && inputType != InputType.Button)
                {
                    controller.inputs[inputReference] = new InputEvents(axisValue);
                }
            }
        }

        public static void PressButton(UFEController controller, ButtonPress button, InputType inputType = InputType.Button)
        {
            if (controller == null)
            {
                return;
            }

            foreach (InputReferences inputReference in controller.inputReferences)
            {
                if (controller.isCPU == false
                    && inputReference.inputType == inputType
                    && inputReference.engineRelatedButton == button
                    && inputType != InputType.HorizontalAxis
                    && inputType != InputType.VerticalAxis)
                {
                    controller.inputs[inputReference] = new InputEvents(true);
                }
            }
        }

        #endregion

        #region UI Methods

        public static void SelectButton(Button button)
        {
            if (button == null)
            {
                return;
            }
        
            button.Select();
        }

        public static void SetButtonInteractable(Button button, bool interactable)
        {
            if (button == null)
            {
                return;
            }

            button.interactable = interactable;      
        }

        public static void SetButtonInteractable(Button[] buttonArray, bool interactable)
        {
            if (buttonArray == null)
            {
                return;
            }

            int length = buttonArray.Length;
            for (int i = 0; i < length; i++)
            {
                SetButtonInteractable(buttonArray[i], interactable);
            }
        }

        public static void SetTextMessage(Text text, string message, Color32? color = null)
        {
            if (text == null)
            {
                return;
            }

            text.text = message;

            if (color != null)
            {
                text.color = (Color32)color;
            }
        }

        public static string GetTextMessage(Text text)
        {
            if (text == null)
            {
                return "";
            }

            return text.text;
        }

        public static void SetToggleIsOn(Toggle toggle, bool isOn)
        {
            if (toggle == null)
            {
                return;
            }
       
            toggle.isOn = isOn;
        }

        #endregion

        #region Unity Editor Methods

#if UNITY_EDITOR
        /// <summary>
        /// Gets the object the property represents.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static object GetTargetObjectOfProperty(UnityEditor.SerializedProperty prop)
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

        public static void SetTargetObjectOfProperty(UnityEditor.SerializedProperty prop, object value)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements.Take(elements.Length - 1))
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

            if (Object.ReferenceEquals(obj, null)) return;

            try
            {
                var element = elements.Last();

                if (element.Contains("["))
                {
                    var tp = obj.GetType();
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    var field = tp.GetField(elementName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var arr = field.GetValue(obj) as System.Collections.IList;
                    arr[index] = value;
                    //var elementName = element.Substring(0, element.IndexOf("["));
                    //var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    //var arr = DynamicUtil.GetValue(element, elementName) as System.Collections.IList;
                    //if (arr != null) arr[index] = value;
                }
                else
                {
                    var tp = obj.GetType();
                    var field = tp.GetField(element, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field != null)
                    {
                        field.SetValue(obj, value);
                    }
                    //DynamicUtil.SetValue(obj, element, value);
                }
            }
            catch
            {
                return;
            }
        }
#endif

        #endregion
    }
}