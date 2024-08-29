using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AOEPatch
{
    public partial class Form1 : Form
    {
        public int patched = 0;
        public string aoepath;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //folderBrowserDialog1.ShowDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                aoepath = path;
            }
            textBox1.Text = aoepath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ttf = 0;
            if(File.Exists(aoepath + "\\EMPIRES.EXE"))
            {
                ttf += 1;
            }
            else
            {
                MessageBox.Show("Không phát hiện file game: 'EMPIRES.EXE' tại, vui lòng thử lại!");
            }
            if (File.Exists(aoepath + "\\EMPIRESX.EXE"))
            {
                ttf += 1;
            }
            else
            {
                MessageBox.Show("Không phát hiện file game: 'EMPIRESX.EXE', vui lòng thử lại!");
            }
            if (ttf == 2)
            {
                patchaoe();
            }    
        }
        void patchaoe()
        {
            patchaoed();
            patchaoex();
            MessageBox.Show("Đã patch thành công AOE!!!");
        }
        void patchaoed()
        {
            using (var stream = new FileStream(aoepath + "\\EMPIRES.EXE", FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = 951407;
                stream.WriteByte(0x33);
                stream.Position = 951408;
                stream.WriteByte(0xC0);
                stream.Position = 951409;
                stream.WriteByte(0x40);
                stream.Position = 951410;
                stream.WriteByte(0x90);
            }
            patched += 1;
        }
        void patchaoex()
        {
            using (var stream = new FileStream(aoepath + "\\EMPIRESX.EXE", FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = 102544;
                stream.WriteByte(0x90);
                stream.Position = 102545;
                stream.WriteByte(0x90);
            }
            patched += 1;
        }

        private void Estbt_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
