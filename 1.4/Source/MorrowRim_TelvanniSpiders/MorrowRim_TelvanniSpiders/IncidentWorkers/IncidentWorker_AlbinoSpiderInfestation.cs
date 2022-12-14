using System;
using UnityEngine;
using Verse;
using RimWorld;
namespace MorrowRim_TelvanniSpiders
{
    public class IncidentWorker_AlbinoSpiderInfestation : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return base.CanFireNowSub(parms);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            Thing t = AlbinoSpiderNestUtility.SpawnTunnels(Mathf.Max(Mathf.Clamp(GenMath.RoundRandom(parms.points / 10), 1, 15), 1), map, false, false, null);
            base.SendStandardLetter(parms, t, Array.Empty<NamedArgument>());
            Find.TickManager.slower.SignalForceNormalSpeedShort();
            return true;
        }
    }
}