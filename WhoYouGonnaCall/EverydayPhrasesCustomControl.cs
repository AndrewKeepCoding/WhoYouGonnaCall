using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.System;

namespace WhoYouGonnaCall;

[TemplatePart(Name = nameof(PhraseStart), Type = typeof(TextBox))]
[TemplatePart(Name = nameof(PhraseEnd), Type = typeof(TextBox))]
public sealed class EverydayPhrasesCustomControl : Control
{
    public static readonly DependencyProperty CompletePhraseCommandProperty =
        DependencyProperty.Register(nameof(CompletePhraseCommand), typeof(ICommand), typeof(EverydayPhrasesCustomControl), new PropertyMetadata(null));

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(string), typeof(EverydayPhrasesCustomControl), new PropertyMetadata(null));

    public static readonly DependencyProperty PhrasesSourceProperty =
        DependencyProperty.Register(nameof(PhrasesSource), typeof(Dictionary<string, string>), typeof(EverydayPhrasesCustomControl), new PropertyMetadata(null));

    public EverydayPhrasesCustomControl()
    {
        this.DefaultStyleKey = typeof(EverydayPhrasesCustomControl);
        CompletePhraseCommand = new RelayCommand(CompletePhrase);
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

    private TextBox? PhraseEnd { get; set; }

    private TextBox? PhraseStart { get; set; }
        
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        
        PhraseStart = GetTemplateChild(nameof(PhraseStart)) as TextBox;
        if (PhraseStart is not null)
        {
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

        PhraseEnd = GetTemplateChild(nameof(PhraseEnd)) as TextBox;
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
