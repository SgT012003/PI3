using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PI3
{
    public partial class Form1 : Form
    {
        string currentDirName = System.IO.Directory.GetCurrentDirectory();
        private SerialPort PI;
        private DateTime dateTime;
        private string in_data;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                fin.Enabled = false;
                MessageBox.Show("Name: Victor Didoff\r\nDiscord: Sg-T-(01)#0001", "Interface DIV");

            }
            catch (Exception ex4)
            {
                ini.Enabled = true;
                fin.Enabled = false;
                MessageBox.Show(ex4.Message, "Erro: C.1121" + "\r\n" + "Falha total da aplicacao" + "\r\n" + "reinicie e tente novamente" + "\r\n" + "Caso nada de certo: Reinstale o Programa");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ini_Click(object sender, EventArgs e)
        {
            PI = new SerialPort();
            PI.BaudRate = 115200;
            PI.PortName = textBox1.Text;
            PI.DataBits = 8;
            PI.DataReceived += PI_DataReceived;

            if (textBox3.Text == "115200")
            {

            } else if (textBox3.Text != "115200")
            {
                PI.BaudRate = Convert.ToInt16(textBox3.Text);
            }

            try
            {
                ini.Enabled = false;
                fin.Enabled = true;
                PI.Open();
            }
            catch (Exception ex)
            {
                ini.Enabled = true;
                fin.Enabled = false;
                MessageBox.Show(ex.Message, "Acesso Negado a porta " + PI.PortName);
            }
        }

        private void PI_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            in_data = PI.ReadLine();

            this.Invoke(new EventHandler(displaydata_event));
        }

        private void displaydata_event(object? sender, EventArgs e)
        {
            textBox2.AppendText(in_data + "\r\n");
        }

        private void fin_Click(object sender, EventArgs e)
        {
            try
            {
                ini.Enabled = true;
                fin.Enabled = false;
                PI.Close();
            }
            catch (Exception ex2)
            {
                ini.Enabled = false;
                fin.Enabled = true;
                MessageBox.Show(ex2.Message, "A porta " + PI.PortName + " não esta aberta");
            }
        }

        private void sav_Click(object sender, EventArgs e)
        {
            try
            {
                string log = @currentDirName + "\\";
                string arquivo = "log.txt";
                System.IO.File.WriteAllText(log + arquivo, textBox2.Text);
                MessageBox.Show("Salvo em " + currentDirName + "\\" + arquivo);
            }
            catch (Exception ex3)
            {
                MessageBox.Show(ex3.Message, "O arquivo nao pode ser Salvo em " + currentDirName);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
