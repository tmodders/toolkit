using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Toolkit.Content.Guns;

/// <summary>
///     Provides a practical example of a bullet item.
/// </summary>
public class ExampleBulletItem : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Ranged;
        
        Item.damage = 10;
        Item.knockBack = 1f;
        
        Item.maxStack = Item.CommonMaxStack;
        
        Item.consumable = true;
        
        // Matches the dimensions of the item's texture.
        Item.width = 16;
        Item.height = 16;

        // Indicates the type of projectile the ammunition shoots.
        Item.shoot = ModContent.ProjectileType<ExampleBulletProjectile>();

        // Indicates the speed of the projectile shot by the ammunition, in pixels per frame.
        Item.shootSpeed = 12f;
        
        // Indicates the type of ammunition the item belongs to.
        Item.ammo = AmmoID.Bullet;
    }
}

/// <summary>
///     Provides a practical example of a bullet projectile.
/// </summary>
public class ExampleBulletProjectile : ModProjectile
{
    public override void SetDefaults()
    {
        // Indicates the projectile's velocity ignores water.
        Projectile.ignoreWater = true;
        
        Projectile.friendly = true;
        Projectile.hostile = false;
        
        Projectile.width = 8;
        Projectile.height = 8;
        
        // Indicates the projectile behaves the same as a bullet.
        AIType = ProjectileID.Bullet;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        // Creates dust effects when colliding with tiles.
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        
        return true;
    }

    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(in SoundID.Item10, Projectile.Center);
    }
}