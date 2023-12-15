using SiraUtil.Affinity;
using System.Collections;
using UnityEngine;
using Zenject;

namespace ExitRoulette.Game
{
    internal class GameCoreHandler : IInitializable, IAffinity
    {
        [Inject] internal PauseMenuManager _pauseMenuManager;

        internal static bool _isInExitRoulette = false;
        internal int _chance = 128;

        public void Initialize()
        {
            _chance = (int)ExitRouletteModifier.customModifier.ModifierSettings.AdditionalSettings["Chance"];
            ExitRouletteModifier.customModifier.Function = ExitAction;
            ExitRouletteModifier.customModifier.CoolDown = (int)ExitRouletteModifier.customModifier.ModifierSettings.AdditionalSettings["SecondsActive"] + 5;
        }

        private void ExitAction(ChatModifiers.API.MessageInfo messageInfo, object[] args)
        {
            if (_isInExitRoulette)
            {
                return;
            }
            SharedCoroutineStarter.instance.StartCoroutine(ExitCoroutine());
        }

        private IEnumerator ExitCoroutine()
        {
            if (_isInExitRoulette)
            {
                yield break;
            }
            _isInExitRoulette = true;
            yield return new WaitForSeconds((float)ExitRouletteModifier.customModifier.ModifierSettings.AdditionalSettings["SecondsActive"]);
            _isInExitRoulette = false;
            yield return null;
        }

        [AffinityPatch(typeof(NoteController), "SendNoteWasCutEvent")]
        [AffinityPostfix]
        private void OnNoteHit()
        {
            if (_isInExitRoulette)
            {
                if (UnityEngine.Random.Range(0, _chance) == 0)
                {
                    _pauseMenuManager.MenuButtonPressed();
                }
            }
        }
    }
}
