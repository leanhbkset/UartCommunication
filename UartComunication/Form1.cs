using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace ComputerToArduino
{
    public partial class Form1 : Form

    {
        bool isConnected = false;
        String[] ports;
        SerialPort port;
        byte[] data;
        byte[] patterns = new byte[] {0x01, 0x02, 0x03, 0x04, 0x05 };
        byte[] side = new byte[] {0x01, 0x02 };
        byte[] strength = new byte[] {0x01, 0x02, 0x03, 0x04, 0x05 };

        public Form1()
        {
            InitializeComponent();
            disableControls();
            getAvailableComPorts();

            data = new byte[5];

            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
                Console.WriteLine(port);
                if (ports[0] != null)
                {
                    btnConnect.Enabled = true;
                    comboBox1.SelectedItem = ports[0];
                }
                else
                {
                    btnConnect.Enabled = false;
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                connect();
            } else
            {
                disconnect();
            }
        }

        void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        private void connect()
        {
            data[0] = 0x43;
            data[1] = 0x31;
            data[2] = 0x31;
            data[3] = 0x31;
            data[4] = 0x00;

            isConnected = true;
            string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
            port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
            if (ports[0] != null)
            {
                port.Open();
                //port.Write("#STAR\n");
                btnConnect.Text = "Disconnect";
                enableControls();
            }
        }


        private void disconnect()
        {
            isConnected = false;
            //port.Write("#STOP\n");
            port.Close();
            btnConnect.Text = "Connect";
            disableControls();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                txtDataSending.Text = data[0].ToString("X") + " " + data[1].ToString("X") + " "
                    + data[2].ToString("X") + " " + data[3].ToString("X") + " "
                    + data[4].ToString("X");

                port.Write(data, 0, data.Length);
            }
        }

        private void enableControls()
        {
            comboBoxPattern.Enabled = true;
            comboBoxSide.Enabled = true;
            comboBoxStrength.Enabled = true;
            btnWrite.Enabled = true;
            groupBox3.Enabled = true;
            comboBoxPattern.Text = "Pattern 1";
            comboBoxSide.Text = "Left";
            comboBoxStrength.Text = "1";

        }

        private void disableControls()
        {
            comboBoxPattern.Enabled = false;
            comboBoxSide.Enabled = false;
            comboBoxStrength.Enabled = false;
            btnWrite.Enabled = false;
            groupBox3.Enabled = false;
            comboBoxPattern.Text = "";
            comboBoxSide.Text = "";
            comboBoxStrength.Text = "";
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnConnect.Enabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            isConnected = false;
            btnConnect.Text = "Connect";
            disableControls();

            disableControls();
            getAvailableComPorts();

            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
                Console.WriteLine(port);
                if (ports[0] != null)
                {
                    btnConnect.Enabled = true;
                    comboBox1.SelectedItem = ports[0];
                }
                else
                {
                    btnConnect.Enabled = false;
                    comboBox1.Text = "";
                }
            }
        }

        private void comboBoxPattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedPatternIndex = comboBoxPattern.SelectedIndex;

            switch (selectedPatternIndex)
            {
                case 0:
                    data[1] = patterns[0];
                    break;
                case 1:
                    data[1] = patterns[1];
                    break;
                case 2:
                    data[1] = patterns[2];
                    break;
                case 3:
                    data[1] = patterns[3];
                    break;
                case 4:
                    data[1] = patterns[4];
                    break;
                default:
                    data[1] = patterns[0];
                    break;
            }
        }

        private void comboBoxSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedSideIndex = comboBoxSide.SelectedIndex;
            switch (selectedSideIndex)
            {
                case 0:
                    data[2] = side[0];
                    break;
                case 1:
                    data[2] = side[1];
                    break;
                default:
                    data[2] = side[0];
                    break;
            }
        }

        private void comboBoxStrength_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedStrengthIndex = comboBoxStrength.SelectedIndex;
            switch(selectedStrengthIndex)
            {
                case 0:
                    data[3] = strength[0];
                    break;
                case 1:
                    data[3] = strength[1];
                    break;
                case 2:
                    data[3] = strength[2];
                    break;
                case 3:
                    data[3] = strength[3];
                    break;
                case 4:
                    data[3] = strength[4];
                    break;
                default:
                    data[3] = strength[0];
                    break;
            }
        }
    }
}
