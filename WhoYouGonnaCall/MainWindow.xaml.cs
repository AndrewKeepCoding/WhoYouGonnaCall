using Microsoft.UI.Xaml;
using System.Collections.Generic;

namespace WhoYouGonnaCall
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            Phrases = new()
            {
                { "Who you gonna call?", "Ghostbusters!" },
                { "I collect spores, ", "molds and fungus." },
                { "He ", "slimed me." },
                { "Ray, when someone asks you if you are a god, ", "you say YES!" },
                { "See you on the other side, Ray.", "Nice working with you, Dr.Venkman." },
            };
        }

        public Dictionary<string, string> Phrases { get; set; }
    }
}
