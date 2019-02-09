using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PS3Lib;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;

namespace MW3_SP_Mod_Menu
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        static PS3API PS3 = new PS3API(SelectAPI.TargetManager);
        public Form1()
        {
            InitializeComponent();
        }

        public static void GetMemoryR(uint Address, ref byte[] Bytes)
        {
            PS3.GetMemory(Address, Bytes);
        }
   
   
        #region Key_IsDown
        public static string Key_IsDown(uint ClientNum)
        {
            uint NameInGame = 0x012320bc;
            uint Key_isDown = 0x01232075;
            uint Index = 0xB0C8;
            byte[] Key = new byte[3];
            GetMemoryR(Key_isDown + (Index * ClientNum), ref Key);
            string mystring = null;
            mystring = BitConverter.ToString(Key);
            string result = mystring.Replace("-", "");
            string result1 = result.Replace(" ", "");
            string key = result1;
            string KeyPressed = "";
            if (key == "000000")
            {
                KeyPressed = "Stand";
            }
            else if (key == "080C20")
            {
                KeyPressed = "[ ] + X + L1";
            }
            else if (key == "000224")
            {
                KeyPressed = "Crouch + R3 + [ ]";
            }
            else if (key == "008001")
            {
                KeyPressed = "R1 + L2";
            }
            else if (key == "082802")
            {
                KeyPressed = "L1 + L3";
            }
            else if (key == "002402")
            {
                KeyPressed = "X + L3";
            }
            else if (key == "000020")
            {
                KeyPressed = "[  ]";
            }
            else if (key == "000200")
            {
                KeyPressed = "Crouch";
            }
            else if (key == "004020")
            {
                KeyPressed = "R2 + [ ]";
            }
            else if (key == "000220")
            {
                KeyPressed = "[ ] + Crouch";
            }
            else if (key == "000100")
            {
                KeyPressed = "Prone";
            }
            else if (key == "400100")
            {
                KeyPressed = "Left + Prone";
            }
            else if (key == "000400")
            {
                KeyPressed = "X";
            }
            else if (key == "000004")
            {
                KeyPressed = "R3";
            }
            else if (key == "002002")
            {
                KeyPressed = "L3";
            }
            else if (key == "004000")
            {
                KeyPressed = "R2";
            }
            else if (key == "008000")
            {
                KeyPressed = "L2";
            }
            else if (key == "080800")
            {
                KeyPressed = "L1";
            }
            else if (key == "000001")
            {
                KeyPressed = "R1";
            }
            else if (key == "002006")
            {
                KeyPressed = "R3 + L3";
            }
            else if (key == "000204")
            {
                KeyPressed = "R3";
            }
            else if (key == "002202")
            {
                KeyPressed = "L3";
            }
            else if (key == "004200")
            {
                KeyPressed = "R2";
            }
            else if (key == "008004")
            {
                KeyPressed = "R3 + L2";
            }
            else if (key == "008200")
            {
                KeyPressed = "L2";
            }
            else if (key == "082902")
            {
                KeyPressed = "Prone + L1 + L3";
            }
            else if (key == "082906")
            {
                KeyPressed = "Prone + L1 + L3 + R3";
            }
            else if (key == "00C100")
            {
                KeyPressed = "Prone + R2 + L2";
            }
            else if (key == "00C000")
            {
                KeyPressed = "R2 + L2";
            }
            else if (key == "002206")
            {
                KeyPressed = "Crouch L3 + R3";
            }
            else if (key == "002222")
            {
                KeyPressed = "Crouch L3 + [ ]";
            }
            else if (key == "Up")
            {
                KeyPressed = "R2 + L2";
            }
            else if (key == "002122")
            {
                KeyPressed = "Prone + L3 + [ ]";
            }
            else if (key == "000420")
            {
                KeyPressed = "X + [ ]";
            }
            else if (key == "002106")
            {
                KeyPressed = "Prone + R3 + L3";
            }
            else
            {
                KeyPressed = key;
            }
            return KeyPressed;
        }
        public static string GetNames(int clientNum)
        {
            uint NameInGame = 0x012320bc;
            uint Index = 0xB0C8;
            string name;
            byte[] name1 = new byte[18];
            GetMemoryR(NameInGame + ((uint)clientNum * Index), ref name1);
            name = Encoding.ASCII.GetString(name1);
            name.Replace(Convert.ToChar(0x0).ToString(), string.Empty);
            return name;
        }
        #endregion
        
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitch1.IsOn)
            {
                PS3.ChangeAPI(SelectAPI.ControlConsole);
            }
            else
            {
                PS3.ChangeAPI(SelectAPI.TargetManager);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (PS3.ConnectTarget())
                {
                    label1.Text = "PS3 Connected | Not Attached";
                    label1.ForeColor = Color.Lime;
                    simpleButton4.Enabled = true;
                    simpleButton1.Enabled = false;
                }
                else
                {
                    groupControl2.Enabled = false;
                    XtraMessageBox.Show("Failed to Connect PS3 !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Failed to Connect PS3 !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Key_IsDown((uint)0) == "R3")
            {
                MainMenu.Stop();
                HostMenuMain.Start();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MainMenu.Start();
            timer2.Start();
            textBox1.Text = PS3.Extension.ReadString(0x012320BC);
            textBox2.Text = PS3.Extension.ReadString(0x0123D184);
            simpleButton3.Enabled = true;
            simpleButton2.Enabled = false;
            toolStripStatusLabel2.Text = "Started !";
            toolStripStatusLabel2.ForeColor = Color.Lime;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            MainMenu.Stop();
            HostMenuMain.Stop();
            B1.Stop();
            B2.Stop();
            B3.Stop();
            B4.Stop();
            B5.Stop();
            Client1.Stop();
            C1.Stop();
            C2.Stop();
            C3.Stop();
            C4.Stop();
            ResetMenu.Stop();
            D1.Stop();
            D2.Stop();
            D3.Stop();
            D4.Stop();
            D5.Stop();
            AccountMenu.Stop();
            E1.Stop();
            E2.Stop();
            E3.Stop();
            E4.Stop();
            E5.Stop();
            TeleportMenu.Stop();
            F1.Stop();
            F2.Stop();
            F3.Stop();
            F4.Stop();
            VisionMenu.Stop();
            G1.Stop();
            G2.Stop();
            G3.Stop();
            G4.Stop();
            G5.Stop();
            G6.Stop();
            OtherMenu.Stop();
            H1.Stop();
            PS3.Extension.WriteString(0x004eb39c, "^5Menu ^1Closed");
            toolStripStatusLabel2.Text = "Not Started !";
            toolStripStatusLabel2.ForeColor = Color.Red;
            simpleButton2.Enabled = true;
            simpleButton3.Enabled = false;
        }
        public static Boolean KeyBoardisOpen()
        {
            if (PS3.Extension.ReadByte(0x73145F) == 0x1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (KeyBoardisOpen() == true)
            {
                timer2.Stop();
            }
            else
            {
                timer2.Start();
            }
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.ihax.fr/forums");
        }
        private void A1_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5M^7ain ^5M^7enu\n\n^2-->^5Host Menu\nClient 1 Menu\nReset Menu\nAccount Menu\nTeleport Menu\nVision Menu\nCredit Menu\n\n\n^3Client 0 : ^2" + textBox1.Text + "\n^3Client 1 : ^2" + textBox2.Text + "^5");
            if (Key_IsDown((uint)0) == "X")
            {
                HostMenuMain.Stop();
                B1.Start();
            }
            if (Key_IsDown((uint)0) == "R1")
            {
                HostMenuMain.Stop();
                Client1.Start();
            }
            if (Key_IsDown((uint)0) == "R3 + L3")
            {
                HostMenuMain.Stop();
                B1.Stop();
                B2.Stop();
                B3.Stop();
                B4.Stop();
                B5.Stop();
                Client1.Stop();
                C1.Stop();
                C2.Stop();
                C3.Stop();
                C4.Stop();
                ResetMenu.Stop();
                D1.Stop();
                D2.Stop();
                D3.Stop();
                D4.Stop();
                D5.Stop();
                AccountMenu.Stop();
                E1.Stop();
                E2.Stop();
                E3.Stop();
                E4.Stop();
                E5.Stop();
                TeleportMenu.Stop();
                F1.Stop();
                F2.Stop();
                F3.Stop();
                F4.Stop();
                VisionMenu.Stop();
                G1.Stop();
                G2.Stop();
                G3.Stop();
                G4.Stop();
                G5.Stop();
                G6.Stop();
                OtherMenu.Stop();
                H1.Stop();
                PS3.Extension.WriteString(0x004eb39c, "^5Menu ^1Closed");
                simpleButton2.Enabled = true;
                toolStripStatusLabel2.Text = "Not Started !";
                toolStripStatusLabel2.ForeColor = Color.Red;
                simpleButton3.Enabled = false;
            }
        }

        private void B1_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5H^7ost ^5M^7enu\n\n^2-->^5God Mode\nMax Ammo\nAll Perks\nRapide Fire\nLock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                B1.Stop();
                B2.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x00, 0x15 };
                PS3.SetMemory(0x012272ea, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                B1.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                B1.Stop();
                B4.Start();
            }
        }

        private void B2_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5H^7ost ^5M^7enu\n\n^5God Mode\n^2-->^5Max Ammo\nAll Perks\nRapide Fire\nLock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                B2.Stop();
                B3.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0xFF, 0xFF, 0xFF };
                PS3.SetMemory(0x01227631, buffer);
                PS3.SetMemory(0x01227649, buffer);
                PS3.SetMemory(0x012276cd, buffer);
                PS3.SetMemory(0x012276d9, buffer);
                PS3.SetMemory(0x012276b5, buffer);
                PS3.SetMemory(0x012276c1, buffer);
                PS3.SetMemory(0x012276d9, buffer);
                PS3.SetMemory(0x012276e5, buffer);
                //
                byte[] buffer1 = new byte[] { 0xFF };
                PS3.SetMemory(0x0122778a, buffer1);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                B2.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                B2.Stop();
                B1.Start();
            }
        }

        private void B3_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5H^7ost ^5M^7enu\n\n^5God Mode\nMax Ammo\n^2-->^5All Perks\nRapide Fire\nLock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                B3.Stop();
                B4.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
                PS3.SetMemory(0x01227788, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                B3.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                B3.Stop();
                B2.Start();
            }
        }

        private void B4_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5H^7ost ^5M^7enu\n\n^5God Mode\nMax Ammo\nAll Perks\n^2-->^5Rapide Fire\nLock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                B4.Stop();
                B5.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0xFF };
                PS3.SetMemory(0x0122778b, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                B4.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                B4.Stop();
                B3.Start();
            }
        }

        private void B5_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5H^7ost ^5M^7enu\n\n^5God Mode\nMax Ammo\nAll Perks\nRapide Fire\n^2-->^5Lock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                B5.Stop();
                B1.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x01 };
                PS3.SetMemory(0x012272ef, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                B5.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                B5.Stop();
                B4.Start();
            }
        }

        private void Client1_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5M^7ain ^5M^7enu\n\n^5Host Menu\n^2-->^5Client 1 Menu\nReset Menu\nAccount Menu\nTeleport Menu\nVision Menu\nCredit Menu\n\n\n^3Client 0 : ^2" + textBox1.Text + "\n^3Client 1 : ^2" + textBox2.Text + "^5");
            if (Key_IsDown((uint)0) == "X")
            {
                Client1.Stop();
                C1.Start();
            }
            if (Key_IsDown((uint)0) == "R1")
            {
                 Client1.Stop();
                ResetMenu.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                Client1.Stop();
                HostMenuMain.Start();
            }
        }

        private void C1_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5C^7lient ^21 ^5M^7enu\n\n^2-->^5God Mode\nMax Ammo\nAll Perks\nRapide Fire\nLock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                C1.Stop();
                C2.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x00, 0x15 };
                PS3.SetMemory(0x012323b2, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                C1.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                C1.Stop();
                C5.Start();
            }
        }

        private void ResetMenu_Tick(object sender, EventArgs e)
        {
            
        }

        private void C2_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5C^7lient ^21 ^5M^7enu\n\nGod Mode\n^2-->^5Max Ammo\nAll Perks\nRapide Fire\nLock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                C2.Stop();
                C3.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0xFF, 0xFF, 0xFF };
                PS3.SetMemory(0x012326f9, buffer);
                PS3.SetMemory(0x01232711, buffer);
                PS3.SetMemory(0x01232771, buffer);
                PS3.SetMemory(0x01232795, buffer);
                PS3.SetMemory(0x0123277d, buffer);
                PS3.SetMemory(0x01232789, buffer);
                PS3.SetMemory(0x012327a1, buffer);
                PS3.SetMemory(0x012327ad, buffer);
                //
                byte[] buffer1 = new byte[] { 0xFF };
                PS3.SetMemory(0x0122778a, buffer1);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                C2.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                C2.Stop();
                C1.Start();
            }
        }

        private void C3_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5C^7lient ^21 ^5M^7enu\n\nGod Mode\nMax Ammo\n^2-->^5All Perks\nRapide Fire\nLock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                C3.Stop();
                C4.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
                PS3.SetMemory(0x01232850, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                C3.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                C3.Stop();
                C2.Start();
            }
        }

        private void C4_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5C^7lient ^21 ^5M^7enu\n\nGod Mode\nMax Ammo\nAll Perks\n^2-->^5Rapide Fire\nLock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                C4.Stop();
                C5.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
               byte[] buffer = new byte[] { 0xFF };
               PS3.SetMemory(0x01232853, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                C4.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                C4.Stop();
                C3.Start();
            }
        }

        private void C5_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5C^7lient ^21 ^5M^7enu\n\nGod Mode\nMax Ammo\nAll Perks\nRapide Fire\n^2-->^5Lock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                C5.Stop();
                C1.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x01 };
                PS3.SetMemory(0x012323b7, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                C5.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                C5.Stop();
                C4.Start();
            }
        }

        private void ResetMenu_Tick_1(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5M^7ain ^5M^7enu\n\n^5Host Menu\nClient 1 Menu\n^2-->^5Reset Menu\nAccount Menu\nTeleport Menu\nVision Menu\nCredit Menu\n\n\n^3Client 0 : ^2" + textBox1.Text + "\n^3Client 1 : ^2" + textBox2.Text + "^5");
            if (Key_IsDown((uint)0) == "X")
            {
                D1.Start();
                ResetMenu.Stop();
                D1.Start();
            }
            if (Key_IsDown((uint)0) == "R1")
            {
                ResetMenu.Stop();
                AccountMenu.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                ResetMenu.Stop();
                Client1.Start();
            }
        }

        private void D1_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5R^7eset ^5M^7enu\n\n^2-->^5God Mode\nMax Ammo\nAll Perks\nRapide Fire\nLock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                D1.Stop();
                D2.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x00, 0x00 };
                PS3.SetMemory(0x012272ea, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                D1.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                D1.Stop();
                D5.Start();
            }
        }

        private void D2_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5R^7eset ^5M^7enu\n\nGod Mode\n^2-->^5Max Ammo\nAll Perks\nRapide Fire\nLock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                D2.Stop();
                D3.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x00, 0x00, 0x00 };
                PS3.SetMemory(0x01227631, buffer);
                PS3.SetMemory(0x01227649, buffer);
                PS3.SetMemory(0x012276b5, buffer);
                PS3.SetMemory(0x012276c1, buffer);
                PS3.SetMemory(0x012276d9, buffer);
                PS3.SetMemory(0x012276e5, buffer);
                //
                byte[] buffer1 = new byte[] { 0x00 };
                PS3.SetMemory(0x0122778a, buffer1);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                D2.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                D2.Stop();
                D1.Start();
            }
        }

        private void D3_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5R^7eset ^5M^7enu\n\nGod Mode\nMax Ammo\n^2-->^5All Perks\nRapide Fire\nLock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                D3.Stop();
                D4.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                PS3.SetMemory(0x01227788, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                D3.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                D3.Stop();
                D2.Start();
            }
        }

        private void D4_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5R^7eset ^5M^7enu\n\nGod Mode\nMax Ammo\nAll Perks\n^2-->^5Rapide Fire\nLock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                D4.Stop();
                D5.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x00 };
                PS3.SetMemory(0x0122778b, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                D4.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                D4.Stop();
                D3.Start();
            }
        }

        private void D5_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5R^7eset ^5M^7enu\n\nGod Mode\nMax Ammo\nAll Perks\nRapide Fire\nn^2-->^5Lock Controller");
            if (Key_IsDown((uint)0) == "R1")
            {
                D5.Stop();
                D1.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x00 };
                PS3.SetMemory(0x012272ef, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                D5.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                D5.Stop();
                D4.Start();
            }
        }

        private void AccountMenu_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5M^7ain ^5M^7enu\n\n^5Host Menu\nClient 1 Menu\nReset Menu\n^2-->^5Account Menu\nTeleport Menu\nVision Menu\nCredit Menu\n\n\n^3Client 0 : ^2" + textBox1.Text + "\n^3Client 1 : ^2" + textBox2.Text + "^5");
            if (Key_IsDown((uint)0) == "X")
            {
                AccountMenu.Stop();
                E1.Start();
            }
            if (Key_IsDown((uint)0) == "R1")
            {
                AccountMenu.Stop();
                TeleportMenu.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                AccountMenu.Stop();
                ResetMenu.Start();
            }
        }

        private void E1_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5A^7ccount ^5M^7enu^5\n\n^2-->^5Level 50\nLevel 0\nLegit Stats\nHight Stats\nMax Stats");
            if (Key_IsDown((uint)0) == "R1")
            {
                E1.Stop();
                E2.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0xF4, 0x35, 0x21 };
                PS3.SetMemory(0x018814f1, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                E1.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                E1.Stop();
                E5.Start();
            }
        }

        private void E2_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5A^7ccount ^5M^7enu^5\n\nLevel 50\n^2-->^5Level 0\nLegit Stats\nHight Stats\nMax Stats");
            if (Key_IsDown((uint)0) == "R1")
            {
                E2.Stop();
                E3.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x00, 0x00, 0x00 };
                PS3.SetMemory(0x018814f1, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                E2.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                E2.Stop();
                E1.Start();
            }
        }

        private void E3_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5A^7ccount ^5M^7enu^5\n\nLevel 50\nLevel 0\n^2-->^5Legit Stats\nHight Stats\nMax Stats");
            if (Key_IsDown((uint)0) == "R1")
            {
                E3.Stop();
                E4.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x75, 0x30 };
                PS3.SetMemory(0x01881591, buffer);
                byte[] buffer1 = new byte[] { 0x14, 0x73 };
                PS3.SetMemory(0x01881595, buffer1);
                byte[] buffer2 = new byte[] { 0xEB };
                PS3.SetMemory(0x01881599, buffer2);
                byte[] buffer3 = new byte[] { 0x16, 0x03 };
                PS3.SetMemory(0x018815a5, buffer3);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                E3.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                E3.Stop();
                E2.Start();
            }
        }

        private void E4_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5A^7ccount ^5M^7enu^5\n\nLevel 50\nLevel 0\nLegit Stats\n^2-->^5Hight Stats\nMax Stats");
            if (Key_IsDown((uint)0) == "R1")
            {
                E4.Stop();
                E5.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x75, 0x30 };
                PS3.SetMemory(0x01881591, buffer);
                byte[] buffer1 = new byte[] { 0x14, 0x73 };
                PS3.SetMemory(0x01881595, buffer1);
                byte[] buffer2 = new byte[] { 0xEB };
                PS3.SetMemory(0x01881599, buffer2);
                byte[] buffer3 = new byte[] { 0x16, 0x03 };
                PS3.SetMemory(0x018815a5, buffer3);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                E4.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                E4.Stop();
                E3.Start();
            }
        }

        private void E5_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5A^7ccount ^5M^7enu^5\n\nLevel 50\nLevel 0\nLegit Stats\nHight Stats\n^2-->^5Max Stats");
            if (Key_IsDown((uint)0) == "R1")
            {
                E5.Stop();
                E1.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x75, 0x30 };
                PS3.SetMemory(0x01881591, buffer);
                byte[] buffer1 = new byte[] { 0x14, 0x73 };
                PS3.SetMemory(0x01881595, buffer1);
                byte[] buffer2 = new byte[] { 0xEB };
                PS3.SetMemory(0x01881599, buffer2);
                byte[] buffer3 = new byte[] { 0x16, 0x03 };
                PS3.SetMemory(0x018815a5, buffer3);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                E5.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                E5.Stop();
                E4.Start();
            }
        }

        private void OtherMenu_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5M^7ain ^5M^7enu\n\n^5Host Menu\nClient 1 Menu\nReset Menu\nAccount Menu\n^2-->^5Teleport Menu\nVision Menu\nCredit Menu\n\n\n^3Client 0 : ^2" + textBox1.Text + "\n^3Client 1 : ^2" + textBox2.Text + "^5");
            if (Key_IsDown((uint)0) == "X")
            {
                TeleportMenu.Stop();
                F1.Start();
            }
            if (Key_IsDown((uint)0) == "R1")
            {
                TeleportMenu.Stop();
                VisionMenu.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                TeleportMenu.Stop();
                AccountMenu.Start();
            }
        }

        private void F1_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5T^7eleport ^5M^7enu\n\n^2-->^5Kill\nDown the map\nBack to map\nSky");
            if (Key_IsDown((uint)0) == "R1")
            {
                F1.Stop();
                F2.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x44, 0x2E, 0xF9, 0x1A, 0x43, 0xCC, 0xE6, 0x66, 0xC4, 0x7D, 0xCA, 0x2F, 0x00 };
                PS3.SetMemory(0x012272F4, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                F1.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                F1.Stop();
                F4.Start();
            }
        }

        private void F2_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5T^7eleport ^5M^7enu^5\n\nKill\n^2-->^5Down the map\nBack to map\nSky");
            if (Key_IsDown((uint)0) == "R1")
            {
                F2.Stop();
                F3.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x43, 0xBB, 0xB8, 0x47, 0x42, 0x66, 0x0B, 0xFD, 0xC2, 0xD3, 0xC0, 0x02, 0x00 };
                PS3.SetMemory(0x012272F4, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                F2.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                F2.Stop();
                F1.Start();
            }
        }

        private void F3_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5T^7eleport ^5M^7enu^5\n\nKill\nDown the map\n^2-->^5Back to map\nSky");
            if (Key_IsDown((uint)0) == "R1")
            {
                F3.Stop();
                F4.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x44, 0x2E, 0xF0, 0x9A, 0x43, 0xCC, 0xE6, 0x66, 0x42, 0x8B, 0xC4, 0xB2, 0x00 };
                PS3.SetMemory(0x012272F4, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                F3.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                F3.Stop();
                F2.Start();
            }
        }

        private void F4_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5T^7eleport ^5M^7enu^5\n\nKill\nDown the map\nBack to map\n^2-->^5Sky");
            if (Key_IsDown((uint)0) == "R1")
            {
                F4.Stop();
                F1.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x44, 0x2E, 0xF9, 0x9A, 0x43, 0xCC, 0xE6, 0x66, 0x45, 0x0E, 0x63, 0xAD, 0x00 };
                PS3.SetMemory(0x012272F4, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                F4.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                F4.Stop();
                F3.Start();
            }
        }

        private void VisionMenu_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5M^7ain ^5M^7enu\n\n^5Host Menu\nClient 1 Menu\nReset Menu\nAccount Menu\nTeleport Menu\n^2-->^5Vision Menu\nCredit Menu\n\n\n^3Client 0 : ^2" + textBox1.Text + "\n^3Client 1 : ^2" + textBox2.Text + "^5");
            if (Key_IsDown((uint)0) == "X")
            {
                G1.Start();
                VisionMenu.Stop();
            }
            if (Key_IsDown((uint)0) == "R1")
            {
                VisionMenu.Stop();
                OtherMenu.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                VisionMenu.Stop();
                TeleportMenu.Start();
            }
        }

        private void G1_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5V^7ision ^5M^7enu\n\n^2-->^5Default\nSnake Mod\nHardcore Mod\nThermal\nRed Vision\nWorm Vision");
            if (Key_IsDown((uint)0) == "R1")
            {
                G1.Stop();
                G2.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x00, 0x00 };
                PS3.SetMemory(0x012272ea, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                G1.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                G1.Stop();
                G6.Start();
            }
        }

        private void G2_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5V^7ision ^5M^7enu\n\n^5Default\n^2-->^5Snake Mod\nHardcore Mod\nThermal\nRed Vision\nWorm Vision");
            if (Key_IsDown((uint)0) == "R1")
            {
                G2.Stop();
                G3.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x00, 0x12 };
                PS3.SetMemory(0x012272ea, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                G2.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                G2.Stop();
                G1.Start();
            }
        }

        private void G3_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5V^7ision ^5M^7enu\n\n^5Default\nSnake Mod\n^2-->^5Hardcore Mod\nThermal\nRed Vision\nWorm Vision");
            if (Key_IsDown((uint)0) == "R1")
            {
                G3.Stop();
                G4.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x40, 0x00 };
                PS3.SetMemory(0x012272ea, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                G3.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                G3.Stop();
                G2.Start();
            }
        }

        private void G4_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5V^7ision ^5M^7enu\n\n^5Default\nSnake Mod\nHardcore Mod\n^2-->^5Thermal\nRed Vision\nWorm Vision");
            if (Key_IsDown((uint)0) == "R1")
            {
                G4.Stop();
                G5.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x00, 0x99 };
                PS3.SetMemory(0x012272ea, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                G4.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                G4.Stop();
                G3.Start();
            }
        }

        private void G5_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5V^7ision ^5M^7enu\n\n^5Default\nSnake Mod\nHardcore Mod\nThermal\n^2-->^5Red Vision\nWorm Vision");
            if (Key_IsDown((uint)0) == "R1")
            {
                G5.Stop();
                G6.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0x90, 0x00 };
                PS3.SetMemory(0x012272ea, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                G5.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                G5.Stop();
                G4.Start();
            }
        }

        private void G6_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5V^7ision ^5M^7enu\n\n^5Default\nSnake Mod\nHardcore Mod\nThermal\nRed Vision\n^2-->^5Worm Vision");
            if (Key_IsDown((uint)0) == "R1")
            {
                G6.Stop();
                G1.Start();
            }
            if (Key_IsDown((uint)0) == "X")
            {
                byte[] buffer = new byte[] { 0xAA, 0x00 };
                PS3.SetMemory(0x012272ea, buffer);
            }
            if (Key_IsDown((uint)0) == "R3")
            {
                G6.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                G6.Stop();
                G5.Start();
            }
        }

        private void OtherMenu_Tick_1(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5M^7ain ^5M^7enu\n\n^5Host Menu\nClient 1 Menu\nReset Menu\nAccount Menu\nTeleport Menu\nVision Menu\n^2-->^5Credit Menu\n\n\n^3Client 0 : ^2" + textBox1.Text + "\n^3Client 1 : ^2" + textBox2.Text + "^5");
            if (Key_IsDown((uint)0) == "X")
            {
                H1.Start();
                OtherMenu.Stop();
            }
            if (Key_IsDown((uint)0) == "R1")
            {
                OtherMenu.Stop();
                HostMenuMain.Start();
            }
            if (Key_IsDown((uint)0) == "L1")
            {
                VisionMenu.Start();
                OtherMenu.Stop();
            }
        }

        private void H1_Tick(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x004eb39c, "^5S^7pec Ops ^5M^7od Menu ^5B^7y ^5M^7rNiato - ^5C^7redit  ^5M^7enu\n\nMango_Knife\nMakeabce\nMrNiato\niHax Team\nBoost4Ever Family");
            
            if (Key_IsDown((uint)0) == "R3")
            {
                H1.Stop();
                HostMenuMain.Start();
            }
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show("R3 To Open Menu\nL1 + R1 to navigate\nX to Validate\nR3 back to the main menu\nL3 + R3 To Exit Menu");
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (PS3.AttachProcess())
                {
                    byte[] buffer = new byte[] { 0x41, 0x00 };
                    PS3.SetMemory(0x00262b38, buffer);
                    PS3.Extension.WriteString(0x004eb39c, "Menu Closed");
                    groupControl2.Enabled = true;
                    label1.Text = "PS3 Connected | Attached";
                    label1.ForeColor = Color.Lime;
                    simpleButton4.Enabled = false;
                    simpleButton1.Enabled = false;
                }
                else
                {
                    groupControl2.Enabled = false;
                    XtraMessageBox.Show("Failed to Attach Process !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Failed to Attach Process !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }
    }
}
