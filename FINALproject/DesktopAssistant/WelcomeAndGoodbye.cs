using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace DesktopAssistant
{
    public class WelcomeAndGoodbye
    {
        public void welcome()
        {
            string welcomeMessage = "Welcome back.";
            PromptBuilder welcomeBuilder = new PromptBuilder();

            welcomeBuilder.StartVoice("IVONA 2 Brian");
            welcomeBuilder.AppendText(welcomeMessage);
            welcomeBuilder.EndVoice();

            using (SpeechSynthesizer sayWelcome = new SpeechSynthesizer())
            {

                sayWelcome.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Senior);
                sayWelcome.Speak(welcomeBuilder);
            }
        }

        public void goodbye()
        {
            string goodbyeMessage = "Goodbye, for now.";
            PromptBuilder builder = new PromptBuilder();
            builder.StartVoice("IVONA 2 Brian");
            builder.AppendText(goodbyeMessage);
            builder.EndVoice();

            using (SpeechSynthesizer sayGoodbye = new SpeechSynthesizer())
            {

                sayGoodbye.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Child);
                sayGoodbye.Speak(builder);
            }
        }

    }
}
