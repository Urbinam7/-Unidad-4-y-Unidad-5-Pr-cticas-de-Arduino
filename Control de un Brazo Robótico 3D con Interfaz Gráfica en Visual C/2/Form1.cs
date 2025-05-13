using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace _2
{
    public partial class Form1 : Form
    {
        // ✅ Declaración global de arduino
        SerialPort arduino;

        List<byte[]> posicionesGuardadas = new List<byte[]>();

        public Form1()
        {
            InitializeComponent();

            // Inicializa el puerto serial
            arduino = new SerialPort("COM8", 115200);
            try
            {
                arduino.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el puerto COM8: " + ex.Message);
            }
        }

        private void trackBarBase_Scroll(object sender, EventArgs e)
        {
            SendServoCommand(1, (byte)trackBarBase.Value);
        }

        private void trackBarArt1_Scroll(object sender, EventArgs e)
        {
            SendServoCommand(2, (byte)trackBarArt1.Value);
        }

        private void trackBarArt2_Scroll(object sender, EventArgs e)
        {
            SendServoCommand(3, (byte)trackBarArt2.Value);
        }

        private void trackBarArt3_Scroll(object sender, EventArgs e)
        {
            SendServoCommand(4, (byte)trackBarArt3.Value);
        }

        private void trackBargripper_Scroll(object sender, EventArgs e)
        {
            SendServoCommand(5, (byte)trackBargripper.Value);
        }

        // Método que envía comando al Arduino
        private void SendServoCommand(byte id, byte angle)
        {
            if (arduino != null && arduino.IsOpen)
            {
                try
                {
                    byte[] data = new byte[] { id, angle };
                    arduino.Write(data, 0, data.Length);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Posiciones de origen sugeridas:
            byte baseStop = 90;       // Detener servo continuo
            byte artCenter = 90;      // Centro articulaciones
            byte gripperStart = 45;   // Semicerrado

            // Mueve los sliders visualmente
            trackBarBase.Value = baseStop;
            trackBarArt1.Value = artCenter;
            trackBarArt2.Value = artCenter;
            trackBarArt3.Value = artCenter;
            trackBargripper.Value = gripperStart;

            // Enviar comandos al Arduino
            SendServoCommand(1, baseStop);
            SendServoCommand(2, artCenter);
            SendServoCommand(3, artCenter);
            SendServoCommand(4, artCenter);
            SendServoCommand(5, gripperStart);
        }
        // BOTÓN: GUARDAR POSICIÓN ACTUAL
        private void button2_Click(object sender, EventArgs e)
        {
            byte[] nuevaPosicion = new byte[]
    {
        (byte)trackBarBase.Value,
        (byte)trackBarArt1.Value,
        (byte)trackBarArt2.Value,
        (byte)trackBarArt3.Value,
        (byte)trackBargripper.Value
    };

            posicionesGuardadas.Add(nuevaPosicion);
            MessageBox.Show("¡Posición guardada!");
        }
        // BOTÓN: REP POSICIÓN ACTUAL
        private void buttonreprovalores_Click(object sender, EventArgs e)
        {

            if (posicionesGuardadas.Count == 0)
            {
                MessageBox.Show("No hay posiciones guardadas.");
                return;
            }

            // Ejemplo: reproducir la PRIMERA posición guardada
            byte[] posicion = posicionesGuardadas[0];

            trackBarBase.Value = posicion[0];
            trackBarArt1.Value = posicion[1];
            trackBarArt2.Value = posicion[2];
            trackBarArt3.Value = posicion[3];
            trackBargripper.Value = posicion[4];

            SendServoCommand(1, posicion[0]);
            SendServoCommand(2, posicion[1]);
            SendServoCommand(3, posicion[2]);
            SendServoCommand(4, posicion[3]);
            SendServoCommand(5, posicion[4]);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
        
        }
    }
}
