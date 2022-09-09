namespace GTA5OnlineTools.Assets.Styles.Attached;

/// <summary>
/// 附加属性-文本
/// </summary>
public class Text
{
    public static string GetValue(DependencyObject obj)
    {
        return (string)obj.GetValue(ValueProperty);
    }

    public static void SetValue(DependencyObject obj, string value)
    {
        obj.SetValue(ValueProperty, value);
    }

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.RegisterAttached("Value", typeof(string), typeof(Text), new PropertyMetadata(default));
}
