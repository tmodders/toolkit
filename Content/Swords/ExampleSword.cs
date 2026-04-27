using Terraria.ID;
using Terraria.ModLoader;

namespace Toolkit.Content.Swords;

/// <summary>
///     Provides a practical example of a sword.
/// </summary>
public class ExampleSword : ModItem
{
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Melee;
        
        Item.damage = 10;
        Item.knockBack = 1f;

        Item.autoReuse = true;

        // Matches the dimensions of the item's texture.
        Item.width = 16;
        Item.height = 16;
        
        Item.UseSound = SoundID.Item1;

        // Sets the item's use time to 25, which means the item will take 25 frames to be used.
        Item.useTime = 25;
        
        // Sets the item's use animation to 25, which means the item's animation will last for 25 frames.
        Item.useAnimation = 25;

        Item.useStyle = ItemUseStyleID.Swing;
    }
}