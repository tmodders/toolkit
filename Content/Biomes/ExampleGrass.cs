using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Content.Biomes;

public class ExampleGrassTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileBlendAll[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;

        // Indicates the tile spreads to dirt.
        TileID.Sets.Grass[Type] = true;
        
        // If your grass spreads to other tile types, set the field below to true:
        // TileID.Sets.GrassSpecial[Type] = true;
        
        // If your grass spreads to both dirt and other tile types, set both fields below to true:
        // TileID.Sets.Grass[Type] = true;
        // TileID.Sets.GrassSpecial[Type] = true;
        
        // If your grass spreads to other tile types, set both fields below accordingly:
        // Main.tileMerge[Type][tileType] = true;
        // Main.tileMerge[tileType][Type] = true;
        
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.ForcedDirtMerging[Type] = true;
        
        TileID.Sets.NeedsGrassFraming[Type] = true;
        TileID.Sets.NeedsGrassFramingDirt[Type] = TileID.Dirt;
        
        TileID.Sets.SpreadOverground[Type] = true;
        TileID.Sets.SpreadUnderground[Type] = true;
        
        // Indicates the tile can be overridden when using WorldGen.OreRunner().
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.DoesntPlaceWithTileReplacement[Type] = true;
        TileID.Sets.ResetsHalfBrickPlacementAttempt[Type] = true;
        
        TileID.Sets.Conversion.MergesWithDirtInASpecialWay[Type] = true;
        
        AddMapEntry(new Color(255, 255, 255));

        MineResist = 0.5f;
        
        HitSound = SoundID.Dig;
        DustType = DustID.GreenMoss;
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        // Makes the tile emits less dust if it failed to be destroyed. Useful for visual hints.
        num = fail ? 1 : 3;
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (fail)
        {
            return;
        }

        var tile = Framing.GetTileSafely(i, j);

        // Instead of actually destroying the tile, we turn the grass into dirt if it was meant to be destroyed.
        tile.TileType = TileID.Dirt;
    }

    public override bool CanExplode(int i, int j)
    {
        // Makes the tile be destroyed instead of turning into dirt, exclusively from explosions.
        WorldGen.KillTile(i, j);
        
        return true;
    }

    public override void FloorVisuals(Player player)
    {
        // Checks whether the player is walking.
        if (MathF.Abs(player.velocity.X) == 0f)
        {
            return;
        }

        // Spawns dust effects whenever the player is walking on the tile.
        Dust.NewDust(player.Bottom, 0, 0, DustType, 0f, -player.velocity.X / 10f);
    }

    public override void RandomUpdate(int i, int j)
    {
        UpdateVines(i, j);   
        UpdateFoliage(i, j);
    }

    private static void UpdateVines(int i, int j)
    {
        const int chance = 15;

        // Checks if we meet the chance conditions to place a vine below the tile.
        if (!WorldGen.genRand.NextBool(chance))
        {
            return;
        }

        var tile = Framing.GetTileSafely(i, j);
        var below = Framing.GetTileSafely(i, j + 1);

        // Checks if we meet the tile conditions to place a vine below the tile.
        if (tile.BottomSlope || below.HasTile || below.LiquidType == LiquidID.Lava)
        {
            return;
        }

        // TODO: Replace with ExampleVine.
        const int type = TileID.Vines;

        WorldGen.PlaceObject(i, j + 1, type);
        
        if (Main.netMode != NetmodeID.Server)
        {
            return;
        }
        
        // Synchronizes the tile placement across the server.
        NetMessage.SendTileSquare(-1, i, j + 1, 1);
    }

    private static void UpdateFoliage(int i, int j)
    {
        const int chance = 15;

        // Checks if we meet the chance conditions to place foliage above the tile.
        if (!WorldGen.genRand.NextBool(chance))
        {
            return;
        }
        
        var tile = Framing.GetTileSafely(i, j);
        var above = Framing.GetTileSafely(i, j - 1);
        
        // Checks if we meet the tile conditions to place foliage above the tile.
        if (tile.BottomSlope || above.HasTile || above.LiquidType == LiquidID.Lava)
        {
            return;
        }
        
        // TODO: Replace with ExampleFoliage.
        const int type = TileID.Vines;

        // TODO: Replace with ExampleFoliage styles.
        var style = Main.rand.Next(0, 8);

        WorldGen.PlaceObject(i, j - 1, type, false, style);
        
        if (Main.netMode != NetmodeID.Server)
        {
            return;
        }
        
        // Synchronizes the tile placement across the server.
        NetMessage.SendTileSquare(-1, i, j + 1, 1);
    }
}