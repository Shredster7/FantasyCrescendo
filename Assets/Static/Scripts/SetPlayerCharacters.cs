// The MIT License (MIT)
// 
// Copyright (c) 2016 Hourai Teahouse
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using HouraiTeahouse.SmashBrew;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetPlayerCharacters : MonoBehaviour, ISubmitHandler {
    [SerializeField]
    CharacterData character;

    public void OnSubmit(BaseEventData eventData) {
        foreach (Player player in Player.ActivePlayers) {
            player.SelectedCharacter = character;
            player.Pallete = 0;
            if (player.ID < 2)
                player.Type = PlayerType.HumanPlayer;
            else
                player.Type = PlayerType.None;
        }
    }
}
