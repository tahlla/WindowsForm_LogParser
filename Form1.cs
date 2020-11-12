//bring these in to use different methods and functions
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

//name of program
namespace WindowsForms_LogParse2
{
 //will link to the Form (GUI)
    public partial class Form1 : Form
    {
        //declaring a variable for later use, and it needed to be declared outside of the button
        string filename;

        //Tells the form to initialize - shows button/text file box on GUI
            public Form1()
        {
            InitializeComponent();
        }

        //Creates an object to store parsed data and its count
        //public class declares a class that has variables and functions
        //a class can be used as an object which is the basis of object oriented programing
        public class DataString
        {
            public string dataname { get; set; }
            public int datacount { get; set; }
        }

        //Declare and initialize a list of the DataString object 
        List<DataString> parseddata = new List<DataString>();

        //This method will execute when button one is clicked
        private void button1_Click(object sender, EventArgs e)
        {
            //if a new file is selected then this clears the text box 
            textBox1.Clear();
            //this will clear the list of DataString objects is cleared
            parseddata.Clear();

           
            //creates an object to open the file explorer
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //If the file explorer opens and everything is okay then the file selected will be uploaded as file name
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string strfilename = openFileDialog1.FileName;
                maskedTextBox1.Text = strfilename;
                filename = strfilename;
            }
        }



         
        
        //This is button2 and will parse data that will be shown in textbox2
        private void button2_Click(object sender, EventArgs e)
        {

            // Read lines from source file
            string[] arr = File.ReadAllLines(filename);
            
            // x = 0 declare and initializes x for the loop set to 0
            //for each is a loop that will go over and over 
            int x = 0;
            foreach (string value in arr)
            {
                
             //Will parse the data read from the file into an array 
             //, \n are delimiters that can split the information 
             //a , or newline will be a chunck of data
            
                string[] tempdataholder = arr[x].Split(new Char[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                x++;

                //This for loop will check the list for the string from tempdataholder (array of strings) 
                //If the string is found it will increase the count for that string 
                //If the string is not found it will add the string to the list as a DataString object and make the count 1
                foreach (string tempstring in tempdataholder)
                {
                    bool found = false;
                    foreach (DataString y in parseddata)
                    {
                        if (tempstring == y.dataname)
                        {
                            found = true;
                            y.datacount += 1;
                        }
                    }
                    if (found == false)
                    {
                        DataString temp1 = new DataString();
                        temp1.dataname = tempstring;
                        temp1.datacount = 1;
                        parseddata.Add(temp1);
                    }
                }



                
            }
            //This loop will print each DataString object and its count
            //is a random variable name to represent the object that will be used to 
            foreach (DataString z in parseddata)
            {
                //Show Item: before dataname 
                textBox1.AppendText("Item: ");
                textBox1.AppendText(z.dataname);
                //Show | Count: before the datacount
                textBox1.AppendText(" | Count: ");
                int num = z.datacount;
                textBox1.AppendText(num.ToString());
                //\r is a carriage return that will move all the way to the left on the next line
                //\n will tell it to also go to a new line at the next line when printing it
                textBox1.AppendText("\r\n");

            }

        }
    }
}


