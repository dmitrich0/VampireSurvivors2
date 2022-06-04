using System.Drawing;
using System.Windows;

namespace VampireSurvivors2
{
    internal class Monster : IAnimarable
    {
        public Image[] Idle { get; set; }
        public Image[] Right { get; set; }
        public Image[] Left { get; set; }
        public int CurrentCooldown { get; set; }   
        public int CoolDown { get; set; }
        public int AttackRange { get; set; }
        public int XP { get; set; }
        public Image Image { get; set; }
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public int Damage { get; set; }
        public System.Drawing.Size Size { get; set; }
        public PointF Position { get; set; }
        public WorldModel World { get; set; }
        public Vector Direction { get; set; }
        public Animator Animator { get; set; }
        public float Speed { get; set; }
        public PointF CentralPosition
        {
            get { return new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2); }
            set { ; }
        }

        public Monster(WorldModel world, PointF pos)
        {
            Position = pos;
            CurrentCooldown = 0;
            World = world;
        }

        public void Move()
        {
            var direction = new Vector(World.Player.CentralPosition.X - CentralPosition.X,
                World.Player.CentralPosition.Y - CentralPosition.Y);
            Direction = direction;
            TryDamagePlayer(direction);
            TryToGetBookDamage(direction);
            direction.Normalize();
            Position = new PointF(Position.X + (float)direction.X * Speed, Position.Y + (float)direction.Y * Speed);
        }

        public void TryDamagePlayer(Vector vector)
        {
            if (vector.Length <= AttackRange)
            {
                if (CurrentCooldown == 0)
                {
                    World.Player.GetDamage(Damage);
                    CurrentCooldown++;
                }
                else if (CurrentCooldown == CoolDown)
                    CurrentCooldown = 0;
                else
                    CurrentCooldown++;
            }
        }

        public void TryToGetBookDamage(Vector vector)
        {
            foreach (var weapon in World.Player.Weapons)
            {
                if (weapon is ProtectionBookWeapon bookWeapon)
                    if (vector.Length <= bookWeapon.AttackRange)
                        GetDamage(bookWeapon.DoDamage(vector));
            }
        }

        public void GetDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                World.SpawnFeaturesAfterDying(this);
        }

        public void MakeAnim()
        {
            Image = Animator.GetCurrentFrame();
        }
    }
}
