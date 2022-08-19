namespace GTA5OnlineTools.Assets.Styles.Attached;

/// <summary>
/// 附加属性-字体图标
/// </summary>
public class Icon
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
        DependencyProperty.RegisterAttached("Value", typeof(string), typeof(Icon), new PropertyMetadata(default));
}
