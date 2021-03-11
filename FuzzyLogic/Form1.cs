using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotFuzzy;

namespace FuzzyLogic
{
    public partial class Form1 : Form
    {
        FuzzyEngine traf;
        MembershipFunctionCollection tex, spi, ss; //textile yarn, stitch per inch, seam strength
        LinguisticVariable _tex, _spi, _ss;
        FuzzyRuleCollection _rules;

        private void button1_Click(object sender, EventArgs e)
        {
            double tex_val, spi_val;
            if(textBox1.Text.Length != 0)
            {
                tex_val = Convert.ToDouble(textBox1.Text);
                spi_val = Convert.ToDouble(textBox2.Text);
                if(tex_val > 16 && spi_val > 6 && tex_val < 60 && spi_val < 17)
                {
                    _tex.InputValue = (tex_val);
                    _tex.Fuzzify("LOW");
                    _spi.InputValue = (spi_val);
                    _spi.Fuzzify("HIGH");
                }
                else if(tex_val < 17 || tex_val > 59)
                {
                    MessageBox.Show("Thread Linear Density Input must be between 16-60");
                }
                else if (spi_val < 8)
                {
                    MessageBox.Show("Seams Per Inch Input must be between 7-16");
                }

            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            setFuzzyEngine();
            traf.Consequent = "SS";
            textBox3.Text = "" + traf.Defuzzify();
        }

        public Form1()
        {
            InitializeComponent();
            setMembers();
            setRules();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        public void setMembers()
        {
            tex = new MembershipFunctionCollection();
            tex.Add(new MembershipFunction("LOW", 15.0, 20.0, 20.0, 32.0));
            tex.Add(new MembershipFunction("MEDIUM", 22.0, 35.0, 47.0, 47.0));
            tex.Add(new MembershipFunction("HIGH", 37.0, 37.0, 50.0, 60.0));
            _tex = new LinguisticVariable("TEX", tex);

            spi = new MembershipFunctionCollection();
            spi.Add(new MembershipFunction("LOW", 6.0, 8.0, 8.0, 11.2));
            spi.Add(new MembershipFunction("MEDIUM", 8.5, 12.0, 12.0, 15.3));
            spi.Add(new MembershipFunction("HIGH", 12.7, 16.0, 16.0, 18.0));
            _spi = new LinguisticVariable("SPI", spi);

            ss = new MembershipFunctionCollection();
            ss.Add(new MembershipFunction("VLOW", 335.13, 410.13, 410.13, 500.0));
            ss.Add(new MembershipFunction("LOW", 410.13, 500.0, 650.0, 650.0));
            ss.Add(new MembershipFunction("MEDIUM", 500.0, 500.0, 650.0, 800.0));
            ss.Add(new MembershipFunction("HIGH", 650.0, 650.0, 800.0, 900.0));
            ss.Add(new MembershipFunction("VHIGH", 800.0, 940.0, 940.0, 1000.0));
            _ss = new LinguisticVariable("SS", ss);
        }

        public void setRules()
        {
            _rules = new FuzzyRuleCollection();
            _rules.Add(new FuzzyRule("IF (TEX IS LOW) AND (SPI IS LOW) THEN SS IS VLOW"));
            _rules.Add(new FuzzyRule("IF (TEX IS LOW) AND (SPI IS MEDIUM) THEN SS IS LOW"));
            _rules.Add(new FuzzyRule("IF (TEX IS LOW) AND (SPI IS HIGH) THEN SS IS MEDIUM"));
            _rules.Add(new FuzzyRule("IF (TEX IS MEDIUM) AND (SPI IS LOW) THEN SS IS LOW"));
            _rules.Add(new FuzzyRule("IF (TEX IS MEDIUM) AND (SPI IS MEDIUM) THEN SS IS MEDIUM"));
            _rules.Add(new FuzzyRule("IF (TEX IS MEDIUM) AND (SPI IS HIGH) THEN SS IS HIGH"));
            _rules.Add(new FuzzyRule("IF (TEX IS HIGH) AND (SPI IS LOW) THEN SS IS MEDIUM"));
            _rules.Add(new FuzzyRule("IF (TEX IS HIGH) AND (SPI IS MEDIUM) THEN SS IS HIGH"));
            _rules.Add(new FuzzyRule("IF (TEX IS HIGH) AND (SPI IS HIGH) THEN SS IS VHIGH"));

        }



        public void setFuzzyEngine()
        {
            traf = new FuzzyEngine();
            traf.LinguisticVariableCollection.Add(_tex);
            traf.LinguisticVariableCollection.Add(_spi);
            traf.LinguisticVariableCollection.Add(_ss);
            traf.FuzzyRuleCollection = _rules;
        }

    }
}
