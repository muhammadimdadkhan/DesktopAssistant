using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop;
using System.Speech.Synthesis;

namespace DesktopAssistant
{
    public partial class Form1 : Form
    {
        public static WelcomeAndGoodbye wAndG;
        public static Speak speak = new Speak();
        public static Calender calender;
        public static string choice;
        // public static iTunesApp app;
        public static int volume = 50;
        public static SpeechRecognitionEngine myVoice = new SpeechRecognitionEngine();
       // public static SpeechRecognitionEngine myVoice2 = new SpeechRecognitionEngine();
        System.Windows.Forms.Timer stopListeningTimer = new System.Windows.Forms.Timer();
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();


       

        public Form1()
        {
            InitializeComponent();
          //  recEngine.RecognizeAsync(RecognizeMode.Multiple);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            DictationGrammar defaultDictationGrammar = new DictationGrammar();

            defaultDictationGrammar.Name = "default dictation";
            defaultDictationGrammar.Enabled = true;
            defaultDictationGrammar.Weight=0.99999f;
            //Set output, load grammar and set up speech recognisition event handler
            myVoice.SetInputToDefaultAudioDevice();
            myVoice.UnloadAllGrammars();
            string[] phrases = getPhrases();
            Grammar ph = new Grammar(new GrammarBuilder(new Choices(phrases)));
            //ph.Weight = 0.00001f;
            myVoice.LoadGrammar(ph);
            //myVoice.LoadGrammar(defaultDictationGrammar);  //MS GRANNAR
            
            myVoice.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(myVoice_SpeechRecognized);
            myVoice.RecognizeAsync(RecognizeMode.Multiple);
            myVoice.MaxAlternates = 3;





        
          //  myVoice2.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(phrases))));

          //  myVoice2.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(myVoice2_SpeechRecognized);
           // myVoice2.RecognizeAsync(RecognizeMode.Multiple);
          //  myVoice2.MaxAlternates = 3;
           // myVoice2.SetInputToDefaultAudioDevice();



           
         // myVoice2.LoadGrammar(defaultDictationGrammar);
            // string[] phrases = getPhrases();
            loadUpWelcome();

            stopListeningTimer.Tick += new EventHandler(time_Tick);
            stopListeningTimer.Interval = 1000;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }





























        Intent meaning = new Intent();


        /////////////////////////////////////////////////////////////////////////////////////////
        











        private void myVoice_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            label2.Text = "Listening...";

            string input = e.Result.Text;
            Console.WriteLine(input);
            textBox1.Text = input;

            richTextBox1.Text +="\n"+ input;


          















































            switch (input.ToUpper())
            {





                //Works with no appointments - Haven't checked when appointment is present
                case ("CALENDER"):
                case ("CHECK CALENDER"):
                case ("APPOINTMENTS"):
                case ("TASKS"):
                    Console.WriteLine("Calender");

                    label2.Text = "Executing...";
                    checkCalender();
                    label2.Text = "Speak Now...";
                    break;


                case ("SHOW TIME"):
                case ("TIME"):
                case ("CURRENT TIME"):
                case ("TELL TIME"):
                case ("SAY TIME"):
                    label2.Text = "Executing...";
                    currentTime();
                    label2.Text = "Speak Now...";
                    break;

                case ("SHOW DAY"):
                case ("DAY"):
                case ("CURRENT DAY"):
                case ("TELL DAY"):
                case ("SAY DAY"):
                case ("SHOW DATE"):
                case ("DATE"):
                case ("CURRENT DATE"):
                case ("TELL DATE"):
                case ("SAY DATE"):
                    label2.Text = "Executing...";
                    currentDate();
                    label2.Text = "Speak Now...";
                    break;

                case ("HELLO"):
                case ("HEY IT"):
                case ("HEY"):
                case ("SUP"):
                case ("GOOD MORNING"):
                case ("GOOD AFTERNOON"):
                case ("GOOD EVENING"):
                    label2.Text = "Executing...";
                    helloResponse();
                    label2.Text = "Speak Now...";
                    break;







                case ("IT QUIET"):
                case ("IT SH"):
                case ("IT VOLUME DOWN"):
                    label2.Text = "Executing...";
                    ITVolume(true);
                    label2.Text = "Speak Now...";
                    break;

                case ("IT LOUD"):
                case ("I CANT HEAR YOU"):
                case ("I CANT HEAR YOU IT"):
                case ("IT VOLUME UP"):
                    label2.Text = "Executing...";
                    ITVolume(false);
                    label2.Text = "Speak Now...";
                    break;

                case ("IT MUTE"):
                case ("MUTE"):
                    label2.Text = "Executing...";
                    ITMute(true);
                    label2.Text = "Speak Now...";
                    break;

                case ("IT SPEAK"):
                case ("UNDO MUTE"):
                    label2.Text = "Executing...";
                    ITMute(false);
                    label2.Text = "Speak Now...";
                    break;

                case ("STOP LISTENING"):
                    label2.Text = "Executing...";
                    stopListening();
                    label2.Text = "Speak Now...";
                    break;

                case ("EXIT CHROME"):
                case ("CLOSE CHROME"):
                    label2.Text = "Executing...";
                    exitChromeWindows();
                    label2.Text = "Speak Now...";
                    break;

                case ("OPEN CHROME"):
                    label2.Text = "Executing...";
                    openChromeWindow();
                    label2.Text = "Speak Now...";
                    break;



                case ("CLOSE MAIL"):
                case ("EXIT MAIL"):
                case ("CLOSE OUTLOOK"):
                case ("EXIT OUTLOOK"):
                    label2.Text = "Executing...";
                    closeEmail();
                    label2.Text = "Speak Now...";
                    break;

                



                case ("MINIMIZE"):
                case ("IT SMALL"):
                    label2.Text = "Executing...";
                    minimize();
                    label2.Text = "Speak Now...";
                    break;

                case ("IT COME BACK"):
                case ("IT NORMAL"):
                case ("NORMAL"):
                    label2.Text = "Executing...";
                    normalSize();
                    label2.Text = "Speak Now...";
                    break;

                case ("QUIT"):
                case ("STOP"):
                case ("END"):
                    label2.Text = "Executing...";
                    endProgram(this);
                    label2.Text = "Speak Now...";
                    break;

                default:
                    label2.Text = "Executing...";
                    //  noCommand();
                    if (input == "Logoff" || input == "logoff")
                    {
                        DialogResult d = MessageBox.Show("Are You Sure You Want to Logoff ? ", "Confrimation", MessageBoxButtons.YesNo);
                        if(d==DialogResult.Yes)
                        {
                            open("shutdown -L");
                        }
                        else if (d == DialogResult.No)
                        { }
                           
                    }
                    if (input.Contains("OPEN") || input.Contains("Open") || input.Contains("open"))
                    {
                        if (input.Contains("MS WORD") || input.Contains("MICROSOFT WORD") || input.Contains("WORD"))
                        {
                            open("winword");

                        }
                        if (input.Contains("POWERPOINT") || input.Contains("MICROSOFT POWERPOINT"))
                        {
                            open("powerpnt");

                        }
                        else
                        {
                            open(input.Replace("OPEN", " "));
                        }
                    }

                    else if (input.Contains("CLOSE") || input.Contains("Close") || input.Contains("close"))
                    {

                        if (input.Contains("MS WORD") || input.Contains("MICROSOFT WORD") || input.Contains("WORD"))
                        {
                            close("winword");

                        }
                        if (input.Contains("POWERPOINT") || input.Contains("MICROSOFT POWERPOINT"))
                        {
                            close("powerpnt");

                        }
                        else
                        {
                            close(input.Replace("CLOSE", " "));
                        }
                    }
                    label2.Text = "Speak Now...";

                    break;
            
            // label2.Text = "Speak Now...";

            
            }
        }

        //..........................................Welcome/Goodbye/Hello Response messages................................

        public static void loadUpWelcome()
        {
            wAndG = new WelcomeAndGoodbye();
            Thread welcomeThread = new Thread(new ThreadStart(wAndG.welcome));
            welcomeThread.IsBackground = true;
            welcomeThread.Start();
        }


        public static void endProgram(Form thisForm)
        {
            wAndG = new WelcomeAndGoodbye();
            Thread goodbyeThread = new Thread(new ThreadStart(wAndG.goodbye));
            goodbyeThread.IsBackground = true;
            goodbyeThread.Start();
            goodbyeThread.Join();
            thisForm.Close();
        }

        public static void helloResponse()
        {

            Thread hello = new Thread(new ThreadStart(speak.hello));
            hello.IsBackground = true;
            hello.Start();
        }

        //....................................................................................................

        //........................................Weather.....................................................

        //.....................................................................................................

        //......................................Email..........................................................

        //.....................................................................................................

        //.....................................Search Files....................................................


        //.....................................................................................................

        //.....................................Check Calender..................................................
        public static void checkCalender()
        {
            calender = new Calender();
            calender.calenderAppointments();
        }
        //.....................................................................................................

        //........................................Google search................................................



        //...................................................................................................

        //........................................Get Time and Date..........................................
        public static void currentTime()
        {

            speak.currentTime();
        }

        public static void currentDate()
        {

            speak.currentDate();
        }
        //..................................................................................................

        //....................................................Itunes Volume.................................



        //..................................................................................................

        //....................................................Other.........................................             

        public static void ITVolume(bool volumeDown)
        {

            speak.ITVol(volumeDown);
        }

        public static void ITMute(bool mute)
        {

            speak.ITMute(mute);
        }

        public static string[] getPhrases()
        {
            string[] phrases = File.ReadAllLines(@"C:\Users\DELL\Desktop\imdad\FINALproject\DesktopAssistant\DesktopAssistant\Grammar");
            int index = 0;
            foreach (string phrase in phrases)
            {
                if (phrase == string.Empty)
                {
                    phrases[index] = "Empty";
                }
                index++;
            }
            return phrases;
        }

        int time = 60;
        public void stopListening()
        {
            time = 60;
            myVoice.RecognizeAsyncStop();
            Console.WriteLine("Not Listening");
            stopListeningTimer.Start();
        }

        private void time_Tick(object sender, EventArgs e)
        {
            time = time - 1;
            Console.WriteLine(time.ToString());
            if (time == 0)
            {
                myVoice.RecognizeAsync(RecognizeMode.Multiple);
                Console.WriteLine("You may speak");
                stopListeningTimer.Stop();
            }
        }
        //..................................................................................................

        private void IT_Click(object sender, EventArgs e)
        {
            time = 1;
        }

        private void exitChromeWindows()
        {
            endProcess("chrome");
        }

        private void openChromeWindow()
        {
            Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe");
        }

        private void noCommand()
        {
            Thread noOptAvail = new Thread(new ThreadStart(() => speak.noOptionAvailable()));
            noOptAvail.IsBackground = true;
            noOptAvail.Start();
        }

        private void showProcesses()
        {
            try
            {
                Process[] allProcesses = Process.GetProcesses();
                foreach (Process item in allProcesses)
                {
                    Console.WriteLine(item.ProcessName.ToString());
                }
            }
            catch (System.Exception exce)
            {
                Console.WriteLine(exce.ToString());
            }
        }

        private void endProcess(string process)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process currentProcess in processes)
            {
                if (currentProcess.ProcessName.ToString().ToUpper().Contains(process.ToUpper()))
                {
                    Console.WriteLine("Process: {0} ID: {1}", currentProcess.ProcessName, currentProcess.Id);
                    currentProcess.Kill();
                }
            }
        }

        private void closeEmail()
        {
            endProcess("outlook");
        }


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void IT_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void minimize()
        {
            this.WindowState = FormWindowState.Minimized;
            DesktopAssistant.Icon = SystemIcons.Application;
            DesktopAssistant.BalloonTipTitle = "IT";
            DesktopAssistant.BalloonTipText = "Running in background";
            DesktopAssistant.ShowBalloonTip(1500);
            DesktopAssistant.Visible = true;
        }


        private void normalSize()
        {
            this.WindowState = FormWindowState.Normal;
            DesktopAssistant.Visible = false;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

           // speak.wantedToSay(textBox1.Text);
            ExecuteCmd();
        }




        public void ExecuteCmd()
        {
            label2.Text = "Listening...";
            string e = textBox1.Text;
            string input = e;
            Console.WriteLine(input);
            textBox1.Text = input;

            richTextBox1.Text += "\n" + input;


















































            switch (input.ToUpper())
            {





                //Works with no appointments - Haven't checked when appointment is present
                case ("CALENDER"):
                case ("CHECK CALENDER"):
                case ("APPOINTMENTS"):
                case ("TASKS"):
                    Console.WriteLine("Calender");

                    label2.Text = "Executing...";
                    checkCalender();
                    label2.Text = "Speak Now...";
                    break;


                case ("SHOW TIME"):
                case ("TIME"):
                case ("CURRENT TIME"):
                case ("TELL TIME"):
                case ("SAY TIME"):
                    label2.Text = "Executing...";
                    currentTime();
                    label2.Text = "Speak Now...";
                    break;

                case ("SHOW DAY"):
                case ("DAY"):
                case ("CURRENT DAY"):
                case ("TELL DAY"):
                case ("SAY DAY"):
                case ("SHOW DATE"):
                case ("DATE"):
                case ("CURRENT DATE"):
                case ("TELL DATE"):
                case ("SAY DATE"):
                    label2.Text = "Executing...";
                    currentDate();
                    label2.Text = "Speak Now...";
                    break;

                case ("HELLO"):
                case ("HEY IT"):
                case ("HEY"):
                case ("SUP"):
                case ("GOOD MORNING"):
                case ("GOOD AFTERNOON"):
                case ("GOOD EVENING"):
                    label2.Text = "Executing...";
                    helloResponse();
                    label2.Text = "Speak Now...";
                    break;







                case ("IT QUIET"):
                case ("IT SH"):
                case ("IT VOLUME DOWN"):
                    label2.Text = "Executing...";
                    ITVolume(true);
                    label2.Text = "Speak Now...";
                    break;

                case ("IT LOUD"):
                case ("I CANT HEAR YOU"):
                case ("I CANT HEAR YOU IT"):
                case ("IT VOLUME UP"):
                    label2.Text = "Executing...";
                    ITVolume(false);
                    label2.Text = "Speak Now...";
                    break;

                case ("IT MUTE"):
                case ("MUTE"):
                    label2.Text = "Executing...";
                    ITMute(true);
                    label2.Text = "Speak Now...";
                    break;

                case ("IT SPEAK"):
                case ("UNDO MUTE"):
                    label2.Text = "Executing...";
                    ITMute(false);
                    label2.Text = "Speak Now...";
                    break;

                case ("STOP LISTENING"):
                    label2.Text = "Executing...";
                    stopListening();
                    label2.Text = "Speak Now...";
                    break;

                case ("EXIT CHROME"):
                case ("CLOSE CHROME"):
                    label2.Text = "Executing...";
                    exitChromeWindows();
                    label2.Text = "Speak Now...";
                    break;

                case ("OPEN CHROME"):
                    label2.Text = "Executing...";
                    openChromeWindow();
                    label2.Text = "Speak Now...";
                    break;



                case ("CLOSE MAIL"):
                case ("EXIT MAIL"):
                case ("CLOSE OUTLOOK"):
                case ("EXIT OUTLOOK"):
                    label2.Text = "Executing...";
                    closeEmail();
                    label2.Text = "Speak Now...";
                    break;





                case ("MINIMIZE"):
                case ("IT SMALL"):
                    label2.Text = "Executing...";
                    minimize();
                    label2.Text = "Speak Now...";
                    break;

                case ("IT COME BACK"):
                case ("IT NORMAL"):
                case ("NORMAL"):
                    label2.Text = "Executing...";
                    normalSize();
                    label2.Text = "Speak Now...";
                    break;

                case ("QUIT"):
                case ("STOP"):
                case ("END"):
                    label2.Text = "Executing...";
                    endProgram(this);
                    label2.Text = "Speak Now...";
                    break;

                default:
                    label2.Text = "Executing...";
                    //  noCommand();
                    if (input == "Logoff" || input == "logoff")
                    {
                        DialogResult d = MessageBox.Show("Are You Sure You Want to Logoff ? ", "Confrimation", MessageBoxButtons.YesNo);
                        if (d == DialogResult.Yes)
                        {
                            open("shutdown -L");
                        }
                        else if (d == DialogResult.No)
                        { }
                    }
                    if (input.Contains("OPEN") || input.Contains("Open") || input.Contains("open"))
                    {
                        if (input.Contains("MS WORD") || input.Contains("MICROSOFT WORD") || input.Contains("WORD"))
                        {
                            open("winword");

                        }
                        if (input.Contains("POWERPOINT") || input.Contains("MICROSOFT POWERPOINT"))
                        {
                            open("powerpnt");

                        }
                        else
                        {
                            open(input.Replace("OPEN", " "));
                        }
                    }

                   else if (input.Contains("CLOSE") || input.Contains("Close")|| input.Contains("close"))
                    {

                        if (input.Contains("MS WORD") || input.Contains("MICROSOFT WORD") || input.Contains("WORD"))
                        {
                            close("winword");

                        }
                        if (input.Contains("POWERPOINT") || input.Contains("MICROSOFT POWERPOINT"))
                        {
                            close("powerpnt");

                        }
                        else
                        {
                            close(input.Replace("CLOSE", " "));
                        }
                    }
                    label2.Text = "Speak Now...";

                    break;
            }
        }
        public void close(string cmd )
        {
            string pn = cmd.Replace(".exe", "");

            foreach (Process p in Process.GetProcessesByName(cmd))
            {
                p.Kill();
            }
        }
        public void open (string cmdd)
        {
            




                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processInfo.FileName = "cmd.exe";
                processInfo.WorkingDirectory = Path.GetDirectoryName(cmdd);
                processInfo.Arguments = "/c START " + Path.GetFileName(cmdd);
                Process.Start(processInfo);
                //winword ,Powerpoint ,excel, chrome,firefox
            
        }
        private void TextBox1_Enter(object sender, EventArgs ee)
        {
            label2.Text = "Listening...";
            string e = textBox1.Text;
            string input = e;
            Console.WriteLine(input);
            textBox1.Text = input;

            richTextBox1.Text += "\n" + input;


















































            switch (input.ToUpper())
            {





                //Works with no appointments - Haven't checked when appointment is present
                case ("CALENDER"):
                case ("CHECK CALENDER"):
                case ("APPOINTMENTS"):
                case ("TASKS"):
                    Console.WriteLine("Calender");

                    label2.Text = "Executing...";
                    checkCalender();
                    label2.Text = "Speak Now...";
                    break;


                case ("SHOW TIME"):
                case ("TIME"):
                case ("CURRENT TIME"):
                case ("TELL TIME"):
                case ("SAY TIME"):
                    label2.Text = "Executing...";
                    currentTime();
                    label2.Text = "Speak Now...";
                    break;

                case ("SHOW DAY"):
                case ("DAY"):
                case ("CURRENT DAY"):
                case ("TELL DAY"):
                case ("SAY DAY"):
                case ("SHOW DATE"):
                case ("DATE"):
                case ("CURRENT DATE"):
                case ("TELL DATE"):
                case ("SAY DATE"):
                    label2.Text = "Executing...";
                    currentDate();
                    label2.Text = "Speak Now...";
                    break;

                case ("HELLO"):
                case ("HEY IT"):
                case ("HEY"):
                case ("SUP"):
                case ("GOOD MORNING"):
                case ("GOOD AFTERNOON"):
                case ("GOOD EVENING"):
                    label2.Text = "Executing...";
                    helloResponse();
                    label2.Text = "Speak Now...";
                    break;







                case ("IT QUIET"):
                case ("IT SH"):
                case ("IT VOLUME DOWN"):
                    label2.Text = "Executing...";
                    ITVolume(true);
                    label2.Text = "Speak Now...";
                    break;

                case ("IT LOUD"):
                case ("I CANT HEAR YOU"):
                case ("I CANT HEAR YOU IT"):
                case ("IT VOLUME UP"):
                    label2.Text = "Executing...";
                    ITVolume(false);
                    label2.Text = "Speak Now...";
                    break;

                case ("IT MUTE"):
                case ("MUTE"):
                    label2.Text = "Executing...";
                    ITMute(true);
                    label2.Text = "Speak Now...";
                    break;

                case ("IT SPEAK"):
                case ("UNDO MUTE"):
                    label2.Text = "Executing...";
                    ITMute(false);
                    label2.Text = "Speak Now...";
                    break;

                case ("STOP LISTENING"):
                    label2.Text = "Executing...";
                    stopListening();
                    label2.Text = "Speak Now...";
                    break;

                case ("EXIT CHROME"):
                case ("CLOSE CHROME"):
                    label2.Text = "Executing...";
                    exitChromeWindows();
                    label2.Text = "Speak Now...";
                    break;

                case ("OPEN CHROME"):
                    label2.Text = "Executing...";
                    openChromeWindow();
                    label2.Text = "Speak Now...";
                    break;



                case ("CLOSE MAIL"):
                case ("EXIT MAIL"):
                case ("CLOSE OUTLOOK"):
                case ("EXIT OUTLOOK"):
                    label2.Text = "Executing...";
                    closeEmail();
                    label2.Text = "Speak Now...";
                    break;





                case ("MINIMIZE"):
                case ("IT SMALL"):
                    label2.Text = "Executing...";
                    minimize();
                    label2.Text = "Speak Now...";
                    break;

                case ("IT COME BACK"):
                case ("IT NORMAL"):
                case ("NORMAL"):
                    label2.Text = "Executing...";
                    normalSize();
                    label2.Text = "Speak Now...";
                    break;

                case ("QUIT"):
                case ("STOP"):
                case ("END"):
                    label2.Text = "Executing...";
                    endProgram(this);
                    label2.Text = "Speak Now...";
                    break;

                default:
                    label2.Text = "Executing...";
                    //  noCommand();
                    if (input.Contains("OPEN"))
                    {
                        if (input.Contains("MS WORD") || input.Contains("MICROSOFT WORD") || input.Contains("WORD"))
                        {
                            open("winword");

                        }
                        if (input.Contains("POWERPOINT") || input.Contains("MICROSOFT POWERPOINT"))
                        {
                            open("powerpnt");

                        }
                        else
                        {
                            open(input.Replace("OPEN", " "));
                        }
                    }

                    else if (input.Contains("CLOSE"))
                    {

                        if (input.Contains("MS WORD") || input.Contains("MICROSOFT WORD") || input.Contains("WORD"))
                        {
                            close("MICROSOFT WORD");

                        }
                        if (input.Contains("POWERPOINT") || input.Contains("MICROSOFT POWERPOINT"))
                        {
                            close("MICROSOFT POWERPOINT");

                        }
                        else
                        {
                            close(input.Replace("CLOSE", " "));
                        }
                    }
                    label2.Text = "Speak Now...";

                    break;
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar)==13)
            {
                ExecuteCmd();
            }
           // 
        }
    }
}
