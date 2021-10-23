using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace MachineProblem
{
    class Widgets
    {
        public Label Lbl(Control windowName, string text, int xpoint, int ypoint, int fontSize)
        {
            Label label = new Label
            {
                Text = text,
                Width = 100,
                Height = 100,
                AutoSize = true,
                Location = new Point(xpoint, ypoint),
                Font = new Font("Bahnschrift SemiLight", fontSize)
            };
            windowName.Controls.Add(label);
            return label;
        }
        public Button Btn(Control windowName, string text, Color color, int width, int height, int xpoint, int ypoint, int fontSize)
        {
            Button button = new Button
            {
                Size = new Size(width, height),
                AutoSize = true,
                Location = new Point(xpoint, ypoint),
                Text = text,
                Font = new Font("Bahnschrift SemiLight", fontSize),
                BackColor = color
            };
            windowName.Controls.Add(button);
            return button;
        }
    }
    class LoginForm
    {
        TextBox Username;
        TextBox Password;
        Form LoginWin;
        Widgets ww = new Widgets();
        public void Login()
        {
            LoginWin = new Form
            {
                Text = "Parking Ticket System",
                BackColor = Color.Linen,
                Width = 700,
                Height = 500,
                StartPosition = FormStartPosition.CenterScreen
            };
            ww.Lbl(LoginWin, "LOGIN", 300, 50, 20);
            ww.Lbl(LoginWin, "Email", 250, 100, 15);
            ww.Lbl(LoginWin, "Password", 250, 180, 15);

            Username = new TextBox()
            {
                Size = new Size(100, 100),
                Width = 180,
                Height = 100,
                Location = new Point(250, 130),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiLight", 15)
            };
            Password = new TextBox()
            {
                Size = new Size(100, 100),
                Width = 180,
                Height = 100,
                Location = new Point(250, 210),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiLight", 15)
            };
            Button LoginButton = ww.Btn(LoginWin, "Login", Color.PeachPuff, 150, 40, 265, 290, 15);
            LoginButton.Click += (object sender, EventArgs e) => CheckCredentials();

            LoginWin.Controls.Add(Username);
            LoginWin.Controls.Add(Password);
            LoginWin.ShowDialog();
        }
        public void CheckCredentials()
        {
            string Username = this.Username.Text;
            string Password = this.Password.Text;

            using (StreamReader reader = new StreamReader("login.txt", true))
            {
                string content = reader.ReadToEnd();
                string[] lines = content.Split('\n');
                foreach (string line in lines)
                {
                    string[] account = line.Split(',');

                    if (Username == account[0].Replace("\r", "") && Password == account[1].Replace("\r", ""))
                    {
                        MessageBox.Show("Login successful.");
                        LoginWin.Hide();
                        MainMenu MenuWindow = new MainMenu();
                        MenuWindow.MyMenu();
                        break;
                    }
                    else MessageBox.Show("Login failed.");
                    break;

                }
            }
        }
    }
    class MainMenu
    {
        Button ManageAccountBtn;
        Button ManageParkBtn;
        Button ClearParkBtn;
        Button ParkSpaceBtn;
        Button ExitBtn;

        Form menuWin;
        Widgets ww = new Widgets();
        public void MyMenu()
        {
            menuWin = new Form
            {
                Text = "Parking Ticket System",
                BackColor = Color.Linen,
                Width = 700,
                Height = 500,
                StartPosition = FormStartPosition.CenterScreen
            };
            ww.Lbl(menuWin, "MENU", 300, 50, 20);

            ManageAccountBtn = ww.Btn(menuWin, "Manage Account", Color.PeachPuff, 200, 30, 240, 100, 15);
            ManageParkBtn = ww.Btn(menuWin, "Manage Parking Lot", Color.PeachPuff, 200, 30, 240, 150, 15);
            ClearParkBtn = ww.Btn(menuWin, "Clear Parking Lot", Color.PeachPuff, 200, 30, 240, 200, 15);
            ParkSpaceBtn = ww.Btn(menuWin, "Check Space", Color.PeachPuff, 200, 30, 240, 250, 15);
            ExitBtn = ww.Btn(menuWin, "Exit", Color.PeachPuff, 200, 30, 240, 300, 15);

            ManageAccountBtn.Click += (object sender, EventArgs e) => ManageAcc();
            ManageParkBtn.Click += (object sender, EventArgs e) => ManagePark();
            ClearParkBtn.Click += (object sender, EventArgs e) => ClearParkingLot();
            ParkSpaceBtn.Click += (object sender, EventArgs e) => CheckParkSpace();
            ExitBtn.Click += (object sender, EventArgs e) => Exit();

            menuWin.ShowDialog();
        }

        public void ManageAcc()
        {
            //display the manage acc window
            menuWin.Hide();
            ManageAccount ManAcc = new ManageAccount();
            ManAcc.ManageAcc();
        }
        public void ManagePark()
        {
            //parking window
            menuWin.Hide();
            ParkingLot ParkLot = new ParkingLot();
            //magdagdag pa text file parameter
            ParkLot.Floor("Floor 1");
        }
        public double CheckTotal()
        {
            string lines;
            double Sum = 0;
            // show total earning
            try
            {
                using (StreamReader reader = new StreamReader("Fees.txt"))
                {
                    while ((lines = reader.ReadLine()) != null)
                    {
                        Sum = Sum + double.Parse(lines);
                    }
                }
            }
            catch
            {
                MessageBox.Show("File does not exist.");
            }
            return Sum;
        }
        public void ClearParkingLot()
        {
            double Total = CheckTotal();
            string msg = string.Format("{0:N2}", Total);
            if (Total == 0)
            {
                MessageBox.Show("There are no earnings yet...");
            }
            else
            {
                //clear parking lot code here

                MessageBox.Show("Parking lot cleared.\nTotal earning of the day is: P " + msg);
            }

        }
        public void CheckParkSpace()
        {
            MessageBox.Show("Available car space per floor: ");
        }
        public void Exit()
        {
            menuWin.Close();
            //exit window
        }
    }
    class ManageAccount
    {
        Form accWin;
        Widgets ww = new Widgets();
        public void ManageAcc()
        {
            accWin = new Form
            {
                Text = "Parking Ticket System",
                BackColor = Color.Linen,
                Width = 700,
                Height = 500,
                StartPosition = FormStartPosition.CenterScreen
            };
            ww.Lbl(accWin, "MANAGE ACCOUNT", 225, 50, 20);

            Button CreateAcc = ww.Btn(accWin, "Create Account", Color.PeachPuff, 200, 30, 240, 125, 15);
            Button DelAcc = ww.Btn(accWin, "Delete Account", Color.PeachPuff, 200, 30, 240, 200, 15);
            Button Back = ww.Btn(accWin, "Back", Color.LightBlue, 60, 40, 10, 350, 10);

            CreateAcc.Click += (object sender, EventArgs e) => NewAccount();
            DelAcc.Click += (object sender, EventArgs e) => RemoveAcc();
            Back.Click += (object sender, EventArgs e) => Return();

            accWin.ShowDialog();
        }

        public void NewAccount()
        {
            accWin.Hide();
            NewAccount Create = new NewAccount();
            Create.NewAccPage();
        }
        public void RemoveAcc()
        {
            accWin.Hide();
            DelAccount Del = new DelAccount();
            Del.Remove();
        }
        public void Return()
        {
            accWin.Hide();
            MainMenu MainPage = new MainMenu();
            MainPage.MyMenu();
        }
    }
    class NewAccount
    {
        TextBox Username;
        TextBox Password;
        Form CreateNew;
        Button CreateBtn;
        Button Back;
        Widgets ww = new Widgets();
        public void NewAccPage()
        {
            CreateNew = new Form
            {
                Text = "Parking Ticket System",
                BackColor = Color.Linen,
                Width = 700,
                Height = 500,
                StartPosition = FormStartPosition.CenterScreen
            };
            ww.Lbl(CreateNew, "CREATE ACCOUNT", 225, 50, 20);

            ww.Lbl(CreateNew, "Email", 250, 100, 15);
            ww.Lbl(CreateNew, "Password", 250, 180, 15);

            Username = new TextBox()
            {
                Size = new Size(100, 100),
                Width = 180,
                Height = 100,
                Location = new Point(250, 130),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiLight", 15)
            };
            Password = new TextBox()
            {
                Size = new Size(100, 100),
                Width = 180,
                Height = 100,
                Location = new Point(250, 210),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiLight", 15)
            };
            CreateBtn = ww.Btn(CreateNew, "Create account", Color.PeachPuff, 150, 40, 265, 290, 15);
            Back = ww.Btn(CreateNew, "Back", Color.LightBlue, 60, 40, 10, 350, 10);

            CreateBtn.Click += (object sender, EventArgs e) => CreateBtn_Click();
            Back.Click += (object sender, EventArgs e) => Return();

            CreateNew.Controls.Add(Username);
            CreateNew.Controls.Add(Password);

            CreateNew.ShowDialog();
        }

        public void CreateBtn_Click()
        {
            string Username = this.Username.Text;
            string Password = this.Password.Text;
            string content = string.Format("{0},{1}", Username, Password);
            try
            {
                using (StreamWriter writer = new StreamWriter("login.txt", true))
                {
                    writer.WriteLine(content);
                }
                MessageBox.Show("Registration successful.");
                this.Username.Clear();
                this.Password.Clear();
            }
            catch
            {
                MessageBox.Show("Registration failed.");
            }
        }
        public void Return()
        {
            CreateNew.Hide();
            ManageAccount ManagePage = new ManageAccount();
            ManagePage.ManageAcc();
        }

    }
    class DelAccount
    {
        Form Delete;
        Button Back;
        Button DelAcc;
        TextBox Input_Line;
        Label mylabel;
        Widgets ww = new Widgets();
        string Content;
        int LineNum = 0;

        public void Remove()
        {
            Delete = new Form
            {
                Text = "Parking Ticket System",
                BackColor = Color.Linen,
                Width = 700,
                Height = 500,
                StartPosition = FormStartPosition.CenterScreen
            };
            ww.Lbl(Delete, "REMOVE ACCOUNT", 225, 50, 20);
            Back = ww.Btn(Delete, "Back", Color.LightBlue, 60, 40, 10, 350, 10);
            Back.Click += (object sender, EventArgs e) => Return();

            try
            {
                if (File.Exists("New login.txt"))
                {
                    Display("New login.txt");
                }
                else
                {
                    Display("login.txt");
                }
            }
            catch
            {
                ww.Lbl(Delete, "Empty...", 225, 100, 15);
            }
            Delete.ShowDialog();
        }
        public Label Display(string txtName)
        {
            int ypointnum = 100;
            string[] lines = File.ReadAllLines(txtName);
            foreach (var line in lines)
            {
                var array = line.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                Content = string.Format("Line {0}   {1} - {2}", LineNum, array[0], array[1]);
                mylabel = ww.Lbl(Delete, Content, 230, ypointnum, 13);
                ypointnum += 30;
                LineNum++;
            }

            ww.Lbl(Delete, "Line to delete:", 150, 350, 13);
            Input_Line = new TextBox()
            {
                Width = 70,
                Height = 100,
                Location = new Point(300, 350),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiLight", 13)
            };
            DelAcc = ww.Btn(Delete, "Delete", Color.PeachPuff, 150, 30, 450, 350, 15);
            DelAcc.Click += (object sender, EventArgs e) => DelAcc_Click();

            Delete.Controls.Add(Input_Line);

            return mylabel;
        }
        public void DelAcc_Click()
        {
            string x;
            try
            {
                LineNum = 0;
                int Line_to_delete = int.Parse(Input_Line.Text);

                using (StreamReader reader = new StreamReader("login.txt"))
                {
                    //set false to overwrite pero walang nangyayari sfnjsdam
                    using (StreamWriter writer = new StreamWriter("New login.txt", false))
                    {
                        while ((x = reader.ReadLine()) != null)
                        {
                            LineNum++;

                            if (LineNum == Line_to_delete)
                                continue;
                            writer.WriteLine(x);
                        }
                    }
                }
                //checking sa console
                Console.WriteLine("Line to del: {0}\nFiles\n{1}", Line_to_delete, x);

                Input_Line.Clear();
                //display accounts with the removed account
                Display("New login.txt");
                MessageBox.Show("Account successfully deleted.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        public void Return()
        {
            Delete.Hide();
            ManageAccount ManagePage = new ManageAccount();
            ManagePage.ManageAcc();
        }
    }
    class ParkingLot
    {
        TextBox PlateNo;
        TextBox TimeIn;
        Form ParkingFloor;

        Button Button1;
        Button Button2;
        Button Button3;
        Button Button4;
        Button Button5;

        Button Button6;
        Button Button7;
        Button Button8;
        Button Button9;
        Button Button10;

        Button Button11;
        Button Button12;
        Button Button13;
        Button Button14;
        Button Button15;

        Button Button16;
        Button Button17;
        Button Button18;
        Button Button19;
        Button Button20;

        Button Floor1;
        Button Floor2;
        Button Floor3;
        Button Floor4;
        Button Floor5;
        Button Back;

        double Fee = 20.00;

        Widgets ww = new Widgets();

        public void Floor(string floorNum)
        {
            ParkingFloor = new Form
            {
                Text = "Parking Ticket System",
                BackColor = Color.Linen,
                Width = 700,
                Height = 500,
                StartPosition = FormStartPosition.CenterScreen
            };

            ww.Lbl(ParkingFloor, floorNum, 10, 10, 15);
            ww.Lbl(ParkingFloor, "PARKING LOT", 250, 10, 20);

            Button1 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 120, 50, 10);
            Button2 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 230, 50, 10);
            Button3 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 340, 50, 10);
            Button4 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 450, 50, 10);
            Button5 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 570, 50, 10);

            Button6 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 120, 120, 10);
            Button7 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 230, 120, 10);
            Button8 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 340, 120, 10);
            Button9 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 450, 120, 10);
            Button10 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 570, 120, 10);

            Button11 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 120, 190, 10);
            Button12 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 230, 190, 10);
            Button13 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 340, 190, 10);
            Button14 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 450, 190, 10);
            Button15 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 570, 190, 10);

            Button16 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 120, 260, 10);
            Button17 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 230, 260, 10);
            Button18 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 340, 260, 10);
            Button19 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 450, 260, 10);
            Button20 = ww.Btn(ParkingFloor, "Unoccupied", Color.DarkSeaGreen, 100, 50, 570, 260, 10);

            Floor1 = ww.Btn(ParkingFloor, "Floor 1", Color.PeachPuff, 30, 30, 10, 50, 10);
            Floor2 = ww.Btn(ParkingFloor, "Floor 2", Color.PeachPuff, 30, 30, 10, 110, 10);
            Floor3 = ww.Btn(ParkingFloor, "Floor 3", Color.PeachPuff, 30, 30, 10, 170, 10);
            Floor4 = ww.Btn(ParkingFloor, "Floor 4", Color.PeachPuff, 30, 30, 10, 230, 10);
            Floor5 = ww.Btn(ParkingFloor, "Floor 5", Color.PeachPuff, 30, 30, 10, 290, 10);
            Back = ww.Btn(ParkingFloor, "Back", Color.LightBlue, 60, 40, 10, 350, 10);

            InputData(); //textbox designs here

            Button1.Click += (object sender, EventArgs e) => Button1_onClick(floorNum);
            Button2.Click += (object sender, EventArgs e) => Button2_onClick(floorNum);
            Button3.Click += (object sender, EventArgs e) => Button3_onClick(floorNum);

            Floor1.Click += (object sender, EventArgs e) => Floor1_Click();
            Floor2.Click += (object sender, EventArgs e) => Floor2_Click();
            Floor3.Click += (object sender, EventArgs e) => Floor3_Click();
            Floor4.Click += (object sender, EventArgs e) => Floor4_Click();
            Floor5.Click += (object sender, EventArgs e) => Floor5_Click();
            Back.Click += (object sender, EventArgs e) => Return();

            ParkingFloor.ShowDialog();
        }

        public void InputData()
        {
            ww.Lbl(ParkingFloor, "Plate no.:", 100, 350, 13);
            PlateNo = new TextBox()
            {
                Width = 150,
                Height = 100,
                Location = new Point(200, 345),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiLight", 13)
            };
            ww.Lbl(ParkingFloor, "Time in:", 400, 350, 13);
            TimeIn = new TextBox()
            {
                Width = 150,
                Height = 100,
                Location = new Point(500, 345),
                AutoSize = true,
                Font = new Font("Bahnschrift SemiLight", 13)
            };
            ParkingFloor.Controls.Add(PlateNo);
            ParkingFloor.Controls.Add(TimeIn);
        }
        public void First_Click(Button BtnNo, string floorNum)
        {
            string Plate = PlateNo.Text;
            string Time = TimeIn.Text;
            string Filename = floorNum + ".txt";
            string BtnText = BtnNo.Text;

            if (Time == "" || Plate == "")
            {
                //do nothing so that an empty string wouldn't save to text file
            }
            else
            {
                try
                {
                    using (StreamWriter write = new StreamWriter(Filename, true))
                    {
                        string data = string.Format("{0}\t{1}\t{2}", Plate, Time, BtnText);
                        write.WriteLine(data);
                    }
                    MessageBox.Show("Data saved successfully.");
                    //clear textboxes
                    PlateNo.Clear();
                    TimeIn.Clear();
                    //change state of button
                    BtnNo.BackColor = Color.PaleVioletRed;
                    BtnNo.Text = "Occupied";
                }
                catch
                {
                    MessageBox.Show("Failed to save data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void Second_Click(Button BtnNo, string floorNum)
        {
            BtnNo.BackColor = Color.DarkSeaGreen;
            BtnNo.Text = "Unoccupied";
            //compute for total fee
            //timeIn - timeOut

            //write fee to text file
            try
            {
                using (StreamWriter write = new StreamWriter("Fees.txt", true))
                {
                    write.WriteLine(Fee);
                }
            }
            catch
            {
                MessageBox.Show("An error occurred.");
            }

            // display the parking fee
            MessageBox.Show("The parking fee is P" + Fee);

        }
        public void Button1_onClick(string floorNum)
        {
            First_Click(Button1, floorNum);
            Button1.Click += (object sender, EventArgs e) => Second_Click(Button1, floorNum);

        }
        public void Button2_onClick(string floorNum)
        {
            First_Click(Button2, floorNum);
            Button2.Click += (object sender, EventArgs e) => Second_Click(Button2, floorNum);
        }
        public void Button3_onClick(string floorNum)
        {
            First_Click(Button3, floorNum);
            Button3.Click += (object sender, EventArgs e) => Second_Click(Button3, floorNum);
        }


        public void Floor1_Click()
        {
            ParkingFloor.Hide();
            //add text file parameter to retrieve data
            Floor("Floor 1");
        }
        public void Floor2_Click()
        {
            ParkingFloor.Hide();
            Floor("Floor 2");
        }
        public void Floor3_Click()
        {
            ParkingFloor.Hide();
            Floor("Floor 3");
        }
        public void Floor4_Click()
        {
            ParkingFloor.Hide();
            Floor("Floor 4");
        }
        public void Floor5_Click()
        {
            ParkingFloor.Hide();
            Floor("Floor 5");
        }
        public void Return()
        {
            ParkingFloor.Hide();
            MainMenu MainPage = new MainMenu();
            MainPage.MyMenu();
        }

    }

    class Program : Form
    {
        static void Main(string[] args)
        {
            //dont mind these...just testing specific pages para wala masyadong pindot

            /*ParkingLot pp = new ParkingLot();
            pp.Floor("Floor 1"); 
            DelAccount dd = new DelAccount();
            dd.Remove();
            */

            LoginForm login = new LoginForm();
            login.Login();

        }
    }
}