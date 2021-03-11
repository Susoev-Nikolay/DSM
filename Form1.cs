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
using System.IO;

namespace Dsm3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[,] main, temp;
        int[,] properties;
        int[] carIndexToPropetyArray;
        Random rand = new Random();
        int[][] normalnoeRaspredilenie;

        static int MAX_O = 2; //TODO rename
        string path = @"D:\temp\distancesByFrame.txt";
        string path2 = @"D:\temp\distancesByLane1.txt";
        string path3 = @"D:\temp\distancesByLane2.txt";
        string path4 = @"D:\temp\distancesByLane3.txt";

        //TODO initiliazie road info in one method to all buttons

        //TODO fix
        private int[,] createRoadLanes()
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
            return new int[0, 0];
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int P = Convert.ToInt32(textBox1.Text);
            int roadLanesCount = Convert.ToInt32(textBox1.Text) + 2;
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
            int speed = 1;

            try
            {
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("");
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception ex)
            {

            }

            int t = 2;
            int tb = 3;
            for (int i = 0; i < roadLength; i++)
            {
                main[i, 0] = 1;
                main[i, roadLanesCount - 1] = 1;
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

            for (int o = 2; o < roadLanesCount - 1; o++)//легковые машины
            {
                for (int i = 0; i < carsL; i++)
                {

                    for (int k = 0; k < 1; k++)
                    {
                        main[t + k, o] = numb;
                    }
                    int dstL = rnd.Next(2, maxdstL + 1);
                    t = t + dstL;
                    numb++;
                }
                t = 0;
            }


            properties = new int[publicTransportAmount + carsL * (P - 1), 7];
            int ticks = Convert.ToInt32(textBox6.Text)+2;
            normalnoeRaspredilenie = new int[properties.GetLength(0)][];
            for (int i = 0; i < properties.GetLength(0); i++)
            {
                normalnoeRaspredilenie[i] = calculateRandomNumbers(ticks, mu);
            }
            for (int i = 0; i < publicTransportAmount + carsL * (P - 1); i++)
            {
                properties[i, 0] = i + 2;
                properties[i, 1] = normalnoeRaspredilenie[i][0];//Convert.ToInt32(Math.Round(-(1 / mu) * Math.Log(rand.NextDouble())));
                if (i < publicTransportAmount)
                {
                    properties[i, 2] = 1;
                }
                else
                {
                    properties[i, 2] = 2;
                }
                properties[i, 3] = 0;
                properties[i, 4] = Convert.ToInt32(rand.NextDouble() < speed);
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

            dataGridView3.RowCount = properties.GetLength(0);
            dataGridView3.ColumnCount = properties.GetLength(1);
            for (int i = 0; i < properties.GetLength(0); i++)
            {
                for (int j = 0; j < properties.GetLength(1); j++)
                {
                    dataGridView3.Rows[i].Cells[j].Value = properties[i, j];
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
            int speed = 1;

            double sv = 0;

            int objectIndex;

            carIndexToPropetyArray = new int[publicTransportAmount + carsL * (P - 1)];

            int prevTickPublicTransporntLaneChangeCount = 0;
            int prevTickCarsLaneChangeCount = 0;

            for (int u = 0; u < Convert.ToInt32(textBox6.Text)+1; u++)
            {
                dataGridView2.Rows.Clear();
                dataGridView4.Rows.Clear();
                int publicTransportCounter = 0;
                int carsCounter = 0;
                int intenc = 0;

                carIndexToPropetyArray = new int[publicTransportAmount + carsL * (P - 1)];

                for (int i = 0; i < carIndexToPropetyArray.Length; i++)
                {
                    carIndexToPropetyArray[i] = Convert.ToInt32(properties[i, 0]);
                }

                int[,] nextRoadState = initNextRoadStateWithRoadBump(roadLength, roadLanesCount);

                for (int roadIndex = 0; roadIndex < roadLength; roadIndex++)
                {
                    for (int laneIndex = 1; laneIndex < roadLanesCount - 1; laneIndex++)
                    {
                        objectIndex = Array.IndexOf(carIndexToPropetyArray, temp[roadIndex, laneIndex]);
                        if (objectIndex != -1)
                        {
                            int secondPartRoadIndex = (roadIndex + 1) % temp.GetLength(0);
                            int nextMoveRoadIndex = (roadIndex + v) % temp.GetLength(0);

                            if (isPublicTransport(properties, objectIndex))
                            {
                                if (isNextObjectSame(properties, temp, objectIndex, nextMoveRoadIndex, laneIndex))
                                {
                                    if (isTimeToChangeLane(properties, objectIndex))
                                    {
                                        properties[objectIndex, 6]++;
                                        if (publicTransportCouldMoveToRight(temp, roadIndex, secondPartRoadIndex, nextMoveRoadIndex, laneIndex)
                                            && laneIndex + 2 < roadLanesCount && (temp[roadIndex, laneIndex + 2] == 0 || temp[roadIndex, laneIndex + 2] == 1)
                                            && properties[objectIndex, 4] == 1 &&
                                                isCarCanMove(roadIndex, laneIndex, 2, 1)
                                                &&
                                                isCarCanMove(roadIndex, laneIndex, 1, 1)
                                            )
                                        {
                                            nextRoadState[nextMoveRoadIndex, laneIndex + 1] = Convert.ToInt32(properties[objectIndex, 0]);
                                            nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex + 1] = Convert.ToInt32(properties[objectIndex, 0]);
                                            properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                            properties[objectIndex, 3]++;
                                            properties[objectIndex, 5]++;
                                            publicTransportCounter++;
                                        }
                                        else if (publicTransportCouldMoveToLeft(temp, roadIndex, secondPartRoadIndex, nextMoveRoadIndex, laneIndex)
                                            && laneIndex - 2 >= 0 && (temp[roadIndex, laneIndex - 2] == 0 || temp[roadIndex, laneIndex - 2] == 1)
                                            && properties[objectIndex, 4] == 1 &&
                                                isCarCanMove(roadIndex, laneIndex, 2, -1)
                                                &&
                                                isCarCanMove(roadIndex, laneIndex, 1, -1)
                                            )
                                        {
                                            nextRoadState[nextMoveRoadIndex, laneIndex - 1] = Convert.ToInt32(properties[objectIndex, 0]);
                                            nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex - 1] = Convert.ToInt32(properties[objectIndex, 0]);
                                            properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                            properties[objectIndex, 3]++;
                                            properties[objectIndex, 5]++;
                                            publicTransportCounter++;
                                        }
                                        else if (properties[objectIndex, 4] == 1 && isCarCanMove(roadIndex, laneIndex, 2, 0))
                                        {
                                            nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                            nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                            properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                            properties[objectIndex, 3]++;
                                        }
                                        else
                                        {
                                            nextRoadState[roadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                            nextRoadState[(roadIndex + 1) % temp.GetLength(0), laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                            properties[objectIndex, 1]--;
                                            if (properties[objectIndex, 1] < 0)
                                            {
                                                properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                                properties[objectIndex, 3]++;
                                            } //костыль от -1 по времени
                                        }
                                    }
                                    else
                                    {
                                        if (properties[objectIndex, 4] == 1 && isCarCanMove(roadIndex, laneIndex, 2, 0))
                                        {
                                            nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                            nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                            properties[objectIndex, 1]--;
                                            properties[objectIndex, 3]++;
                                            if (properties[objectIndex, 1] < 0)
                                            {
                                                properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                                properties[objectIndex, 3]++;
                                            } //костыль от -1 по времени
                                        }
                                        else
                                        {
                                            MessageBox.Show("jopa");
                                            nextRoadState[roadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                            nextRoadState[(roadIndex + 1) % temp.GetLength(0), laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                            properties[objectIndex, 1]--;
                                            if (properties[objectIndex, 1] < 0)
                                            {
                                                properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                                properties[objectIndex, 3]++;
                                            } //костыль от -1 по времени
                                        }
                                    }

                                }
                                properties[objectIndex, 4] = Convert.ToInt32(rand.NextDouble() < speed);
                            }

                            if (isCar(properties, objectIndex))
                            {

                                if (isTimeToChangeLane(properties, objectIndex))
                                {
                                    properties[objectIndex, 6]++;
                                    if (carCouldMoveToRight(temp, roadIndex, nextMoveRoadIndex, laneIndex)
                                        && laneIndex + 2 < roadLanesCount && (temp[roadIndex, laneIndex + 2] == 0 || temp[roadIndex, laneIndex + 2] == 1)
                                        && properties[objectIndex, 4] == 1 && isCarCanMove(roadIndex, laneIndex, 1, 1))
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex + 1] = Convert.ToInt32(properties[objectIndex, 0]);
                                        properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                        properties[objectIndex, 3]++;
                                        properties[objectIndex, 5]++;
                                        carsCounter++;
                                    }
                                    else if (carCouldMoveToLeft(temp, roadIndex, nextMoveRoadIndex, laneIndex)
                                        && laneIndex - 2 >= 0 && (temp[roadIndex, laneIndex - 2] == 0 || temp[roadIndex, laneIndex - 2] == 1)
                                        && properties[objectIndex, 4] == 1 && isCarCanMove(roadIndex, laneIndex, 1, -1))
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex - 1] = Convert.ToInt32(properties[objectIndex, 0]);
                                        properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                        properties[objectIndex, 3]++;
                                        properties[objectIndex, 5]++;
                                        carsCounter++;
                                    }
                                    else if (properties[objectIndex, 4] == 1 && isCarCanMove(roadIndex, laneIndex, 1, 0))
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                        properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                        properties[objectIndex, 3]++;
                                    }
                                    else
                                    {
                                        nextRoadState[roadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                        properties[objectIndex, 1]--;
                                        properties[objectIndex, 3]++;
                                        MessageBox.Show("jopa");
                                        if (properties[objectIndex, 1] < 0)
                                        {                              
                                            properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                            properties[objectIndex, 3]++;
                                        } //костыль от -1 по времени
                                    }
                                }
                                else
                                {
                                    if (properties[objectIndex, 4] == 1 && isCarCanMove(roadIndex, laneIndex, 1, 0))
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex] = temp[roadIndex, laneIndex];
                                        properties[objectIndex, 1]--;
                                        properties[objectIndex, 3]++;
                                        if (properties[objectIndex, 1] < 0)
                                        {
                                            properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                            properties[objectIndex, 3]++;
                                        } //костыль от -1 по времени
                                    }
                                    else
                                    {
                                        nextRoadState[roadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                        properties[objectIndex, 1]--;
                                        if (properties[objectIndex, 1] < 0)
                                        {
                                            properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                            properties[objectIndex, 3]++;
                                        } //костыль от -1 по времени
                                    }
                                }
                                properties[objectIndex, 4] = Convert.ToInt32(rand.NextDouble() < speed);

                            }

                        }
                    }
                }

                textBox7.Text = textBox7.Text + ";" + Convert.ToString(publicTransportCounter);
                textBox8.Text = textBox8.Text + ";" + Convert.ToString(carsCounter);
                textBox14.Text = textBox14.Text + ";" + Convert.ToString(carsCounter+ publicTransportCounter);


                int curTickPublicTransporntLaneChangeCount = 0;
                int curTickCarsLaneChangeCount = 0;
                for (int i = 0; i < properties.GetLength(0); i++)
                {
                    if (isPublicTransport(properties, i))
                    {
                        curTickPublicTransporntLaneChangeCount += properties[i, 6];
                    }
                    else if (isCar(properties, i))
                    {
                        curTickCarsLaneChangeCount += properties[i, 6];
                    }
                }

                textBox9.Text = textBox9.Text + ";" + Convert.ToString(curTickPublicTransporntLaneChangeCount - prevTickPublicTransporntLaneChangeCount);
                textBox10.Text = textBox10.Text + ";" + Convert.ToString(curTickCarsLaneChangeCount - prevTickCarsLaneChangeCount);
                textBox13.Text = textBox13.Text + ";" + Convert.ToString((curTickCarsLaneChangeCount - prevTickCarsLaneChangeCount)+(curTickPublicTransporntLaneChangeCount - prevTickPublicTransporntLaneChangeCount));

                prevTickCarsLaneChangeCount = curTickCarsLaneChangeCount;
                prevTickPublicTransporntLaneChangeCount = curTickPublicTransporntLaneChangeCount;

                dataGridView4.RowCount = temp.GetLength(0);
                dataGridView4.ColumnCount = temp.GetLength(1);
                for (int i = 0; i < temp.GetLength(0); i++)
                {
                    for (int j = 0; j < temp.GetLength(1); j++)
                    {
                        dataGridView4.Rows[i].Cells[j].Value = temp[i, j];
                        if (temp[i, j] != 0)
                        {
                            dataGridView4.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Black;
                        }
                    }
                }
                dataGridView4.AutoResizeColumns();


                //интенсивность
                for (int laneIndex = 1; laneIndex < roadLanesCount - 1; laneIndex++)
                {
                    if (temp[roadLength - 1, laneIndex] != 0)
                    {
                        intenc++;
                    }
                }
                


                if (u > 0 && u % 10 == 0)
                {
                    //Средняя скорость
                    for (int i = 0; i < properties.GetLength(0); i++)
                    {
                        sv += properties[i, 3]-1;
                    }
                    sv = sv / (10* properties.GetLength(0));
                    textBox11.Text = textBox11.Text + sv.ToString()+";";

                    intenc = intenc/((roadLanesCount - 2)*10);
                    textBox12.Text = textBox12.Text + intenc.ToString() + ";";
                    sv = 0;
                    intenc = 0;
                }

                
                
                
                textBox12.Text = textBox12.Text + intenc.ToString() + ";";




                /*List<List<int>> distances = new List<List<int>>();

                for (int laneIndex = 1; laneIndex < roadLanesCount - 1; laneIndex++)
                {
                    List<int> tem = new List<int>();
                    int i = 0;
                    int j = 0;

                    int lengthBetweenCars;
                    for (int roadIndex = 0; roadIndex < roadLength; roadIndex++)
                    {
                        objectIndex = Array.IndexOf(carIndexToPropetyArray, temp[roadIndex, laneIndex]);
                        if (objectIndex != -1)
                        {
                            int nextMoveRoadIndex = (roadIndex + v) % temp.GetLength(0);
                            if (temp[roadIndex, laneIndex] != 0 && isCar(properties, objectIndex))
                                i++;
                            if (isPublicTransport(properties, objectIndex))
                            {
                                if (isNextObjectSame(properties, temp, objectIndex, nextMoveRoadIndex, laneIndex))
                                {
                                    i++;
                                }
                            }
                        }
                    }

                    int[] t = new int[i];
                    for (int roadIndex = 0; roadIndex < roadLength; roadIndex++)
                    {
                        if (temp[roadIndex, laneIndex] != 0)
                        {
                            objectIndex = Array.IndexOf(carIndexToPropetyArray, temp[roadIndex, laneIndex]);
                            int nextMoveRoadIndex = (roadIndex + v) % temp.GetLength(0);

                            if (isCar(properties, objectIndex))
                            {
                                t[j] = roadIndex;
                                j++;
                            }
                            if (isPublicTransport(properties, objectIndex))
                            {
                                if (isNextObjectSame(properties, temp, objectIndex, nextMoveRoadIndex, laneIndex))
                                {
                                    t[j] = roadIndex;
                                    j++;
                                }
                            }
                        }
                    }
                    int[] distancesBetweenCars = new int[i];
                    for (int k = 0; k < i - 1; k++)
                    {
                        if (temp[t[k], laneIndex] == temp[(t[k] + 1) % temp.GetLength(0), laneIndex])
                        {
                            distancesBetweenCars[k] = t[k + 1] - t[k] - 2;
                            tem.Add(t[k + 1] - t[k] - 2);
                        }
                        else
                        {
                            distancesBetweenCars[k] = t[k + 1] - t[k] - 1;
                            tem.Add(t[k + 1] - t[k] - 1);
                        }
                    }
                    if (temp[t[i - 1], laneIndex] == temp[(t[i - 1] + 1) % temp.GetLength(0), laneIndex])
                    {
                        distancesBetweenCars[i - 1] = roadLength - t[i - 1] + t[0] - 2;
                        tem.Add(roadLength - t[i - 1] + t[0] - 2);
                    }
                    else
                    {
                        distancesBetweenCars[i - 1] = roadLength - t[i - 1] + t[0] - 1;
                        tem.Add(roadLength - t[i - 1] + t[0] - 1);
                    }
                    distances.Add(tem);
                    /*int startcar = -1;
                    List<int> tem = new List<int>();
                    int roadIndex = 0;
                    distances.Add(tem);
                    int prevCarIndex=-1;
                    int lengthBetweenCars = 0;
                    while (temp[roadIndex, laneIndex] == 0)
                    {
                        roadIndex = (roadIndex + 1) % temp.GetLength(0);
                    }

                    while (roadIndex!=startcar)
                    {
                        if (temp[roadIndex, laneIndex] != 0)
                        {

                            if (startcar == -1)
                            {
                                startcar = roadIndex;
                            }
                            if (prevCarIndex == -1)
                            {
                                prevCarIndex = roadIndex;
                            }
                            else 
                            {
                                int prevRoadIndex = roadIndex == 0 ? temp.GetLength(0) - 1 : roadIndex - 1;
                                if (temp[prevRoadIndex, laneIndex] == 0 || temp[prevRoadIndex, laneIndex] != temp[roadIndex, laneIndex])
                                {
                                    if (roadIndex < prevCarIndex)
                                    {
                                        tem.Add(temp.GetLength(0)-prevCarIndex+roadIndex+1);
                                    }
                                    tem.Add(roadIndex-prevCarIndex+1);

                                }
                                prevCarIndex = roadIndex;
                            }

                        }
                        roadIndex = (roadIndex + 1) % temp.GetLength(0);


                    }
                }

                string[] colors = { ", 'color', 'b'", ", 'color', 'r'", ", 'color', 'k'" };

                string b = "";
                b = b + "\n hold on \n";
                for (int i = 0; i < 1; i++)
                {
                    b = b + "b" + i.ToString() + "=[";
                    for (int j = 0; j < distances[i].Count; j++)
                    {
                        b = b + distances[i][j].ToString() + ";";
                    }
                    b = b + "];\n";
                    b = b + "plot(1:1:size(b" + i.ToString() + "), b" + i.ToString() + ",'or'" + colors[i] + ");\n";
                }

                b = b + "xlim([0,10]);\n";
                b = b + "ylim([0,25]);\n";

                b = b + "title('Крайняя правая полоса');";
                b = b + "xlabel('Номер пары машин');";
                b = b + "ylabel('Расстояние в паре');";

                string c = "";
                c = c + "\n hold on \n";
                for (int i = 1; i < 2; i++)
                {
                    c = c + "c" + i.ToString() + "=[";
                    for (int j = 0; j < distances[i].Count; j++)
                    {
                        c = c + distances[i][j].ToString() + ";";
                    }
                    c = c + "];\n";
                    c = c + "plot(1:1:size(c" + i.ToString() + "), c" + i.ToString() + ",'or'" + colors[i] + ");\n";
                }

                c = c + "xlim([0,10]);\n";
                c = c + "ylim([0,25]);\n";

                c = c + "title('Средняя полоса');";
                c = c + "xlabel('Номер пары машин');";
                c = c + "ylabel('Расстояние в паре');";

                string d = "";//пара машин, расстояние в паре  Средняя полоса Крайняя правая полоса
                d = d + "\n hold on \n";
                for (int i = 2; i < 3; i++)
                {
                    d = d + "d" + i.ToString() + "=[";
                    for (int j = 0; j < distances[i].Count; j++)
                    {
                        d = d + distances[i][j].ToString() + ";";
                    }
                    d = d + "];\n";
                    d = d + "plot(1:1:size(d" + i.ToString() + "), d" + i.ToString() + ",'or'" + colors[i] + ");\n";
                }

                d = d + "xlim([0,10]);\n";
                d = d + "ylim([0,25]);\n";

                d = d + "title('Крайняя левая полоса');";
                d = d + "xlabel('Номер пары машин');";
                d = d + "ylabel('Расстояние в паре');";

                /*string f = "";//пара машин, расстояние в паре
                f = f + "\n hold on \n";
                for (int i = 1; i < 2; i++)
                {
                    f = f + "f" + i.ToString() + "=[";
                    for (int j = 0; j < distances[i].Count; j++)
                    {
                        f = f + distances[i][j].ToString() + ";";
                    }
                    f = f + "];\n";
                    f = f + "plot(1:1:size(f" + i.ToString() + "), f" + i.ToString() + ",'or'" + colors[i] + ");\n";
                }

                f = f + "xlim([0,10]);\n";
                f = f + "ylim([0,25]);\n";

                string a = "";
                for (int i = 0; i < distances.Count; i++)
                {
                    a = a + "a" + i.ToString() + "=[";
                    for (int j = 0; j < distances[i].Count; j++)
                    {
                        a = a + distances[i][j].ToString() + ";";
                    }
                    a = a + "];\n";
                }
                a = a + "\n hold on \n";

                for (int i = 0; i < distances.Count; i++)
                {
                    a = a + "plot(1:1:size(a" + i.ToString() + "), a" + i.ToString() + ",'or'" + colors[i] + ");\n";
                }
                a = a + "\n figure; \n";



                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(a.ToString());
                        fs.Write(info, 0, info.Length);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                try
                {
                    using (FileStream fs = new FileStream(path2, FileMode.Append, FileAccess.Write))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(b.ToString());
                        fs.Write(info, 0, info.Length);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    using (FileStream fs = new FileStream(path3, FileMode.Append, FileAccess.Write))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(c.ToString());
                        fs.Write(info, 0, info.Length);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    using (FileStream fs = new FileStream(path4, FileMode.Append, FileAccess.Write))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(d.ToString());
                        fs.Write(info, 0, info.Length);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                temp = nextRoadState;
                */
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

                dataGridView3.RowCount = properties.GetLength(0);
                dataGridView3.ColumnCount = properties.GetLength(1);
                for (int i = 0; i < properties.GetLength(0); i++)
                {
                    for (int j = 0; j < properties.GetLength(1); j++)
                    {
                        dataGridView3.Rows[i].Cells[j].Value = properties[i, j];

                    }
                }
                dataGridView3.AutoResizeColumns();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView4.Rows.Clear();

            int P = Convert.ToInt32(textBox1.Text);
            int roadLanesCount = Convert.ToInt32(textBox1.Text) + 2;
            int roadLength = Convert.ToInt32(textBox2.Text) / 5;
            double rho = Convert.ToDouble(textBox3.Text);//плотность
            double mu = Convert.ToDouble(textBox5.Text);//мю
            int v = Convert.ToInt32(textBox4.Text) / 5;
            int carsL = Convert.ToInt32(Math.Round(roadLength / 2 * rho));
            int publicTransportAmount = Convert.ToInt32(Math.Round(roadLength / 4 * rho));

            int objectIndex;
            int publicTransportCounter = 0;
            int carsCounter = 0;
            carIndexToPropetyArray = new int[publicTransportAmount + carsL * (P - 1)];

            int speed = 1;

            for (int i = 0; i < carIndexToPropetyArray.Length; i++)
            {
                carIndexToPropetyArray[i] = Convert.ToInt32(properties[i, 0]);
            }

            int[,] nextRoadState = initNextRoadStateWithRoadBump(roadLength, roadLanesCount);

            for (int roadIndex = 0; roadIndex < roadLength; roadIndex++)
            {
                for (int laneIndex = 1; laneIndex < roadLanesCount - 1; laneIndex++)
                {
                    objectIndex = Array.IndexOf(carIndexToPropetyArray, temp[roadIndex, laneIndex]);
                    if (objectIndex != -1)
                    {
                        int secondPartRoadIndex = (roadIndex + 1) % temp.GetLength(0);
                        int nextMoveRoadIndex = (roadIndex + v) % temp.GetLength(0);

                        if (isPublicTransport(properties, objectIndex)) {
                            if (isNextObjectSame(properties, temp, objectIndex, nextMoveRoadIndex, laneIndex))
                            {
                                if (isTimeToChangeLane(properties, objectIndex)) {
                                    if (publicTransportCouldMoveToRight(temp, roadIndex, secondPartRoadIndex, nextMoveRoadIndex, laneIndex)
                                        && laneIndex + 2 < roadLanesCount && (temp[roadIndex, laneIndex + 2] == 0 || temp[roadIndex, laneIndex + 2] == 1)
                                        && properties[objectIndex, 4] == 1 &&
                                            isCarCanMove(roadIndex, laneIndex, 2, 1) 
                                            &&
                                            isCarCanMove(roadIndex, laneIndex, 1, 1)
                                        )
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex + 1] = Convert.ToInt32(properties[objectIndex, 0]);
                                        nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex + 1] = Convert.ToInt32(properties[objectIndex, 0]);
                                        properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                        properties[objectIndex, 3]++;
                                        properties[objectIndex, 5]++;
                                        publicTransportCounter++;
                                    }
                                    else if (publicTransportCouldMoveToLeft(temp, roadIndex, secondPartRoadIndex, nextMoveRoadIndex, laneIndex)
                                        && laneIndex - 2 >= 0 && (temp[roadIndex, laneIndex - 2] == 0 || temp[roadIndex, laneIndex - 2] == 1)
                                        && properties[objectIndex, 4] == 1 &&
                                            isCarCanMove(roadIndex, laneIndex, 2, -1)
                                            &&
                                            isCarCanMove(roadIndex, laneIndex, 1, -1)
                                        )
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex - 1] = Convert.ToInt32(properties[objectIndex, 0]);
                                        nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex - 1] = Convert.ToInt32(properties[objectIndex, 0]);
                                        properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                        properties[objectIndex, 3]++;
                                        properties[objectIndex, 5]++;
                                        publicTransportCounter++;
                                    }
                                    else if (properties[objectIndex, 4] == 1 && isCarCanMove(roadIndex, laneIndex, 2, 0))
                                    {
                                        nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                        nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                        properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                        properties[objectIndex, 3]++;
                                    }
                                    else
                                    {
                                        nextRoadState[roadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                        nextRoadState[(roadIndex + 1) % temp.GetLength(0), laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                        properties[objectIndex, 1]--;
                                        if (properties[objectIndex, 1] < 0)
                                        {
                                            properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                            properties[objectIndex, 3]++;
                                        } //костыль от -1 по времени
                                    }
                                }
                                else
                                { 
                                    if (properties[objectIndex, 4] == 1 && isCarCanMove(roadIndex, laneIndex, 2, 0)) { 
                                        nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                        nextRoadState[(nextMoveRoadIndex + 1) % temp.GetLength(0), laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                        properties[objectIndex, 3]++;
                                        properties[objectIndex, 1]--;
                                        if (properties[objectIndex, 1] < 0)
                                        {
                                            properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                            properties[objectIndex, 3]++;
                                        } //костыль от -1 по времени
                                    }
                                    else
                                    {
                                        nextRoadState[roadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                        nextRoadState[(roadIndex + 1) % temp.GetLength(0), laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                        properties[objectIndex, 1]--;
                                        if (properties[objectIndex, 1] < 0)
                                        {
                                            properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                            properties[objectIndex, 3]++;
                                        } //костыль от -1 по времени
                                    }
                                }

                            }
                            properties[objectIndex, 4] = Convert.ToInt32(rand.NextDouble() < speed);
                        }

                        if (isCar(properties, objectIndex)) {

                            if (isTimeToChangeLane(properties, objectIndex))
                            {
                                if (carCouldMoveToRight(temp, roadIndex, nextMoveRoadIndex, laneIndex)
                                    && laneIndex + 2 < roadLanesCount && (temp[roadIndex, laneIndex + 2] == 0 || temp[roadIndex, laneIndex + 2] == 1)
                                    && properties[objectIndex, 4] == 1 && isCarCanMove(roadIndex, laneIndex, 1, 1))
                                {
                                    nextRoadState[nextMoveRoadIndex, laneIndex + 1] = Convert.ToInt32(properties[objectIndex, 0]);
                                    properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                    properties[objectIndex, 3]++;
                                    properties[objectIndex, 5]++;
                                    carsCounter++;
                                }
                                else if (carCouldMoveToLeft(temp, roadIndex, nextMoveRoadIndex, laneIndex)
                                    && laneIndex - 2 >= 0 && (temp[roadIndex, laneIndex - 2] == 0 || temp[roadIndex, laneIndex - 2] == 1)
                                    && properties[objectIndex, 4] == 1 && isCarCanMove(roadIndex, laneIndex, 1, -1))
                                {
                                    nextRoadState[nextMoveRoadIndex, laneIndex - 1] = Convert.ToInt32(properties[objectIndex, 0]);
                                    properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                    properties[objectIndex, 3]++;
                                    properties[objectIndex, 5]++;
                                    carsCounter++;
                                }
                                else if (properties[objectIndex, 4] == 1 && isCarCanMove(roadIndex, laneIndex, 1, 0))
                                {
                                    nextRoadState[nextMoveRoadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                    properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                    properties[objectIndex, 3]++;
                                }
                                else
                                {
                                    nextRoadState[roadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                    properties[objectIndex, 1]--;
                                    if (properties[objectIndex, 1] < 0)
                                    {
                                        properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                        properties[objectIndex, 3]++;
                                    } //костыль от -1 по времени
                                }
                            }
                            else 
                            {
                                if (properties[objectIndex, 4] == 1 && isCarCanMove(roadIndex, laneIndex, 1, 0)){ 
                                    nextRoadState[nextMoveRoadIndex, laneIndex] = temp[roadIndex, laneIndex];
                                    properties[objectIndex, 3]++;
                                    properties[objectIndex, 1]--;
                                    if (properties[objectIndex, 1] < 0)
                                    {
                                        properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                        properties[objectIndex, 3]++;
                                    } //костыль от -1 по времени
                                }
                                else
                                {
                                    nextRoadState[roadIndex, laneIndex] = Convert.ToInt32(properties[objectIndex, 0]);
                                    properties[objectIndex, 1]--;
                                    if (properties[objectIndex, 1] < 0)
                                    {
                                        properties[objectIndex, 1] = normalnoeRaspredilenie[objectIndex][properties[objectIndex, 3]];//calculateTimeToMove(mu);
                                        properties[objectIndex, 3]++;
                                    } //костыль от -1 по времени
                                }
                            }
                            properties[objectIndex, 4] = Convert.ToInt32(rand.NextDouble() < speed);

                        }
                       
                    }
                }
            }

            label7.Text = label7.Text + " " + Convert.ToString(publicTransportCounter);
            label8.Text = label8.Text + " " + Convert.ToString(carsCounter);

            dataGridView4.RowCount = temp.GetLength(0);
            dataGridView4.ColumnCount = temp.GetLength(1);
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    dataGridView4.Rows[i].Cells[j].Value = temp[i, j];
                    if (temp[i, j] != 0)
                    {
                        dataGridView4.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Black;
                    }
                }
            }
            dataGridView4.AutoResizeColumns();



            /*List<List<int>> distances = new List<List<int>>();

            for (int laneIndex = 1; laneIndex < roadLanesCount - 1; laneIndex++)
            {
                List<int> tem = new List<int>();
                int i = 0;
                int j = 0;
                
                int lengthBetweenCars;
                for (int roadIndex = 0; roadIndex < roadLength; roadIndex++)
                {
                    objectIndex = Array.IndexOf(carIndexToPropetyArray, temp[roadIndex, laneIndex]);
                    if (objectIndex != -1)
                    {
                        int nextMoveRoadIndex = (roadIndex + v) % temp.GetLength(0);
                        if (temp[roadIndex, laneIndex] != 0 && isCar(properties, objectIndex))
                            i++;
                        if (isPublicTransport(properties, objectIndex))
                        {
                            if (isNextObjectSame(properties, temp, objectIndex, nextMoveRoadIndex, laneIndex))
                            {
                                i++;
                            }
                        }

                    }                   
                }

                int[] t = new int[i];
                for (int roadIndex = 0; roadIndex < roadLength; roadIndex++)
                {
                    if (temp[roadIndex, laneIndex] != 0)
                    {
                        objectIndex = Array.IndexOf(carIndexToPropetyArray, temp[roadIndex, laneIndex]);
                        int nextMoveRoadIndex = (roadIndex + v) % temp.GetLength(0);

                        if (isCar(properties, objectIndex)) {
                            t[j] = roadIndex;
                            j++;
                        }
                        if (isPublicTransport(properties, objectIndex))
                        {
                            if (isNextObjectSame(properties, temp, objectIndex, nextMoveRoadIndex, laneIndex))
                            {
                                t[j] = roadIndex;
                                j++;
                            }
                        }
                    }
                }
                int[] distancesBetweenCars = new int[i];
                for (int k = 0; k < i-1; k++)
                {
                    if (temp[t[k], laneIndex] == temp[(t[k]+1) % temp.GetLength(0), laneIndex])
                    {
                        distancesBetweenCars[k] = t[k + 1] - t[k] - 2;
                        tem.Add(t[k + 1] - t[k] - 2);
                    }
                    else
                    {
                        distancesBetweenCars[k] = t[k + 1] - t[k] - 1;
                        tem.Add(t[k + 1] - t[k] - 1);
                    }                      
                }
                if (temp[t[i - 1], laneIndex] == temp[(t[i-1] + 1) % temp.GetLength(0), laneIndex])
                {
                    distancesBetweenCars[i - 1] = roadLength - t[i - 1] + t[0] - 2;
                    tem.Add(roadLength - t[i - 1] + t[0] - 2);
                }
                else
                {
                    distancesBetweenCars[i - 1] = roadLength - t[i - 1] + t[0] - 1;
                    tem.Add(roadLength - t[i - 1] + t[0] - 1);
                }
                distances.Add(tem);
                /*int startcar = -1;
                List<int> tem = new List<int>();
                int roadIndex = 0;
                distances.Add(tem);
                int prevCarIndex=-1;
                int lengthBetweenCars = 0;
                while (temp[roadIndex, laneIndex] == 0)
                {
                    roadIndex = (roadIndex + 1) % temp.GetLength(0);
                }

                while (roadIndex!=startcar)
                {
                    if (temp[roadIndex, laneIndex] != 0)
                    {
                        
                        if (startcar == -1)
                        {
                            startcar = roadIndex;
                        }
                        if (prevCarIndex == -1)
                        {
                            prevCarIndex = roadIndex;
                        }
                        else 
                        {
                            int prevRoadIndex = roadIndex == 0 ? temp.GetLength(0) - 1 : roadIndex - 1;
                            if (temp[prevRoadIndex, laneIndex] == 0 || temp[prevRoadIndex, laneIndex] != temp[roadIndex, laneIndex])
                            {
                                if (roadIndex < prevCarIndex)
                                {
                                    tem.Add(temp.GetLength(0)-prevCarIndex+roadIndex+1);
                                }
                                tem.Add(roadIndex-prevCarIndex+1);

                            }
                            prevCarIndex = roadIndex;
                        }
                        
                    }
                    roadIndex = (roadIndex + 1) % temp.GetLength(0);


                }
            }

            string[] colors = { ", 'color', 'b'", ", 'color', 'r'", ", 'color', 'k'" };

            string b = "";
            b = b + "\n hold on \n";
            for (int i = 0; i < 1; i++)
            {
                b = b + "b" + i.ToString() + "=[";
                for (int j = 0; j < distances[i].Count; j++)
                {
                    b = b + distances[i][j].ToString() + ";";
                }
                b = b + "];\n";
                b = b + "plot(1:1:size(b" + i.ToString() + "), b" + i.ToString() + ",'or'" + colors[i] + ");\n";
            }

            b = b + "xlim([0,10]);\n";
            b = b + "ylim([0,25]);\n";

            b = b + "title('Крайняя правая полоса');";
            b = b + "xlabel('Номер пары машин');";
            b = b + "ylabel('Расстояние в паре');";

            string c = "";
            c = c + "\n hold on \n";
            for (int i = 1; i < 2; i++)
            {
                c = c + "c" + i.ToString() + "=[";
                for (int j = 0; j < distances[i].Count; j++)
                {
                    c = c + distances[i][j].ToString() + ";";
                }
                c = c + "];\n";
                c = c + "plot(1:1:size(c" + i.ToString() + "), c" + i.ToString() + ",'or'" + colors[i] + ");\n";
            }

            c = c + "xlim([0,10]);\n";
            c = c + "ylim([0,25]);\n";

            c = c + "title('Средняя полоса');";
            c = c + "xlabel('Номер пары машин');";
            c = c + "ylabel('Расстояние в паре');";

            string d = "";//пара машин, расстояние в паре  Средняя полоса Крайняя правая полоса
            d = d + "\n hold on \n";
            for (int i = 2; i < 3; i++)
            {
                d = d + "d" + i.ToString() + "=[";
                for (int j = 0; j < distances[i].Count; j++)
                {
                    d = d + distances[i][j].ToString() + ";";
                }
                d = d + "];\n";
                d = d + "plot(1:1:size(d" + i.ToString() + "), d" + i.ToString() + ",'or'" + colors[i] + ");\n";
            }

            d = d + "xlim([0,10]);\n";
            d = d + "ylim([0,25]);\n";

            d = d + "title('Крайняя левая полоса');";
            d = d + "xlabel('Номер пары машин');";
            d = d + "ylabel('Расстояние в паре');";

            /*string f = "";//пара машин, расстояние в паре
            f = f + "\n hold on \n";
            for (int i = 1; i < 2; i++)
            {
                f = f + "f" + i.ToString() + "=[";
                for (int j = 0; j < distances[i].Count; j++)
                {
                    f = f + distances[i][j].ToString() + ";";
                }
                f = f + "];\n";
                f = f + "plot(1:1:size(f" + i.ToString() + "), f" + i.ToString() + ",'or'" + colors[i] + ");\n";
            }

            f = f + "xlim([0,10]);\n";
            f = f + "ylim([0,25]);\n";

            string a="";
            for (int i=0; i< distances.Count; i++)
            {
                a = a + "a" + i.ToString()+"=[";
                for (int j=0;j<distances[i].Count; j++)
                {
                    a=a+distances[i][j].ToString()+";";
                }
                a = a + "];\n";
            }
            a = a + "\n hold on \n";
            
            for (int i = 0; i < distances.Count; i++)
            {
                a = a + "plot(1:1:size(a" + i.ToString() + "), a"+ i.ToString() + ",'or'"+colors[i]+");\n";
            }
            a = a + "\n figure; \n";



            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    byte [] info = new UTF8Encoding(true).GetBytes(a.ToString());
                    fs.Write(info, 0, info.Length);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                using (FileStream fs = new FileStream(path2, FileMode.Append, FileAccess.Write))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(b.ToString());
                    fs.Write(info, 0, info.Length);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                using (FileStream fs = new FileStream(path3, FileMode.Append, FileAccess.Write))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(c.ToString());
                    fs.Write(info, 0, info.Length);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                using (FileStream fs = new FileStream(path4, FileMode.Append, FileAccess.Write))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(d.ToString());
                    fs.Write(info, 0, info.Length);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            */
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

            dataGridView3.RowCount = properties.GetLength(0);
            dataGridView3.ColumnCount = properties.GetLength(1);
            for (int i = 0; i < properties.GetLength(0); i++)
            {
                for (int j = 0; j < properties.GetLength(1); j++)
                {
                    dataGridView3.Rows[i].Cells[j].Value = properties[i, j];

                }
            }
            dataGridView3.AutoResizeColumns();
        }

        /*private void move()
        {

        }*/
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

        //TODO rename "properties" to something with more sense
        private bool isPublicTransport(int[,] properties, int objectIndex)
        {
            return properties[objectIndex, 2] == 1;
        }

        //TODO rename "properties" to something with more sense
        //TODO Mb you should rename "Car" to something more acceptable in current context ?
        private bool isCar(int[,] properties, int objectIndex)
        {
            return properties[objectIndex, 2] == 2;
        }

        private bool isTimeToChangeLane(int[,] properties, int objectIndex)
        {
            return properties[objectIndex, 1] == 0;
        }
        private bool isNextObjectSame(int[,] properties, int[,] temp, int objectIndex, int nextObjectIndex, int laneIndex)
        {
            return temp[nextObjectIndex, laneIndex] == properties[objectIndex, 0];
        }
        //temp[roadIndex, laneIndex]== temp[secondPartIndex, laneIndex] &&
        private bool publicTransportCouldMoveToRight(int[,] temp, int roadIndex, int secondPartIndex, int nextRoadIndex, int laneIndex)
        {
            int rightLaneIndex = laneIndex + 1;
            return temp[roadIndex, rightLaneIndex] == 0 && temp[secondPartIndex, rightLaneIndex] == 0
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
        // проверяем на -1, так как попадаем в отбойник
        private bool isForvardCarMoving(int roadIndex, int laneIndex, int roadOffSet, int laneOffSet)
        {
            bool forwardCanNotMoving=false;
            int ind = 0;

            while (temp[(roadIndex + roadOffSet + ind) % temp.GetLength(0), laneIndex + laneOffSet] != 0 && (roadIndex + roadOffSet + ind) % temp.GetLength(0) != (roadIndex + roadOffSet) % temp.GetLength(0)) 
            {
                int propIndex = Array.IndexOf(carIndexToPropetyArray, temp[(roadIndex + roadOffSet + ind) % temp.GetLength(0), laneIndex + laneOffSet]);
                forwardCanNotMoving = propIndex != -1 && properties[propIndex, 4] == 1;
                ind++;
            }
            
            return forwardCanNotMoving;
        }

        private bool isRoadCellEmpty(int roadIndex, int laneIndex, int roadOffSet, int laneOffSet) {
            //MessageBox.Show(""+ temp[(roadIndex + roadOffSet) % temp.GetLength(0), laneIndex + laneOffSet]);

            return temp[(roadIndex + roadOffSet) % temp.GetLength(0), laneIndex + laneOffSet] == 0;
        }

        private bool isCarCanMove(int roadIndex, int laneIndex, int roadOffSet, int laneOffSet)
        {
            return isRoadCellEmpty(roadIndex, laneIndex, roadOffSet, laneOffSet) || isForvardCarMoving(roadIndex, laneIndex, roadOffSet, laneOffSet); 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private bool carCouldMoveToLeft(int[,] temp, int roadIndex, int nextRoadIndex, int laneIndex)
        {
            int leftLaneIndex = laneIndex - 1;
            return temp[roadIndex, leftLaneIndex] == 0 && temp[nextRoadIndex, leftLaneIndex] == 0;
        }
    }

    
}
