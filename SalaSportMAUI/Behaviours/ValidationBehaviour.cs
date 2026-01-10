using Microsoft.Maui.Controls;

namespace SalaSportMAUI.Behaviours;

public class ValidationBehaviour : Behavior<Entry>
{
    protected override void OnAttachedTo(Entry bindable)
    {
        bindable.TextChanged += OnTextChanged;
        base.OnAttachedTo(bindable);
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        bindable.TextChanged -= OnTextChanged;
        base.OnDetachingFrom(bindable);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;

        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            entry.BackgroundColor = Colors.LightPink;
        }
        else
        {
            entry.BackgroundColor = Colors.White;
        }
    }
}
