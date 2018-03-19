using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APRvsAPY
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            seeResultsButton.Enabled = false;
            resetButton.Enabled = false;
        }

                                                    // BORROW VS INVEST

        private void seeResultsButton_Click(object sender, EventArgs e)
        {
            determineAPR(out double totalCost);
            determineAPY(out double totalGain);
            determineColors(totalCost, totalGain);
            determineTip(totalCost, totalGain);

            resetButton.Enabled = true;
        }
    
        // Deremnine Annual Percentage rate

        private void determineAPR(out double totalCost)
        {
            
            string loanAmount = textBoxLoanAmount.Text;
            string loanTerm = comboBoxLoanTerm.Text;
            string APRtext = textBoxAPR.Text;
            double APR = (double.Parse(APRtext) / 100) / 12;
            //formula
            double partOneA = 1 + APR;
            double partOneB = Math.Pow(partOneA, double.Parse(loanTerm));
            double partOne = APR * partOneB;
            double partTwo = partOneB - 1;
            double partThree = partOne / partTwo;
            double monthlyPayment = double.Parse(loanAmount) * partThree;
            //end formula
            totalCost = (monthlyPayment * double.Parse(loanTerm)) - double.Parse(loanAmount);
            costLabel.Text = String.Format("{0:C}", totalCost);

        }

        // Determine Annual Percentage Yield 

        private void determineAPY(out double totalGain)
        {
            string depositAmount = textBoxDepositAmount.Text;
            string depositTerm = comboBoxDepositTerm.Text;
            string interestRateText = textBoxAPY.Text;
            double interestRate = (double.Parse(interestRateText) / 100) / 12;
            //formula
            double pOneA = 1 + interestRate;
            double pOneB = Math.Pow(pOneA, double.Parse(depositTerm));
            double pOneC = pOneB - 1;
            totalGain = double.Parse(depositAmount) * pOneC;
            //end formula 
            gainLabel.Text = String.Format("{0:C}", totalGain);
        }
        
        // Determine result colors (total loan cost vs total deposit gain) 

        private void determineColors(double totalCost, double totalGain)
        {
            if (totalCost > totalGain)
            {
                costLabel.ForeColor = System.Drawing.Color.Green;
                gainLabel.ForeColor = System.Drawing.Color.Red;
            }
            if (totalCost < totalGain)
            {
                costLabel.ForeColor = System.Drawing.Color.Red;
                gainLabel.ForeColor = System.Drawing.Color.Green;
            }
            
        }

        // Determine suggestion 

        private void determineTip(double totalCost, double totalGain)
        {
            if (totalCost > totalGain)
            {
                double result = totalCost - totalGain;
                tipLabel.Text = String.Format("You can save {0:C} by paying off your current loan!",
                    result);
                tipLabelTwo.Text = String.Format(""
                    );
                tipLabelThree.Text = String.Format(""
                    );

                determineRefiCost(out double totalRefiCost);
                
                if (totalGain > totalRefiCost)
                {
                    double depositAmount = double.Parse(textBoxDepositAmount.Text);
                    double earningsAfterRefi = (totalGain - totalRefiCost) + result;
                    tipLabel.Text = String.Format("You can save {0:C} by paying off your current loan;",
                    result
                    );
                    tipLabelTwo.Text = String.Format("However, you can earn up to {0:C} if you take advantage of our low rates",
                        earningsAfterRefi
                    );
                    tipLabelThree.Text = String.Format("and deposit {0:C} into {1}M Share Certificate!",
                        depositAmount,
                        comboBoxDepositTerm.Text
                    );
                }
            }

            if (totalCost < totalGain)
            {
                double results = totalGain - totalCost;
                string cashAmountText = textBoxDepositAmount.Text;
                double cashAmount = double.Parse(cashAmountText);
                string term = comboBoxDepositTerm.Text;
                
                tipLabel.Text = String.Format("You can earn up to {0:C} by depositing {1:C} into {2}M Share Certificate!",
                    results,
                    cashAmount,
                    term);
                tipLabelTwo.Text = String.Format(""
                    );
                tipLabelThree.Text = String.Format(""
                    );
                determineRefiCost(out double totalRefiCost);
                if (totalCost > totalRefiCost)
                {
                    double refiResult = totalCost - totalRefiCost;
                    double totalEarnings = results + refiResult;
                    tipLabel.Text = String.Format("You can earn up to {0:C} by depositing {1:C} into {2}M Share Certificate!",
                    results,
                    cashAmount,
                    term
                    );
                    tipLabelTwo.Text = String.Format("Additionally, you can earn up to {0:C} if you take advantage of our low rates,",
                        refiResult
                    );
                    tipLabelThree.Text = String.Format("totaling {0:C} in earnings!",
                        totalEarnings
                    );
                }
            }
        }
        
        // Determine refi cost
        private void determineRefiCost(out double totalRefiCost)
        {

            string loanAmount = textBoxLoanAmount.Text;
            string loanTerm = comboBoxLoanTerm.Text;
            double APR = (3.45 / 100) / 12;
            double partOneA = 1 + APR;
            double partOneB = Math.Pow(partOneA, double.Parse(loanTerm));
            double partOne = APR * partOneB;
            double partTwo = partOneB - 1;
            double partThree = partOne / partTwo;
            double monthlyPayment = double.Parse(loanAmount) * partThree;
            //end formula
            totalRefiCost = (monthlyPayment * double.Parse(loanTerm)) - double.Parse(loanAmount);
        }

        private void textBoxLoanAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) &
                (Keys)e.KeyChar != Keys.Back & e.KeyChar != ('.') &
                (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBoxDepositAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) &
                (Keys)e.KeyChar != Keys.Back & e.KeyChar != ('.') &
                (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void comboBoxLoanTerm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) &
                (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void comboBoxDepositTerm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) &
                (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBoxAPR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) &
                (Keys)e.KeyChar != Keys.Back & e.KeyChar != ('.') &
                (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBoxAPY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) &
                (Keys)e.KeyChar != Keys.Back & e.KeyChar != ('.') &
                (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBoxLoanAmount_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLoanAmount.Text))
            {
                e.Cancel = true;
                textBoxLoanAmount.Focus();
                errorProvider.SetError(textBoxLoanAmount, "Please enter your loan amount");
            }
            else
            { 
                e.Cancel = false;
                errorProvider.SetError(textBoxLoanAmount, null);
            }
        }

        private void textBoxDepositAmount_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDepositAmount.Text))
            {
                e.Cancel = true;
                textBoxDepositAmount.Focus();
                errorProvider.SetError(textBoxDepositAmount, "Please enter your deposit amount");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(textBoxDepositAmount, null);
            }
        }


        private void comboBoxLoanTerm_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxLoanTerm.Text))
            {
                e.Cancel = true;
                comboBoxLoanTerm.Focus();
                errorProvider.SetError(comboBoxLoanTerm, "Please choose your term");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(comboBoxLoanTerm, null);
            }
        }

        private void comboBoxDepositTerm_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxDepositTerm.Text))
            {
                e.Cancel = true;
                comboBoxDepositTerm.Focus();
                errorProvider.SetError(comboBoxDepositTerm, "Please choose your term");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(comboBoxDepositTerm, null);
            }
        }

        private void textBoxAPR_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAPR.Text))
            {
                e.Cancel = true;
                textBoxAPR.Focus();
                errorProvider.SetError(textBoxAPR, "Please enter Annual Percentage Rate");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(textBoxAPR, null);
            }
        }

        private void textBoxAPY_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAPY.Text))
            {
                e.Cancel = true;
                textBoxAPY.Focus();
                errorProvider.SetError(textBoxAPY, "Please enter Annual Percentage Yield");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(textBoxAPY, null);
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Form1 NewForm = new Form1();
            NewForm.Show();
            this.Dispose(false);
        }

        private void textBoxAPY_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAPY.Text.Trim().Length > 0)
            {
                seeResultsButton.Enabled = true;
            }
            else
            {
                seeResultsButton.Enabled = false;
            }
        }


        // END BORROW VS INVEST

        // LOAN COMPARISON

        private void seeResultsButton1_Click(object sender, EventArgs e)
        {
            
            loanOneResults();
            loanTwoResults();
            loanThreeResuts();
        }

        private void resetButton1_Click(object sender, EventArgs e)
        {
            Form1 NewForm = new Form1();
            NewForm.Show();
            this.Dispose(false);
        }

        private void loanOneResults()
        {

            if (textBoxAmount1.Text.Trim().Length > 0)
            {
                string loanAmount = textBoxAmount1.Text;
                string loanTerm = comboBoxTerm1.Text;
                string APRtext = textBoxAPR1.Text;
                double APR = (double.Parse(APRtext) / 100) / 12;
                //formula
                double partOneA = 1 + APR;
                double partOneB = Math.Pow(partOneA, double.Parse(loanTerm));
                double partOne = APR * partOneB;
                double partTwo = partOneB - 1;
                double partThree = partOne / partTwo;
                double monthlyPayment = double.Parse(loanAmount) * partThree;
                //end formula
                double totalCost = (monthlyPayment * double.Parse(loanTerm)) - double.Parse(loanAmount);
                labelMonthlyPayment1.Text = String.Format("{0:C}", monthlyPayment);
                labelLoanCost1.Text = String.Format("{0:C}", totalCost);
            }
            else
            {
                return;
            }
        }

        private void loanTwoResults()
        {
            if (textBoxAmount2.Text.Trim().Length > 0)
            {
                string loanAmount = textBoxAmount2.Text;
                string loanTerm = comboBoxTerm2.Text;
                string APRtext = textBoxAPR2.Text;
                double APR = (double.Parse(APRtext) / 100) / 12;
                //formula
                double partOneA = 1 + APR;
                double partOneB = Math.Pow(partOneA, double.Parse(loanTerm));
                double partOne = APR * partOneB;
                double partTwo = partOneB - 1;
                double partThree = partOne / partTwo;
                double monthlyPayment = double.Parse(loanAmount) * partThree;
                //end formula
                double totalCost = (monthlyPayment * double.Parse(loanTerm)) - double.Parse(loanAmount);
                labelMonthlyPayment2.Text = String.Format("{0:C}", monthlyPayment);
                labelLoanCost2.Text = String.Format("{0:C}", totalCost);
            }
            else
            {
                return;
            }
        }

        private void loanThreeResuts()
        {
            if (textBoxAmount3.Text.Trim().Length > 0)
            {
                string loanAmount = textBoxAmount3.Text;
                string loanTerm = comboBoxTerm3.Text;
                string APRtext = textBoxAPR3.Text;
                double APR = (double.Parse(APRtext) / 100) / 12;
                //formula
                double partOneA = 1 + APR;
                double partOneB = Math.Pow(partOneA, double.Parse(loanTerm));
                double partOne = APR * partOneB;
                double partTwo = partOneB - 1;
                double partThree = partOne / partTwo;
                double monthlyPayment = double.Parse(loanAmount) * partThree;
                //end formula
                double totalCost = (monthlyPayment * double.Parse(loanTerm)) - double.Parse(loanAmount);
                labelMonthlyPayment3.Text = String.Format("{0:C}", monthlyPayment);
                labelLoanCost3.Text = String.Format("{0:C}", totalCost);
            }
            else
            {
                return;
            }
            
        }
        // END LOAN COMPARISON



        // CD EARLY WITHDRAWAL


        private void resultsButton_Click(object sender, EventArgs e)
        {
            determinePotentialGain();
            determineCurrentGain(out double totalGain);
            determinePenaltyGain(out double totalPenaltyGain);
            determineEarlyWithdrawalPenalty(totalGain, totalPenaltyGain, out double earlyWithdrawal);
            determineNewGain(earlyWithdrawal);
            numberOfTerms(earlyWithdrawal);
            
        }

        private void determinePotentialGain()
        {
            double term = double.Parse(textBoxInitialTerm.Text);
            string depositAmount = textBoxInitialDepositAmount.Text;
            string interestRateText = textBoxAPY1.Text;
            double interestRate = (double.Parse(interestRateText) / 100) / 12;
            //formula
            double pOneA = 1 + interestRate;
            double pOneB = Math.Pow(pOneA, term);
            double pOneC = pOneB - 1;
            double totalNewGain = double.Parse(depositAmount) * pOneC;
            //end formula 
            labelPotentialGain.Text = String.Format("{0:C}", totalNewGain);
        }

        private void determineNewGain(double earlyWithdrawal)
        {

            double term = double.Parse(textBoxNewTerm.Text);
            string depositAmount = textBoxNewDepositAmount.Text;
            string interestRateText = textBoxNewAPY.Text;
            double interestRate = (double.Parse(interestRateText) / 100) / 12;
            //formula
            double pOneA = 1 + interestRate;
            double pOneB = Math.Pow(pOneA, term);
            double pOneC = pOneB - 1;
            double totalNewGain = double.Parse(depositAmount) * pOneC;
            //end formula 
            labelNewGain.Text = String.Format("{0:C}", totalNewGain);

            

        }
        private void numberOfTerms(double earlyWithdrawal)
        {


            int term = 1;
            string depositAmount = textBoxNewDepositAmount.Text;
            string interestRateText = textBoxNewAPY.Text;
            double interestRate = (double.Parse(interestRateText) / 100) / 12;
            //formula
            double pOneA = 1 + interestRate;
            double pOneB = Math.Pow(pOneA, term);
            double pOneC = pOneB - 1;
            double totalNewGain = double.Parse(depositAmount) * pOneC;
            double numberOfTerms1 = earlyWithdrawal / totalNewGain;
            double numberOfTerms = Math.Round(numberOfTerms1);
            labelNumberOfTerms.Text = numberOfTerms.ToString();
            
            
        }
        private void determineEarlyWithdrawalPenalty(double totalGain, double totalPenaltyGain, out double earlyWithdrawal)
        {
            earlyWithdrawal = totalGain - totalPenaltyGain;
            earlyWithdrawalLabel.Text = String.Format("{0:C}", earlyWithdrawal);
        }

        private void determineCurrentGain(out double totalGain)
        {
            double totalDays = DateTime.Today.Subtract(dateTimePickerContractDate.Value.Date).TotalDays;
            double currentTerm1 = totalDays / 30;
            double currentTerm = Math.Round(currentTerm1);

            string depositAmount = textBoxInitialDepositAmount.Text;
            string interestRateText = textBoxAPY1.Text;
            double interestRate = (double.Parse(interestRateText) / 100) / 12;
            //formula
            double pOneA = 1 + interestRate;
            double pOneB = Math.Pow(pOneA, currentTerm);
            double pOneC = pOneB - 1;
            totalGain = double.Parse(depositAmount) * pOneC;
            //end formula 
           

        }

        private void determinePenaltyGain(out double totalPenaltyGain)
        {
            double totalDays = DateTime.Today.Subtract(dateTimePickerContractDate.Value.Date).TotalDays;
            double currentTerm1 = totalDays / 30;
            double currentTerm = Math.Round(currentTerm1);
        
            decimal penalty = numericUpDownPenalty.Value;
            double penalty1 = currentTerm - (double)penalty;

            string depositAmount = textBoxInitialDepositAmount.Text;
            string interestRateText = textBoxAPY1.Text;
            double interestRate = (double.Parse(interestRateText) / 100) / 12;
            //formula
            double pOneA = 1 + interestRate;
            double pOneB = Math.Pow(pOneA, penalty1);
            double pOneC = pOneB - 1;
            totalPenaltyGain = double.Parse(depositAmount) * pOneC;
            //end formula 
            totalPenaltyLabel.Text = String.Format("{0:C}", totalPenaltyGain);

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }


        // END CD EARLY WITHDRAWAL
    }
}
