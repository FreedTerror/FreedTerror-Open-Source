using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UFE3D;

public class DefaultCreditsScreenScrollRect : CreditsScreen{
	#region public instance properties
	public AudioClip music;
	public AudioClip onLoadSound;
	public AudioClip selectSound;
	public AudioClip cancelSound;
	public AudioClip moveCursorSound;
    public bool stopPreviousSoundEffectsOnLoad = false;
	public float delayBeforePlayingMusic = 0.1f;
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private float scrollRectScrollSpeed;
    #endregion

    #region public override methods
    public override void DoFixedUpdate(
		IDictionary<InputReferences, InputEvents> player1PreviousInputs,
		IDictionary<InputReferences, InputEvents> player1CurrentInputs,
		IDictionary<InputReferences, InputEvents> player2PreviousInputs,
		IDictionary<InputReferences, InputEvents> player2CurrentInputs
	){
		base.DoFixedUpdate(player1PreviousInputs, player1CurrentInputs, player2PreviousInputs, player2CurrentInputs);

		this.DefaultNavigationSystem(
			player1PreviousInputs,
			player1CurrentInputs,
			player2PreviousInputs,
			player2CurrentInputs,
			this.moveCursorSound,
			this.selectSound,
			this.cancelSound,
			this.GoToMainMenuScreen
		);

        if (player1CurrentInputs != null)
        {
            foreach (KeyValuePair<InputReferences, InputEvents> pair in player1CurrentInputs)
            {
                if (pair.Key.inputType != InputType.VerticalAxis) continue;

                int axisRawValue = (int)pair.Value.axisRaw;

                if (axisRawValue >= 1)
                {
                    if (scrollRect.normalizedPosition.y < 1)
                    {
                        Vector2 normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, scrollRect.normalizedPosition.x + scrollRectScrollSpeed);

                        if (normalizedPosition.y >= 1)
                        {
                            normalizedPosition.y = 1;
                        }

                        float anchoredPositionX = scrollRect.content.anchoredPosition.x;

                        scrollRect.normalizedPosition = normalizedPosition;

                        scrollRect.content.anchoredPosition = new Vector2(anchoredPositionX, scrollRect.content.anchoredPosition.y);
                    }
                    else
                    {
                        Vector2 normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, 1);

                        float anchoredPositionX = scrollRect.content.anchoredPosition.x;

                        scrollRect.normalizedPosition = normalizedPosition;

                        scrollRect.content.anchoredPosition = new Vector2(anchoredPositionX, scrollRect.content.anchoredPosition.y);
                    }
                }
                else if (axisRawValue <= -1)
                {
                    if (scrollRect.normalizedPosition.y > 0)
                    {
                        Vector2 normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, scrollRect.normalizedPosition.y - scrollRectScrollSpeed);

                        if (normalizedPosition.y <= 0)
                        {
                            normalizedPosition.y = 0;
                        }

                        float anchoredPositionX = scrollRect.content.anchoredPosition.x;

                        scrollRect.normalizedPosition = normalizedPosition;

                        scrollRect.content.anchoredPosition = new Vector2(anchoredPositionX, scrollRect.content.anchoredPosition.y);
                    }
                    else
                    {
                        Vector2 normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, scrollRect.normalizedPosition.y);

                        float anchoredPositionX = scrollRect.content.anchoredPosition.x;

                        scrollRect.normalizedPosition = normalizedPosition;

                        scrollRect.content.anchoredPosition = new Vector2(anchoredPositionX, scrollRect.content.anchoredPosition.y);
                    }
                }
            }
        }

        if (player2CurrentInputs != null)
        {
            foreach (KeyValuePair<InputReferences, InputEvents> pair in player2CurrentInputs)
            {
                if (pair.Key.inputType != InputType.VerticalAxis) continue;

                int axisRawValue = (int)pair.Value.axisRaw;

                if (axisRawValue >= 1)
                {
                    if (scrollRect.normalizedPosition.y < 1)
                    {
                        Vector2 normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, scrollRect.normalizedPosition.x + scrollRectScrollSpeed);

                        if (normalizedPosition.y >= 1)
                        {
                            normalizedPosition.y = 1;
                        }

                        float anchoredPositionX = scrollRect.content.anchoredPosition.x;

                        scrollRect.normalizedPosition = normalizedPosition;

                        scrollRect.content.anchoredPosition = new Vector2(anchoredPositionX, scrollRect.content.anchoredPosition.y);
                    }
                    else
                    {
                        Vector2 normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, 1);

                        float anchoredPositionX = scrollRect.content.anchoredPosition.x;

                        scrollRect.normalizedPosition = normalizedPosition;

                        scrollRect.content.anchoredPosition = new Vector2(anchoredPositionX, scrollRect.content.anchoredPosition.y);
                    }
                }
                else if (axisRawValue <= -1)
                {
                    if (scrollRect.normalizedPosition.y > 0)
                    {
                        Vector2 normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, scrollRect.normalizedPosition.y - scrollRectScrollSpeed);

                        if (normalizedPosition.y <= 0)
                        {
                            normalizedPosition.y = 0;
                        }

                        float anchoredPositionX = scrollRect.content.anchoredPosition.x;

                        scrollRect.normalizedPosition = normalizedPosition;

                        scrollRect.content.anchoredPosition = new Vector2(anchoredPositionX, scrollRect.content.anchoredPosition.y);
                    }
                    else
                    {
                        Vector2 normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, scrollRect.normalizedPosition.y);

                        float anchoredPositionX = scrollRect.content.anchoredPosition.x;

                        scrollRect.normalizedPosition = normalizedPosition;

                        scrollRect.content.anchoredPosition = new Vector2(anchoredPositionX, scrollRect.content.anchoredPosition.y);
                    }
                }
            }
        }
    }

	public override void OnShow (){
		base.OnShow ();
		this.HighlightOption(this.FindFirstSelectable());
		
		if (this.music != null){
			UFE.DelayLocalAction(delegate(){UFE.PlayMusic(this.music);}, this.delayBeforePlayingMusic);
		}
		
		if (this.stopPreviousSoundEffectsOnLoad){
			UFE.StopSounds();
		}
		
		if (this.onLoadSound != null){
			UFE.DelayLocalAction(delegate(){UFE.PlaySound(this.onLoadSound);}, this.delayBeforePlayingMusic);
		}
	}
    #endregion
}
