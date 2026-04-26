using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Content.Guns;

/// <summary>
///     Provides a practical example of a gun that shoots bullets in bursts.
/// </summary>
public class ExampleBurstsGunItem : ModItem
{
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Ranged;
        
        Item.consumeAmmoOnLastShotOnly = true;
        
        // Indicates the item does not deal contact damage.
        Item.noMelee = true;

        // Matches the dimensions of the item's texture.
        Item.width = 16;
        Item.height = 16;

        Item.UseSound = SoundID.Item4;
        
        // The amount of bursts.
        var bursts = 3;
        
        // The duration of each burst.
        var duration = 4;
        
        // Indicates the item fires one shot every 4 frames during the burst.
        Item.useTime = duration;
        
        // Indicates the animation lasts long enough to fire all shots in the burst.
        Item.useAnimation = duration * bursts;

        var delay = duration * bursts + 10;

        // Indicates the item waits until all bursts are shot plus an additional 10 frames before it can be reused.
        Item.reuseDelay = delay;
        
        Item.useStyle = ItemUseStyleID.Shoot;

        // Indicates the velocity of the projectile that the item shoots, in pixels per frame.
        Item.shootSpeed = 16f;

        // Indicates the type of the projectile that the item shoots.
        Item.shoot = ModContent.ProjectileType<ExampleBulletProjectile>();

        // Indicates the type of ammo that the projectile uses.
        Item.useAmmo = AmmoID.Bullet;
    }
}