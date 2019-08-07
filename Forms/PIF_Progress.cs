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

namespace NetworkGUI.Forms
{
    public partial class PIF_Progress : Form
    {

        List<int> Years;
        //labels for text
        string endYearLabel;
        string startYearLabel;
        string curYearLabel;

        //file names
        string file;
        string inputFile;

        //function vars
        int order = -1;
        bool isNull = false;

        public PIF_Progress(List<int> Y, string f, string inFile, int o, bool isN)
        {
            InitializeComponent();
            Shown += new EventHandler(Form1_Shown);

            Years = Y;
            file = f;
            inputFile = inFile;
            order = o;
            isNull = isN;

            //labels
            endYearLabel = Years[Years.Count - 1].ToString();
            startYearLabel = Years[0].ToString();
            curYearLabel = "0000";

            label1.Text = "Saving year " + curYearLabel + " from " + startYearLabel + " -" + endYearLabel;
            progressBar1.Maximum = Years.Count;

            //*WARNING*
            //do no edit below
            //--------------------------------------------------------------------------------------------------------------------
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }


        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) //work here
        {
            PathBasedImbalance PIF = new PathBasedImbalance();
            for (int p = 0; p < progressBar1.Maximum; p++)
            {
                //for (int j = 0; j < 1000000000; j++) { }
                backgroundWorker1.ReportProgress(p +1);
                curYearLabel = Years[p].ToString();

                var output_writer_data = new StreamWriter(file, true);
                double[,] matrix = PIF.supportScript(inputFile, order, isNull, Years[p]);


                //Angela
                int totalRows = matrix.GetLength(0);
                int totalCols = matrix.GetLength(1);

                for (int i = 0; i < totalRows; i++)
                {
                    string[] dataRow = new string[totalCols];
                    for (int j = 0; j < totalCols; j++)
                    {
                        dataRow[j] = matrix[i, j].ToString();//>= 0? mTable[i, j].ToString() : "#N/A";
                    }
                    output_writer_data.WriteLine(String.Join(",", dataRow));
                    output_writer_data.Flush();
                }
                output_writer_data.Close();
            }
        }

        void Form1_Shown(object sender, EventArgs e)
        {
            // Start the background worker
            backgroundWorker1.RunWorkerAsync();
        }

        // Back on the 'UI' thread so we can update the progress bar
        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Saving year " + curYearLabel + " from " + startYearLabel + " -" + endYearLabel;
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done");
            Close();
        }

     












        
        private void PIF_Progress_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
