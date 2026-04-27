using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Toolkit.Content.Biomes;

public class ExampleGrassSeedsItem : ModItem
{
    public override void SetDefaults()
    {
        Item.maxStack = Item.CommonMaxStack;
        
        Item.consumable = true;

        Item.autoReuse = true;

        // Marks that the item's swing direction will change accordingly to the player's direction.
        Item.useTurn = true;
        
        // Matches the dimensions of the item's texture.
        Item.width = 16;
        Item.height = 16;

        // Sets the item's use time to 15, which means the item will take 15 frames to be used.
        Item.useTime = 15;
        
        // Sets the item's use animation to 15, which means the item's animation will last for 15 frames.
        Item.useAnimation = 15;
    }

    public override bool? UseItem(Player player)
    {
        // Checks whether the local player is being handled.
        if (player.whoAmI != Main.myPlayer)
        {
            return false;
        }

        var x = Player.tileTargetX;
        var y = Player.tileTargetY;
        
        var tile = Framing.GetTileSafely(x, y);

        // Checks whether the tile meet the conditions to be overridden by the seeds.
        if (!tile.HasTile || tile.TileType != TileID.Dirt)
        {
            return false;
        }
        
        // Checks whether the player is attempting to use the item in a tile within placement range.
        var ranges = player.IsTargetTileInItemRange(Item);

        if (!ranges)
        {
            return false;
        }
        
        WorldGen.PlaceTile(x, y, ModContent.TileType<ExampleGrassTile>(), true);
        
        // Synchronizes the tile placement across the server.
        NetMessage.SendTileSquare(-1, x, y);
        
        return true;
    }
}

public class ExampleGrassTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileBlendAll[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;

        // Marks that the tile spreads to dirt.
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
        
        // Marks that the tile can be overridden when using WorldGen.OreRunner().
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.DoesntPlaceWithTileReplacement[Type] = true;
        TileID.Sets.ResetsHalfBrickPlacementAttempt[Type] = true;
        
        TileID.Sets.Conversion.MergesWithDirtInASpecialWay[Type] = true;
        
        AddMapEntry(new Color(255, 255, 255));

        MineResist = 0.5f;
        
        HitSound = SoundID.Dig;
        DustType = DustID.Dirt;
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        // Makes the tile emits less dust if it failed to be destroyed. Useful for visual hints.
        num = fail ? 1 : 3;
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        // Checks if the tile was meant to be destroyed.
        if (fail)
        {
            return;
        }

        var tile = Framing.GetTileSafely(i, j);

        // Instead of destroying the tile, we turn the tile into dirt.
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
        var speed = MathF.Abs(player.velocity.X);
        
        // Checks whether the player is walking.
        if (speed == 0f)
        {
            return;
        }
        
        // Makes dust spawn more frequently the faster the player is.
        const float rate = 30f;
        
        var chance = (int)MathHelper.Clamp(rate / speed, 1f, rate);

        if (!Main.rand.NextBool(chance))
        {
            return;
        }

        // Spawns dust effects whenever the player is walking on the tile.
        Dust.NewDust(player.BottomLeft, player.width, 0, DustType, 0f, -player.velocity.X / 10f);
    }

    public override void RandomUpdate(int i, int j)
    {
        UpdateVines(i, j);   
        UpdateFoliage(i, j);
    }

    private static void UpdateVines(int i, int j)
    {
        const int chance = 15;

        // Checks whether the tiles meet the chance conditions to place a vine below the tile.
        if (!WorldGen.genRand.NextBool(chance))
        {
            return;
        }

        var tile = Framing.GetTileSafely(i, j);
        var below = Framing.GetTileSafely(i, j + 1);

        // Checks whether the tiles meet the conditions to place a vine below the tile.
        if (tile.BottomSlope || below.HasTile || below.LiquidType == LiquidID.Lava)
        {
            return;
        }

        // TODO: Replace with ExampleVine.
        const int type = TileID.Vines;

        WorldGen.PlaceObject(i, j + 1, type);
        
        // Synchronizes the tile placement across the server.
        NetMessage.SendTileSquare(-1, i, j + 1);
    }

    private static void UpdateFoliage(int i, int j)
    {
        const int chance = 15;

        // Checks whether the tiles meet the chance conditions to place a foliage above the tile.
        if (!WorldGen.genRand.NextBool(chance))
        {
            return;
        }
        
        var tile = Framing.GetTileSafely(i, j);
        var above = Framing.GetTileSafely(i, j - 1);
        
        // Checks whether the tiles meet the conditions to place foliage above the tile.
        if (tile.BottomSlope || above.HasTile || above.LiquidType == LiquidID.Lava)
        {
            return;
        }
        
        // TODO: Replace with ExampleFoliage.
        const int type = TileID.Plants;

        // TODO: Replace with ExampleFoliage styles.
        var style = Main.rand.Next(0, 8);

        WorldGen.PlaceObject(i, j - 1, type, false, style);
        
        // Synchronizes the tile placement across the server.
        NetMessage.SendTileSquare(-1, i, j + 1);
    }
}