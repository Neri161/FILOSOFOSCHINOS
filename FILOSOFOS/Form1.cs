using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FILOSOFOS
{
    public partial class Form1 : Form
    {
        static int[] palillos = { 0, 0, 0, 0, 0 };  //Semaforo para utilizacion de palillos 
        static int[] filosofos = { 0, 0, 0, 0, 0 }; //semaforo para filosofos (pensando = 0 y comiendo = 1)
        static int[] conador = { 0, 0, 0, 0, 0 }; //contador de platos de los filosofos 
        static bool play = true; //variable punto de ruptura para pausa
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //variable para para deshabilitar el check de hilos 
            btnTerminar.Enabled = false; //deshabilitar boton Terminar 
        }
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            play = true; //variable para iniciar los ciclos
            // creacion de hilos 
            int n = 0;
            for (n = 0; n < 5; n++)
                new Thread(comer).Start(n);

            btnIniciar.Enabled = false; //deshabilitar boton Iniciar 
            btnTerminar.Enabled = true; //habilitar boton Terminar
        }

        //El algoritmo de Dekker es un algoritmo de programación concurrente para 
        //exclusión mutua, que permite a dos procesos o hilos de ejecución compartir 
        //un recurso sin conflictos.Fue uno de los primeros algoritmos de exclusión 
        //mutua inventados, implementado por Edsger Dijkstra.

        void comer(object filosofo) //algoritmo de Dekker
        {
            int izq, der; //instancia variables derecha e izquierda
            izq = (int)filosofo; // variable lado izquierdo 
            der = verificar((int)filosofo); // ejecutar metodo para saber el lado derecho
            while (play == true)//inicio de ciclo
            {
                //soltar palillos
                if (filosofos[(int)filosofo] == 1)
                {
                    palillos[izq] = 0; //soltar palillo izquierdo
                    palillos[der] = 0; //soltar palillo derecho
                    filosofos[(int)filosofo] = 0; //poner filosofo a pensar
                    foto2((int)filosofo); //cambiar graficamente a los filosofos
                }
                //toma palillos y come
                if ((palillos[izq] == 0) && (palillos[der] == 0))
                {
                    palillos[izq] = 1; //tomar palillo izquierdo
                    palillos[der] = 1; //tomar palillo derecho
                    Thread.Sleep(500); // "COMER" dormir palillos
                    filosofos[(int)filosofo] = 1; //comer filosofo
                    conador[(int)filosofo] += 1; //aumentar plato
                    foto((int)filosofo); //cambiar graficamente a los filosofos
                }
                Thread.Sleep(100);//pensar filosofo
            }
        }
        int verificar(int n)
        {
            if ((n + 1) > 4) //verificar si n es mayor a 5 si lo es retorna 0
            {
                return 0;
            }
            return n + 1;//retornar n + 1
        }
        void foto(int n)
        {
            if (n == 0)
            {
                //tomar palillos
                pbpalillo1.BackgroundImage = FILOSOFOS.Properties.Resources.palilloUso;
                pbpalillo5.BackgroundImage = FILOSOFOS.Properties.Resources.palilloUso;
                //Chino Comer 
                pbChino1.BackgroundImage = FILOSOFOS.Properties.Resources.chinoComiendo;
                //agregar record
                txtChino1.Text = conador[n].ToString();
            }
            if (n == 1)
            {
                //tomar palillos
                pbpalillo1.BackgroundImage = FILOSOFOS.Properties.Resources.palilloUso;
                pbpalillo2.BackgroundImage = FILOSOFOS.Properties.Resources.palilloUso;
                //Chino Comer 
                pbChino2.BackgroundImage = FILOSOFOS.Properties.Resources.chinoComiendo;
                //agregar record
                txtChino2.Text = conador[n].ToString();
            }
            if (n == 2)
            {
                //tomar palillos
                pbpalillo2.BackgroundImage = FILOSOFOS.Properties.Resources.palilloUso;
                pbpalillo3.BackgroundImage = FILOSOFOS.Properties.Resources.palilloUso;
                //Chino Comer 
                pbChino3.BackgroundImage = FILOSOFOS.Properties.Resources.chinoComiendo;
                //agregar record
                txtChino3.Text = conador[n].ToString();
            }
            if (n == 3)
            {
                //tomar palillos
                pbpalillo3.BackgroundImage = FILOSOFOS.Properties.Resources.palilloUso;
                pbpalillo4.BackgroundImage = FILOSOFOS.Properties.Resources.palilloUso;
                //Chino Comer 
                pbChino4.BackgroundImage = FILOSOFOS.Properties.Resources.chinoComiendo;
                //agregar record
                txtChino4.Text = conador[n].ToString();
            }
            if (n == 4)
            {
                //tomar palillos
                pbpalillo4.BackgroundImage = FILOSOFOS.Properties.Resources.palilloUso;
                pbpalillo5.BackgroundImage = FILOSOFOS.Properties.Resources.palilloUso;
                //Chino Comer 
                pbChino5.BackgroundImage = FILOSOFOS.Properties.Resources.chinoComiendo;
                //agregar record
                txtChino5.Text = conador[n].ToString();
            }
        }
        void foto2(int n)
        {
            if (n == 0)
            {
                //chino pensando
                pbChino1.BackgroundImage = FILOSOFOS.Properties.Resources.chino;
                //soltar palillos
                pbpalillo1.BackgroundImage = FILOSOFOS.Properties.Resources.palillo;
                pbpalillo5.BackgroundImage = FILOSOFOS.Properties.Resources.palillo;
            }
            if (n == 1)
            {
                //soltar palillos
                pbpalillo1.BackgroundImage = FILOSOFOS.Properties.Resources.palillo;
                pbpalillo2.BackgroundImage = FILOSOFOS.Properties.Resources.palillo;
                //chino pensando
                pbChino2.BackgroundImage = FILOSOFOS.Properties.Resources.chino;
            }
            if (n == 2)
            {
                //soltar palillos
                pbpalillo2.BackgroundImage = FILOSOFOS.Properties.Resources.palillo;
                pbpalillo3.BackgroundImage = FILOSOFOS.Properties.Resources.palillo;
                //chino pensando
                pbChino3.BackgroundImage = FILOSOFOS.Properties.Resources.chino;
            }
            if (n == 3)
            {
                //soltar palillos
                pbpalillo3.BackgroundImage = FILOSOFOS.Properties.Resources.palillo;
                pbpalillo4.BackgroundImage = FILOSOFOS.Properties.Resources.palillo;
                //chino pensando
                pbChino4.BackgroundImage = FILOSOFOS.Properties.Resources.chino;
            }
            if (n == 4)
            {
                //soltar palillos
                pbpalillo4.BackgroundImage = FILOSOFOS.Properties.Resources.palillo;
                pbpalillo5.BackgroundImage = FILOSOFOS.Properties.Resources.palillo;
                //chino pensando
                pbChino5.BackgroundImage = FILOSOFOS.Properties.Resources.chino;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            play = false; //poner interrupcion en los procesos 
            Thread.Sleep(1000);//pausar hilo por 1 seg
            for (int i = 0; i < palillos.Length; i++)
                palillos[i] = 0;//soltar todos los palillos
            for (int i = 0; i < filosofos.Length; i++)
            {
                filosofos[i] = 0; //poner a pensar filosofos 
                foto2(i); //hacer graficamente 
            }
            btnIniciar.Enabled = true; //habilitar boton iniciar
            btnIniciar.Text = "Continuar"; //renombrar boton iniciar a continuar
            btnTerminar.Enabled = false; //deshabilitar boton terminar
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            play = false; //hacer punto de interrupcion
            Thread.Sleep(1000); //pausar hilo 1s
            MessageBox.Show("Sayounara Senpai 💔"); //mensaje de despedida 
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"El problema de la cena de los filósofos o problema de los filósofos cenando (dining philosophers problem) es un problema clásico de las ciencias de la computación propuesto por Edsger Dijkstra en 1965 para representar el problema de la sincronización de procesos en un sistema operativo. Se trata de lanzar cinco procesos (filósofos) y ponerles a competir por obtener unos recursos. La solución consiste en configurar para que estos procesos (filósofos) puedan acceder a los recursos (dos tenedores) y desarrollar el trabajo (puedan comer). El problema es que el algoritmo de solución debe ser justo y no debe permitir que uno de ellos ocupe todo el sistema y no deje comer a los demás o que entre ellos se bloqueen y ninguno pueda tener acceso paralizando todo el trabajo.", "MAS INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
