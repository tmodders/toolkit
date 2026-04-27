using Terraria.ID;
using Terraria.ModLoader;

namespace Toolkit.Content.Tools;

/// <summary>
///     Provides a practical example of a hammer.
/// </summary>
public class ExampleHammer : ModItem
{
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Melee;
                
        Item.damage = 10;
        Item.knockBack = 1f;

        // Marks that melee speed modifiers will only affect the rate at which the item swings as a weapon, but wont increase its mining speed.
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        
        Item.autoReuse = true;
        
        // Marks that the item's swing direction will change accordingly to the player's direction.
        Item.useTurn = true;
        
        // Matches the dimensions of the item's texture.
        Item.width = 16;
        Item.height = 16;
        
        // Sets the power of the hammer.
        Item.hammer = 50;

        Item.UseSound = SoundID.Item1;

        // Sets the item's use time to 10, which means the item will take 15 frames to be used.
        Item.useTime = 15;
        
        // Sets the item's use animation to 15, which means the item's animation will last for 15 frames.
        Item.useAnimation = 15;
        
        Item.useStyle = ItemUseStyleID.Swing;
    }
}