using System.Windows;
using System.Windows.Controls;

namespace ToDoList.View.Components
{
    /// <summary>
    /// Interaction logic for TaskItem.xaml
    /// </summary>
    public partial class TaskItem : UserControl
    {
        public static readonly DependencyProperty TextProperty
        = DependencyProperty.Register(nameof(Text), typeof(string), typeof(TaskItem), new PropertyMetadata());

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public TaskItem()
        {
            InitializeComponent();
        }
    }
}
