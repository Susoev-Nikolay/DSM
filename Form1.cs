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

namespace Dsm3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[,] main, temp;
        int[,] per;
        int[] pert;
        Random rand = new Random();
        int[][] normalnoeRaspredilenie;

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


            per = new int[publicTransportAmount + carsL * (P - 1), 4];
            int ticks = Convert.ToInt32(textBox6.Text);
            normalnoeRaspredilenie = new int[per.GetLength(0)][];
            for (int i = 0; i < per.GetLength(0); i++)
            {
                normalnoeRaspredilenie[i] = calculateRandomNumbers(ticks, mu);
            }
            for (int i = 0; i < publicTransportAmount + carsL * (P - 1); i++)
            {
                per[i, 0] = i + 2;
                per[i, 1] = normalnoeRaspredilenie[i][0];//Convert.ToInt32(Math.Round(-(1 / mu) * Math.Log(rand.NextDouble())));
                if (i < publicTransportAmount)
                {
                    per[i, 2] = 1;
                }
                else 
                {
                    per[i, 2] = 2;
                }
                per[i, 3] = 0;
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
            int P = Convert.ToInt32(textBox1.Text);
            int roadLanesCount = Convert.ToInt32(textBox1.Text) + 2;
            int roadLength = Convert.ToInt32(textBox2.Text) / 5;
            double rho = Convert.ToDouble(textBox3.Text);//плотность
            double mu = Convert.ToDouble(textBox5.Text);//мю
            int v = Convert.ToInt32(textBox4.Text) / 5;
            int carsL = Convert.ToInt32(Math.Round(roadLength / 2 * rho));
            int publicTransportAmount = Convert.ToInt32(Math.Round(roadLength / 4 * rho));

            int objectIndex;
           
            pert = new int[publicTransportAmount + carsL * (P - 1)];

            int prevTickPublicTransporntLaneChangeCount = 0;
            int prevTickCarsLaneChangeCount = 0;

            for (int u = 0; u < Convert.ToInt32(textBox6.Text); u++)
            {
                dataGridView2.Rows.Clear();
                int publicTransportCounter = 0;
                int carsCounter = 0;


                for (int i = 0; i < pert.Length; i++)
                {
                    pert[i] = Convert.ToInt32(per[i, 0]);
                }

                int[,] nextRoadState = initNextRoadStateWithRoadBump(roadLength, roadLanesCount);

                for (int roadIndex = 0; roadIndex < roadLength; roadIndex++)
                {
                    for (int laneIndex = 0; laneIndex < roadLanesCount; laneIndex++)
                    {
                        objectIndex = Array.IndexOf(pert, temp[roadIndex, laneIndex]);
                        if (objectIndex != -1)
                        {
                            int secondPartRoadIndex = (roadIndex + 1) % temp.GetLength(0);
                            int nextMoveRoadIndex = (roadIndex + v) % temp.GetLength(0);
                            if (isPublicTransport(per, objectIndex))
                            {

                                if (isNextObjectSame(per, temp, objectIndex, nextMoveRoadIndex, laneIndex) && isTimeToChangeLane(per, objectIndex))
                                {
                                    if (publicTransportCouldMoveToRight(temp, roadIndex, secondPartRoadIndex, nextMoveRoadIndex, laneIndex))
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex + 1] = Convert.ToInt32(per[objectIndex, 0]);
                                        nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex + 1] = Convert.ToInt32(per[objectIndex, 0]);
                                        per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                        per[objectIndex, 3]++;
                                        publicTransportCounter++;
                                    }
                                    else if (publicTransportCouldMoveToLeft(temp, roadIndex, secondPartRoadIndex, nextMoveRoadIndex, laneIndex))
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex - 1] = Convert.ToInt32(per[objectIndex, 0]);
                                        nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex - 1] = Convert.ToInt32(per[objectIndex, 0]);
                                        per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                        per[objectIndex, 3]++;
                                        publicTransportCounter++;
                                    }
                                    else
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(per[objectIndex, 0]);
                                        nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex] = Convert.ToInt32(per[objectIndex, 0]);
                                        per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                        per[objectIndex, 3]++;
                                    }
                                }
                                else if (temp[roadIndex, laneIndex] == per[objectIndex, 0] && temp[secondPartRoadIndex, laneIndex] == per[objectIndex, 0])
                                {
                                    nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(per[objectIndex, 0]);
                                    nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex] = Convert.ToInt32(per[objectIndex, 0]);
                                    per[objectIndex, 1]--;
                                    if (per[objectIndex, 1] < 0)
                                    {
                                        per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                        per[objectIndex, 3]++;
                                    } //костыль от -1 по времени
                                }
                            }

                            if (isCar(per, objectIndex))
                            {
                                if (isTimeToChangeLane(per, objectIndex))
                                {
                                    if (carCouldMoveToRight(temp, roadIndex, nextMoveRoadIndex, laneIndex))
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex + 1] = Convert.ToInt32(per[objectIndex, 0]);
                                        per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                        per[objectIndex, 3]++;
                                        carsCounter++;
                                    }
                                    else if (carCouldMoveToLeft(temp, roadIndex, nextMoveRoadIndex, laneIndex))
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex - 1] = Convert.ToInt32(per[objectIndex, 0]);
                                        per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                        per[objectIndex, 3]++;
                                        carsCounter++;
                                    }
                                    else
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(per[objectIndex, 0]);
                                        per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                        per[objectIndex, 3]++;
                                    }
                                }
                                else if (temp[roadIndex, laneIndex] == per[objectIndex, 0])
                                {
                                    nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(per[objectIndex, 0]);
                                    per[objectIndex, 1]--;
                                    if (per[objectIndex, 1] < 0)
                                    {
                                        per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                        per[objectIndex, 3]++;
                                    }//костыль от -1 по времени
                                }
                                else
                                {
                                    MessageBox.Show("ЛЕГКОВОВЙ" + (objectIndex + 2).ToString());
                                }
                            }
                        }
                    }
                }

                textBox7.Text = textBox7.Text + ";" + Convert.ToString(publicTransportCounter);
                textBox8.Text = textBox8.Text + ";" + Convert.ToString(carsCounter);

                int curTickPublicTransporntLaneChangeCount = 0;
                int curTickCarsLaneChangeCount = 0;
                for (int i = 0; i < per.GetLength(0); i++)
                {
                    if (isPublicTransport(per, i))
                    {
                        curTickPublicTransporntLaneChangeCount += per[i, 3];
                    }
                    else if (isCar(per,i))
                    {
                        curTickCarsLaneChangeCount += per[i, 3];
                    }
                }

                textBox9.Text = textBox9.Text + ";" + Convert.ToString(curTickPublicTransporntLaneChangeCount-prevTickPublicTransporntLaneChangeCount);
                textBox10.Text = textBox10.Text + ";" + Convert.ToString(curTickCarsLaneChangeCount-prevTickCarsLaneChangeCount);
                prevTickCarsLaneChangeCount = curTickCarsLaneChangeCount;
                prevTickPublicTransporntLaneChangeCount = curTickPublicTransporntLaneChangeCount;

                temp = nextRoadState;

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
            
            int objectIndex;
            int publicTransportCounter = 0;
            int carsCounter = 0;
            pert = new int[publicTransportAmount + carsL * (P - 1)];

            for (int i = 0; i < pert.Length; i++)
            {
                pert[i] = Convert.ToInt32(per[i,0]);
            }

            int[,] nextRoadState = initNextRoadStateWithRoadBump(roadLength,roadLanesCount);

            for (int roadIndex = 0; roadIndex < roadLength; roadIndex++)
            {
                for (int laneIndex = 0; laneIndex < roadLanesCount; laneIndex++)
                {
                    objectIndex = Array.IndexOf(pert, temp[roadIndex, laneIndex]);
                    if (objectIndex != -1)
                    {
                        int secondPartRoadIndex = (roadIndex + 1) % temp.GetLength(0);
                        int nextMoveRoadIndex = (roadIndex + v) % temp.GetLength(0);
                        if (isPublicTransport(per, objectIndex)) {
                            
                            if (isNextObjectSame(per, temp, objectIndex, nextMoveRoadIndex, laneIndex) && isTimeToChangeLane(per, objectIndex))
                            {
                                if (publicTransportCouldMoveToRight(temp, roadIndex, secondPartRoadIndex, nextMoveRoadIndex, laneIndex))
                                {
                                    nextRoadState[nextMoveRoadIndex, laneIndex + 1] = Convert.ToInt32(per[objectIndex, 0]);
                                    nextRoadState[(nextMoveRoadIndex+1) % temp.GetLength(0), laneIndex + 1] = Convert.ToInt32(per[objectIndex, 0]);
                                    per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex,3]];//calculateTimeToMove(mu);
                                    per[objectIndex, 3]++;
                                    publicTransportCounter++;
                                    MessageBox.Show(temp.GetLength(0).ToString());
                                }
                                else if (publicTransportCouldMoveToLeft(temp, roadIndex, secondPartRoadIndex, nextMoveRoadIndex, laneIndex))
                                {
                                    nextRoadState[nextMoveRoadIndex, laneIndex - 1] = Convert.ToInt32(per[objectIndex, 0]);
                                    nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex - 1] = Convert.ToInt32(per[objectIndex, 0]);
                                    per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                    per[objectIndex, 3]++;
                                    publicTransportCounter++;
                                }
                                else
                                {
                                    nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(per[objectIndex, 0]);
                                    nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex] = Convert.ToInt32(per[objectIndex, 0]);
                                    per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                    per[objectIndex, 3]++;
                                }
                            }
                            else if (temp[roadIndex, laneIndex] == per[objectIndex, 0] && temp[secondPartRoadIndex, laneIndex] == per[objectIndex, 0])
                            {
                                nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(per[objectIndex, 0]);
                                nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex] = Convert.ToInt32(per[objectIndex, 0]);
                                per[objectIndex, 1]--;
                                if (per[objectIndex, 1] < 0)
                                {
                                    per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                    per[objectIndex, 3]++;
                                } //костыль от -1 по времени
                            }
                        }

                        if (isCar(per,objectIndex)) { 
                            if  (isTimeToChangeLane(per,objectIndex))
                            {
                                if (carCouldMoveToRight(temp, roadIndex, nextMoveRoadIndex, laneIndex))
                                {
                                    nextRoadState[nextMoveRoadIndex, laneIndex + 1] = Convert.ToInt32(per[objectIndex, 0]);
                                    per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                    per[objectIndex, 3]++;
                                    carsCounter++;
                                }
                                else if (carCouldMoveToLeft(temp, roadIndex, nextMoveRoadIndex, laneIndex))
                                {
                                    nextRoadState[nextMoveRoadIndex, laneIndex - 1] = Convert.ToInt32(per[objectIndex, 0]);
                                    per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                    per[objectIndex, 3]++;
                                    carsCounter++;
                                }
                                else
                                {
                                    nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(per[objectIndex, 0]);
                                    per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                    per[objectIndex, 3]++;
                                }
                            }
                            else if (temp[roadIndex, laneIndex] == per[objectIndex, 0])
                            {
                                nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(per[objectIndex, 0]);
                                per[objectIndex, 1]--;
                                if (per[objectIndex, 1] < 0)
                                {
                                    per[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][per[objectIndex, 3]];//calculateTimeToMove(mu);
                                    per[objectIndex, 3]++;
                                }//костыль от -1 по времени
                            }
                            else
                            {
                                MessageBox.Show("ЛЕГКОВОВЙ" + (objectIndex+2).ToString());
                            }
                        }
                    }
                }
            }

            label7.Text = label7.Text + " " + Convert.ToString(publicTransportCounter);
            label8.Text = label8.Text + " " + Convert.ToString(carsCounter);

            temp = nextRoadState;

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
        private int[,] initNextRoadStateWithRoadBump(int roadLength, int roadLanesCount)
        {
            int[,] nextRoadState = new int[roadLength, roadLanesCount];
            for (int i = 0; i < roadLength; i++)
            {
                nextRoadState[i, 0] = 1;
                nextRoadState[i, roadLanesCount - 1] = 1;
            }
            return nextRoadState;
        }

        private int[] calculateRandomNumbers(int ticksCount, double mu)
        {
            int[] randomNumbers = new int[ticksCount];
            for (int i = 0; i < ticksCount; i++)
            {
                randomNumbers[i] = calculateTimeToMove(mu);
                Thread.Sleep(1);
            }
            return randomNumbers;
        }

        private int calculateTimeToMove(double mu)
        {
            return Convert.ToInt32(Math.Round(-(1 / mu) * Math.Log(rand.NextDouble())));
        }

        //TODO rename "per" to something with more sense
        private bool isPublicTransport(int[,] per, int objectIndex)
        {
            return per[objectIndex, 2] == 1;
        }

        //TODO rename "per" to something with more sense
        //TODO Mb you should rename "Car" to something more acceptable in current context ?
        private bool isCar(int[,] per, int objectIndex)
        {
            return per[objectIndex, 2] == 2;
        }

        private bool isTimeToChangeLane(int[,] per, int objectIndex)
        {
            return per[objectIndex, 1] == 0;
        }
        private bool isNextObjectSame(int[,] per, int[,] temp, int objectIndex, int nextObjectIndex, int laneIndex)
        {
            return temp[nextObjectIndex, laneIndex] == per[objectIndex, 0];
        }
        //temp[roadIndex, laneIndex]== temp[secondPartIndex, laneIndex] &&
        private bool publicTransportCouldMoveToRight(int[,] temp, int roadIndex, int secondPartIndex, int nextRoadIndex, int laneIndex)
        {
            int rightLaneIndex = laneIndex + 1;
            return  temp[roadIndex, rightLaneIndex] == 0 && temp[secondPartIndex, rightLaneIndex] == 0
            && temp[nextRoadIndex, rightLaneIndex] == 0 && temp[(nextRoadIndex + 1) % temp.GetLength(0), rightLaneIndex] == 0;
        }

        private bool publicTransportCouldMoveToLeft(int[,] temp, int roadIndex, int secondPartIndex, int nextRoadIndex, int laneIndex)
        {
            int leftLaneIndex = laneIndex - 1;
            return temp[roadIndex, leftLaneIndex] == 0 && temp[secondPartIndex, leftLaneIndex] == 0
            && temp[nextRoadIndex, leftLaneIndex] == 0 && temp[(nextRoadIndex + 1) % temp.GetLength(0), leftLaneIndex] == 0;
        }

        private bool carCouldMoveToRight(int[,] temp, int roadIndex, int nextRoadIndex, int laneIndex)
        {
            int rightLaneIndex = laneIndex + 1;
            return temp[roadIndex, rightLaneIndex] == 0 && temp[nextRoadIndex, rightLaneIndex] == 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private bool carCouldMoveToLeft(int[,] temp, int roadIndex, int nextRoadIndex, int laneIndex)
        {
            int leftLaneIndex = laneIndex - 1;
            return temp[roadIndex, leftLaneIndex] == 0 && temp[nextRoadIndex, leftLaneIndex] == 0;
        }
    }

    
}
