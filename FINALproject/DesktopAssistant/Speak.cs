using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace DesktopAssistant
{
    public class Speak
    {
        SpeechSynthesizer IT = new SpeechSynthesizer();

        public Speak()
        {
            IT.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Child);
        }
        public void noOptionAvailable()
        {
            IT.Speak("There is currently no command for this");
        }
        public void wantedToSay(string s)
        {
           // IT.Speak(" ,"+s);
        }
        //Say opening file as well
        public void askForFilename()
        {
            IT.Speak("What file are you looking for?");
        }

        public void sayAppointment(string[] whatToSay)
        {
            //whatToSay[0] = index
            //whatToSay[1] = Subject
            //whatToSay[2] = Date       
            string[] date = whatToSay[2].Split('/');
            string newDate = date[1] + "/" + date[0];
            //whatToSay[3] = Time
            string[] time = whatToSay[3].Split(':');
            string newTime = time[0] + ":" + time[1];
            //whatToSay[4] = Location

            PromptBuilder appointmentBuilder = new PromptBuilder();
            appointmentBuilder.StartVoice("IVONA 2 Brian");
            appointmentBuilder.AppendText("Appointment " + whatToSay[0]);
            appointmentBuilder.AppendText(", subject is: " + whatToSay[1]);
            appointmentBuilder.AppendSsmlMarkup(", on <say-as interpret-as=\"date_md\">" + newDate + "</say-as>");
            appointmentBuilder.AppendSsmlMarkup(" <say-as interpret-as=\"time\">" + newTime + "</say-as>");
            appointmentBuilder.AppendText(", at " + whatToSay[4]);
            appointmentBuilder.EndVoice();

            IT.Speak(appointmentBuilder);
            appointmentBuilder.ClearContent();
        }

        public void sayAppointmentError()
        {
            IT.Speak("No Appointments found for the next 7 days.");
        }

        public void searchFor()
        {
            IT.Speak("What do you want to search for?");
        }

        public void currentTime()
        {
            string timeString = DateTime.Now.ToShortTimeString();
            PromptBuilder timeBuilder = new PromptBuilder();
            timeBuilder.StartVoice("IVONA 2 Brian");
            timeBuilder.AppendText("The time is: ");
            timeBuilder.AppendSsmlMarkup(" <say-as interpret-as=\"time\">" + timeString + "</say-as>");
            timeBuilder.EndVoice();

            IT.Speak(timeBuilder);
        }

        public void currentDate()
        {
            string dateString = DateTime.Now.ToShortDateString();
            PromptBuilder dateBuilder = new PromptBuilder();
            dateBuilder.StartVoice("IVONA 2 Brian");
            dateBuilder.AppendText("The date is: ");
            dateBuilder.AppendSsmlMarkup(" <say-as interpret-as=\"date_md\">" + dateString + "</say-as>");
            dateBuilder.EndVoice();

            IT.Speak(dateBuilder);
        }

        public void hello()
        {
            DateTime timeNow = DateTime.Now;
            string username = Environment.UserName;
            if (timeNow.Hour >= 0 && timeNow.Hour < 12)
            {
                IT.Speak("Good morning " + username);
            }
            else if (timeNow.Hour >= 12 && timeNow.Hour < 18)
            {
                IT.Speak("Good afternoon " + username);
            }
            else if (timeNow.Hour >= 18 && timeNow.Hour < 24)
            {
                IT.Speak("Good evening " + username);
            }
        }

        public void sayForecast(string[] conditions)
        {
            IT.Speak("Tomorrows forecast is " + conditions[5] + " with highs of " + conditions[6] + " and lows of " + conditions[7]);
        }

        public void sayWeather(string[] conditions)
        {
            PromptBuilder builder = new PromptBuilder();
            builder.StartVoice("IVONA 2 Brian");
            builder.AppendText("The weather in hale sowen is " + conditions[0] + " at " + conditions[1] + " degrees. There is a wind speed of " + conditions[2] + " miles per hour with highs of " + conditions[3] + " and lows of " + conditions[4]);
            builder.EndVoice();
            IT.Speak(builder);
        }

        public void loading()
        {
            IT.Speak("Loading, Please hold");
        }

        int originalVolume = 50;
        public void ITMute(bool mute)
        {
            if (mute)
            {
                IT.Speak("Muting");
                originalVolume = IT.Volume;
                IT.Volume = 0;
            }
            else
            {
                IT.Volume = originalVolume;
                IT.Speak("Volume levels restored");
            }
        }


        public void ITVol(bool sh)
        {
            int volume = IT.Volume;
            if (sh)
            {
                if (IT.Volume == 0 || (IT.Volume - 20) < 0)
                {
                    IT.Volume = 20;
                    IT.Speak("Muted");
                    IT.Volume = 0;
                }
                else
                {
                    IT.Volume -= 20;
                    IT.Speak("Volume decreased");
                }
            }
            else
            {
                if (IT.Volume == 100 || (IT.Volume + 20) > 100)
                {
                    IT.Speak("I cannot get any louder");
                }
                else
                {
                    IT.Volume += 20;
                    IT.Speak("Volume increased");
                }
            }
        }

        public void tooManyRecipients()
        {
            IT.Speak("Maximum number of recipients added");
        }

        public void closing()
        {
            IT.SpeakAsyncCancelAll();
            IT.Dispose();
        }

    }
}
