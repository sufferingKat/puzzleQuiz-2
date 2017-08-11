using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace puzzleQuiz_2
{
    public partial class Form1 : Form
    {
        int m_cost = 0;
        int m_pay = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void MakeInfoInt(double cost, double pay)
        {
            try
            {
                m_cost = Convert.ToInt32(cost * 100);
                m_pay = Convert.ToInt32(pay * 100);
            }
            catch (OverflowException)
            {
                richTextBox1.AppendText("Number too large.\n");
            }
        }

        public void PrintInfo(string text)
        {
            richTextBox1.AppendText(text);
        }

        public void UpdateLabels(Counters counter, string text)
        {
            switch (counter)
            {
                case Counters.PENNY:
                    label1Penny.Text = text;
                    break;
                case Counters.TWOPENCE:
                    label2Penny.Text = text;
                    break;
                case Counters.FIVEPENCE:
                    label5Penny.Text = text;
                    break;
                case Counters.TENPENCE:
                    label10Penny.Text = text;
                    break;
                case Counters.TWENTYPENCE:
                    label20Penny.Text = text;
                    break;
                case Counters.FIFTYPENCE:
                    label50Penny.Text = text;
                    break;
                case Counters.POUND:
                    label1Pounds.Text = text;
                    break;
                case Counters.TWOPOUND:
                    label2Pounds.Text = text;
                    break;
                case Counters.FIVEPOUND:
                    label5Pounds.Text = text;
                    break;
                case Counters.TENPOUND:
                    label10Pounds.Text = text;
                    break;
                case Counters.TWENTYPOUND:
                    label20Pounds.Text = text;
                    break;
                case Counters.FIFTYPOUND:
                    label50Pounds.Text = text;
                    break;
                case Counters.HUNDREDPOUND:
                    label100Pounds.Text = text;
                    break;
                case Counters.TWOHUNDREDPOUND:
                    label200Pounds.Text = text;
                    break;
                case Counters.FIVEHUNDREDPOUND:
                    label500Pounds.Text = text;
                    break;
                case Counters.THOUSANDPOUND:
                    label1000Pounds.Text = text;
                    break;
                default:
                    richTextBox1.AppendText("Error updating labels.");
                    break;
            }
        }

        void ClearLabels()
        {
            for (Counters i = Counters.FIVEPENCE; i < Counters.END; i++)
                UpdateLabels(i, "");
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear(); //clear window from previous messages and results
            ClearLabels();

            if (double.TryParse(itemCostBox.Text, out double tempCost)) { } //attempt to parse the user input
            else
            {
                richTextBox1.AppendText("Failed to parse Cost.\n");
            }
            if (double.TryParse(userPayBox.Text, out double tempPay)) { }
            else
            {
                richTextBox1.AppendText("Failed to parse Pay.\n");
            }

            MakeInfoInt(tempCost, tempPay); //convert to int and make sure numbers are within correct range

            CoinCalculation calculateThing = new CoinCalculation();

            if ((m_cost > 0) && (m_pay > 0)) //ensure inputs are not negative or null
            {
                if (m_cost == m_pay) //make sure there is a point in running calculations
                {
                    richTextBox1.Clear();
                    richTextBox1.AppendText("Price matches input.\nNo return neccessary.");
                }
                if (m_cost > m_pay) //make sure user paid enough
                {
                    richTextBox1.Clear();
                    richTextBox1.AppendText("Not enough money.");
                }
                else
                {
                    calculateThing.RunCalculate(m_cost, m_pay);
                    calculateThing.PostValues(this);
                }
            }
            else
            {
                richTextBox1.AppendText("Something went wrong.");
            }
            
        }
    }
}
