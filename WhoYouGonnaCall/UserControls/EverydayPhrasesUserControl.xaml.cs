using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.System;

namespace WhoYouGonnaCall;

public sealed partial class EverydayPhrasesUserControl : UserControl
{
    public static readonly DependencyProperty CompletePhraseCommandProperty =
        DependencyProperty.Register(nameof(CompletePhraseCommand), typeof(ICommand), typeof(EverydayPhrasesUserControl), new PropertyMetadata(null));

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(string), typeof(EverydayPhrasesUserControl), new PropertyMetadata(null));

    public static readonly DependencyProperty PhrasesSourceProperty =
        DependencyProperty.Register(nameof(PhrasesSource), typeof(Dictionary<string, string>), typeof(EverydayPhrasesUserControl), new PropertyMetadata(null));

    public EverydayPhrasesUserControl()
    {
        this.InitializeComponent();

        CompletePhraseCommand = new RelayCommand(CompletePhrase);

        PhraseStart.TextChanging += (_, _) =>
        {
            PhraseEnd.Text = string.Empty;
        };

        PhraseStart.KeyDown += (_, e) =>
        {
            if (e.Key is VirtualKey.Enter)
            {
                CompletePhrase();
            }
        };
    }

    public ICommand CompletePhraseCommand
    {
        get { return (ICommand)GetValue(CompletePhraseCommandProperty); }
        set { SetValue(CompletePhraseCommandProperty, value); }
    }

    public string Header
    {
        get { return (string)GetValue(HeaderProperty); }
        set { SetValue(HeaderProperty, value); }
    }

    public Dictionary<string, string> PhrasesSource
    {
        get { return (Dictionary<string, string>)GetValue(PhrasesSourceProperty); }
        set { SetValue(PhrasesSourceProperty, value); }
    }

    private void CompletePhrase()
    {
        if (PhrasesSource is not null)
        {
            string key = PhraseStart.Text;
            if (PhrasesSource.ContainsKey(key) is true)
            {
                PhraseEnd.Text = PhrasesSource[key];
            }
            else
            {
                PhraseEnd.Text = $"\"{key}\"? What's that?";
            }
        }
    }
}
