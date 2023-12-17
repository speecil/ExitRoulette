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
            _isInExitRoulette = false;
            _chance = int.Parse(ExitRouletteModifier.customModifier.ModifierSettings.AdditionalSettings["Chance"].ToString());
            ExitRouletteModifier.customModifier.Function = ExitAction;
            ExitRouletteModifier.customModifier.CoolDown = int.Parse((int.Parse(ExitRouletteModifier.customModifier.ModifierSettings.AdditionalSettings["SecondsActive"].ToString()) + 5).ToString());
        }

        private void ExitAction(ChatModifiers.API.MessageInfo messageInfo, object[] args)
        {
            if (_isInExitRoulette)
            {
                return;
            }
            SharedCoroutineStarter.instance.StopUniqueCoroutine(ExitCoroutine);
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
