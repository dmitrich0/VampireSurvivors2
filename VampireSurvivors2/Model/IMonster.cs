using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace VampireSurvivors2
{
    internal interface IMonster
    {
        float Speed { get; set; }
        int CurrentCooldown { get; set; }
        int CoolDown { get; set; }
        int AttackRange { get; set; }
        int XP { get; set; }
        Image Image { get; set; }
        float Health { get; set; }
        float MaxHealth { get; set; }
        int Damage { get; set; }
        Size Size { get; set; }
        PointF Position { get; set; }
        WorldModel World { get; set; }
        System.Windows.Vector Direction { get; set; }
        Animator Animator { get; set; }
        PointF CentralPosition { get; set; }

        void Move();
        void TryDamagePlayer(System.Windows.Vector vector);
        void TryToGetBookDamage(System.Windows.Vector vector);
        void GetDamage(int damage, WorldModel world);
        void MakeAnim();

    }
}
