using Terraria.ID;
using Terraria.ModLoader;

namespace Toolkit.Content.Guns;

/// <summary>
///     Provides a practical example of a gun that shoots bullets in bursts.
/// </summary>
public class ExampleBurstsGunItem : ModItem
{
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Ranged;
        
        Item.damage = 10;
        Item.knockBack = 1f;
        
        Item.consumeAmmoOnLastShotOnly = true;
        
        // Marks that the item does not deal any contact damage.
        Item.noMelee = true;

        // Matches the dimensions of the item's texture.
        Item.width = 16;
        Item.height = 16;

        Item.UseSound = SoundID.Item4;
        
        // The amount of bursts.
        const int bursts = 3;
        
        // The duration of each burst, in frames.
        const int duration = 4;
        
        // Sets the item's use time to 4, which means the item will take 4 frames to be used.
        Item.useTime = duration;
        
        // Sets the item's use animation to 12, which means the item's animation will last for 12 frames.
        Item.useAnimation = duration * bursts;
        
        // Note: Making useAnimation greater than useTime will make the item look like it was used only once, despite being used several times during the animation.

        const int delay = duration * bursts + 10;

        // Defines that the item waits until all bursts are shot plus an additional 10 frames before it can be reused.
        Item.reuseDelay = delay;
        
        Item.useStyle = ItemUseStyleID.Shoot;

        // Sets the velocity of the projectile that the item shoots, in pixels per frame.
        Item.shootSpeed = 16f;

        // Sets the type of the projectile that the item shoots.
        Item.shoot = ModContent.ProjectileType<ExampleBulletProjectile>();

        // Sets the type of ammo that the item uses.
        Item.useAmmo = AmmoID.Bullet;
    }
}