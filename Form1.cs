using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dsm3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[,] main, temp;
        double[,] per;
        int[] pert;

        static int MAX_O = 2; //TODO rename

        //TODO initiliazie road info in one method to all buttons

        //TODO fix
        private int [,] createRoadLanes() 
        {
            // for (int o = 1; o < MAX_O; o++)//цикл для выделенной полосы
            // {
            //     for (int i = 0; i < publicTransportAmount; i++)
            //     {
            //         for (int k = 0; k < 2; k++)
            //         {
            //             main[tb + k, o] = numb;
            //             main[tb + k, o] = numb;
            //         }
            //         int dstL2 = rnd.Next(3, maxdstB + 1);
            //         tb = tb + dstL2;
            //         numb++;
            //     }
            //     tb = 0;
            // }
            return new int[0,0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int P = Convert.ToInt32(textBox1.Text);
            int roadLanesCount = Convert.ToInt32(textBox1.Text)+2;
            int roadLength = Convert.ToInt32(textBox2.Text) / 5;
            double rho = Convert.ToDouble(textBox3.Text);//плотность
            double mu = Convert.ToDouble(textBox5.Text);//мю
            int v = Convert.ToInt32(textBox4.Text);
            int carsL = Convert.ToInt32(Math.Round(roadLength / 2 * rho));
            int maxdstL = Convert.ToInt32(roadLength / carsL);
            int publicTransportAmount = Convert.ToInt32(Math.Round(roadLength / 4 * rho));
            int maxdstB = Convert.ToInt32(roadLength / publicTransportAmount);
            var rand = new Random();
            main = new int[roadLength, roadLanesCount];
            int numb = 2;

            int t = 2;
            int tb = 3;
            for (int i = 0; i < roadLength; i++)
            {
                main[i, 0] = 1;
                main[i, roadLanesCount-1] = 1;
            }
            //TODO replace cycle to main = createRoadLanes();
            for (int o = 1; o < 2; o++)//цикл для выделенной полосы
            {
                for (int i = 0; i < publicTransportAmount; i++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        main[tb + k, o] = numb;
                        main[tb + k, o] = numb;
                    }
                    int dstL2 = rnd.Next(3, maxdstB + 1);
                    tb = tb + dstL2;
                    numb++;
                }
                tb = 0;
            }

            for (int o = 2; o < roadLanesCount-1; o++)//легковые машины
            {
                for (int i = 0; i < carsL; i++)
                {

                    for (int k = 0; k < 1; k++)
                    {
                        main[t + k, o] = numb;
                        main[t + k, o] = numb;
                    }
                    int dstL = rnd.Next(2, maxdstL + 1);
                    t = t + dstL;
                    numb++;
                }
                t = 0;
            }

            per = new double[publicTransportAmount + carsL * (P - 1), 3];
            for (int i = 0; i < publicTransportAmount + carsL * (P - 1); i++)
            {
                per[i, 0] = i + 2;
                per[i, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                if (i < publicTransportAmount)
                {
                    per[i, 2] = 1;
                }
                else 
                {
                    per[i, 2] = 2;
                }
                
            }
            temp = main;
            dataGridView1.RowCount = main.GetLength(0);
            dataGridView1.ColumnCount = main.GetLength(1);
            for (int i = 0; i < main.GetLength(0); i++)
            {
                for (int j = 0; j < main.GetLength(1); j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = main[i, j];
                    if (main[i, j] != 0)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Black;
                    }
                }
            }
            dataGridView1.AutoResizeColumns();

            dataGridView3.RowCount = per.GetLength(0);
            dataGridView3.ColumnCount = per.GetLength(1);
            for (int i = 0; i < per.GetLength(0); i++)
            {
                for (int j = 0; j < per.GetLength(1); j++)
                {
                    dataGridView3.Rows[i].Cells[j].Value = per[i, j];
                }
            }
            dataGridView3.AutoResizeColumns();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int u = 0; u < Convert.ToInt32(textBox6.Text); u++)
            {
                int P = Convert.ToInt32(textBox1.Text);
                int roadLanesCount = Convert.ToInt32(textBox1.Text) + 2;//полосы
                int roadLength = Convert.ToInt32(textBox2.Text) / 5;//длина
                double rho = Convert.ToDouble(textBox3.Text);//плотность
                double mu = Convert.ToDouble(textBox5.Text);//мю
                int v = Convert.ToInt32(textBox4.Text) / 5;
                int carsL = Convert.ToInt32(Math.Round(roadLength / 2 * rho));
                int maxdstL = Convert.ToInt32(roadLength / carsL);
                int publicTransportAmount = Convert.ToInt32(Math.Round(roadLength / 4 * rho));
                int maxdstB = Convert.ToInt32(roadLength / publicTransportAmount);
                var rand = new Random();
                int t = 0;
                int publicTransportCounter = 0;
                int a = 0;
                pert = new int[publicTransportAmount + carsL * (P - 1)];

                for (int i = 0; i < pert.Length; i++)
                {
                    pert[i] = Convert.ToInt32(per[i, 0]);
                }

                int[,] temp2 = new int[roadLength, roadLanesCount];
                for (int i = 0; i < roadLength; i++)
                {
                    temp2[i, 0] = 1;
                    temp2[i, roadLanesCount - 1] = 1;
                }

                for (int i = 0; i < roadLength; i++)
                {
                    for (int j = 0; j < roadLanesCount; j++)
                    {
                        t = Array.IndexOf(pert, temp[i, j]);
                        if (t != -1)
                        {
                            int iplusodin = (i + 1) % temp.GetLength(0);
                            int iplusv = (i + v) % temp.GetLength(0);
                            int iplusvplusodin = (i + v + 1) % temp.GetLength(0);
                            if (temp[i, j] == per[t, 0])
                            {
                                if (per[t, 2] == 1)
                                {
                                    if (temp[iplusodin, j] == per[t, 0] && per[t, 1] == 0)//если подошло время попытаться перестроиться ГРУЗОВОЙ машине
                                    {
                                        if (temp[i, j + 1] == 0 && temp[iplusodin, j + 1] == 0 && temp[iplusv, j + 1] == 0 && temp[iplusvplusodin, j + 1] == 0)
                                        {
                                            temp2[iplusv, j + 1] = Convert.ToInt32(per[t, 0]);
                                            temp2[iplusvplusodin, j + 1] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            publicTransportCounter++;
                                        }
                                        else if (temp[i, j - 1] == 0 && temp[iplusodin, j - 1] == 0 && temp[iplusv, j - 1] == 0 && temp[iplusvplusodin, j - 1] == 0)
                                        {
                                            temp2[iplusv, j - 1] = Convert.ToInt32(per[t, 0]);
                                            temp2[iplusvplusodin, j - 1] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            publicTransportCounter++;
                                        }
                                        else
                                        {
                                            temp2[iplusv, j] = Convert.ToInt32(per[t, 0]);
                                            temp2[iplusvplusodin, j] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                        }
                                    }
                                    else if (temp[(i) % temp.GetLength(0), j] == per[t, 0] && temp[iplusodin, j] == per[t, 0])
                                    {
                                        temp2[iplusv, j] = Convert.ToInt32(per[t, 0]);
                                        temp2[iplusvplusodin, j] = Convert.ToInt32(per[t, 0]);
                                        per[t, 1]--;
                                        if (per[t, 1] < 0)
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble())); //костыль от -1 по времени
                                    }
                                }

                                if (per[t, 2] == 2)
                                {
                                    if (per[t, 1] == 0)//если подошло время попытаться перестроиться ЛЕГКОВОВЙ машине
                                    {
                                        if (temp[(i) % temp.GetLength(0), j + 1] == 0 && temp[iplusv, j + 1] == 0)
                                        {
                                            temp2[iplusv, j + 1] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            a++;
                                        }
                                        else if (temp[(i) % temp.GetLength(0), j - 1] == 0 && temp[iplusv, j - 1] == 0)
                                        {
                                            temp2[iplusv, j - 1] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            a++;
                                        }
                                        else
                                        {
                                            temp2[iplusv, j] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                        }
                                    }
                                    else if (temp[(i) % temp.GetLength(0), j] == per[t, 0])
                                    {
                                        temp2[iplusv, j] = Convert.ToInt32(per[t, 0]);
                                        per[t, 1]--;
                                        if (per[t, 1] < 0)
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));//костыль от -1 по времени
                                    }
                                    else
                                    {
                                        MessageBox.Show("ЛЕГКОВОВЙ" + (t + 2).ToString());
                                    }
                                }
                            }
                        }

                        ////////////////////////////////////////////////////////////////////////////////////////////////
                        /*
                        else if (i == roadLength - 1)
                        {
                            t = Array.IndexOf(pert, temp[i, j]);

                            if (t != -1)
                            {
                                if (temp[i, j] == per[t, 0])
                                {
                                    if (per[t, 2] == 1)
                                    {
                                        if (temp[i + 1, j] == per[t, 0] && per[t, 1] == 0)//если подошло время попытаться перестроиться ГРУЗОВОЙ машине
                                        {
                                            if (temp[i, j + 1] == 0 && temp[i + 1, j + 1] == 0 && temp[i + v, j + 1] == 0 && temp[i + v + 1, j + 1] == 0)
                                            {
                                                temp2[i + v, j + 1] = Convert.ToInt32(per[t, 0]);
                                                temp2[i + v + 1, j + 1] = Convert.ToInt32(per[t, 0]);
                                                per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                                publicTransportCounter++;
                                            }
                                            else if (temp[i, j - 1] == 0 && temp[i + 1, j - 1] == 0 && temp[i + v, j - 1] == 0 && temp[i + v + 1, j - 1] == 0)
                                            {
                                                temp2[i + v, j - 1] = Convert.ToInt32(per[t, 0]);
                                                temp2[i + v + 1, j - 1] = Convert.ToInt32(per[t, 0]);
                                                per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                                publicTransportCounter++;
                                            }
                                            else
                                            {
                                                temp2[i + v, j] = Convert.ToInt32(per[t, 0]);
                                                temp2[i + v + 1, j] = Convert.ToInt32(per[t, 0]);
                                                per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            }
                                        }
                                        else if (temp[i, j] == per[t, 0] && temp[i + 1, j] == per[t, 0])
                                        {
                                            temp2[i + v, j] = Convert.ToInt32(per[t, 0]);
                                            temp2[i + v + 1, j] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1]--;
                                            if (per[t, 1] < 0)
                                                per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble())); //костыль от -1 по времени
                                        }
                                    }

                                    if (per[t, 2] == 2)
                                    {
                                        if (per[t, 1] == 0)//если подошло время попытаться перестроиться ЛЕГКОВОВЙ машине
                                        {
                                            if (temp[i, j + 1] == 0 && temp[i + v, j + 1] == 0)
                                            {
                                                temp2[i + v, j + 1] = Convert.ToInt32(per[t, 0]);
                                                per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                                a++;
                                            }
                                            else if (temp[i, j - 1] == 0 && temp[i + v, j - 1] == 0)
                                            {
                                                temp2[i + v, j - 1] = Convert.ToInt32(per[t, 0]);
                                                per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                                a++;
                                            }
                                            else
                                            {
                                                temp2[i + v, j] = Convert.ToInt32(per[t, 0]);
                                                per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            }
                                        }
                                        else if (temp[i, j] == per[t, 0])
                                        {
                                            temp2[i + v, j] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1]--;
                                            if (per[t, 1] < 0)
                                                per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));//костыль от -1 по времени
                                        }
                                        else
                                        {
                                            MessageBox.Show("ЛЕГКОВОВЙ" + (t + 2).ToString());
                                        }
                                    }
                                }
                            }
                        }
                        */

                    }
                }

                textBox7.Text = textBox7.Text + ";" + Convert.ToString(publicTransportCounter);
                textBox8.Text = textBox8.Text + ";" + Convert.ToString(a);

                temp = temp2;
            }
            dataGridView2.RowCount = temp.GetLength(0);
            dataGridView2.ColumnCount = temp.GetLength(1);
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = temp[i, j];
                    if (temp[i, j] != 0)
                    {
                        dataGridView2.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Black;
                    }
                }
            }
            dataGridView2.AutoResizeColumns();

            dataGridView3.RowCount = per.GetLength(0);
            dataGridView3.ColumnCount = per.GetLength(1);
            for (int i = 0; i < per.GetLength(0); i++)
            {
                for (int j = 0; j < per.GetLength(1); j++)
                {
                    dataGridView3.Rows[i].Cells[j].Value = per[i, j];

                }
            }
            dataGridView3.AutoResizeColumns();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();

            int P = Convert.ToInt32(textBox1.Text);
            int roadLanesCount = Convert.ToInt32(textBox1.Text)+2;
            int roadLength = Convert.ToInt32(textBox2.Text) / 5;
            double rho = Convert.ToDouble(textBox3.Text);//плотность
            double mu = Convert.ToDouble(textBox5.Text);//мю
            int v = Convert.ToInt32(textBox4.Text) / 5;
            int carsL = Convert.ToInt32(Math.Round(roadLength / 2 * rho));
            int publicTransportAmount = Convert.ToInt32(Math.Round(roadLength / 4 * rho));
            var rand = new Random();
            int t;
            int publicTransportCounter = 0;
            int a = 0;
            pert = new int[publicTransportAmount + carsL * (P - 1)];

            for (int i = 0; i < pert.Length; i++)
            {
                pert[i] = Convert.ToInt32(per[i,0]);
            }

            int[,] temp2 = new int[roadLength, roadLanesCount];
            for (int i = 0; i < roadLength; i++)
            {
                temp2[i, 0] = 1;
                temp2[i, roadLanesCount - 1] = 1;
            }

            for (int i = 0; i < roadLength; i++)
            {
                for (int j = 0; j < roadLanesCount; j++)
                {
                        t = Array.IndexOf(pert, temp[i, j]);
                        if (t != -1)//
                        {
                        int iplusodin = (i + 1) % temp.GetLength(0);
                        int iplusv= (i + v) % temp.GetLength(0);
                        int iplusvplusodin = (i + v + 1) % temp.GetLength(0);
                            //if (temp[i, j] == per[t, 0]) { //если текущий элемент массива является транспортным средством
                                if (per[t, 2] == 1) { //если это грузова машина
                                    if (temp[iplusodin, j] == per[t, 0] && per[t, 1] == 0)//если подошло время попытаться перестроиться ГРУЗОВОЙ машине
                                    {
                                        if (temp[i, j + 1] == 0 && temp[iplusodin, j + 1] == 0 && temp[iplusv, j + 1] == 0 && temp[iplusvplusodin, j + 1] == 0)
                                        {
                                            temp2[iplusv, j + 1] = Convert.ToInt32(per[t, 0]);
                                            temp2[iplusvplusodin, j + 1] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            publicTransportCounter++;
                                    MessageBox.Show(temp.GetLength(0).ToString());
                                        }
                                        else if (temp[i, j - 1] == 0 && temp[iplusodin, j - 1] == 0 && temp[iplusv, j - 1] == 0 && temp[iplusvplusodin, j - 1] == 0)
                                        {
                                            temp2[iplusv, j - 1] = Convert.ToInt32(per[t, 0]);
                                            temp2[iplusvplusodin, j - 1] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            publicTransportCounter++;
                                        }
                                        else
                                        {
                                            temp2[iplusv, j] = Convert.ToInt32(per[t, 0]);
                                            temp2[iplusvplusodin, j] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                        }
                                    }
                                    else if (temp[i, j] == per[t, 0] && temp[iplusodin, j] == per[t, 0])
                                    {
                                        temp2[iplusv, j] = Convert.ToInt32(per[t, 0]);
                                        temp2[iplusvplusodin, j] = Convert.ToInt32(per[t, 0]);
                                        per[t, 1]--;
                                        if (per[t, 1] < 0)
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble())); //костыль от -1 по времени
                                    }
                                }

                                if (per[t, 2] == 2) { 
                                    if  (per[t, 1] == 0)//если подошло время попытаться перестроиться ЛЕГКОВОВЙ машине
                                    {
                                        if (temp[i, j + 1] == 0  && temp[iplusv, j + 1] == 0)
                                        {
                                            temp2[iplusv, j + 1] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            a ++;
                                        }
                                        else if (temp[i, j - 1] == 0  && temp[iplusv, j - 1] == 0)
                                        {
                                            temp2[iplusv, j - 1] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            a ++;
                                        }
                                        else
                                        {
                                            temp2[iplusv, j] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                        }
                                    }
                                    else if (temp[i, j] == per[t, 0])
                                    {
                                        temp2[iplusv, j] = Convert.ToInt32(per[t, 0]);
                                        per[t, 1]--;
                                        if (per[t, 1] < 0)
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));//костыль от -1 по времени
                                    }
                                    else
                                    {
                                        MessageBox.Show("ЛЕГКОВОВЙ" + (t+2).ToString());
                                    }
                                }
                        //}
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////
                    /*
                    else if (i == roadLength - 1)
                    {
                        t = Array.IndexOf(pert, temp[i, j]);
                        
                        if (t != -1)
                        {
                            if (temp[i, j] == per[t, 0])
                            {
                                if (per[t, 2] == 1)
                                {
                                    if (temp[i + 1, j] == per[t, 0] && per[t, 1] == 0)//если подошло время попытаться перестроиться ГРУЗОВОЙ машине
                                    {
                                        if (temp[i, j + 1] == 0 && temp[i + 1, j + 1] == 0 && temp[i + v, j + 1] == 0 && temp[i + v + 1, j + 1] == 0)
                                        {
                                            temp2[i + v, j + 1] = Convert.ToInt32(per[t, 0]);
                                            temp2[i + v + 1, j + 1] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            publicTransportCounter++;
                                        }
                                        else if (temp[i, j - 1] == 0 && temp[i + 1, j - 1] == 0 && temp[i + v, j - 1] == 0 && temp[i + v + 1, j - 1] == 0)
                                        {
                                            temp2[i + v, j - 1] = Convert.ToInt32(per[t, 0]);
                                            temp2[i + v + 1, j - 1] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            publicTransportCounter++;
                                        }
                                        else
                                        {
                                            temp2[i + v, j] = Convert.ToInt32(per[t, 0]);
                                            temp2[i + v + 1, j] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                        }
                                    }
                                    else if (temp[i, j] == per[t, 0] && temp[i + 1, j] == per[t, 0])
                                    {
                                        temp2[i + v, j] = Convert.ToInt32(per[t, 0]);
                                        temp2[i + v + 1, j] = Convert.ToInt32(per[t, 0]);
                                        per[t, 1]--;
                                        if (per[t, 1] < 0)
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble())); //костыль от -1 по времени
                                    }
                                }

                                if (per[t, 2] == 2)
                                {
                                    if (per[t, 1] == 0)//если подошло время попытаться перестроиться ЛЕГКОВОВЙ машине
                                    {
                                        if (temp[i, j + 1] == 0 && temp[i + v, j + 1] == 0)
                                        {
                                            temp2[i + v, j + 1] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            a++;
                                        }
                                        else if (temp[i, j - 1] == 0 && temp[i + v, j - 1] == 0)
                                        {
                                            temp2[i + v, j - 1] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                            a++;
                                        }
                                        else
                                        {
                                            temp2[i + v, j] = Convert.ToInt32(per[t, 0]);
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));
                                        }
                                    }
                                    else if (temp[i, j] == per[t, 0])
                                    {
                                        temp2[i + v, j] = Convert.ToInt32(per[t, 0]);
                                        per[t, 1]--;
                                        if (per[t, 1] < 0)
                                            per[t, 1] = Math.Round(-(1 / mu) * Math.Log(rand.NextDouble()));//костыль от -1 по времени
                                    }
                                    else
                                    {
                                        MessageBox.Show("ЛЕГКОВОВЙ" + (t + 2).ToString());
                                    }
                                }
                            }
                        }
                    }
                    */

                }
            }

            label7.Text = label7.Text + " " + Convert.ToString(publicTransportCounter);
            label8.Text = label8.Text + " " + Convert.ToString(a);

            temp = temp2;

            dataGridView2.RowCount = temp.GetLength(0);
            dataGridView2.ColumnCount = temp.GetLength(1);
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = temp[i, j];
                    if (temp[i, j] != 0)
                    {
                        dataGridView2.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Black;
                    }
                }
            }
            dataGridView2.AutoResizeColumns();

            dataGridView3.RowCount = per.GetLength(0);
            dataGridView3.ColumnCount = per.GetLength(1);
            for (int i = 0; i < per.GetLength(0); i++)
            {
                for (int j = 0; j < per.GetLength(1); j++)
                {
                    dataGridView3.Rows[i].Cells[j].Value = per[i, j];
                    
                }
            }
            dataGridView3.AutoResizeColumns();
        }
    }
}
