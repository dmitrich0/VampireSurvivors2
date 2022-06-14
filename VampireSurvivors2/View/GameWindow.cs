using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Forms;
using VampireSurvivors2.Model;
using VampireSurvivors2.Model.Weapons;

namespace VampireSurvivors2.View
{
    internal partial class GameWindow : Form
    {
        private WorldModel world;
        private Stopwatch visibleTimer;
        private PrivateFontCollection myFontCollection;
        private FontFamily myFont;
        private Player player;
        private List<Keys> activeKeys;
        private SoundPlayer musicPlayer;
        public Timer MainTimer;
        private PointF menuPos;
        private Image menuImage;
        private bool isMenuOpened;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            visibleTimer = new Stopwatch();
            myFontCollection = new PrivateFontCollection();
            activeKeys = new List<Keys>();
            MainTimer = new Timer { Interval = 30 };
            world = new WorldModel(ClientSize.Width, ClientSize.Height, 60);
            player = world.Player;
            musicPlayer = new SoundPlayer(Resources.music);
            myFontCollection.AddFontFile(@"font.otf");
            myFont = myFontCollection.Families[0];
            BackgroundImage = Resources.bg;
            MainTimer.Tick += Update;
            MainTimer.Start();
            visibleTimer.Start();
            KeyDown += AddKeys;
            KeyUp += RemoveKeys;
            musicPlayer.PlayLooping();
            Text = @"Vampire Survivors 2";
            Icon = Resources.iconScull;
            menuImage = Resources.nextLevel;
            menuPos = new PointF((float)(Size.Width / 2 - menuImage.Width / 1.5), 120);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            DrawTime(g);
            DrawEntities(g);
            DrawMonsters(g);
            DrawPlayerAndHud(g);
            DrawKillsCounter(g);
            DrawWeaponsTable(g);
            DrawRacingSoulBullets(g);
            if (world.IsLevelChanged() || isMenuOpened)
                ShowNewLevelWindow(g);
        }

        private void DrawPlayerAndHud(Graphics g)
        {
            const int xpPadding = 8;
            const int xpBorders = 2;
            var xpWidth = (int)(((float)player.CurrentXp / (float)player.XpToNextLevel) * world.WorldWidth - 2 * xpPadding);
            var levelPosition = new PointF(world.WorldWidth - 80, xpPadding - 2);
            var xpRectangle = new Rectangle(xpPadding, xpPadding, xpWidth, 25);
            var xpBg = new Rectangle(xpPadding - xpBorders, xpPadding - xpBorders,
                (int)(world.WorldWidth - 2 * xpPadding) + xpBorders * 2, 25 + xpBorders * 2);
            var xpBorder = new Rectangle(xpPadding - xpBorders * 2, xpPadding - xpBorders * 2,
                (int)(world.WorldWidth - 2 * xpPadding) + xpBorders * 4, 25 + xpBorders * 4);

            g.DrawImage(player.Image, player.Position.X, player.Position.Y,
                (float)player.Size.Width, (float)player.Size.Height);

            g.FillRectangle(Brushes.LightYellow, xpPadding - 2, 50 - 2, player.MaxHealth * 3 + 4, 25 + 4);
            g.FillRectangle(Brushes.Black, xpPadding, 50, player.MaxHealth * 3, 25);
            g.FillRectangle(Brushes.DarkRed, xpPadding, 50, player.Health * 3, 25);

            g.FillRectangle(Brushes.Gold, xpBorder);
            g.FillRectangle(Brushes.Black, xpBg);
            g.FillRectangle(Brushes.Blue, xpRectangle);

            if (player.ProtectionBookWeapon != null)
                g.DrawEllipse(Pens.Gold, Extensions.GetCircleRect(player.CentralPosition.X, 
                    player.CentralPosition.Y, player.ProtectionBookWeapon.AttackRange));

            g.DrawString("LV " + player.Level, new Font(myFont, 14),
                Brushes.AntiqueWhite, levelPosition);
        }

        private void DrawKillsCounter(Graphics g)
        {
            var skullRect = new RectangleF(world.WorldWidth - 300, 40, 25, 25);
            var textPosition = new PointF(skullRect.Location.X + skullRect.Width + 10, skullRect.Location.Y - 4);
            g.DrawImage(Resources.skull, skullRect);
            g.DrawString(player.Killed.ToString(), new Font(myFont, 14), Brushes.AntiqueWhite, textPosition);
        }

        private void DrawTime(Graphics g)
        {
            var seconds = (int)visibleTimer.Elapsed.TotalSeconds % 60;
            var minutes = (int)visibleTimer.Elapsed.TotalMinutes;
            var timePosition = new PointF((float)(ClientSize.Width / 2.15), 30);
            var targetTimePosition = new PointF((float)(ClientSize.Width / 2.082), 90);
            var totalTime = seconds % 60 >= 10 ? minutes + ":" + seconds
                : minutes + ":0" + seconds;
            g.DrawString(totalTime, new Font(myFont, 36), Brushes.BlanchedAlmond, timePosition);
            g.DrawString("10:00", new Font(myFont, 18), Brushes.Gray, targetTimePosition);
        }

        private void DrawEntities(Graphics g)
        {
            foreach (var entity in world.Entities)
                g.DrawImage(entity.Image, entity.Position);
        }
        
        private void DrawMonsters(Graphics g)
        {
            foreach (var monster in world.Monsters.ToList())
            {
                var healthWidth = (monster.Health / monster.MaxHealth) * monster.Size.Width;
                const int healthHeight = 3;
                g.DrawImage(monster.Image, monster.Position.X, monster.Position.Y, monster.Size.Width, monster.Size.Height);
                if (!(Math.Abs(monster.Health - monster.MaxHealth) > 0)) continue;
                g.FillRectangle(Brushes.Red, monster.Position.X, monster.Position.Y + monster.Size.Height + 10,
                    healthWidth, healthHeight);
                g.DrawRectangle(Pens.Black, monster.Position.X, monster.Position.Y + monster.Size.Height + 10,
                    monster.Size.Width, healthHeight);
            }
        }

        private void DrawRacingSoulBullets(Graphics g)
        {
            foreach (var bullet in world.RacingSoulBullets.ToList())
                g.DrawImage(bullet.Image, bullet.Position);
        }

        private void DrawWeaponsTable(Graphics g)
        {
            var padding = 0;
            foreach (var weapon in player.Weapons)
            {
                var pos = new PointF(350 + padding, 48);
                g.DrawImage(weapon.Icon, pos);
                padding += 40;
            }
        }

        private void AddKeys(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
            if (!activeKeys.Contains(e.KeyCode))
                activeKeys.Add(e.KeyCode);
        }

        private void RemoveKeys(object sender, KeyEventArgs e)
        {
            if (activeKeys.Contains(e.KeyCode))
                activeKeys.Remove(e.KeyCode);
        }

        private Vector GetPlayerDirection()
        {
            var direction = new Vector();
            if (activeKeys.Contains(Keys.W))
                direction.Y += -1;
            if (activeKeys.Contains(Keys.A))
                direction.X += -1;
            if (activeKeys.Contains(Keys.S))
                direction.Y += 1;
            if (activeKeys.Contains(Keys.D))
                direction.X += 1;
            if (direction.X == 0 && direction.Y == 0)
                return new Vector(0, 0);
            direction.Normalize();
            return direction;
        }

        public void ShowNewLevelWindow(Graphics g)
        {
            MainTimer.Stop();
            isMenuOpened = true;
            var levelPos = new PointF(menuPos.X + 375, menuPos.Y + 179);
            g.DrawImage(menuImage, menuPos);
            g.DrawString(player.Level.ToString(), new Font(myFont, 20), Brushes.DarkGoldenrod, levelPos);
            const int dy = 115;
            var i = 0;
            foreach (var weapon in world.AllWeapons)
            {
                var pos = new PointF(menuPos.X + 180, menuPos.Y + 260 + i * dy);
                g.DrawImage(weapon.Icon, pos);
                var textPos = new PointF(pos.X + 60, pos.Y + 5);
                g.DrawString(weapon.LevelUpDescription, new Font(myFont, 8), Brushes.Black, textPos);
                i++;
            }
            if (Controls.Count == 0)
                AddButtonsOnLevelWindow();
        }

        public void AddButtonsOnLevelWindow()
        {
            const int btnHeight = 87;
            const int btnWidth = 385;
            const int dy = 115;
            for (var i = 0; i < 3; i++)
            {
                var button = new Button()
                {
                    Location = new System.Drawing.Point((int)menuPos.X + 153, (int)menuPos.Y + 230 + i * dy),
                    Size = new System.Drawing.Size(btnWidth, btnHeight),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.Transparent,
                };
                button.FlatAppearance.BorderSize = 0;
                switch (i)
                {
                    case 0:
                        button.Click += (s, e) =>
                        {
                            if (player.RacingSoulWeapon == null)
                            {
                                player.RacingSoulWeapon = new RacingSoulWeapon(world);
                                player.Weapons.Add(player.RacingSoulWeapon);
                            }
                            else
                                player.RacingSoulWeapon.WeaponLevel++;
                            MainTimer.Start();
                            isMenuOpened = false;
                            Controls.Clear();
                        };
                        break;
                    case 1:
                        button.Click += (s, e) =>
                        {
                            if (player.DeathRingWeapon == null)
                            {
                                player.DeathRingWeapon = new DeathRingWeapon(world);
                                player.Weapons.Add(player.DeathRingWeapon);
                            }
                            else
                                player.DeathRingWeapon.WeaponLevel++;
                            MainTimer.Start();
                            isMenuOpened = false;
                            Controls.Clear();
                        };
                        break;
                    case 2:
                        button.Click += (s, e) =>
                        {
                            if (player.ProtectionBookWeapon == null)
                            {
                                player.ProtectionBookWeapon = new ProtectionBookWeapon();
                                player.Weapons.Add(player.ProtectionBookWeapon);
                            }
                            else
                            {
                                player.ProtectionBookWeapon.WeaponLevel++;
                                player.ProtectionBookWeapon.AttackRange = 120 + 10 * player.ProtectionBookWeapon.WeaponLevel;
                            }
                            MainTimer.Start();
                            isMenuOpened = false;
                            Controls.Clear();
                        };
                        break;
                }
                Controls.Add(button);
            }
        }

        public void Update(object sender, EventArgs e)
        {
            world.SpawnMonster();
            world.MoveMonsters();
            world.CheckEntities();
            world.CheckBullets();
            world.CheckDeathRing();
            player.Move(GetPlayerDirection());
            player.MakeAnim();
            Invalidate();
        }
    }
}
