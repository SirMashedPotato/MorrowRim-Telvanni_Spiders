using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

namespace MorrowRim_TelvanniSpiders
{
	[StaticConstructorOnStartup]
    public class AlbinoSpiderNestSpawner : ThingWithComps
	{
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look(ref secondarySpawnTick, "secondarySpawnTick", 0, false);
			Scribe_Values.Look(ref spawnHive, "spawnHive", true, false);
			Scribe_Values.Look(ref insectsPoints, "insectsPoints", 0f, false);
			Scribe_Values.Look(ref spawnedByInfestationThingComp, "spawnedByInfestationThingComp", false, false);
		}

		public override void SpawnSetup(Map map, bool respawningAfterLoad)
		{
			base.SpawnSetup(map, respawningAfterLoad);
			if (!respawningAfterLoad)
			{
				secondarySpawnTick = Find.TickManager.TicksGame + ResultSpawnDelay.RandomInRange.SecondsToTicks();
			}
			CreateSustainer();
		}

		public override void Tick()
		{
			if (Spawned)
			{
				sustainer.Maintain();
				Vector3 vector = Position.ToVector3Shifted();
				if (Rand.MTBEventOccurs(DustMoteSpawnMTB, 1f, 1.TicksToSeconds()))
				{
					FleckMaker.ThrowDustPuffThick(new Vector3(vector.x, 0f, vector.z)
					{
						y = AltitudeLayer.MoteOverhead.AltitudeFor()
					}, Map, Rand.Range(1.5f, 3f), new Color(1f, 1f, 1f, 2.5f));
				}
				if (secondarySpawnTick <= Find.TickManager.TicksGame)
				{
					sustainer.End();
					Map map = Map;
					IntVec3 position = Position;
					Destroy(DestroyMode.Vanish);
					Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDefOf.MorrowRim_AlbinoSpider);
					GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(position, map, 2, null), map, WipeMode.Vanish);
					pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent);
				}
			}
		}

		public override void Draw()
		{
			Rand.PushState();
			Rand.Seed = thingIDNumber;
			for (int i = 0; i < 6; i++)
			{
				DrawDustPart(Rand.Range(0f, 360f), Rand.Range(0.9f, 1.1f) * Rand.Sign * 4f, Rand.Range(1f, 1.5f));
			}
			Rand.PopState();
		}

		private void DrawDustPart(float initialAngle, float speedMultiplier, float scale)
		{
			float num = (Find.TickManager.TicksGame - secondarySpawnTick).TicksToSeconds();
			Vector3 pos = Position.ToVector3ShiftedWithAltitude(AltitudeLayer.Filth);
			pos.y += 0.0454545468f * Rand.Range(0f, 1f);
			Color value = new Color(0.470588237f, 0.384313732f, 0.3254902f, 0.7f);
			matPropertyBlock.SetColor(ShaderPropertyIDs.Color, value);
			Matrix4x4 matrix = Matrix4x4.TRS(pos, Quaternion.Euler(0f, initialAngle + speedMultiplier * num, 0f), Vector3.one * scale);
			Graphics.DrawMesh(MeshPool.plane10, matrix, TunnelMaterial, 0, null, 0, matPropertyBlock);
		}

		private void CreateSustainer()
		{
			LongEventHandler.ExecuteWhenFinished(delegate
			{
				SoundDef tunnel = RimWorld.SoundDefOf.Tunnel;
				sustainer = tunnel.TrySpawnSustainer(SoundInfo.InMap(this, MaintenanceType.PerTick));
			});
		}

		private int secondarySpawnTick;

		public bool spawnHive = true;

		public float insectsPoints;

		public bool spawnedByInfestationThingComp;

		private Sustainer sustainer;

		private static MaterialPropertyBlock matPropertyBlock = new MaterialPropertyBlock();

		private readonly FloatRange ResultSpawnDelay = new FloatRange(26f, 30f);

		[TweakValue("Gameplay", 0f, 1f)]
		private static readonly float DustMoteSpawnMTB = 0.2f;

		private static readonly Material TunnelMaterial = MaterialPool.MatFrom("Things/Filth/Grainy/GrainyA", ShaderDatabase.Transparent);
	}
}