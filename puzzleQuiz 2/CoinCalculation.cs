using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace puzzleQuiz_2
{
    public enum Counters //ordered weirdly due to how it's calculated
    {
        FIVEPENCE,
        TWOPENCE,
        PENNY,
        FIFTYPENCE,
        TWENTYPENCE,
        TENPENCE,
        FIVEPOUND,
        TWOPOUND,
        POUND,
        FIFTYPOUND,
        TWENTYPOUND,
        TENPOUND,
        FIVEHUNDREDPOUND,
        TWOHUNDREDPOUND,
        HUNDREDPOUND,
        THOUSANDPOUND,
        END
    }
    class CoinLibrary
        // currently no need for a function that edits a value, could be added later
    {
        private Dictionary<Counters, int> countLibrary = new Dictionary<Counters, int>();


        public CoinLibrary()
        {
            for (Counters i = Counters.FIVEPENCE; i < (Counters.END); i++)
            {
                countLibrary.Add(i, 0);
            }
        }
        public void SetCoin(Counters cType, int amount) //for adding a new coin
        {
            countLibrary[cType] = amount;
        }

        public int GetCoin(Counters cType) //for retrieving how many of a coin there are
        {
            return countLibrary[cType];
        }
    }

    class CoinCalculation
    {
        CoinLibrary coinLib = new CoinLibrary();

        private int costInt;

        void CalculateReturns()
        {
            for (Counters i = Counters.FIVEPENCE; i < (Counters.END);)
            {
                if (i == Counters.THOUSANDPOUND && costInt >= 1)
                {
                    coinLib.SetCoin(i, costInt);
                    break;
                }

                if (costInt >= 1)
                {
                    if ((costInt % 10) >= 5) //check for needing a 5 coin/bill
                    {
                        coinLib.SetCoin(i, 1);
                        costInt -= 5;
                    }
                    i++;
                    if ((costInt % 10) >= 2) //check for needing a 2 coin/bill
                    {
                        int tempV = ((costInt % 10) / 2);
                        coinLib.SetCoin(i, tempV);
                        costInt -= (tempV * 2);
                    }
                    i++;
                    if ((costInt % 10) == 1) //check for a 1 coin/bill
                    {
                        coinLib.SetCoin(i, 1);
                        costInt -= 1;
                    }
                    i++;

                    costInt = (costInt / 10); //remove last digit and continue

                    if (costInt == 0)
                        break; //if empty, break early
                }
                else
                    break;
            }
        }

        public void RunCalculate(int itemCost, int userPay)
        {
            costInt = userPay - itemCost;

            CalculateReturns();
        }

        public void PostValues(Form1 form)
        {
            for (Counters i = Counters.FIVEPENCE; i < (Counters.END); i++)
            {
                if (coinLib.GetCoin(i) != 0)
                {
                    //form.PrintInfo(Convert.ToString(coinLib.GetCoin(i)) + " " + ((Counters)i).ToString().ToLower() + "\n");
                    form.UpdateLabels(i, Convert.ToString(coinLib.GetCoin(i)) + " " + ((Counters)i).ToString().ToLower());
                }
            }
        }
    }
}
